using System;
using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Schema;

namespace AgileCoding.Extentions.Xml
{
    public static class XmlExtentions
    {
        public static string StripNamespacePrefix(this string self)
        {
            int indexOfColon = self.IndexOf(':');

            if (indexOfColon >= 0)
            {
                indexOfColon++;
                return self.Substring(indexOfColon, self.Length - indexOfColon);
            }

            return self;
        }

        /// <summary>
        /// Validatea XML string to a schema and returns a log. If Xml string contains '&' char it will be exscaped safely
        /// Please note: other XML charaters are NOT escaped because this code interprests the XML as a document and not on a field by field basis.
        /// TODO: implement ILogger log parameter.
        /// </summary>
        /// <param name="xmlString">Xml String to validate</param>
        /// <param name="schemaString">Schema used to validate string</param>
        /// <param name="log">Log</param>
        /// <returns>true if validation passed and false it there was an error</returns>
        public static (bool ValidationSucces, string ResultString) ValidateXMLWithLog(this string xmlString, string schemaString, ref StringBuilder log)
        {
            string tempXmlString = xmlString.ToString();
            StringBuilder messages = new StringBuilder("Loading Schema...");
            XmlReader? reader = null;
            bool schmaLoadError = false;
            XmlSchema? schema = null;
            try
            {
                schema = XmlSchema.Read(new StringReader(schemaString), (sender, args) =>
                {
                    if (sender == null)
                        return;

                    messages.AppendLine("\nError loading Schema : ").AppendLine(args.Exception.ToString());
                    schmaLoadError = true;
                });
                if (!schmaLoadError && schema != null)
                {
                    messages.AppendLine("Success!!");
                }

                XmlReaderSettings xmlReaderSettings = new XmlReaderSettings();
                try
                {
                    if (schema != null)
                    {
                        xmlReaderSettings.Schemas.Add(schema);
                        xmlReaderSettings.ValidationType = ValidationType.Schema;
                        messages.Append("Loading XML Data...");
                        reader = XmlReader.Create(new StringReader(tempXmlString), xmlReaderSettings);
                        messages.AppendLine("Success!!");
                    }
                    else
                    {
                        messages.AppendLine("Failure to load schema!!");
                        schmaLoadError = true;
                    }
                }
                catch (Exception ex)
                {
                    messages.AppendLine("Failure!!").AppendLine(ex.ToString());
                    schmaLoadError = true;
                }

                try
                {
                    if (reader != null)
                    {
                        messages.Append("Validating XML...");
                        new XmlDocument().Load(reader);
                        messages.AppendLine("Success!!");
                    }
                    else
                    {
                        messages.AppendLine("Failure to load reader!!");
                        schmaLoadError = true;
                    }
                }
                catch (Exception ex2)
                {
                    if (tempXmlString.Contains("&"))
                    {
                        ReplaceXmlCharater(ref xmlString, tempXmlString, messages, ref schmaLoadError, xmlReaderSettings, "&", "&amp;");
                    }
                    else
                    {
                        messages.AppendLine("\nError Validating XML!!").AppendLine("\nError Details : ").AppendLine(ex2.ToString());
                        schmaLoadError = true;
                    }
                }
                log.Append(messages.ToString());
            }
            catch (Exception ex3)
            {
                schmaLoadError = true;
                messages.AppendLine("More details on error : ").AppendLine().AppendLine(ex3.ToString());
                throw;
            }
            return (!schmaLoadError, xmlString);
        }

        private static void ReplaceXmlCharater(ref string xmlString, string tempXmlString, StringBuilder messages, ref bool schmaLoadError, XmlReaderSettings xmlReaderSettings, string searchChar, string replaceChar)
        {
            var tempXML = tempXmlString.Replace(searchChar, replaceChar);
            try
            {
                new XmlDocument().Load(XmlReader.Create(new StringReader(tempXML), xmlReaderSettings));
                xmlString = tempXML;
            }
            catch (Exception ex)
            {
                messages
                    .AppendLine("\nError Validating XML!!")
                    .AppendLine("\nError Details : ")
                    .AppendLine(ex.ToString());
                schmaLoadError = true;
            }
        }
    }
}

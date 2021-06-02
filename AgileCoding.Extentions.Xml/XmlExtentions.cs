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
    }
}

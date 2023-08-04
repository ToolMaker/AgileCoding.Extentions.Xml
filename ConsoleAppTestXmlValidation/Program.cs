using AgileCoding.Extentions.Xml;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleAppTestXmlValidation
{
    class Program
    {
        static void Main(string[] args)
        {
            //<?xml version=\"1.0\" encoding=\"UTF-8\" ?>
            string SchemaString = "<xs:schema xmlns:xs=\"http://www.w3.org/2001/XMLSchema\"><xs:element name=\"name\" type=\"xs:string\"/></xs:schema>";
            string XMLString = "<name>testing &</name>";
            StringBuilder log = new StringBuilder();

            var result = XMLString.ValidateXMLWithLog(SchemaString, ref log);
            Console.WriteLine($"Result {result}");
            Console.ReadLine();
        }
    }
}

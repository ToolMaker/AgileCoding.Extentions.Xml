AgileCoding.Extensions.Xml
==========================

Overview
--------

AgileCoding.Extensions.Xml is a .NET library that offers a set of extension methods to make handling XML easier and more convenient. It's built for .NET 6.0.

Installation
------------

This library is distributed via NuGet. To install, use the following command in the NuGet Package Manager console:

bash

`Install-Package AgileCoding.Extensions.Xml -Version 2.0.5`

Features
--------

This library offers the following extension methods:

-   `StripNamespacePrefix`: Strips namespace prefix from a string.
-   `ValidateXMLWithLog`: Validates XML string against a schema and returns validation results as well as a log of the validation process.

Usage
-----

Here is an example of how you can use these methods in your code:

csharp
```using AgileCoding.Extentions.Xml;
using System.Text;

string xmlString = "<root xmlns=\"http://example.com\">...</root>";
string schemaString = "...";

// Strip namespace prefix from a string
string strippedString = "ns:tagName".StripNamespacePrefix(); // "tagName"

// Validate XML with log
StringBuilder log = new StringBuilder();
(bool success, string resultString) = xmlString.ValidateXMLWithLog(schemaString, ref log);
```

Documentation
-------------

For more detailed information about the usage of this library, refer to the [official documentation](https://github.com/ToolMaker/AgileCoding.Extentions.Xml/wiki).

License
-------

This project is licensed under the terms of the MIT license. For more information, refer to the [LICENSE](https://github.com/ToolMaker/AgileCoding.Extentions.Xml/blob/main/LICENSE) file.

Contributing
------------

Contributions are welcome! Please see our [contributing guidelines](https://github.com/ToolMaker/AgileCoding.Extentions.Xml/blob/main/CONTRIBUTING.md) for more details.
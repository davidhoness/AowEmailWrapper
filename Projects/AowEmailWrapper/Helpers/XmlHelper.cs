using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Xml;
using System.Xml.Serialization;
using System.Xml.Schema;

namespace AowEmailWrapper.Helpers
{
    public static class XmlHelper
    {
        public static string Serialize(System.Object theObject)
        {
            System.Xml.Serialization.XmlSerializer _serializer;
            System.Xml.XmlDocument _xmlDoc;
            System.Xml.XmlNode _attribute;
            System.IO.MemoryStream _memory;
            System.IO.StreamReader _reader;

            try
            {
                // Write the stream to memory
                _serializer = new XmlSerializer(theObject.GetType());
                _memory = new MemoryStream();
                _reader = new StreamReader(_memory);

                // Serialize the object
                _serializer.Serialize(_memory, theObject);

                // Set memory stream to the beginning
                _memory.Seek(0, SeekOrigin.Begin);

                // Load memory into an XML document
                _xmlDoc = new XmlDocument();
                _xmlDoc.Load(_reader);

                // Remove the xmlns:xsd & xmlns:xsi namespace declarations - they're not necessary!
                _attribute = _xmlDoc.DocumentElement.Attributes.GetNamedItem("xmlns:xsd");
                if (_attribute != null)
                {
                    _xmlDoc.DocumentElement.RemoveAttribute(_attribute.LocalName, _attribute.NamespaceURI);
                }
                _attribute = _xmlDoc.DocumentElement.Attributes.GetNamedItem("xmlns:xsi");
                if (_attribute != null)
                {
                    _xmlDoc.DocumentElement.RemoveAttribute(_attribute.LocalName, _attribute.NamespaceURI);
                }

                // Close streams
                _reader.Close();
                _memory.Close();

                return _xmlDoc.DocumentElement.OuterXml.ToString();
            }

            finally
            {
                _serializer = null;
                _reader = null;
                _memory = null;
                _attribute = null;
                _xmlDoc = null;
            }
        }

        public static T Deserialize<T>(string theXml)
        {
            T value = default(T);

            if (!string.IsNullOrEmpty(theXml))
            {
                XmlSerializer serial = new XmlSerializer(typeof(T));
                {
                    using (StringReader reader = new StringReader(theXml))
                    {
                        try
                        {
                            return (T)Convert.ChangeType(serial.Deserialize(reader), typeof(T));
                        }
                        finally
                        {
                            serial = null;
                        }
                    }
                }
            }

            return value;
        }

        public static string FormatXmlPretty(string input)
        {
            string returnVal = null;

            using (StringWriter stringWriter = new StringWriter())
            {

                XmlDocument doc = new XmlDocument();

                doc.LoadXml(input);

                //create reader and writer

                XmlNodeReader xmlReader = new XmlNodeReader(doc);

                XmlTextWriter xmlWriter = new XmlTextWriter(stringWriter);

                //set formatting options

                xmlWriter.Formatting = Formatting.Indented;

                xmlWriter.Indentation = 1;

                xmlWriter.IndentChar = '\t';

                //write the document formatted

                xmlWriter.WriteNode(xmlReader, true);

                returnVal = stringWriter.ToString();

                xmlWriter.Close();
                xmlWriter = null;
                xmlReader.Close();
                xmlReader = null;
                doc = null;
            }

            return returnVal;
        }
    }
}

using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace MJ_CAIS.Common.XmlData
{
    public static class XmlUtils
    {
        public static string SerializeToXml<T>(T model, XmlSerializerNamespaces ns = null)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(T));
            StringBuilder builder = new StringBuilder();

            using (Utf8StringWriter writer = new Utf8StringWriter(builder))
            {
                serializer.Serialize(writer, model, ns);
            }

            string result = builder.ToString();
            return result;
        }

        public static T DeserializeXml<T>(string xml)
        {
            if (string.IsNullOrEmpty(xml))
            {
                return default(T);
            }

            XmlSerializer serializer = new XmlSerializer(typeof(T));
            using (StringReader reader = new StringReader(xml))
            {
                T element = (T)serializer.Deserialize(reader);
                return element;
            }
        }

        public static string? GetValueOrNull(this XmlNode node)
        {
            return node != null ? node.InnerText : null;
        }
        public static string GetNumbersFromString(string? inputValue)
        {
            string result = "";

            for (int i = 0; i < inputValue?.Length; i++)
            {
                if (Char.IsDigit(inputValue[i]))
                    result += inputValue[i];
            }

            return result;
        }

        public static XmlElement ToXmlElement(string input, bool preserveWhitespaces = false, bool removeXmlTag = true)
        {
            if (string.IsNullOrEmpty(input))
            {
                return null;
            }

            XmlDocument document = new XmlDocument();
            document.PreserveWhitespace = preserveWhitespaces;
            document.LoadXml(input);

            if (removeXmlTag && document.ChildNodes.Count > 1)
            {
                XmlElement element = document.ChildNodes[1] as XmlElement;
                return element;
            }

            return document.DocumentElement;
        }

        public static string BeautifyXml(string xml, int indentCharsCount = 4)
        {
            if (string.IsNullOrEmpty(xml))
            {
                return xml;
            }

            try
            {
                var newLine = Environment.NewLine;
                var doc = new XmlDocument();
                doc.LoadXml(xml);

                var sb = new StringBuilder();
                var settings = new XmlWriterSettings
                {
                    Indent = true,
                    IndentChars = new string(' ', indentCharsCount),
                    NewLineChars = newLine,
                    NewLineHandling = NewLineHandling.Replace,
                    OmitXmlDeclaration = true
                };

                using (XmlWriter writer = XmlWriter.Create(sb, settings))
                {
                    doc.Save(writer);
                }

                if (doc.FirstChild.NodeType == XmlNodeType.XmlDeclaration)
                {
                    // If contains xml tag, add it to result
                    sb.Insert(0, doc.FirstChild.OuterXml + newLine);
                }

                var result = sb.ToString();
                return result;
            }
            catch (Exception)
            {
                return xml;
            }
        }

        public static bool IsValidXml(string xml)
        {
            if (string.IsNullOrEmpty(xml))
            {
                return false;
            }

            try
            {
                XmlDocument document = new XmlDocument();
                document.LoadXml(xml);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public static string RemoveXmlDeclaration(string xml, bool preserveWhitespace = true)
        {
            XmlDocument doc = new XmlDocument();
            doc.PreserveWhitespace = preserveWhitespace;
            doc.LoadXml(xml);

            foreach (XmlNode node in doc)
            {
                if (node.NodeType == XmlNodeType.XmlDeclaration)
                {
                    doc.RemoveChild(node);
                }
            }

            var result = doc.OuterXml.Trim();
            return result;
        }
    }
}

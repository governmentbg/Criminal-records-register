using MJ_CAIS.Common.XmlData;
using System.Globalization;
using System.Xml;

namespace MJ_CAIS.DataAccess.Entities
{
    public partial class EEcrisMessage
    {
        public static EEcrisMessage ParseXml(string xml)
        {
            var result = new EEcrisMessage();
            var doc = new XmlDocument();
            doc.LoadXml(xml);
            var root = doc.DocumentElement;

            // Add the namespace.  
            XmlNamespaceManager nsmgr = new XmlNamespaceManager(doc.NameTable);
            nsmgr.AddNamespace("", "http://ec.europa.eu/ECRIS-RI/file-monitor-v1.0");
            nsmgr.AddNamespace("ns2", "http://ec.europa.eu/ECRIS-RI/domain-v1.0");
            nsmgr.AddNamespace("ns3", "http://ec.europa.eu/ECRIS-RI/commons-v1.0");

            if (root.ChildNodes.Count > 0)
            {
                result.MsgTimestamp = DateTimeOffset.Parse(root.FirstChild.InnerText, null, DateTimeStyles.RoundtripKind).DateTime;
                var node = root.ChildNodes[1];
                if (node != null)
                {
                    result.Identifier = node.SelectSingleNode("ns2:MessageIdentifier", nsmgr).GetValueOrNull();
                    result.EcrisIdentifier = node.SelectSingleNode("ns2:MessageEcrisIdentifier", nsmgr).GetValueOrNull();
                    result.FromAuthId = node.SelectSingleNode("ns2:MessageSendingMemberState", nsmgr).GetValueOrNull();
                    result.ToAuthId = node.SelectSingleNode("ns2:MessageReceivingMemberState", nsmgr).GetValueOrNull();
                    result.EcrisMsgConvictionId = node.SelectSingleNode("ns2:NotificationMessageConviction/ns2:ConvictionID", nsmgr).GetValueOrNull();

                    var personNode = node.SelectSingleNode("ns2:MessagePerson", nsmgr);
                    if (personNode != null)
                    {
                        result.Firstname = personNode.SelectSingleNode("ns2:PersonName/ns2:Forename", nsmgr).GetValueOrNull();
                        result.Surname = personNode.SelectSingleNode("ns2:PersonName/ns2:SecondSurname", nsmgr).GetValueOrNull();
                        result.Familyname = personNode.SelectSingleNode("ns2:PersonName/ns2:Surname", nsmgr).GetValueOrNull();

                        int sex;
                        if (int.TryParse(personNode.SelectSingleNode("ns2:PersonSex", nsmgr).GetValueOrNull(), out sex))
                        {
                            result.Sex = sex;
                        }

                        int year = 0;
                        int.TryParse(personNode.SelectSingleNode("ns2:PersonBirthDate/ns3:DateYear", nsmgr).GetValueOrNull(), out year);

                        int month = 1;
                        string? xmlMonth = personNode.SelectSingleNode("ns2:PersonBirthDate/ns3:DateMonthDay/ns3:DateMonth", nsmgr).GetValueOrNull();
                        if (xmlMonth != null)
                        {
                            int.TryParse(xmlMonth.Substring(2, 2), out month);
                            if (month == 0) month = 1;
                        }

                        int day = 1;
                        string xmlDay = personNode.SelectSingleNode("ns2:PersonBirthDate/ns3:DateMonthDay/ns3:DateDay", nsmgr).GetValueOrNull();
                        if (xmlMonth != null)
                        {
                            Int32.TryParse(xmlDay.Substring(3, 2), out day);
                            if (day == 0) day = 1;
                        }

                        if (year > 0)
                        {
                            result.BirthDate = new DateTime(year, month, day);
                        }
                        result.BirthCountry = personNode.SelectSingleNode("ns2:PersonBirthPlace/ns2:PlaceCountryReference", nsmgr).GetValueOrNull();

                        var townNamesNodes = personNode.SelectNodes("ns2:PersonBirthPlace/ns2:PlaceTownName/ns3:MultilingualTextLinguisticRepresentation", nsmgr);
                        if (townNamesNodes != null)
                        {
                            string townNames = string.Empty;
                            foreach (XmlNode name in townNamesNodes)
                            {
                                if (name.NextSibling != null)
                                {
                                    if (name.Attributes != null && name.Attributes.GetNamedItem("languageCode") != null)
                                    {
                                        townNames += string.Format("{0}({1});", name.InnerText, name.Attributes.GetNamedItem("languageCode").InnerText);
                                    }
                                    else
                                    {
                                        townNames += string.Format("{0};", name.InnerText);
                                    }

                                }
                                else
                                {
                                    townNames += name.InnerText;
                                }
                            }
                            result.BirthCity = townNames;
                        }

                        var nationalityNodes = personNode.SelectNodes("ns2:PersonNationalityReference", nsmgr);
                        if (nationalityNodes != null)
                        {

                            result.Nationality1Code = nationalityNodes[0] != null ? nationalityNodes[0].InnerText : null;
                            result.Nationality2Code = nationalityNodes[1] != null ? nationalityNodes[1].InnerText : null;
                        }
                    }
                }
            }
            return result;
        }
    }
}

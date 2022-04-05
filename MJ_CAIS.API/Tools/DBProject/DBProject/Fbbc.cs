using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace DBProject
{
    public static class Fbbc
    {
        private static string _sql = @"select
                               [ID]
                              ,[EGN]
                              ,[REGDOC_TYPE]
                              ,[JUDGE]
                              ,[DATE_RECEIVED]
                              ,[COUNTRY_ID]
                              ,[COUNTRY]
                              ,[FNAME]
                              ,[MNAME]
                              ,[FAMILY]
                              ,[BIRTHPLACE]
                              ,[BIRTHCOUNTRY]
                              ,[BIRTHDAY]
                              ,[DATE_BEGIN]
                              ,[DATE_END]
                              ,[ANOTACIA]
                              ,[MOTHER_FNAME]
                              ,[MOTHER_MNAME]
                              ,[MOTHER_FAMILY]
                              ,[FATHER_FNAME]
                              ,[FATHER_MNAME]
                              ,[FATHER_FAMILY]
                              ,[GDKP_NUMBER]
                              ,[GDKP_DATE]
                              ,[GDKP_DELO]
                              ,[GDKP_TOM]
                              ,[GDKP_STR]
                              ,[NJR_COUNTRY]
                              ,[NJR_ID]
                              ,[NJR_ID_FIRST]
                              ,[ECRIS_COUNTRY]
                              ,[ECRIS_ID]
                              ,[ECRIS_CONV_ID]
                              ,[ECRIS_UPDATED_CONV_TYPE]
                              ,[ECRIS_UPDATED_CONV_ID]
                              ,[ECRIS_RELATED_CONV_ID]
                              ,[USER_ENTERED]
                              ,[USER_UPDATED]
                              ,[XML_DATA]
                              ,[DATE_INS]
                              ,[DATE_LAST_UPD]
                              ,[USER_NAME]
                              ,[IMAGE_FILE_NAME]
                              ,[DATE_ISSUE]
                              ,[FLAG_ADM_PENALITY]
                              ,[DATE_DECISION]
                              ,[DATE_DECISION_FINAL]
                              ,[SEQUENTIAL_NUMBER]
                              ,[DATE_DESTROYED]
                                ,cast(null as varchar) FROM_AUTH_ID,
								cast(null as varchar) TO_AUTH_ID,
								cast(null as varchar) IDENTIFIER,
								cast(null as varchar) ECRIS_IDENTIFIER,
								cast(null as datetime) MSG_TIMESTAMP,
								cast(null as datetime) BIRTH_DATE,
								cast(null as varchar)BIRTH_COUNTRY,
								cast(null as varchar)BIRTH_CITY,
								cast(null as varchar)FBBC_ID,
								cast(null as varchar)FIRSTNAME,
								cast(null as varchar)SURNAME,
								cast(null as varchar)FAMILYNAME,
								cast(null as int)SEX,
								cast(null as varchar)NATIONALITY1_CODE,
								cast(null as varchar)NATIONALITY2_CODE,
								cast(null as varchar)ECRIS_MSG_CONVICTION_ID
                from[FBBC].[dbo].[T_OSAD] ";

        public static void GetData(string sourceConnectionString, string destConnString)
        {
            DataTable dt = new DataTable();
           
            using (SqlConnection conn = new SqlConnection(sourceConnectionString))
            using (SqlDataAdapter sda = new SqlDataAdapter(_sql, conn))
            {
                sda.SelectCommand.CommandType = CommandType.Text;
                sda.Fill(dt);
            }
            ParceXml(dt);
            SaveUsingOracleBulkCopy("Z_IMPORT_FBBC_TEST", dt, destConnString);
        }
        //todo може да се ползва клас от DataAccess
        public partial class EEcrisMessage
        {

            public string? RequestMsgId { get; set; }
            public string? FromAuthId { get; set; }
            public string? ToAuthId { get; set; }
            public string? Identifier { get; set; }
            public string? EcrisIdentifier { get; set; }
            public DateTime? MsgTimestamp { get; set; }
            public string? ResponseTypeId { get; set; }
            public string? EcrisMsgStatus { get; set; }
            public DateTime? BirthDate { get; set; }
            public string? BirthCountry { get; set; }
            public string? BirthCity { get; set; }
            public string? FbbcId { get; set; }
            public string? Firstname { get; set; }
            public string? Surname { get; set; }
            public string? Familyname { get; set; }
            public decimal? Sex { get; set; }
            public string? Nationality1Code { get; set; }
            public string? Nationality2Code { get; set; }
            public string? MsgTypeId { get; set; }
            public string? CreatedBy { get; set; }
            public DateTime? CreatedOn { get; set; }
            public string? UpdatedBy { get; set; }
            public DateTime? UpdatedOn { get; set; }
            public string? EcrisMsgConvictionId { get; set; }
        }

        public static EEcrisMessage GetDataFromXml(string xml)
        {
            EEcrisMessage resultMsg = new EEcrisMessage();
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(xml);
            XmlNode root = doc.DocumentElement;

            // Add the namespace.  
            XmlNamespaceManager nsmgr = new XmlNamespaceManager(doc.NameTable);
            nsmgr.AddNamespace("", "http://ec.europa.eu/ECRIS-RI/file-monitor-v1.0");
            nsmgr.AddNamespace("ns2", "http://ec.europa.eu/ECRIS-RI/domain-v1.0");
            nsmgr.AddNamespace("ns3", "http://ec.europa.eu/ECRIS-RI/commons-v1.0");

            if (root.ChildNodes.Count > 0)
            {
                resultMsg.MsgTimestamp = DateTimeOffset.Parse(root.FirstChild.InnerText, null, System.Globalization.DateTimeStyles.RoundtripKind).DateTime;
                var ecrisMessageNode = root.ChildNodes[1];
                if (ecrisMessageNode != null)
                {
                    resultMsg.Identifier = GetValueOrNull(ecrisMessageNode.SelectSingleNode("ns2:MessageIdentifier", nsmgr));
                    resultMsg.EcrisIdentifier = GetValueOrNull(ecrisMessageNode.SelectSingleNode("ns2:MessageEcrisIdentifier", nsmgr));
                    resultMsg.FromAuthId = GetValueOrNull(ecrisMessageNode.SelectSingleNode("ns2:MessageSendingMemberState", nsmgr));
                    resultMsg.ToAuthId = GetValueOrNull(ecrisMessageNode.SelectSingleNode("ns2:MessageReceivingMemberState", nsmgr));
                    resultMsg.EcrisMsgConvictionId = GetValueOrNull(ecrisMessageNode.SelectSingleNode("ns2:NotificationMessageConviction/ns2:ConvictionID", nsmgr));

                    var personNode = ecrisMessageNode.SelectSingleNode("ns2:MessagePerson", nsmgr);
                    if (personNode != null)
                    {
                        resultMsg.Firstname = GetValueOrNull(personNode.SelectSingleNode("ns2:PersonName/ns2:Forename", nsmgr));
                        resultMsg.Surname = GetValueOrNull(personNode.SelectSingleNode("ns2:PersonName/ns2:SecondSurname", nsmgr));
                        resultMsg.Familyname = GetValueOrNull(personNode.SelectSingleNode("ns2:PersonName/ns2:Surname", nsmgr));
                        int sex;
                        if(int.TryParse(GetValueOrNull(personNode.SelectSingleNode("ns2:PersonSex", nsmgr)), sex))
                        {
                            resultMsg.Sex = sex;
                        }
                        
                        int year = 0;
                        int.TryParse(GetValueOrNull(personNode.SelectSingleNode("ns2:PersonBirthDate/ns3:DateYear", nsmgr)), out year);

                        int month = 1;
                        string xmlMonth = GetValueOrNull(personNode.SelectSingleNode("ns2:PersonBirthDate/ns3:DateMonthDay/ns3:DateMonth", nsmgr));
                        if (xmlMonth != null)
                        {
                            int.TryParse(xmlMonth.Substring(2, 2), out month);
                            if (month == 0) month = 1;
                        }

                        int day = 1;
                        string xmlDay = GetValueOrNull(personNode.SelectSingleNode("ns2:PersonBirthDate/ns3:DateMonthDay/ns3:DateDay", nsmgr));
                        if (xmlMonth != null)
                        {
                            Int32.TryParse(xmlDay.Substring(3, 2), out day);
                            if (day == 0) day = 1;
                        }

                        if (year > 0)
                        {
                            resultMsg.BirthDate = new DateTime(year, month, day);
                        }
                        resultMsg.BirthCountry = GetValueOrNull(personNode.SelectSingleNode("ns2:PersonBirthPlace/ns2:PlaceCountryReference", nsmgr));

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
                                        townNames += String.Format("{0}({1});", name.InnerText, name.Attributes.GetNamedItem("languageCode").InnerText);
                                    }
                                    else
                                    {
                                        townNames += String.Format("{0};", name.InnerText);
                                    }

                                }
                                else
                                {
                                    townNames += name.InnerText;
                                }
                            }
                            resultMsg.BirthCity = townNames;
                        }

                        var nationalityNodes = personNode.SelectNodes("ns2:PersonNationalityReference", nsmgr);
                        if (nationalityNodes != null)
                        {

                            resultMsg.Nationality1Code = nationalityNodes[0] != null ? nationalityNodes[0].InnerText : DBNull.Value;
                            resultMsg.Nationality2Code = nationalityNodes[1] != null ? nationalityNodes[1].InnerText : DBNull.Value;
                        }
                    }
                }
            }
            return resultMsg;
        }

        public static void ParceXml(DataTable dataTable)
        {
            foreach(DataRow row in dataTable.Rows)
            {
                if (row["REGDOC_TYPE"].ToString().Equals("5"))
                {
                    var msg = GetDataFromXml(row["XML_DATA"].ToString());
                    row["MSG_TIMESTAMP"] = msg.MsgTimestamp;
                    row["IDENTIFIER"] = msg.Identifier;
                    row["ECRIS_IDENTIFIER"] = msg.EcrisIdentifier;
                    row["FROM_AUTH_ID"] = msg.FromAuthId; //todo да се извлече Id от номенклатурата, за сега се извлича абревиатурата на държавата
                    row["TO_AUTH_ID"] = msg.ToAuthId//todo да се извлече Id от номенклатурата, за сега се извлича абревиатурата на държавата
                            row["ECRIS_MSG_CONVICTION_ID"] = msg.EcrisMsgConvictionId;
                    row["FIRSTNAME"] = msg.Firstname;
                    row["SURNAME"] = msg.Surname;
                    row["FAMILYNAME"] = msg.Familyname;
                    row["SEX"] = msg.Sex;
                    Int32 row["BIRTH_DATE"] = msg.BirthDate;

                    row["BIRTH_COUNTRY"] = msg.BirthCountry;

                    row["BIRTH_CITY"] = msg.BirthCity;


                    row["NATIONALITY1_CODE"] = msg.Nationality1Code; //todo да се извлече по код от номенклатурата, за сега работи защото при миграцията id==код
                    row["NATIONALITY2_CODE"] = msg.Nationality2Code; //todo да се извлече по код от номенклатурата, за сега работи защото при миграцията id==код

                }
            }
        }

        public static string? GetValueOrNull(XmlNode node)
        {
            return node != null ? node.InnerText : null;
        }
        public static void SaveUsingOracleBulkCopy(string qualifiedTableName, DataTable dataTable, string connectionString)
        {
            try
            {
                OracleConnection oracleConnection = new OracleConnection(connectionString);

                oracleConnection.Open();
                using (OracleBulkCopy bulkCopy = new OracleBulkCopy(oracleConnection))
                {
                    bulkCopy.DestinationTableName = qualifiedTableName;
                    bulkCopy.BulkCopyTimeout = 0;
                    bulkCopy.WriteToServer(dataTable);
                }
                oracleConnection.Close();
                oracleConnection.Dispose();
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}

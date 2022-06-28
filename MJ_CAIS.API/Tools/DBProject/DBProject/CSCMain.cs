using MJ_CAIS.DataAccess.Entities;
using Oracle.ManagedDataAccess.Client;
using System.Data;
using System.Xml;
using MJ_CAIS.Common.XmlData;

namespace DBProject
{
    public static class CSCMain
    {
        private static string _sql = @"select
               s.service_id, 
               s.creator_id, 
               s.create_date, 
               s.modifier_id, 
               s.modify_date, 
               s.site_id, 
               s.service_date, 
               s.request_type, 
               s.service_status, 
               s.status_date, 
               s.service_result, 
               s.purpose, 
               s.person_id, 
               s.judge, 
               s.judge_date, 
               s.user_id, 
               s.person_request, 
               s.certificate_number, 
               s.message_id, 
               s.city_code_answer, 
               s.address_answer, 
               s.reabilitation, 
               s.xml_reg, 
               s.xml_answer, 
               s.egn, 
               s.lnch, 
               s.r1, 
               s.r2, 
               s.application_number, 
               s.application_number_date,
               p.person_id_ident person_id_ident,
                cast(null as nvarchar2(200)) FIRSTNAME,
                cast(null as nvarchar2(200)) SURNAME,
                cast(null as nvarchar2(200)) FAMILYNAME,
                cast(null as nvarchar2(200)) FULLNAME,
                cast(null as number) SEX,
                cast(null as nvarchar2(200)) MOTHER_FULLNAME,
                cast(null as nvarchar2(200)) FATHER_FULLNAME,
                cast(null as date) BIRTH_DATE,
                cast(null as nvarchar2(200)) BIRTH_COUNTRY_ID,
                cast(null as nvarchar2(200)) birth_city_id,
                cast(null as nvarchar2(200)) BIRTH_PLACE_OTHER,
                cast(null as varchar2(200)) INNER_EGN,
                cast(null as varchar2(200)) INNER_LNCH,
                cast(null as nvarchar2(200)) NATIONALITY1_CODE,
                cast(null as nvarchar2(200)) NATIONALITY2_CODE,
                cast(null as nvarchar2(200)) NATIONALITY3_CODE,
                cast(null as nvarchar2(200)) NATIONALITY4_CODE,
                cast(null as nvarchar2(200)) NATIONALITY5_CODE,
                cast(null as nvarchar2(200)) HAS_ERROR,
                cast(null as VARCHAR2(4000)) ERROR
                from services s
                left join persons p on  s.person_id = p.person_id";
        public static void GetData(string sourceConnectionString, string destConnString)
        {
            DataTable dt = new DataTable();
            using (OracleConnection conn = new OracleConnection(sourceConnectionString)) //oracleConnection.Open();
            using (OracleDataAdapter oda = new OracleDataAdapter(_sql, conn))
            {
                oda.SelectCommand.CommandType = CommandType.Text;
                oda.Fill(dt);
            }




            TransformTableRows(dt);
            DbHelper.SaveUsingOracleBulkCopy("Z_Z_SERVICES", dt, destConnString);
        }

        public static void TransformTableRows(DataTable dataTable)
        {
            foreach (DataRow row in dataTable.Rows)
            {

                Person person = ParseXml(row["XML_ANSWER"].ToString());
                row["FIRSTNAME"] = person.Firstname;
                row["SURNAME"] = person.Surname;
                row["FAMILYNAME"] = person.Familyname;
                row["FULLNAME"] = person.Fullname; 
                if(person.Sex != null)
                {
                    row["SEX"] = person.Sex;
                }
                row["MOTHER_FULLNAME"] = person.MotherFullname;
                row["FATHER_FULLNAME"] = person.FatherFullname;
                if(person.BirthDate != null)
                {
                    row["BIRTH_DATE"] = person.BirthDate;
                }
                
                //row["BIRTH_COUNTRY_ID"] = person.BirthCountryId;
                //row["BIRTH_CITY_ID"] = person.BirthCityId;
                row["BIRTH_PLACE_OTHER"] = person.BirthPlaceOther;
                row["NATIONALITY1_CODE"] = person.NationalityCode1;
                row["NATIONALITY2_CODE"] = person.NationalityCode2;
                row["NATIONALITY3_CODE"] = person.NationalityCode3;
                row["NATIONALITY4_CODE"] = person.NationalityCode4;
                row["NATIONALITY5_CODE"] = person.NationalityCode5;

                row["INNER_EGN"] = person.Egn;
                row["INNER_LNCH"] = person.Lnch;

                row["HAS_ERROR"] = person.HasError;
                row["ERROR"] = person.ErrorMessage;
            }
        }

        public static Person ParseXml(string xml)
        {
            var result = new Person();
            try 
            { 
            var doc = new XmlDocument();
            doc.LoadXml(xml);
            var root = doc.DocumentElement;

            // Add the namespace.  
            //XmlNamespaceManager nsmgr = new XmlNamespaceManager(doc.NameTable);
            //nsmgr.AddNamespace("", "http://ec.europa.eu/ECRIS-RI/file-monitor-v1.0");
            //nsmgr.AddNamespace("ns2", "http://ec.europa.eu/ECRIS-RI/domain-v1.0");
            //nsmgr.AddNamespace("ns3", "http://ec.europa.eu/ECRIS-RI/commons-v1.0");

            if (root.ChildNodes.Count > 0)
            {
                var node = root.SelectSingleNode("Person");
                if (node != null)
                {
                    result.Firstname = node.SelectSingleNode("Name").GetValueOrNull();
                    result.Surname = node.SelectSingleNode("Surname").GetValueOrNull();
                    result.Familyname = node.SelectSingleNode("Family").GetValueOrNull();
                    result.Fullname = node.SelectSingleNode("ForeignerNames").GetValueOrNull();

                    result.MotherFullname = node.SelectSingleNode("MotherNames").GetValueOrNull();
                    result.FatherFullname = node.SelectSingleNode("FatherNames").GetValueOrNull();

                    result.BirthPlaceOther = node.SelectSingleNode("BirthPlace").GetValueOrNull();

                    decimal? sex = null;
                    //Male, Unkonwn, Female
                    string gender = node.SelectSingleNode("Gendre").GetValueOrNull();
                    
                    if (gender == "Male")
                    {
                        sex = 1;
                    }
                    if (gender == "Female")
                    {
                        sex = 2;
                    }
                    if (gender == "None")
                    {
                        sex = 0;
                    }
                    result.Sex = sex;

                    result.Egn = node.SelectSingleNode("EGN").GetValueOrNull();
                    result.Lnch = node.SelectSingleNode("LNCH").GetValueOrNull();

                    DateTime birthDate;
                    if (DateTime.TryParse(node.SelectSingleNode("BirthDate").GetValueOrNull(), out birthDate))
                    {
                        result.BirthDate = birthDate;
                    }

                    var nationalityNodes = node.SelectNodes("Nationalities/Nationality");
                    if (nationalityNodes != null)
                    {
                        if (nationalityNodes[0] != null)
                        {
                            result.NationalityCode1 = nationalityNodes[0].SelectSingleNode("ID").GetValueOrNull();
                        }
                        if (nationalityNodes[1] != null)
                        {
                            result.NationalityCode2 = nationalityNodes[1].SelectSingleNode("ID").GetValueOrNull();
                        }
                        if (nationalityNodes[2] != null)
                        {
                            result.NationalityCode3 = nationalityNodes[2].SelectSingleNode("ID").GetValueOrNull();
                        }
                        if (nationalityNodes[3] != null)
                        {
                            result.NationalityCode4 = nationalityNodes[3].SelectSingleNode("ID").GetValueOrNull();
                        }
                        if (nationalityNodes[4] != null)
                        {
                            result.NationalityCode5 = nationalityNodes[4].SelectSingleNode("ID").GetValueOrNull();
                        }
                    }
                   
                }
                }
            }
            catch (Exception ex)
            {
                result.HasError = "true";
                result.ErrorMessage = ex.Message.Length > 4000 ? ex.Message[..4000] : ex.Message;
            }
            return result;
        }

    }
}

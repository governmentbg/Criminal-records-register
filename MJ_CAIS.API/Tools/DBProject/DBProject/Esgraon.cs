using MJ_CAIS.DataAccess.Entities;
using Oracle.ManagedDataAccess.Client;
using System.Data;
using System.Xml;
using MJ_CAIS.Common.XmlData;


namespace DBProject
{
    public static class Esgraon
    {
        private static string _sql = @"select
                                    person_esgr_id, 
                                    creator_id, 
                                    create_date, 
                                    modifier_id, 
                                    modify_date, 
                                    site_id, 
                                    egn, 
                                    given_name, 
                                    surname, 
                                    family_name, 
                                    birthdate, 
                                    birthplace_code, 
                                    sex, 
                                    mothers_names, 
                                    fathers_names, 
                                    city_code, 
                                    street_building, 
                                    birthplace_text, 
                                    person_esgraon_pk, 
                                    mothers_birthdate, 
                                    fathers_birthdate
                from PERSON_ESGRAON";
        public static void GetData(string sourceConnectionString, string destConnString)
        {
            DataTable dt = new DataTable();
            using (OracleConnection conn = new OracleConnection(sourceConnectionString)) //oracleConnection.Open();
            using (OracleDataAdapter oda = new OracleDataAdapter(_sql, conn))
            {
                oda.SelectCommand.CommandType = CommandType.Text;
                oda.Fill(dt);
            }


            DbHelper.SaveUsingOracleBulkCopy("Z_PERSON_ESGRAON", dt, destConnString);
        }
    }
}

using MJ_CAIS.DataAccess.Entities;
using System.Data;
using System.Data.SqlClient;

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

            TransformTableRows(dt);
            DbHelper.SaveUsingOracleBulkCopy("Z_IMPORT_FBBC_TEST", dt, destConnString);
        }

        public static void TransformTableRows(DataTable dataTable)
        {
            foreach(DataRow row in dataTable.Rows)
            {
                if (row["REGDOC_TYPE"].ToString().Equals("5"))
                {
                    var msg = EEcrisMessage.ParseXml(row["XML_DATA"].ToString());
                    row["MSG_TIMESTAMP"] = msg.MsgTimestamp;
                    row["IDENTIFIER"] = msg.Identifier;
                    row["ECRIS_IDENTIFIER"] = msg.EcrisIdentifier;
                    row["FROM_AUTH_ID"] = msg.FromAuthId; //todo да се извлече Id от номенклатурата, за сега се извлича абревиатурата на държавата
                    row["TO_AUTH_ID"] = msg.ToAuthId; //todo да се извлече Id от номенклатурата, за сега се извлича абревиатурата на държавата
                    row["ECRIS_MSG_CONVICTION_ID"] = msg.EcrisMsgConvictionId;
                  //  row["FIRSTNAME"] = msg.Firstname;
                  //  row["SURNAME"] = msg.Surname;
                  //  row["FAMILYNAME"] = msg.Familyname;
                    row["SEX"] = msg.Sex;
                    row["BIRTH_DATE"] = msg.BirthDate;

                    row["BIRTH_COUNTRY"] = msg.BirthCountry;

                    row["BIRTH_CITY"] = msg.BirthCity;


                 //   row["NATIONALITY1_CODE"] = msg.Nationality1Code; //todo да се извлече по код от номенклатурата, за сега работи защото при миграцията id==код
                 //   row["NATIONALITY2_CODE"] = msg.Nationality2Code; //todo да се извлече по код от номенклатурата, за сега работи защото при миграцията id==код

                }
            }
        }
    }
}

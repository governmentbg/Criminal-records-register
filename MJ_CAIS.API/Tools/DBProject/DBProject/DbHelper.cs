using Oracle.ManagedDataAccess.Client;
using System.Data;

namespace DBProject
{
    public class DbHelper
    {
        public static void SaveUsingOracleBulkCopy(string qualifiedTableName, DataTable dataTable, string connectionString)
        {
            OracleConnection oracleConnection = new OracleConnection(connectionString);
            try
            {
                oracleConnection.Open();
                using (OracleBulkCopy bulkCopy = new OracleBulkCopy(oracleConnection))
                {
                    bulkCopy.DestinationTableName = qualifiedTableName;
                    bulkCopy.BulkCopyTimeout = 0;
                    bulkCopy.WriteToServer(dataTable);
                }
                
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                oracleConnection.Close();
                oracleConnection.Dispose();
            }
        }
    }
}

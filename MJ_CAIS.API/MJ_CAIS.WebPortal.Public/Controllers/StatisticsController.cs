using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using MJ_CAIS.DataAccess;
using MJ_CAIS.WebSetup.Utils;
using Newtonsoft.Json.Converters;
using Oracle.ManagedDataAccess.Client;
using System.Data;
using System.Runtime.Serialization;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace MJ_CAIS.WebPortal.Public.Controllers
{
    public class DateFormatConverter : JsonConverter<DateTime>
    {
        private readonly string Format;
        public DateFormatConverter()
        {
            Format = "yyyy-MM-dd";
        }

        public override void Write(Utf8JsonWriter writer, DateTime date, JsonSerializerOptions options)
        {
            writer.WriteStringValue(date.ToString(Format));
        }
        public override DateTime Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            return DateTime.ParseExact(reader.GetString(), Format, null);
        }
    }

    public class BulletinStatistics
    {
        [JsonConverter(typeof(DateFormatConverter))]
        public DateTime Month { get; set; }
        public string CourtName { get; set; }
        public string CourtCode { get; set; }
        public int ConvictionBulletin { get; set; }
        public int Bulletin78A { get; set; }
    }
    public class StatisticsController : BaseController
    {
        private readonly IMemoryCache _memoryCache;
        private readonly ILogger<StatisticsController> _logger;
        private readonly CaisDbContext _dbContext;

        public StatisticsController(ILogger<StatisticsController> logger, CaisDbContext dbContext,
            IMemoryCache memoryCache)
        {
            _logger = logger;
            _dbContext = dbContext;
            _memoryCache = memoryCache;
        }

        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            List<BulletinStatistics> bulletinStatistics = 
                _memoryCache.Get("BulletinStatistics") as List<BulletinStatistics>;
            if (bulletinStatistics == null)
            {
                bulletinStatistics = new List<BulletinStatistics>();
                DataSet ds = new DataSet();
                using (OracleConnection oracleConnection = new OracleConnection(_dbContext.Database.GetConnectionString()))
                {
                    // Create command
                    OracleCommand cmd = new OracleCommand("STATISTICS.open_data_statistics", oracleConnection);
                    cmd.CommandType = CommandType.StoredProcedure;

                    // Set parameters
                    cmd.Parameters.Add(new OracleParameter("p_out", OracleDbType.RefCursor, null, ParameterDirection.Output));

                    OracleDataAdapter resultDataSet = new OracleDataAdapter(cmd);
                    try
                    {
                        await oracleConnection.OpenAsync();
                        resultDataSet.Fill(ds);
                        foreach (DataRow row in ds.Tables[0].Rows)
                        {
                            var b = new BulletinStatistics()
                            {
                                Month = DateTime.Parse(row["MONTH_CREATED"]?.ToString()),
                                CourtName = row["NAME"]?.ToString(),
                                CourtCode = row["CODE"]?.ToString(),
                                Bulletin78A = Int32.Parse(row["B78"]?.ToString()),
                                ConvictionBulletin = Int32.Parse(row["CONV"]?.ToString()),
                            };
                            bulletinStatistics.Add(b);
                        }
                        _memoryCache.Set("BulletinStatistics", bulletinStatistics, TimeSpan.FromDays(1));
                        return Ok(bulletinStatistics);
                    }
                    catch (Exception exception)
                    {
                        // todo: log
                        throw;
                    }
                    finally
                    {
                        oracleConnection.Close();
                        oracleConnection.Dispose();
                    }
                }
            }
            else
            {
                return Ok(bulletinStatistics);
            }
        }
    }
}

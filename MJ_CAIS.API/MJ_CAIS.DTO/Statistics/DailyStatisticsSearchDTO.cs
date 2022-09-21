using MJ_CAIS.DTO.Shared;

namespace MJ_CAIS.DTO.Statistics
{
    public class DailyStatisticsSearchDTO : PeriodSearchDTO
    {
        public string? Authority { get; set; }
        public string? Status { get; set; }
        public string? StatisticsType { get; set; }
    }
}

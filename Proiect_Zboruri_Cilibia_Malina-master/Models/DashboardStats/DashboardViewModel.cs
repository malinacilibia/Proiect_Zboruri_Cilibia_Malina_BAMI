namespace Proiect_Zboruri_Cilibia_Malina.Models.DashboardStats
{
    public class DashboardViewModel
    {
        public int TotalPredictions { get; set; }

        public List<ClassStat> ClassStats { get; set; } = new();

        public List<AgeBucketStat> AgeBuckets { get; set; } = new();

        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }
    }
}

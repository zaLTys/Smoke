namespace UI.ViewModels
{
    public class ActivityViewModel
    {
        public long Id { get; set; }

        public long ApiRequestId { get; set; } 

        public string ActivityType { get; set; } = string.Empty;

        public decimal Distance { get; set; }

        public decimal Points { get; set; }

        public DateTime StartDate { get; set; }

    }
}

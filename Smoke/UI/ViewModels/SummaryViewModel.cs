namespace UI.ViewModels
{
    public abstract class SummaryViewModel
    {
        public decimal WalkingKm { get; set; } = default!;
        public decimal RunningKm { get; set; } = default!;
        public decimal SkatingKm { get; set; } = default!;
        public decimal CyclingKm { get; set; } = default!;
        public decimal Points { get; set; } = default!;
        public int Activities { get; set; } = default!;
    }
}

﻿namespace BeFit.Application.DataTransferObjects
{
    public class UserPropertiesDto
    {
        public decimal Weight { get; set; }
        public decimal Height { get; set; }
        public decimal FatRate { get; set; }
        public decimal SuggestedFatRate { get; set; }
        public decimal SuggestedWeight { get; set; }
        public decimal DailyCalories { get; set; }
        public decimal FatBurnCalories { get; set; }
        public decimal WeightGainCalories { get; set; }
        public decimal MaintenanceCalories { get; set; }
        public ProteinDto NeededProtein { get; set; } = null!;
        public CarbohydrateDto NeededCarbohydrate { get; set; } = null!;
        public FatDto NeededFat { get; set; } = null!;
        public string UserId { get; set; } = null!;
        public UserDto User { get; set; } = null!;
    }
}

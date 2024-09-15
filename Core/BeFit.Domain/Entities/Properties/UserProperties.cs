﻿using BeFit.Domain.Entities.Base;
using BeFit.Domain.Entities.Enums;
using BeFit.Domain.Entities.Exercise;
using BeFit.Domain.Entities.Identity;
using BeFit.Domain.Entities.Macros;

namespace BeFit.Domain.Entities
{
    public class UserProperties : BaseEntity
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
        public Activity? Activity { get; set; }
        public BodyDecision? BodyDecision { get; set; }
        public Protein NeededProtein { get; set; } = null!;
        public Carbohydrate NeededCarbohydrate { get; set; } = null!;
        public Fat NeededFat { get; set; } = null!;
        public string UserId { get; set; } = null!;
        public User User { get; set; } = null!;
        public decimal BMR => User.Gender == Gender.Male ? (10 * Weight) + ((decimal)6.25 * Height) - (5 * User.Age) + 5 : (10 * Weight) + ((decimal)6.25 * Height) - (5 * User.Age) - 161;
    }
}

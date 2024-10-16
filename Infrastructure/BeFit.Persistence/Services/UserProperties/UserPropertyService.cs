﻿using AutoMapper;
using BeFit.Application.Common;
using BeFit.Application.DataTransferObjects.Create;
using BeFit.Application.DataTransferObjects.Update;
using BeFit.Application.Repositories;
using BeFit.Application.Services.Identity;
using BeFit.Domain.Entities;
using BeFit.Domain.Entities.Enums;
using Microsoft.AspNetCore.Http;

namespace BeFit.Persistence.Services.Identity
{
    public class UserPropertyService(IRepository<UserProperties> repository, IMapper mapper, IUnitOfWork uow) : IUserPropertyService
    {
        public async Task<ServiceResponse<NoContent>> Create(CreateUserPropertiesDto model)
        {
            ArgumentNullException.ThrowIfNull(nameof(model));
            var entity = mapper.Map<UserProperties>(model);
            FillProperties(entity);
            await repository.CreateAsync(entity);
            await uow.SaveChangesAsync();
            return ServiceResponse<NoContent>.Success(StatusCodes.Status201Created);
        }
        public async Task<ServiceResponse<NoContent>> Update(UpdateUserPropertiesDto model)
        {
            var userProperty = mapper.Map<UserProperties>(model);
            userProperty.Height = model.Height;
            userProperty.Weight = model.Weight;
            userProperty.Activity = model.Activity;
            userProperty.BodyDecision = model.BodyDecision;
            FillProperties(userProperty);
            repository.Update(userProperty);
            await uow.SaveChangesAsync();
            return ServiceResponse<NoContent>.Success(StatusCodes.Status204NoContent);
        }
        private static void FillProperties(UserProperties model)
        {
            model.MaintenanceCalories = CalculateMaintenanceCalories(model);
            model.FatBurnCalories = CalculateFatBurnCalories(model);
            model.WeightGainCalories = CalculateWeightGainCalories(model);
            model.SuggestedWeight = CalculateSuggestedWeight(model);
            model.SuggestedFatRate = CalculateSuggestedFatRate(model);
            model.NeededProtein = CalculateNeededProtein(model); //make primitive
            model.NeededCarbohydrate = CalculateNeededCarb(model); //make primitive
            model.NeededFat = CalculateNeededFat(model); //make primitive
        }
        private static decimal CalculateMaintenanceCalories(UserProperties model)
        {
            return model.Activity!.ActivityCoefficient * model.BMR;
        }
        private static decimal CalculateFatBurnCalories(UserProperties model)
        {
            return (model.Activity!.ActivityCoefficient * model.BMR) - 200;
        }
        private static decimal CalculateWeightGainCalories(UserProperties model)
        {
            var calorie = CalculateMaintenanceCalories(model);
            return calorie += (calorie * (decimal)0.2);
        }
        private static decimal CalculateSuggestedWeight(UserProperties model)
        {
            return (decimal)24.9 * (model.Height * model.Height);
        }
        private static decimal CalculateSuggestedFatRate(UserProperties model)
        {
            var fatRate = ((decimal)1.20 * model.Weight / (model.Height * model.Height)) + ((decimal)0.23 * (decimal)model.User.Age);
            fatRate -= model.User.Gender switch
            {
                Gender.Male => (decimal)5.4,
                Gender.Female => (decimal)16.2,
                _ => throw new ArgumentOutOfRangeException()
            };
            return fatRate;
        }
        private static decimal CalculateNeededProtein(UserProperties model)
        {
            decimal protein;
            var bodyWeightWithoutFat = model.Weight - ((model.Weight * model.FatRate) / 100);
            protein = model.BodyDecision switch
            {
                BodyDecision.LoseFat => bodyWeightWithoutFat * (decimal)2.5,
                BodyDecision.MaintainWeight => bodyWeightWithoutFat * (decimal)1.9,
                _ => bodyWeightWithoutFat * (decimal)2.2
            };
            return protein;
        }
        private static decimal CalculateNeededFat(UserProperties model)
        {
            decimal fat;
            var bodyWeightWithoutFat = model.Weight - ((model.Weight * model.FatRate) / 100);
            fat = model.BodyDecision switch
            {
                BodyDecision.LoseFat => bodyWeightWithoutFat * (decimal)1.1,
                BodyDecision.MaintainWeight => ((model.DailyCalories * 25) / 100) / 9,
                _ => bodyWeightWithoutFat * (decimal)1.5 //?
            };
            return fat;
        }
        private static decimal CalculateNeededCarb(UserProperties model)
        {
            var fat = CalculateNeededFat(model);
            var protein = CalculateNeededProtein(model);
            var fatCalorie = fat * 9;
            var proteinCalorie = protein * 4;
            var carb = (model.DailyCalories - (fatCalorie + proteinCalorie)) / 4;
            return carb;
        }
    }
}

using IntelligencePipeline.Configuration;
using IntelligencePipeline.Models.Reports;
using System;
using System.Collections.Generic;
using System.Text;

namespace IntelligencePipeline.Validation;

class SoldierValidator : BaseValidator
{
    protected override ValidationResult ValidateSpecificFields(Report report)
    {
        if (report is not SoldierReport soldierValidator)
            return ValidationResult.Failure($"Invalid {report.GetSourceType()}. Must be Soldier");


        if (soldierValidator.SoldierName.Length < BusinessRules.Soldier.MinNameLength || soldierValidator.SoldierName.Length > BusinessRules.Soldier.MaxNameLength)
            return ValidationResult.Failure($"Invalid {nameof(soldierValidator.SoldierName)}: must be between {BusinessRules.Soldier.MinNameLength} and {BusinessRules.Soldier.MaxNameLength}");
        
        if (soldierValidator.SoldierID.Length != BusinessRules.Soldier.IdRequiredLength)
            return ValidationResult.Failure($"Invalid {nameof(soldierValidator.SoldierID)}: specific format requirement");
        
        if (soldierValidator.Unit.Length < BusinessRules.Soldier.MinUnitLength || soldierValidator.Unit.Length > BusinessRules.Soldier.MaxUnitLength)
            return ValidationResult.Failure($"Invalid {nameof(soldierValidator.Unit)}: must be between {BusinessRules.Soldier.MinUnitLength} and {BusinessRules.Soldier.MaxUnitLength}");
        
        if (soldierValidator.ConfidenceLevel < BusinessRules.Soldier.MinConfidenceLevel || soldierValidator.ConfidenceLevel > BusinessRules.Soldier.MaxConfidenceLevel)
            return ValidationResult.Failure($"Invalid {nameof(soldierValidator.ConfidenceLevel)}: must be between {BusinessRules.Soldier.MinConfidenceLevel} and {BusinessRules.Soldier.MaxConfidenceLevel}");
        
        return ValidationResult.Success();
        }
    }
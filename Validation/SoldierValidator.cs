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
    if(report is not SoldierReport soldierValidator) {
            return ValidationResult.Failure("Error: this report is not soldier report");
        }
        if (soldierValidator.SoldierName.Length< BusinessRules.Soldier.MinNameLength || soldierValidator.SoldierName.Length> BusinessRules.Soldier.MaxNameLength) {
            return ValidationResult.Failure($"Error: SoldierName is longer or shorter from the valid length. its should be between {BusinessRules.Soldier.MinNameLength} to {BusinessRules.Soldier.MaxNameLength} characters");
        }
        if (soldierValidator.SoldierID.Length!=BusinessRules.Soldier.IdRequiredLength) {
            return ValidationResult.Failure("Error: SoldierID is longer or shorter from the valid length. its should be exectly {BusinessRules.Soldier.IdRequiredLength} characters");
        }
        if (soldierValidator.Unit.Length < BusinessRules.Soldier.MinUnitLength || soldierValidator.Unit.Length> BusinessRules.Soldier.MaxUnitLength)
        {
            return ValidationResult.Failure($"Error: Unit is longer or shorter from the valid length. its should be between {BusinessRules.Soldier.MinUnitLength} to {BusinessRules.Soldier.MaxUnitLength} characters");
        }
        if (soldierValidator.ConfidenceLevel< BusinessRules.Soldier.MinConfidenceLevel || soldierValidator.ConfidenceLevel > BusinessRules.Soldier.MaxConfidenceLevel)
        {
            return ValidationResult.Failure($"Error: ConfidenceLevel is greater or less then the valid values. its should be in range of {BusinessRules.Soldier.MinConfidenceLevel} to {BusinessRules.Soldier.MaxConfidenceLevel}");
        }
        return ValidationResult.Success();
    }
}
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
        if (soldierValidator.SoldierName.Length<2 || soldierValidator.SoldierName.Length>50) {
            return ValidationResult.Failure("Error: SoldierName is longer or shorter from the valid length. its should be between 2 to 50 characters{*}");
        }
        if (soldierValidator.SoldierID.Length!=7) {
            return ValidationResult.Failure("Error: SoldierID is longer or shorter from the valid length. its should be exectly 7 characters{*}");
        }
        if (soldierValidator.Unit.Length <2 || soldierValidator.Unit.Length>50)
        {
            return ValidationResult.Failure("Error: Unit is longer or shorter from the valid length. its should be between 2 to 50 characters{*}");
        }
        if (soldierValidator.ConfidenceLevel<1 || soldierValidator.ConfidenceLevel > 5)
        {
            return ValidationResult.Failure("Error: ConfidenceLevel is greater or less then the valid values. its should be in range 1 to 5{*}");
        }
        return ValidationResult.Success();
    }
}
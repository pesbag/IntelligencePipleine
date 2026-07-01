using IntelligencePipeline.Models.Reports;
using System;
using System.Collections.Generic;
using System.Text;

namespace IntelligencePipeline.Validation;
class SoldierValidator : IValidator
{
    public ValidationResult Validate(Report report) {
    if(report is not SoldierReport soldierValidator) {
            return ValidationResult.Failure("Error: this report is not soldier report");
        }
        if (soldierValidator.SoldierName.Length<2 || soldierValidator.SoldierName.Length>50) {
            return ValidationResult.Failure("Error: SoldierName is longer or shorter from the valid length. its should be between 2 to 50");
        }
        if (soldierValidator.SoldierI) { }
    }
    return ValidationResult.Success();
}
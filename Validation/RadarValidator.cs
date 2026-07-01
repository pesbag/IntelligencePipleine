using IntelligencePipeline.Models.Reports;
using System;
using System.Collections.Generic;
using System.Text;

namespace IntelligencePipeline.Validation;
class RadarValidator: BaseValidator
{
    protected override ValidationResult ValidateSpecificFields(Report report)
    {
        if(report is not RadarReport radarReport)
        {
            return ValidationResult.Failure("Error: this report is not radar report");
        }
        if(radarReport.Speed>2000 || radarReport.Speed<0)
        {
            return ValidationResult.Failure("Error: the speed range should be in range of 0 to 2000 {*}");
        }
        if (radarReport.Direction > 360|| radarReport.Direction < 0)
        {
            return ValidationResult.Failure("Error: the direction range should be in range of 0 to 360 {*}");
        }
        if (radarReport.Distance > 10000 || radarReport.Distance < 100)
        {
            return ValidationResult.Failure("Error: the distance range should be in range of 100 to 10000 {*}");
        }
        return ValidationResult.Success();
    }
}
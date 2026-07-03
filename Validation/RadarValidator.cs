using IntelligencePipeline.Configuration;
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
        if(radarReport.Speed> BusinessRules.Radar.MaxSpeed || radarReport.Speed< BusinessRules.Radar.MinSpeed)
        {
            return ValidationResult.Failure($"Error: the speed range should be in range of {BusinessRules.Radar.MinSpeed} to {BusinessRules.Radar.MaxSpeed}");
        }
        if (radarReport.Direction > BusinessRules.Radar.MaxDirection || radarReport.Direction < BusinessRules.Radar.MinDirection)
        {
            return ValidationResult.Failure($"Error: the direction range should be in range of {BusinessRules.Radar.MinDirection} to {BusinessRules.Radar.MaxDirection}");
        }
        if (radarReport.Distance > BusinessRules.Radar.MaxDistance || radarReport.Distance < BusinessRules.Radar.MinDistance)
        {
            return ValidationResult.Failure($"Error: the distance range should be in range of {BusinessRules.Radar.MinDistance} to {BusinessRules.Radar.MaxDistance}");
        }
        return ValidationResult.Success();
    }
}
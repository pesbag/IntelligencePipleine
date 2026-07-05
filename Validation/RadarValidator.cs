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
            return ValidationResult.Failure($"Invalid {report.GetSourceType()}. Must be Radar");
        
        if(radarReport.Speed> BusinessRules.Radar.MaxSpeed || radarReport.Speed< BusinessRules.Radar.MinSpeed)
            return ValidationResult.Failure($"Invalid {nameof(radarReport.Speed)}: must be between {BusinessRules.Radar.MinSpeed} and {BusinessRules.Radar.MaxSpeed}");
        
        if (radarReport.Direction > BusinessRules.Radar.MaxDirection || radarReport.Direction < BusinessRules.Radar.MinDirection)
            return ValidationResult.Failure($"Invalid {nameof(radarReport.Direction)}: must be between {BusinessRules.Radar.MinDirection} and {BusinessRules.Radar.MaxDirection}");
        
        if (radarReport.Distance > BusinessRules.Radar.MaxDistance || radarReport.Distance < BusinessRules.Radar.MinDistance)
            return ValidationResult.Failure($"Invalid {nameof(radarReport.Distance)}: must be between {BusinessRules.Radar.MinDistance} and {BusinessRules.Radar.MaxDistance}");
        
        return ValidationResult.Success();
    }
}
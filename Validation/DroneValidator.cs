using IntelligencePipeline.Configuration;
using IntelligencePipeline.Models.Reports;
using System;
using System.Collections.Generic;
using System.Text;

namespace IntelligencePipeline.Validation;
class DroneValidator:BaseValidator
{
    protected override ValidationResult ValidateSpecificFields(Report report)
    {
        if (report is not DroneReport droneReport)
            return ValidationResult.Failure($"Invalid {report.GetSourceType()}. Must be Drone");
        
        if (droneReport.Altitude < BusinessRules.Drone.MinAltitude || droneReport.Altitude > BusinessRules.Drone.MaxAltitude)
            return ValidationResult.Failure($"Invalid {nameof(droneReport.Altitude)}: must be between {BusinessRules.Drone.MinAltitude} and {BusinessRules.Drone.MaxAltitude}");
        
        if (droneReport.ImageQuality < BusinessRules.Drone.MinImageQuality || droneReport.ImageQuality > BusinessRules.Drone.MaxImageQuality)
            return ValidationResult.Failure($"Invalid {nameof(droneReport.ImageQuality)}: must be between {BusinessRules.Drone.MinImageQuality} and {BusinessRules.Drone.MaxImageQuality}");
        
        return ValidationResult.Success();
    }
}
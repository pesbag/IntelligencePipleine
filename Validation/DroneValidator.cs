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
        {
            return ValidationResult.Failure("Error: this report is not Drone report");
        }
        if (droneReport.Altitude < BusinessRules.Drone.MinAltitude || droneReport.Altitude > BusinessRules.Drone.MaxAltitude)
        {
            return ValidationResult.Failure($"Error: the altitude should be between {BusinessRules.Drone.MinAltitude} to {BusinessRules.Drone.MaxAltitude}");
        }
        if (droneReport.ImageQuality < BusinessRules.Drone.MinImageQuality || droneReport.ImageQuality > BusinessRules.Drone.MaxImageQuality)
        {
            return ValidationResult.Failure($"Error: the quality of image should be between {BusinessRules.Drone.MinImageQuality } to {BusinessRules.Drone.MinImageQuality}");
        }
        return ValidationResult.Success();
    }
}
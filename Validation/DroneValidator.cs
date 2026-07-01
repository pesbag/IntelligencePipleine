using IntelligencePipeline.Models.Reports;
using System;
using System.Collections.Generic;
using System.Text;

namespace IntelligencePipeline.Validation;
class DroneValidator:BaseValidator
{
    public ValidationResult Validate(Report report)
    {
        if (report is not DroneReport droneReport)
        {
            return ValidationResult.Failure( "Error: this report is not Drone report");
        }
        if (droneReport.Altitude < 100 || droneReport.Altitude > 10000)
        {
            return ValidationResult.Failure("Error: the altitude should be between 100 to 10000");
        }
        if (droneReport.ImageQuality < 1 || droneReport.ImageQuality > 100)
        {
            return ValidationResult.Failure("Error: the quality of image should be between 1 to 100");
        }
        return ValidationResult.Success(); // DroneReport passd successfully
    }
}
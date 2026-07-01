using IntelligencePipeline.Models.Reports;
using System;
using System.Collections.Generic;
using System.Text;

namespace IntelligencePipeline.Validation;
public class RadarValidator:IValidator {
    public ValidationResult Validate(Report report);
}
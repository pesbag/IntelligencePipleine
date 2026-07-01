using IntelligencePipeline.Models.Reports;
using System;
using System.Collections.Generic;
using System.Text;

namespace IntelligencePipeline.Validation;
class SigalValidator:IValidator {
    public ValidationResult Validate(Report report);
}
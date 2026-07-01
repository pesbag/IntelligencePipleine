using IntelligencePipeline.Models.Reports;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace IntelligencePipeline.Validation;
interface IValidator
{
    ValidationResult Validate(Report report);
}
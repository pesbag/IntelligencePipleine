using IntelligencePipeline.Configuration;
using IntelligencePipeline.Models.Enums;
using IntelligencePipeline.Models.Reports;
using IntelligencePipeline.Validation;
using System;
using System.Collections.Generic;
using System.Text;

namespace IntelligencePipeline.Validation
{
    class SignalValidator : BaseValidator

    {
        protected override ValidationResult ValidateSpecificFields(Report report)
        {
            SignalReport? signalReport = report as SignalReport;
            
            if (signalReport == null)
                return ValidationResult.Failure($"Invalid {report.GetSourceType()}. Must be Siganl");


            if (signalReport.Frequency < BusinessRules.Signal.MinFrequency || signalReport.Frequency > BusinessRules.Signal.MaxFrequency)
                return ValidationResult.Failure($"Invalid {nameof(signalReport.Frequency)}: must be between {BusinessRules.Signal.MinFrequency} and {BusinessRules.Signal.MaxFrequency}");
            
            if (signalReport.Content.Length < BusinessRules.Signal.MinContentLength || signalReport.Content.Length > BusinessRules.Signal.MaxContentLength)
                return ValidationResult.Failure($"Invalid {nameof(signalReport.Content)}: must be between {BusinessRules.Signal.MinContentLength} and {BusinessRules.Signal.MaxContentLength}");
            
            if (signalReport.SignalStrength < BusinessRules.Signal.MinSignalStrength || signalReport.SignalStrength > BusinessRules.Signal.MaxSignalStrength)
                return ValidationResult.Failure($"Invalid {nameof(signalReport.SignalStrength)}: must be between {BusinessRules.Signal.MinSignalStrength} and {BusinessRules.Signal.MaxSignalStrength}");

            bool isHebrew = string.Equals(signalReport.Language.ToString(), Language.Hebrew.ToString(), StringComparison.OrdinalIgnoreCase);
            bool isArabic = string.Equals(signalReport.Language.ToString(), Language.Arabic.ToString(), StringComparison.OrdinalIgnoreCase);
            bool isEnglish = string.Equals(signalReport.Language.ToString(), Language.English.ToString(), StringComparison.OrdinalIgnoreCase);
            bool isRussian = string.Equals(signalReport.Language.ToString(), Language.Russian.ToString(), StringComparison.OrdinalIgnoreCase);
            bool isOther = string.Equals(signalReport.Language.ToString(), Language.Other.ToString(), StringComparison.OrdinalIgnoreCase);

            if (!isHebrew && !isOther && !isRussian && !isEnglish && !isArabic)
                return ValidationResult.Failure($"Invalid {nameof(signalReport.Language)}. Must be: {Language.Hebrew}, {Language.Arabic}, {Language.English}, {Language.Russian}, {Language.Other}");
            return ValidationResult.Success();
        }
    }
}
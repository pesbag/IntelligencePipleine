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
            {
                return ValidationResult.Failure("Error: this report is not signal report");
            }
            if (signalReport.Frequency < BusinessRules.Signal.MinFrequency || signalReport.Frequency > BusinessRules.Signal.MaxFrequency)
            {
                return ValidationResult.Failure($"Error: frequency report should be in range of {BusinessRules.Signal.MinFrequency} to {BusinessRules.Signal.MaxFrequency}");
            }
            if (signalReport.Content.Length < BusinessRules.Signal.MinContentLength || signalReport.Content.Length > BusinessRules.Signal.MaxContentLength)
            {
                return ValidationResult.Failure($"Error: content report should be in range of {BusinessRules.Signal.MinContentLength} to {BusinessRules.Signal.MaxContentLength} characters");
            }
            if (signalReport.SignalStrength < BusinessRules.Signal.MinSignalStrength || signalReport.SignalStrength > BusinessRules.Signal.MaxSignalStrength)
            {
                return ValidationResult.Failure($"Error: signalStrength should be in range of {BusinessRules.Signal.MinSignalStrength} to {BusinessRules.Signal.MaxSignalStrength}");
            }

            bool isHebrew = string.Equals(signalReport.Language.ToString(), Language.Hebrew.ToString(), StringComparison.OrdinalIgnoreCase);
            bool isArabic = string.Equals(signalReport.Language.ToString(), Language.Arabic.ToString(), StringComparison.OrdinalIgnoreCase);
            bool isEnglish = string.Equals(signalReport.Language.ToString(), Language.English.ToString(), StringComparison.OrdinalIgnoreCase);
            bool isRussian = string.Equals(signalReport.Language.ToString(), Language.Russian.ToString(), StringComparison.OrdinalIgnoreCase);
            bool isOther = string.Equals(signalReport.Language.ToString(), Language.Other.ToString(), StringComparison.OrdinalIgnoreCase);

            if (!isHebrew && !isOther && !isRussian && !isEnglish && !isArabic)
            {
                return ValidationResult.Failure($"Error: language report is illegal it should be one of {Language.Hebrew.ToString()}" +
                    $",{Language.Arabic.ToString()},{Language.English.ToString()},{Language.Russian.ToString()},{Language.Other.ToString()}");
            }
            return ValidationResult.Success();
        }
    }
}
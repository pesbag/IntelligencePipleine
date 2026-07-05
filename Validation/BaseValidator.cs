using IntelligencePipeline.Configuration;
using IntelligencePipeline.Models.Reports;
using System;
using System.Collections;
namespace IntelligencePipeline.Validation;
abstract class BaseValidator : IValidator
{
	public ValidationResult Validate(Report report) {
		ValidationResult result = ValidateCommonFields(report);
		if (!result.IsValid)
		{
			return result;
		}
		return ValidateSpecificFields(report);
	}
	protected ValidationResult ValidateCommonFields(Report report) {
		if (report.Timestamp.CompareTo(DateTime.Now)>0)
			return ValidationResult.Failure($"Invalid {nameof(report.Timestamp)}: cannot be in the future");
		
		if (report.Timestamp < BusinessRules.baseValidator.MinValidDate)
			return ValidationResult.Failure($"Invalid {nameof(report.Timestamp)}: must be between {BusinessRules.baseValidator.MinValidDate} and {DateTime.Now}");
		
		if (report.Latitude > BusinessRules.baseValidator.MaxLatitude || report.Latitude < BusinessRules.baseValidator.MinLatitude)
			return ValidationResult.Failure($"Invalid {nameof(report.Latitude)}: must be between {BusinessRules.baseValidator.MinLatitude} and {BusinessRules.baseValidator.MaxLatitude}");
		
		if(report.Longitude> BusinessRules.baseValidator.MaxLongitude || report.Longitude < BusinessRules.baseValidator.MinLongitude)
			return ValidationResult.Failure($"Invalid {nameof(report.Longitude)}: must be between {BusinessRules.baseValidator.MinLongitude} and {BusinessRules.baseValidator.MaxLongitude}");

		if (string.IsNullOrWhiteSpace(report.Description))
			return ValidationResult.Failure($"Missing required field: {nameof(report.Description)}");
		
		if (report.Description.Length < BusinessRules.baseValidator.MinDescriptionLength || report.Description.Length > BusinessRules.baseValidator.MaxDescriptionLength)
			return ValidationResult.Failure($"Invalid {nameof(report.Description)}: must be between {BusinessRules.baseValidator.MinDescriptionLength} and {BusinessRules.baseValidator.MaxDescriptionLength}");

		return ValidationResult.Success();
	}
	protected abstract ValidationResult ValidateSpecificFields(Report report);
}
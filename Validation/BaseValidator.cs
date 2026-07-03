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
			return ValidationResult.Failure("Error: time of report can not be in the future");
		
		if (report.Timestamp.CompareTo(BusinessRules.baseValidator.MinValidDate) < 0)
			return ValidationResult.Failure($"Error: time of report can not be before {BusinessRules.baseValidator.MinValidDate}");
		
		if (report.Latitude > BusinessRules.baseValidator.MaxLatitude || report.Latitude < BusinessRules.baseValidator.MinLatitude)
			return ValidationResult.Failure($"Error: Latitude of report should be between {BusinessRules.baseValidator.MinLatitude} to {BusinessRules.baseValidator.MaxLatitude}");
		
		if(report.Longitude> BusinessRules.baseValidator.MaxLongitude || report.Longitude < BusinessRules.baseValidator.MinLongitude)
			return ValidationResult.Failure($"Error: Longitude of report should be between {BusinessRules.baseValidator.MinLongitude} to {BusinessRules.baseValidator.MaxLongitude}");
		
		if(string.IsNullOrEmpty(report.Description?.Trim()) || report.Description.Length > BusinessRules.baseValidator.MaxDescriptionLength || report.Description.Length < BusinessRules.baseValidator.MinDescriptionLength)
			return ValidationResult.Failure($"Error: Description length should be between {BusinessRules.baseValidator.MinDescriptionLength} to {BusinessRules.baseValidator.MaxDescriptionLength} characters and Description can not br null");
		
		return ValidationResult.Success();
	}
	protected abstract ValidationResult ValidateSpecificFields(Report report);
}
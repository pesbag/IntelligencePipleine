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
		DateTime minDate = new DateTime(2020, 1, 1);
		if (report.Timestamp.CompareTo(DateTime.Now)>0)
			return ValidationResult.Failure("Error: time of report can not be in the future");
		if (report.Timestamp.CompareTo(minDate) < 0)
			return ValidationResult.Failure($"Error: time of report can not be before {minDate}");
		if (report.Latitude > 33.5000 || report.Latitude < 29.5000)
			return ValidationResult.Failure($"Error: Latitude of report should be between{1} to {1}");
		if(report.Longitude>36.0000 || report.Longitude<34.0000)
			return ValidationResult.Failure($"Error: Longitude of report should be between{1} to {1}");
		if(string.IsNullOrEmpty(report.Description?.Trim()) || report.Description.Length>500 || report.Description.Length<10)
			return ValidationResult.Failure($"Error: Description length should be between{1} to {1} characters and Description can not br null");
		return ValidationResult.Success();
	}
	protected abstract ValidationResult ValidateSpecificFields(Report report);
}
using IntelligencePipeline.Models.Enums;
using IntelligencePipeline.Models.Reports;
using System;
using System.Collections.Generic;
using System.Reflection.Metadata;
using System.Text;
using System.Text.RegularExpressions;

namespace IntelligencePipeline.Calculators;

class PriorityCalculator
{
    public Priority Calculate(Report report) {
        bool isMissile = string.Equals(signalReport.Language.ToString(), WordToIdentify.missile.ToString(), StringComparison.OrdinalIgnoreCase);
        string vehicleExists = $@"\b{WordToIdentify.vehicle.ToString()}\b";
        bool isVehicleExists = Regex.IsMatch(Content, vehicleExists, RegexOptions.IgnoreCase);
    }
}
using IntelligencePipeline.Configuration;
using IntelligencePipeline.Models.Enums;
using IntelligencePipeline.Models.Reports;
using System;
using System.Collections.Generic;
using System.Text;

namespace IntelligencePipeline.Calculators;
class ReliabilityCalculator
{
    public int Calculate(Report report) 
    {
        int reliability=report.CalculateReliabilityScore();
        if(reliability>= BusinessRules.baseValidator.MinReliabilityScore && reliability<= BusinessRules.baseValidator.MaxReliabilityScore)
            return reliability;
        
        else if(reliability > BusinessRules.baseValidator.MaxReliabilityScore)
        {
            reliability = BusinessRules.baseValidator.MaxReliabilityScore;
        }
        
        else if (reliability< BusinessRules.baseValidator.MinReliabilityScore)
        {
            reliability = BusinessRules.baseValidator.MinReliabilityScore;
        }
        return reliability;
    }
}
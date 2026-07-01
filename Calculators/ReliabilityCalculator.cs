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
        if(reliability>=1 && reliability<=10)
            return reliability;
        
        else if(reliability > 10)
        {
            reliability = 10;
        }
        
        else if (reliability<1)
        {
            reliability =1;
        }
        return reliability;
    }
}
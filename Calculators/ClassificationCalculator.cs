using IntelligencePipeline.Models.Enums;
using IntelligencePipeline.Models.Reports;
using System;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.Text;

namespace Calculators;

class ClassificationCalculator
{
    public Classification Calculate(Report report)
    {
        //if (report == null){return Classification.Unclassified;}

        bool hasTopSecretKeywords = ContainsKeyword(report.Description, WordToIdentify.target.ToString(), WordToIdentify.attack.ToString(), WordToIdentify.missile.ToString());
        if (report.Priority == Priority.Critical || hasTopSecretKeywords)
        {
            return Classification.TopSecret;
        }

        bool isSignalSource = report is SignalReport;
        bool hasSecretKeywords = ContainsKeyword(report.Description, WordToIdentify.weapon.ToString(), WordToIdentify.border.ToString());
        if (report.Priority == Priority.High || isSignalSource || hasSecretKeywords)
        {
            return Classification.Secret;
        }

        bool isSoldierSource = report is SoldierReport;
        if (report.Priority == Priority.Medium || isSoldierSource)
        {
            return Classification.Restricted;
        }
        return Classification.Unclassified;
    }
    private bool ContainsKeyword(string text, params string[] keywords)
    {
        if (string.IsNullOrEmpty(text)) { return false; }

        foreach (string keyword in keywords)
        {
            string pattern = $@"\b{keyword}\b";
            if (Regex.IsMatch(text, pattern, RegexOptions.IgnoreCase))
            {
                return true;
            }
        }
        return false;
    }
}
using IntelligencePipeline.Configuration;
using IntelligencePipeline.Models.Enums;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;

namespace IntelligencePipeline.Models.Reports;
class SignalReport:Report
{
    private double _frequency;
    private string _content=string.Empty;
    private Language _language;
    private int _signalStrength;
    public double Frequency {
        get { return _frequency; }
        protected set { _frequency = value; }
    }
    public string Content {
        get { return _content; }
        protected set { _content = value; }
    }
    public Language Language {
        get { return _language; }
        protected set { _language = value; }
    }
    public int SignalStrength {
        get { return _signalStrength; }
        set { _signalStrength = value; }
    }
    public override string GetSourceType() => "Signal";
    public override int CalculateReliabilityScore() {
        int Base = BusinessRules.Signal.BaseReliability;
        
        string vehicleKeyword = $@"\b{WordToIdentify.vehicle.ToString()}\b";
        bool hasVehicleKeyword = Regex.IsMatch(Content, vehicleKeyword, RegexOptions.IgnoreCase);
        string attackKeyword = $@"\b{WordToIdentify.attack.ToString()}\b";
        bool hasAttackKeyword = Regex.IsMatch(Content, attackKeyword, RegexOptions.IgnoreCase);
        string targetKeyword = $@"\b{WordToIdentify.target.ToString()}\b";
        bool hasTargetKeyword = Regex.IsMatch(Content, targetKeyword, RegexOptions.IgnoreCase);
        string borderKeyword = $@"\b{WordToIdentify.border.ToString()}\b";
        bool hasBorderKeyword = Regex.IsMatch(Content, borderKeyword, RegexOptions.IgnoreCase);     
        
        if (SignalStrength >= BusinessRules.Signal.StrongSignalThreshold)
            Base += BusinessRules.Signal.StrongSignalBonus;
        
        else if (SignalStrength >= BusinessRules.Signal.MediumSignalThreshold)
            Base += BusinessRules.Signal.MediumSignalBonus;
        
        if (SignalStrength < BusinessRules.Signal.WeakSignalLimit)
            Base -= BusinessRules.Signal.WeakSignalPenalty;
        Base += (hasVehicleKeyword ? 1 : 0) + (hasAttackKeyword ? 1 : 0) + (hasTargetKeyword ? 1 : 0) + (hasBorderKeyword ? 1:0);
        
        return Base;
    }
    public SignalReport(int reportId, DateTime timestamp, double latitude,double longitude, string description,double frequency, string content, Language language,int signalStrength)
            :base(reportId, timestamp, latitude, longitude, description) 
    {
        Frequency = frequency;
        Content = content;
        Language = language;
        SignalStrength = signalStrength;
    }


}
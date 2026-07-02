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
    private string _content;
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
        int Base = 5;
        
        string vehicleKeyword = $@"\b{WordToIdentify.vehicle.ToString()}\b";
        bool hasVehicleKeyword = Regex.IsMatch(Content, vehicleKeyword, RegexOptions.IgnoreCase);
        string attackKeyword = $@"\b{WordToIdentify.vehicle.ToString()}\b";
        bool hasAttackKeyword = Regex.IsMatch(Description, attackKeyword, RegexOptions.IgnoreCase);
        string targetKeyword = $@"\b{WordToIdentify.vehicle.ToString()}\b";
        bool hasTargetKeyword = Regex.IsMatch(Description, targetKeyword, RegexOptions.IgnoreCase);
        string borderKeyword = $@"\b{WordToIdentify.vehicle.ToString()}\b";
        bool hasBorderKeyword = Regex.IsMatch(Description, borderKeyword, RegexOptions.IgnoreCase);
        
        if (SignalStrength >= -40)
            Base += 3;
        else if (SignalStrength >= -70)
            Base += 2;
        if (SignalStrength < -100)
            Base -= 2;
        Base += (hasVehicleKeyword ? 1 : 0) + (hasAttackKeyword ? 1 : 0) + (hasTargetKeyword ? 1 : 0) + (hasBorderKeyword ? 1:0);
        return Base;
    }
    public SignalReport(int reportId, DateTime timestamp, double latitude,double longitude, string description,double frequency, string content, Language language,int signalStrength)
            :base(reportId, timestamp, latitude, longitude, description) { }


}
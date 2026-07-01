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
        //attack,target,border
        string vehicleExists = $@"\b{WordToIdentify.vehicle.ToString()}\b";
        bool isVehicleExists = Regex.IsMatch(Content, vehicleExists, RegexOptions.IgnoreCase);
        string attackExists = $@"\b{WordToIdentify.vehicle.ToString()}\b";
        bool isAttackExists = Regex.IsMatch(Description, attackExists, RegexOptions.IgnoreCase);
        string targetExists = $@"\b{WordToIdentify.vehicle.ToString()}\b";
        bool isTargetExists = Regex.IsMatch(Description, targetExists, RegexOptions.IgnoreCase);
        string borderExists = $@"\b{WordToIdentify.vehicle.ToString()}\b";
        bool isBorderExists = Regex.IsMatch(Description, borderExists, RegexOptions.IgnoreCase);
        if (SignalStrength >= -40)
            Base += 3;
        else if (SignalStrength >= -70)
            Base += 2;
        if (SignalStrength < -100)
            Base -= 2;
        Base += (isVehicleExists ? 1 : 0) + (isAttackExists ? 1 : 0) + (isTargetExists ? 1 : 0) + (isBorderExists? 1:0);
        return Base;
    }
    public SignalReport(int reportId, DateTime timestamp, double latitude,double longitude, string description,double frequency, string content, Language language,int signalStrength)
            :base(reportId, timestamp, latitude, longitude, description) { }


}
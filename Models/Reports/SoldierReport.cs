using IntelligencePipeline.Configuration;
using IntelligencePipeline.Models.Enums;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace IntelligencePipeline.Models.Reports;

class SoldierReport:Report
{
    private string _soldierName = string.Empty;
    private string _soldierID = string.Empty;
    private string _unit = string.Empty;
    private int _confidenceLevel;
    public string SoldierName {
        get { return _soldierName; }
        protected set { _soldierName = value; }
    }
    public string SoldierID {
        get { return _soldierID; }
        protected set { _soldierID = value; }
    }
    public string Unit {
        get { return _unit; }
        protected set { _unit = value; }
    }
    public int ConfidenceLevel {
        get { return _confidenceLevel; }
        protected set { _confidenceLevel = value; }
    }
    public override string GetSourceType() => "Soldier";

    public override int CalculateReliabilityScore()
    {
        string weaponKeyword = $@"\b{WordToIdentify.weapon.ToString()}\b";
        bool hasWeaponKeyword = Regex.IsMatch(Description, weaponKeyword, RegexOptions.IgnoreCase);
        string vehicleKeyword = $@"\b{WordToIdentify.vehicle.ToString()}\b";
        bool hasVehicleKeyword = Regex.IsMatch(Description, vehicleKeyword, RegexOptions.IgnoreCase);
        string movementKeyword = $@"\b{WordToIdentify.movement.ToString()}\b";
        bool hasMovementKeyword = Regex.IsMatch(Description, movementKeyword, RegexOptions.IgnoreCase);
        string explosionKeyword = $@"\b{WordToIdentify.explosion.ToString()}\b";
        bool hasExplosionKeyword = Regex.IsMatch(Description, explosionKeyword, RegexOptions.IgnoreCase);

        int Base = BusinessRules.Soldier.BaseReliability;
        Base += ConfidenceLevel;
        Base += (hasWeaponKeyword ? 1 : 0) + (hasVehicleKeyword ? 1 : 0) + (hasMovementKeyword ? 1 : 0) + (hasExplosionKeyword ? 1 : 0);
        return Base;
    }

    public SoldierReport(int reportId, DateTime timestamp, double latitude, double longitude, string description, string soldierName, string soldierID, string unit, int confidenceLevel) : base(reportId, timestamp, latitude, longitude, description)
    {
        SoldierName = soldierName;
        SoldierID = soldierID;
        Unit = unit;
        ConfidenceLevel = confidenceLevel;
    }
}

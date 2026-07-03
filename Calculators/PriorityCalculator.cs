using IntelligencePipeline.Configuration;
using IntelligencePipeline.Models.Enums;
using IntelligencePipeline.Models.Reports;
using System;
using System.Text.RegularExpressions;

namespace IntelligencePipeline.Calculators
{
    class PriorityCalculator
    {
        public Priority Calculate(Report report)
        {
            if (report == null) return Priority.Low;

            RadarReport? radarReport = report as RadarReport;
            SignalReport? signalReport = report as SignalReport;
            DroneReport? droneReport = report as DroneReport;
            SoldierReport? soldierReport = report as SoldierReport;

            bool hasCriticalKeywords = ContainsKeyword(report.Description,
                WordToIdentify.missile.ToString(),
                WordToIdentify.explosion.ToString(),
                WordToIdentify.attack.ToString(),
                WordToIdentify.fire.ToString());

            bool isCriticalRadarSpeed = radarReport != null && radarReport.Speed >= BusinessRules.Radar.criticalSpeedThreshold;

            bool isSignalWithTargetAndAttack = signalReport != null &&
                ContainsKeyword(signalReport.Description, WordToIdentify.vehicle.ToString()) &&
                ContainsKeyword(signalReport.Description, WordToIdentify.attack.ToString());

            if (hasCriticalKeywords || isCriticalRadarSpeed || isSignalWithTargetAndAttack)
            {
                return Priority.Critical;
            }

            bool hasHighKeywords = ContainsKeyword(report.Description,
                WordToIdentify.weapon.ToString(),
                WordToIdentify.suspicious.ToString(),
                WordToIdentify.border.ToString());

            bool isHighDroneAltitude = droneReport != null && droneReport.Altitude < BusinessRules.Drone.HighPriorityAltitudeThreshold;
            bool isHighRadarSpeed = radarReport != null && radarReport.Speed >= BusinessRules.Radar.highSpeedThreshold;

            bool isHighSoldierMovement = soldierReport != null &&
                soldierReport.ConfidenceLevel >= BusinessRules.Soldier.HighConfidenceThreshold &&
                ContainsKeyword(soldierReport.Description, WordToIdentify.movement.ToString());

            if (hasHighKeywords || isHighDroneAltitude || isHighRadarSpeed || isHighSoldierMovement)
            {
                return Priority.High;
            }

            bool hasMediumKeywords = ContainsKeyword(report.Description,
                WordToIdentify.movement.ToString(),
                WordToIdentify.vehicle.ToString(),
                WordToIdentify.activity.ToString());

            bool isMediumRadarSpeed = radarReport != null && radarReport.Speed >= BusinessRules.Radar.MediumSpeedThreshold;
            bool isHighReliability = report.CalculateReliabilityScore() >= BusinessRules.Metrics.MediumPriorityReliabilityThreshold;

            if (hasMediumKeywords || isMediumRadarSpeed || isHighReliability)
            {
                return Priority.Medium;
            }

            return Priority.Low;
        }

        private bool ContainsKeyword(string text, params string[] keywords)
        {
            if (string.IsNullOrEmpty(text)) {return false;}

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
}
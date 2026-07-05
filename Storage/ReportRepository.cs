using IntelligencePipeline.Models.Enums;
using IntelligencePipeline.Models.Reports;
using IntelligencePipeline.Storage;
using System;
using System.Collections.Generic;
using System.Reflection.Metadata;
using System.Text;
using System.Text.RegularExpressions;

namespace IntelligencePipeline.Storage;
class ReportRepository
{
    private List<Report> _reports;
    public ReportRepository()
    {
        _reports = [];
    }

    public void Add(Report report) {
        _reports.Add(report);
    }

    public List<Report> GetAll()
    {
        return _reports;
    }

    public List<Report> GetByStatus(ReportStatus status) {
        List<Report> reportStatusList= new List<Report>();
        for(int s =0; s < _reports.Count; s++)
        {
            if (_reports[s].Status == status)
            {
                reportStatusList.Add(_reports[s]);
            }
        }
        return reportStatusList;
    }

    public List<Report> GetByPriority(Priority priority) {
        List<Report> reportPriorityList = new List<Report>();
        for(int p = 0; p < _reports.Count; p++)
        {
            if (_reports[p].Priority == priority)
            {
                reportPriorityList.Add(_reports[p]);
            }
        }
        return reportPriorityList;
    }

    public List<Report> SortByTimestamp()
    {
        return _reports.OrderByDescending(r => r.Timestamp).ToList();
    }

    public List<Report> SortByPriority()
    {
        return _reports.OrderByDescending(r => r.Priority).ToList();
    }

    public List<Report> SortByReliabilityScore()
    {
        return _reports.OrderByDescending(r => r.ReliabilityScore).ToList();
    }

    public List<Report> GetBySourceType(string source)
    {
        List<Report> reportSourceTypeList = new List<Report>();
        for (int i = 0; i < _reports.Count; i++)
        {
            if (_reports[i].GetSourceType().ToLower() == source.ToLower())
            {
                reportSourceTypeList.Add(_reports[i]);
            }
        }
        return reportSourceTypeList;
    }

    public List<Report> GetByDateRange(DateTime startDate, DateTime endDate)
    {
        List<Report> reportsInValueRange = new List<Report>();

        for (int i = 0; i < _reports.Count; i++)
            if (_reports[i].Timestamp >= startDate && _reports[i].Timestamp <= endDate)
                reportsInValueRange.Add(_reports[i]);

        return reportsInValueRange;
    }
    public List<Report> GetByClassification(Classification classification)
    {
        List<Report> reportPriorityList = new List<Report>();
        for (int i = 0; i < _reports.Count; i++)
        {
            if (_reports[i].Classification == classification)
            {
                reportPriorityList.Add(_reports[i]);
            }
        }
        return reportPriorityList;
    }

    public List<Report> Search(string keyword) {
        List<Report> SearchKeyWord = new List<Report>();
        string findKeyword = $@"\b{keyword}\b";
        
        for (int i = 0; i < _reports.Count; i++)
        {
            if (!string.IsNullOrEmpty(_reports[i].Description) && Regex.IsMatch(_reports[i].Description, findKeyword, RegexOptions.IgnoreCase))
            {
                SearchKeyWord.Add(_reports[i]);
            }
        }
        return SearchKeyWord;
    }

    public Report? GetById(int reportId) {
        for(int i = 0; i < _reports.Count; i++)
        {
            if (_reports[i].ReportId == reportId)
            {
                return _reports[i];
            }
        }
        return null;
    }

    public void UpdateStatus(int reportId, ReportStatus newStatus) {
        bool reportIdFound = false;
        
        for(int i = 0; i < _reports.Count; i++)
        {
            if (_reports[i].ReportId == reportId)
            {
                reportIdFound = true;
                _reports[i].Status = newStatus;
                break;
            }
        }
        
        if (!reportIdFound)
            Console.WriteLine($"Error:  reportId {reportId} was not found, can not update the status");
    }

    public int GetTotalCount() {
        return _reports.Count;
    }
    public int GetCountByStatus(ReportStatus status) {
        int countByStatus = 0;
        for(int i=0; i < _reports.Count; i++)
        {
            if (_reports[i].Status == status)
            {
                countByStatus += 1;
            }
        }
        return countByStatus;
    } 
}
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
        //List<Report> reportList = new List<Report>();
        //for(int report = 0; report < _reports.Count; report++)
        //{
        //    reportList.Add(_reports[report]);
        //}
        //return reportList;
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
        for(int p =0; p< _reports.Count; p++)
        {
            if (_reports[p].Priority == priority)
            {
                reportPriorityList.Add(_reports[p]);
            }
        }
        return reportPriorityList;
    }

    public List<Report> Search(string keyword) {
        List<Report> SearchKeyWord = new List<Report>();
        string findKeyword = $@"\b{keyword}\b";
        //bool hasVehicleKeyword=false; 
        
        for (int i = 0; i < _reports.Count; i++)
        {
            if (Regex.IsMatch(_reports[i].Description, findKeyword, RegexOptions.IgnoreCase))
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
        //if(newStatus!=)
        bool reportIdFound = false;
        for(int i = 0; i < _reports.Count; i++)
        {
            if (_reports[i].ReportId == reportId)
            {
                reportIdFound = true;
                _reports[i].Status = newStatus;
            }
        }
        if (!reportIdFound)
        {
            Console.WriteLine("Error: reportId was not found, can not update the status");
        }
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
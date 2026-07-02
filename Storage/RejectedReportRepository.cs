using IntelligencePipeline.Models.Reports;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace IntelligencePipeline.Storage;

class RejectedReportRepository
{
    private List<Report> _rejectedReports;

    public RejectedReportRepository()
    {
        _rejectedReports = [];
    }

    public void Add(Report report) {
        _rejectedReports.Add(report);
    }

    public List<Report> GetAll() 
    {
        return _rejectedReports;
    }

    public int GetTotalCount()
    {
        return _rejectedReports.Count;
    }

    public List<Report> GetByReason(string reasonKeyword)
    {
        List<Report> SearchKeyWord = new List<Report>();
        string findKeyword = $@"\b{reasonKeyword}\b";

        for (int i = 0; i < _rejectedReports.Count; i++)
        {
            if (Regex.IsMatch(_rejectedReports[i].DescriptionRejectionReason, reasonKeyword, RegexOptions.IgnoreCase))
            {
                SearchKeyWord.Add(_rejectedReports[i]);
            }
        }
        return SearchKeyWord;
    }

}

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace CrystalDecisions.CrystalReports
{
    public interface IReport
    {
        void Generate(string filename);
    }

    public class CrystalReportsReport : IReport
    {
        private string filename;
        private string connectionString;

        private ReportDocument rpt;

        public CrystalReportsReport(string filename, string connectionString)
        {
            this.filename = filename;
            this.connectionString = connectionString;
        }

        public void Generate(string output)
        {
            rpt.Load(filename);
            // rpt.SetDatabaseLogon()

            rpt.ExportToDisk(ReportDocument.ExportFormatType.PortableDocFormat, output);
        }
    }

    public class ReportDocument
    {
        public ReportDocument()
        {
            Database = new Database();
        }

        public Database Database { get; set; }


        public void Load(string filename)
        {
            Console.WriteLine($"Load {filename}");
        }

        public void SetDatabaseLogon(string user, string password)
        {

        }

        public void ExportToDisk(ExportFormatType format, string filename)
        {

        }

        public enum ExportFormatType
        {
            PortableDocFormat,
            Word,
            Excel
        }
    }

    public struct ConnectionInfo
    {
        public string ServerName;
        public string DatabaseName;
        public string UserID;
        public string Password;
    }

    public class Table
    {
        public LogOnInfo LogOnInfo { get; set; }

        public void ApplyLogOnInfo(LogOnInfo logOnInfo)
        {

        }
    }

    public class Database
    {
        public IEnumerable<Table> Tables { get; set; }

        public Database()
        {
            Tables = new Collection<Table>();
        }
    }

    public class LogOnInfo
    {
        public ConnectionInfo ConnectionInfo { get; set; }

        public LogOnInfo()
        {
            ConnectionInfo = new ConnectionInfo();
        }
    }


}

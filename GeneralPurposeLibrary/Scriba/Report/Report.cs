using System.Collections.Generic;

namespace GeneralPurposeLibrary.Scriba.Report
{
    public abstract class ReportOutputItem
    {
    }

    public abstract class ReportOutputTable
    {
        private IEnumerable<ReportOutputItem> _Items;

        public IEnumerable<ReportOutputItem> Items
        {
            get
            {
                return this._Items;
            }
        }
    }

    public class ReportGearArguments
    {
    }

    public class ReportGearOutputFormat
    {
    }

    public abstract class ReportGear
    {
        private ReportOutputTable _Table;

        public ReportOutputTable Table
        {
            get
            {
                return this._Table;
            }
        }

        private ReportGearArguments _Arguments;

        public ReportGear(ReportGearArguments arguments)
        {
            _Arguments = arguments;
        }

        public abstract void Process();

        public abstract void SaveAs(ReportGearOutputFormat format);
    }
}
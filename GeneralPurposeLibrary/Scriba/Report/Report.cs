using System;
using System.Collections.Generic;
using System.Diagnostics;

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
        public readonly Action ManageStartProcess = null;
        public readonly Action ManageFinishProcess = null;
    }

    public class ReportGearOutputFormat
    {
    }

    public enum AlgorithmVersion
    {
        VERSION1,
        VERSION2,
        VERSION3,
        VERSION4,
        VERSION5,
        VERSION6,
        VERSION7,
        VERSION8
    }

    public abstract class ReportGear
    {

        protected String _Prefix;

        protected Stopwatch _swRelation;

        protected AlgorithmVersion _AlgorithmVersion;

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


            if(_Arguments.ManageStartProcess != null)
            {
                this.ManageStartProcess = _Arguments.ManageStartProcess;
            }

            if (_Arguments.ManageFinishProcess != null)
            {
                this.ManageFinishProcess = _Arguments.ManageFinishProcess;
            }

        }

        protected void DefaultManageStartProcess()
        {
            _swRelation = new Stopwatch();
            _swRelation.Start();
            Console.WriteLine(String.Format("{0} starts ...", this._Prefix));
        }

        protected void DefaultManageFinishProcess()
        {
            GC.Collect();
            GC.WaitForPendingFinalizers();
            _swRelation.Stop();
            Console.WriteLine("... and {0} takes {1} ms to be executed", this._Prefix, _swRelation.ElapsedMilliseconds);
        }

        protected Action ManageStartProcess = null;
        protected Action ManageFinishProcess = null;


        public void Process()
        {
            if (this.ManageStartProcess != null)
            {
                this.ManageStartProcess();
            }
        
            this.InnerProcess();

            if (this.ManageFinishProcess != null)
            {
                this.ManageFinishProcess();
            }

        }

        protected abstract void InnerProcess();

        public abstract void SaveAs(ReportGearOutputFormat format);
    }
}
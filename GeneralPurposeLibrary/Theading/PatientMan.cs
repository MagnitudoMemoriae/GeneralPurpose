using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace GeneralPurposeLibrary.Theading
{
    public class PatientMan
    {
        private Action _DoWork = null;
        private Boolean _IsDoWorkSubscribe = false;
        private DoWorkEventHandler _DoWorkHandler;

        private Action _RunWorkerCompleted = null;
        private Boolean _IsRunWorkerCompletedSubscribe = false;
        private RunWorkerCompletedEventHandler _RunWorkerCompletedHandler;

        private Action<int> _ProgressChanged = null;
        private Boolean _IsProgressChangedSubscribe = false;
        private ProgressChangedEventHandler _ProgressChangedHandler;

        private BackgroundWorker _Worker = new BackgroundWorker();

        public PatientMan()
        {

        }

        public PatientMan(Action doWorkAction,
                            Action runWorkerCompleted)
        {
            this.SubcribeDoWork(doWorkAction);
            this.SubcribeRunWorkerCompleted(runWorkerCompleted);
        }

        private void SubcribeDoWork(Action doWorkAction)
        {
            this._DoWork = doWorkAction;
            this._DoWorkHandler = delegate { this._DoWork(); };
            this._Worker.DoWork += _DoWorkHandler;
            this._IsDoWorkSubscribe = true;
        }

        private void SubcribeRunWorkerCompleted(Action runWorkerCompleted)
        {
            this._RunWorkerCompleted = runWorkerCompleted;
            this._RunWorkerCompletedHandler
                =
            delegate {
                this._RunWorkerCompleted();
                this.UnsubscribeEvents();
            };
            this._Worker.RunWorkerCompleted += this._RunWorkerCompletedHandler;
            this._IsRunWorkerCompletedSubscribe = true;
        }

        private void SubcribeProgressChanged(Action<int> progressChanged)
        {
            this._ProgressChanged = progressChanged;
            this._ProgressChangedHandler = (obj, ev) =>
            {
                this._ProgressChanged(ev.ProgressPercentage);
            };
            this._Worker.ProgressChanged += this._ProgressChangedHandler;
            this._Worker.WorkerReportsProgress = true;
            this._IsProgressChangedSubscribe = true;
        }

        public void RunWorkerAsync()
        {
            this._Worker.RunWorkerAsync();
        }

        public void ReportProgress(int percentage)
        {
            if (this._ProgressChanged != null)
            {
                if (this._IsProgressChangedSubscribe == true)
                {
                    this._Worker.ReportProgress(percentage);
                }
                else
                {
                    throw new ArgumentNullException();
                }
            }
            else
            {
                throw new NullReferenceException();
            }
        }

        private void UnsubscribeEvents()
        {
            if (this._IsDoWorkSubscribe == true)
            {
                this._Worker.DoWork -= this._DoWorkHandler;
                this._IsDoWorkSubscribe = false;
            }

            if (this._IsRunWorkerCompletedSubscribe == true)
            {
                this._Worker.RunWorkerCompleted -= this._RunWorkerCompletedHandler;
                this._IsRunWorkerCompletedSubscribe = false;
            }

            if (this._IsProgressChangedSubscribe == true)
            {
                this._Worker.ProgressChanged -= this._ProgressChangedHandler;
                this._Worker.WorkerReportsProgress = false;
                this._IsProgressChangedSubscribe = false;
            }
        }

        public void Dispose()
        {
            this.UnsubscribeEvents();
            this._Worker.Dispose();
        }

        public void SetEvents(Action doWorkAction,
                            Action runWorkerCompleted,
                            Action<int> progressChanged)
        {
            this.UnsubscribeEvents();

            this.SubcribeDoWork(doWorkAction);
            this.SubcribeRunWorkerCompleted(runWorkerCompleted);
            this.SubcribeProgressChanged(progressChanged);
        }
    }

    public class SandBoxPatientMan
    {
        public SandBoxPatientMan(int algo)
        {

            switch (algo)
            {
                case 0:
                    {
                        Action doWork = delegate
                        {
                            // Do something long
                            for (int i = 0; i < 10; i++)
                            {
                                System.Threading.Thread.Sleep(1000);
                            }
                        };

                        Action runWorkerCompleted = delegate
                        {
                            // Do something after you did something long

                            Console.WriteLine("runWorkerCompleted " + algo);
                        };

                        PatientMan patientMan = new PatientMan(doWork, runWorkerCompleted);

                        patientMan.RunWorkerAsync();
                    }
                    break;

                case 1:
                    {
                        PatientMan patientMan = new PatientMan();

                        Action doWork = delegate
                        {
                            // Do something long

                            for (int i = 0; i < 10; i++)
                            {
                                System.Threading.Thread.Sleep(1000);

                                patientMan.ReportProgress(i);
                            }
                        };

                        Action runWorkerCompleted = delegate
                        {
                            // Do something after you did something long

                            Console.WriteLine("runWorkerCompleted " + algo);
                        };

                        Action<int> progressChanged = (percentage) =>
                        {
                            // Report a progress during a long process

                            Console.WriteLine("progressChanged " + percentage);
                        };

                        patientMan.SetEvents(doWork, runWorkerCompleted, progressChanged);

                        patientMan.RunWorkerAsync();
                    }
                    break;
            }



        }
    }
}

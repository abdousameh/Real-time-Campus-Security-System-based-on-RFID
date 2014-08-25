/*
#############################################################################
#                                                                           #
#              IMPINJ CONFIDENTIAL AND PROPRIETARY                          #
#                                                                           #
# This source code is the sole property of Impinj, Inc. Reproduction or     #
# utilization of this source code in whole or in part is forbidden without  #
# the prior written consent of Impinj, Inc.                                 #
#                                                                           #
# (c) Copyright Impinj, Inc. 2010. All rights reserved.                     #
#                                                                           #
#############################################################################
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Impinj.OctaneSdk;
using System.ComponentModel;
using System.Windows.Input;

namespace Octane_ViewModel
{
    public partial class SpeedwayReaderViewModel
    {
        public ICommand ResumeAsyncCommand { get; set; }

        private void _resumeAsyncExecute(object obj)
        {
            _log(LogLevel.Verbose, "ResumeAsync to '{0}'", ReaderName);

            var backgroundWorker = new BackgroundWorker();
            backgroundWorker.WorkerReportsProgress = true;
            backgroundWorker.WorkerSupportsCancellation = true;
            backgroundWorker.DoWork += new DoWorkEventHandler(_resumeDoWork);
            backgroundWorker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(_resumeRunWorkerCompleted);

            backgroundWorker.RunWorkerAsync(ReaderName);
        }

        private bool _resumeAsyncCanExecute(object obj)
        {
            return true;
        }
        private void _resumeRunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            StatusLabel = "Resume Complete";
        }

        private void _resumeDoWork(object sender, DoWorkEventArgs e)
        {
            StatusLabel = "Resuming Reader";

            try
            {
                ResumeEventsAndReports();
            }
            catch (Exception ex)
            {
                _log(LogLevel.Error, ex.Message);
            }
        }
    }
}

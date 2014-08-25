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
using System.Windows.Input;
using System.ComponentModel;
using Impinj.OctaneSdk;

namespace Octane_ViewModel
{
    public partial class SpeedwayReaderViewModel
    {
        public ICommand ClearSettingsAsyncCommand { get; set; }

        private void _clearSettingsAsyncExecute(object obj)
        {
            _log(LogLevel.Verbose, "ClearSettingsAsync to '{0}'", ReaderName);

            var backgroundWorker = new BackgroundWorker();
            backgroundWorker.WorkerReportsProgress = true;
            backgroundWorker.WorkerSupportsCancellation = true;
            backgroundWorker.DoWork += new DoWorkEventHandler(_clearSettingsDoWork);
            backgroundWorker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(_clearSettingsRunWorkerCompleted);

            backgroundWorker.RunWorkerAsync(ReaderName);
        }

        private bool _clearSettingsAsyncCanExecute(object obj)
        {
            return true;
        }
        private void _clearSettingsRunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            StatusLabel = "Cleared Settings";
        }

        private void _clearSettingsDoWork(object sender, DoWorkEventArgs e)
        {
            StatusLabel = "Clearing Settings";
            ClearSettings();
            OnPropertyChanged("Settings");
        }
    }
}

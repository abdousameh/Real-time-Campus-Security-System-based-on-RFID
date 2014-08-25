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
using Impinj.OctaneSdk;
using System.ComponentModel;

namespace Octane_ViewModel
{
    public partial class SpeedwayReaderViewModel
    {
        public ICommand QueryFactorySettingsAsyncCommand { get; set; }

        private void _queryFactorySettingsAsyncExecute(object obj)
        {
            _log(LogLevel.Verbose, "QueryFactorySettingsAsync to '{0}'", ReaderName);

            var backgroundWorker = new BackgroundWorker();
            backgroundWorker.WorkerReportsProgress = true;
            backgroundWorker.WorkerSupportsCancellation = true;
            backgroundWorker.DoWork += new DoWorkEventHandler(_queryFactorySettingsAsyncDoWork);
            backgroundWorker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(_queryFactorySettingsAsyncRunWorkerCompleted);

            backgroundWorker.RunWorkerAsync(ReaderName);
        }

        private bool _queryFactorySettingsAsyncCanExecute(object obj)
        {
            return true;
        }
        private void _queryFactorySettingsAsyncRunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            StatusLabel = "Query Factory Settings Complete";
        }

        private void _queryFactorySettingsAsyncDoWork(object sender, DoWorkEventArgs e)
        {
            StatusLabel = "Querying Factory Settings";

            var settings = QueryFactorySettings();

            settings.Report.Mode = ReportMode.BatchAfterStop;
            settings.Report.IncludeAntennaPortNumber = true;
            settings.Report.IncludeChannel = true;
            settings.Report.IncludeFirstSeenTime = true;
            settings.Report.IncludeLastSeenTime = true;
            settings.Report.IncludePeakRssi = true;
            settings.Report.IncludePhaseAngle = true;
            settings.Report.IncludeSeenCount = true;

            Settings = SettingsViewModel.Factory(settings, _mainWindowCallback);

            OnPropertyChanged("Settings");
        }
    }
}

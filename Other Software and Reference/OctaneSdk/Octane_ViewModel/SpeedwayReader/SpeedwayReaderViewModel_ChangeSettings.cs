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
using System.Windows.Input;
using System.ComponentModel;

namespace Octane_ViewModel
{
    public partial class SpeedwayReaderViewModel
    {
        public ICommand ChangeSettingsAsyncCommand { get; set; }

        private void _changeSettingsAsyncExecute(object obj)
        {
            _log(LogLevel.Verbose, "ChangeSettingsAsync to '{0}'", ReaderName);

            var backgroundWorker = new BackgroundWorker();
            backgroundWorker.WorkerReportsProgress = true;
            backgroundWorker.WorkerSupportsCancellation = true;
            backgroundWorker.DoWork += new DoWorkEventHandler(_ChangeSettingsAsyncDoWork);
            backgroundWorker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(_ChangeSettingsAsyncRunWorkerCompleted);

            backgroundWorker.RunWorkerAsync(ReaderName);
        }

        private bool _changeSettingsAsyncCanExecute(object obj)
        {
            return true;
        }
        private void _ChangeSettingsAsyncRunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            StatusLabel = "Change Settings Complete";
        }

        private void _ChangeSettingsAsyncDoWork(object sender, DoWorkEventArgs e)
        {
            StatusLabel = "Changing Settings";

            try
            {
                ApplySettings(Settings.Settings);

                var status = QueryStatus(StatusRefresh.JustGpis);
                status.ReaderIdentity = ReaderIdentity;
                Status = StatusViewModel.Factory(status);

                var featureSet = QueryFeatureSet();
                FeatureSet = FeatureSetViewModel.Factory(featureSet);
            }
            catch (Exception ex)
            {
                _log(LogLevel.Error, ex.Message);
            }
        }
    }
}

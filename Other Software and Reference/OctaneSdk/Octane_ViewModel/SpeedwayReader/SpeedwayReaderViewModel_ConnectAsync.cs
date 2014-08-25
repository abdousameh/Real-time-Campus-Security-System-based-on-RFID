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
using System.Timers;
using System.ComponentModel;
using System.Windows.Media;
using System.Threading;
using Impinj.OctaneSdk;

namespace Octane_ViewModel
{
    public partial class SpeedwayReaderViewModel
    {
        void _timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            Progress = _timerTimeoutCount++;

            if (_timerTimeoutCount >= _timerTimeoutCountMax)
            {
                StatusLabel = "Timed out";
            }
        }

        /// <summary>
        /// This is the background worker delegate that is handles the reader connect.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void connectAsynchRequestDoWork(object sender, DoWorkEventArgs e)
        {
            var backgroundWorker = sender as BackgroundWorker;

            try
            {
                // setup the timer, so we can report progress
                // the interface to teton is synchronous, so
                // this timeout-timer will be used to make it
                // unsynchronous
                
                // use 100 marks
                _timerTimeoutCountMax = 100;
                // start at mark #1
                _timerTimeoutCount = 1;
                // set update interval to 100 ms, so 100 timeout ticks = 10,000 ms
                // or a 10 second timeout for connect
                _connectAsyncTimeoutTimer.Interval = 100;
                // start the timer
                _connectAsyncTimeoutTimer.Start();

                StatusLabel = "Connecting";
                _log(LogLevel.Verbose, "'{0}' {1}", ReaderName, StatusLabel);
                Connect(ReaderName);

                ClearSettings();

                var settings = QueryFactorySettings();
                settings.Report.Mode = ReportMode.Individual;
                settings.Report.IncludeAntennaPortNumber = true;
                settings.Report.IncludeChannel = true;
                settings.Report.IncludeFirstSeenTime = true;
                settings.Report.IncludeLastSeenTime = true;
                settings.Report.IncludePeakRssi = true;
                settings.Report.IncludePhaseAngle = true;
                settings.Report.IncludeSeenCount = true;
                ApplySettings(settings);

                StatusLabel = "Querying Feature Set";
                _log(LogLevel.Verbose, "'{0}' {1}", ReaderName, StatusLabel);
                var featureSet = QueryFeatureSet();
                FeatureSet = FeatureSetViewModel.Factory(featureSet);

                StatusLabel = "Querying Settings";
                _log(LogLevel.Verbose, "'{0}' {1}", ReaderName, StatusLabel);

                Settings = SettingsViewModel.Factory(settings, _mainWindowCallback);

                StatusLabel = "Querying Status";
                
                OnPropertyChanged("DisplayLabel");

                _log(LogLevel.Verbose, "'{0}' {1}", ReaderName, StatusLabel);

                var status = QueryStatus();
                status.ReaderIdentity = ReaderIdentity;
                Status = StatusViewModel.Factory(status);
            }
            catch (OctaneSdkException octaneException)
            {
                _log(LogLevel.Error, octaneException.Message);
                IsApplicationConnected = false;
            }
            catch (Exception ex)
            {
                _log(LogLevel.Error, ex.Message);
                IsApplicationConnected = false;
            }

            Progress = 100;
            backgroundWorker.CancelAsync();
        }

        void connectAsyncRunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            _log(LogLevel.Verbose, "'{0}' ConnectAsync completed.", ReaderName);

            _connectAsyncTimeoutTimer.Stop();
        }
    }
}

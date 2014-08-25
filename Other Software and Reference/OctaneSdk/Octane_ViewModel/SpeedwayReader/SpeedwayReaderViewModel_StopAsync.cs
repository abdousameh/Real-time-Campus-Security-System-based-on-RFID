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
using System.ComponentModel;
using Impinj.OctaneSdk;

namespace Octane_ViewModel
{
    public partial class SpeedwayReaderViewModel
    {
        void stopAsyncDoWork(object sender, DoWorkEventArgs e)
        {
            var backgroundWorker = sender as BackgroundWorker;

            try
            {
                // use 100 marks
                _timerTimeoutCountMax = 20;
                // start at mark #1
                _timerTimeoutCount = 1;
                // set update interval to 100 ms, so 20 timeout ticks = 2,000 ms
                // or a 10 second timeout for connect
                _connectAsyncTimeoutTimer.Interval = 100;
                // start the timer
                _connectAsyncTimeoutTimer.Start();

                StatusLabel = "Stopping";
                _log(LogLevel.Verbose, "'{0}' {1}", ReaderName, StatusLabel);
                Stop();
            }
            catch (OctaneSdkException octaneException)
            {
                _log(LogLevel.Error, octaneException.Message);
            }
            catch (Exception ex)
            {
                _log(LogLevel.Error, ex.Message);
            }

            Progress = 100;
            backgroundWorker.CancelAsync();
        }

        void stopAsyncRunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            _log(LogLevel.Verbose, "'{0}' StopAsync completed.", ReaderName);

            _connectAsyncTimeoutTimer.Stop();
        }
    }
}

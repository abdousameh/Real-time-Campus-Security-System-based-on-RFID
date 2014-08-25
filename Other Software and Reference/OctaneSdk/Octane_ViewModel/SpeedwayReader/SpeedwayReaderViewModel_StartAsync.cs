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
        void startAsyncDoWork(object sender, DoWorkEventArgs e)
        {
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

                StatusLabel = "Starting";
                _log(LogLevel.Verbose, "'{0}' {1}", ReaderName, StatusLabel);
                Start();
            }
            catch (OctaneSdkException octaneException)
            {
                _log(LogLevel.Error, octaneException.Message);
            }
            catch (Exception ex)
            {
                _log(LogLevel.Error, ex.Message);
            }
        }

        void startAsyncRunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            Progress = 100;

            _log(LogLevel.Verbose, "'{0}' StartAsync completed.", ReaderName);

            _connectAsyncTimeoutTimer.Stop();
        }
    }
}

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
using System.Windows.Input;
using System.Threading;
using Impinj.OctaneSdk;

namespace Octane_ViewModel
{
    public partial class MainWindowViewModel
    {
        /*
         *  Create Readers Asynch components
         */

        /// <summary>
        /// This is the handler for the New Reader Queue's timer. To control how readers are added
        /// to the application, they are added to a queue. As the timer expires, a new reader's model
        /// is dequeue'd. That model has a thread created for it, and is then added to the main windows
        /// readers collection.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void _createReadersTimer_Elapsed(object sender, ElapsedEventArgs e)
        {
            // make sure there is something in the queue
            if (_newReaderQueue.Count > 0)
            {
                // grab the next model
                var newReaderModel = _newReaderQueue.Dequeue();
                Log(LogLevel.Verbose, "Dequeued NewReader '{0}', with {1} left in queue.", newReaderModel.ReaderName, _newReaderQueue.Count);
                // call create
                _createReader(newReaderModel);
            }
            else
            {
                Log(LogLevel.Warning, "CreateReader fired, but there were no readers to dequeue. CreateReader's timer was stopped.");
                _createReaderTimer.Stop();
            }
        }

        /// <summary>
        /// This saves the new reader's settings before creating a new thread and
        /// passing off the new reader model to that thread via a private property.
        /// The thread is responsible for creating the view model and marshaling it
        /// to the main window.
        /// </summary>
        /// <param name="newReaderModel"></param>
        private void _createReader(NewReaderModel newReaderModel)
        {
            var readerName = newReaderModel.ReaderName;

            var speedwayReaderViewModel = SpeedwayReaderViewModel.Factory(newReaderModel, this);

            if (!string.IsNullOrEmpty(newReaderModel.ReaderIdentity))
            {
                speedwayReaderViewModel.ReaderIdentity = newReaderModel.ReaderIdentity;
            }

            if (newReaderModel.IsIncluded)
            {
                AddReader(speedwayReaderViewModel);

                if (newReaderModel.IsAutoConnected)
                {
                    speedwayReaderViewModel.ConnectAsyncCommand.Execute(null);
                }
            }
        }
    }
}

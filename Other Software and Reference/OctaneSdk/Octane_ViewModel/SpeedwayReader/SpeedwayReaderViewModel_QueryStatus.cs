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
using System.Windows.Input;
using System.Windows.Controls;

namespace Octane_ViewModel
{
    public partial class SpeedwayReaderViewModel
    {
        public ICommand QueryStatusAsyncCommand { get; set; }

        private void _queryStatusAsyncCommandExecute(object obj)
        {
            var refresh = StatusRefresh.JustGpis;
            var comboBoxItem = obj as ComboBoxItem;
            if (null != comboBoxItem)
            {
                refresh = (StatusRefresh)Enum.Parse(typeof(StatusRefresh), comboBoxItem.Content.ToString());
            }

            _log(LogLevel.Verbose, "QueryStatusAsync to '{0}'", ReaderName);

            var backgroundWorker = new BackgroundWorker();
            backgroundWorker.WorkerReportsProgress = true;
            backgroundWorker.WorkerSupportsCancellation = true;
            backgroundWorker.DoWork += new DoWorkEventHandler(_queryStatusDoWork);
            backgroundWorker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(_queryStatusRunWorkerCompleted);

            backgroundWorker.RunWorkerAsync(refresh.ToString());
        }

        private bool _queryStatusAsyncCommandCanExecute(object obj)
        {
            return true;
        }

        private void _queryStatusRunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            StatusLabel = "Query Status Completed";
        }

        private void _queryStatusDoWork(object sender, DoWorkEventArgs e)
        {
            var refresh = (StatusRefresh)Enum.Parse(typeof(StatusRefresh), e.Argument.ToString());

            StatusLabel = "Querying Status";
            var status = QueryStatus(refresh);
            Status.Status = status;

            OnPropertyChanged("Status");
        }
    }
}

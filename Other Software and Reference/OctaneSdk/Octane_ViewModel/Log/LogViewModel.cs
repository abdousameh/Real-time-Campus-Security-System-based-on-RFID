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
using System.Collections.ObjectModel;
using System.Windows.Input;
using System.Threading;

namespace Octane_ViewModel
{
    public class LogViewModel : ViewModelBase
    {
        public LogViewModel()
        {
            Logs = new ObservableCollection<LogEntryWpf>();
            ClearLogEntriesCommand = new DelegateCommand(_clearLogEntriesExecute, _clearLogEntriesCanExecute);
        }

        /// <summary>
        /// This is the source "list" for the log entry list in the LogView user control.
        /// </summary>
        public ObservableCollection<LogEntryWpf> Logs { get; set; }

        public bool IsLogEntryCountNonZero
        {
            get
            {
                return Logs.Count > 0;
            }
        }

        // Clear LogEntries Command

        public ICommand ClearLogEntriesCommand { get; set; }

        private void _clearLogEntriesExecute(object obj)
        {
            Logs.Clear();
        }

        private bool _clearLogEntriesCanExecute(object obj)
        {
            return true;
        }

        internal void Insert(int index, LogEntryWpf log)
        {
            log.Rank = Logs.Count;
            Logs.Insert(0, log);
        }
    }
}

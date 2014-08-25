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
using System.Threading;
using System.Windows.Media;

namespace Octane_ViewModel
{
    public class LogEntryWpf : LogEntry
    {
        public int ManagedThreadId { get; set; }
        public Brush Color { get; set; }
        public int Rank { get; set; }

        internal static LogEntryWpf Convert(LogEntry logEntry)
        {
            var entry = new LogEntryWpf();
            entry.Level = logEntry.Level;
            entry.Message = logEntry.Message;
            entry.Origin = logEntry.Origin;
            entry.ReaderIdentity = logEntry.ReaderIdentity;
            entry.Timestamp = logEntry.Timestamp;
            entry.ManagedThreadId = Thread.CurrentThread.ManagedThreadId;

            return entry;
        }
    }
}

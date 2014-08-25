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

namespace Octane_ViewModel
{
    public interface IMainWindowCallback
    {
        void AddLogEntry(LogEntryWpf logEntry);
        void AddReader(SpeedwayReaderViewModel viewModel);
        void AddTag(TagWpf tag);
        void AddTags(List<TagWpf> tag);
        void ClearLogEntries();
        void ClearTags();
        void RemoveReader(string readerName);
        void StatusChangeNotification(string readerName, bool isConnected);
    }
}

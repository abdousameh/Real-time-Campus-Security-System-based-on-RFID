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
    public partial class MainWindowViewModel
    {
        public void LoadReaders(string autoStartPath)
        {
            LoadReadersAsyncCommand.Execute(autoStartPath);
        }

        public void StartAll()
        {
            StartAllCommand.Execute(null);
        }

        public void StopAll()
        {
            StopAllCommand.Execute(null);
        }

        public void ConnectAll()
        {
            ConnectAllCommand.Execute(null);
        }

        public void DisconnectAll()
        {
            DisconnectAllCommand.Execute(null);
        }
    }
}

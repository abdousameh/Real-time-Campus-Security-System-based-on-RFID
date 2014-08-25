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
using System.Threading;
using System.Windows.Threading;
using System.Timers;

namespace Octane_ViewModel
{
    public partial class MainWindowViewModel
    {
        private void _initializeProperties()
        {
            // loaded from default settings

            // private
            _mainMutex = new Mutex();
            _newReaderQueue = new Queue<NewReaderModel>();
            // public
            Readers = new ObservableCollection<SpeedwayReaderViewModel>();
            LogViewModel = new LogViewModel();
            TagPaneViewModel = new TagPaneViewModel();
            // additional setup for create readers async
            _createReaderTimer = new System.Timers.Timer();
            _createReaderTimer.Elapsed += new ElapsedEventHandler(_createReadersTimer_Elapsed);
            _createReaderTimer.Interval = SettingsUI.Default.ConnectReaderIntervalInMs;
        }

        #region // Primary UI Display Properties

        public bool IsReaderCountNonZero
        {
            get
            {
                return Readers.Count > 0;
            }
        }

        #endregion

        private System.Timers.Timer _createReaderTimer { get; set; }
        private Dispatcher _dispatcher { get; set; }
        private Mutex _mainMutex { get; set; }
        private System.Timers.Timer _marshalTagTimer { get; set; }
        private Queue<NewReaderModel> _newReaderQueue;

        public string Label { get; set; }
        public LogViewModel LogViewModel { get; set; }
        public ObservableCollection<SpeedwayReaderViewModel> Readers { get; set; }

        public TagPaneViewModel TagPaneViewModel { get; set; }
    }
}

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
using Impinj.OctaneSdk;
using System.Collections.ObjectModel;
using System.Threading;
using System.Windows.Media;

namespace Octane_ViewModel
{
    public partial class SpeedwayReaderViewModel
    {
        private void _initializeProperties()
        {
            // private
            _connectAsyncTimeoutTimer = new System.Timers.Timer();
            _connectAsyncTimeoutTimer.Elapsed += new ElapsedEventHandler(_timer_Elapsed);
            // public
            StatusLabel = "…";

            ConnectButtonStyle = "onButton";
            ConnectButtonVisibility = "Collapsed";
        }

        // styles
        private string _connectButtonStyle;
        public string ConnectButtonStyle
        {
            get { return _connectButtonStyle; }
            set
            {
                _connectButtonStyle = value;
                OnPropertyChanged("ConnectButtonStyle");
            }
        }

        private string _connectButtonVisibility;
        public string ConnectButtonVisibility
        {
            get { return _connectButtonVisibility; }
            set
            {
                _connectButtonVisibility = value;
                OnPropertyChanged("ConnectButtonVisibility");
            }
        }

        #region // Primary UI Display Properties

        public string DisplayLabel
        {
            get
            {
                var result = ReaderName;
                // if the reader name and id are the same, then just
                // use the name, if the id is different, print the name in
                // parens after the name
                if(!ReaderName.Equals(ReaderIdentity.ToString()))
                    result = string.Format("{0} ({1})", ReaderIdentity, ReaderName);

                // go did up the model name
                if (null != FeatureSet && null != FeatureSet.FeatureSet)
                {
                    var model = FeatureSet.FeatureSet.ModelName;
                    var index = model.IndexOf(" R");
                    model = model.Substring(index + 1);

                    if (!string.IsNullOrEmpty(model))
                    {
                        result = string.Format("{0} @ {1}", model, result);
                    }
                }

                return result;
            }
        }

        #region // Status Label

        private string _statusLabel;

        public string StatusLabel
        {
            get { return _statusLabel; }
            set
            {
                _statusLabel = value;
                OnPropertyChanged("StatusLabel");
            }
        }

        #endregion

        #endregion // Primary UI Display Properties

        #region

        public int Progress
        {
            get { return _progress; }
            set
            {
                _progress = value;
                OnPropertyChanged("Progress");
            }
        }
        private int _progress;

        #endregion

        private IMainWindowCallback _mainWindowCallback { get; set; }
        private System.Timers.Timer _connectAsyncTimeoutTimer { get; set; }
        private int _timerTimeoutCount { get; set; }
        private int _timerTimeoutCountMax { get; set; }

        private StatusViewModel _status;
        public StatusViewModel Status
        {
            get { return _status; }
            set
            {
                _status = value;
                OnPropertyChanged("Status");
            }
        }

        private SettingsViewModel _settings { get; set; }

        public SettingsViewModel Settings
        {
            get { return _settings; }
            set
            {
                _settings = value;
                OnPropertyChanged("Settings");
            }
        }

        private FeatureSetViewModel _featureSet;

        public FeatureSetViewModel FeatureSet
        {
            get { return _featureSet; }
            set
            {
                _featureSet = value;
                OnPropertyChanged("FeatureSet");
            }
        }

        public bool _isConnected;

        public bool IsApplicationConnected
        {
            get { return _isConnected; }
            set
            {
                _isConnected = value;
                OnPropertyChanged("IsApplicationConnected");
            }
        }

        public string ReaderName { get; set; }
        public Brush Color { get; set; }
    
    }
}

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
using System.Windows;
using System.Diagnostics;
using Impinj.OctaneSdk;
using System.Collections.ObjectModel;

namespace Octane_ViewModel
{
    public class EditSettingsWindowViewModel
    {
        public EditSettingsWindowViewModel()
        {
            foreach (var r in Application.Current.Resources.Keys)
            {
                Style style = Application.Current.Resources[r] as Style;
                if (null != style)
                {
                    var message = string.Format("Name {0}, Target {1}", r, style.TargetType);
                    Trace.WriteLine(message);
                }
            }

            ReaderModes = ReaderModeModel.ReaderModes;
            SearchModes = SearchModeModel.SearchModes;
            AutoStartModes = AutoStartModeModel.AutoStartModes;
            AutoStopModes = AutoStopModeModel.AutoStopModes;
            ReportModes = ReportModeModel.ReportModes;
        }

        public event EventHandler ChangeSettingsClick; 

        public bool AutoStartGpiPortNumber1IsChecked
        {
            get { return Settings.AutoStart.GpiPortNumber == 1; }
            set
            {
                if (value)
                {
                    Settings.AutoStart.GpiPortNumber = 1;
                }
            }
        }

        public bool AutoStartGpiPortNumber2IsChecked
        {
            get { return Settings.AutoStart.GpiPortNumber == 2; }
            set
            {
                if (value)
                {
                    Settings.AutoStart.GpiPortNumber = 2;
                }
            }
        }

        public bool AutoStartGpiPortNumber3IsChecked
        {
            get { return Settings.AutoStart.GpiPortNumber == 3; }
            set
            {
                if (value)
                {
                    Settings.AutoStart.GpiPortNumber = 3;
                }
            }
        }

        public bool AutoStartGpiPortNumber4IsChecked
        {
            get { return Settings.AutoStart.GpiPortNumber == 4; }
            set
            {
                if (value)
                {
                    Settings.AutoStart.GpiPortNumber = 4;
                }
            }
        }

        public bool AutoStopGpiPortNumber1IsChecked
        {
            get { return Settings.AutoStop.GpiPortNumber == 1; }
            set
            {
                if (value)
                {
                    Settings.AutoStop.GpiPortNumber = 1;
                }
            }
        }

        public bool AutoStopGpiPortNumber2IsChecked
        {
            get { return Settings.AutoStop.GpiPortNumber == 2; }
            set
            {
                if (value)
                {
                    Settings.AutoStop.GpiPortNumber = 2;
                }
            }
        }

        public bool AutoStopGpiPortNumber3IsChecked
        {
            get { return Settings.AutoStop.GpiPortNumber == 3; }
            set
            {
                if (value)
                {
                    Settings.AutoStop.GpiPortNumber = 3;
                }
            }
        }

        public bool AutoStopGpiPortNumber4IsChecked
        {
            get { return Settings.AutoStop.GpiPortNumber == 4; }
            set
            {
                if (value)
                {
                    Settings.AutoStop.GpiPortNumber = 4;
                }
            }
        }

        public ObservableCollection<ReaderModeModel> ReaderModes { get; set; }
        public ObservableCollection<SearchModeModel> SearchModes { get; set; }
        public ObservableCollection<AutoStartModeModel> AutoStartModes { get; set; }
        public ObservableCollection<AutoStopModeModel> AutoStopModes { get; set; }
        public ObservableCollection<ReportModeModel> ReportModes { get; set; }
        public Settings Settings { get; set; }

        public void ChangeSettings()
        {
            if (null != ChangeSettingsClick)
            {
                ChangeSettingsClick(Settings, new EventArgs());
            }
        }
    }
}

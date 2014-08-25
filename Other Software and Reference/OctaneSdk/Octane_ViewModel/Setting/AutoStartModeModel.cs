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
using System.Collections.ObjectModel;

namespace Octane_ViewModel
{
    public class AutoStartModeModel
    {
        public string Label { get; set; }
        public AutoStartMode AutoStartMode { get; set; }

        private static ObservableCollection<AutoStartModeModel> _autoStartModes;

        public static ObservableCollection<AutoStartModeModel> AutoStartModes
        {
            get
            {
                if (null == _autoStartModes)
                {
                    _autoStartModes = new ObservableCollection<AutoStartModeModel>();

                    _autoStartModes.Add(new AutoStartModeModel { AutoStartMode = AutoStartMode.GpiTrigger, Label = "GPI Trigger" });
                    _autoStartModes.Add(new AutoStartModeModel { AutoStartMode = AutoStartMode.Immediate, Label = "Immediate" });
                    _autoStartModes.Add(new AutoStartModeModel { AutoStartMode = AutoStartMode.None, Label = "None" });
                    _autoStartModes.Add(new AutoStartModeModel { AutoStartMode = AutoStartMode.Periodic, Label = "Periodic" });
                }
                return _autoStartModes;
            }
            set { }
        }
    }
}

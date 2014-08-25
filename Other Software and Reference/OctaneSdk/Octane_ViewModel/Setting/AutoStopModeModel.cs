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
    public class AutoStopModeModel
    {
        public string Label { get; set; }
        public AutoStopMode AutoStopMode { get; set; }

        private static ObservableCollection<AutoStopModeModel> _autoStopModes;

        public static ObservableCollection<AutoStopModeModel> AutoStopModes
        {
            get
            {
                if (null == _autoStopModes)
                {
                    _autoStopModes = new ObservableCollection<AutoStopModeModel>();

                    _autoStopModes.Add(new AutoStopModeModel { AutoStopMode = AutoStopMode.Duration, Label = "Duration" });
                    _autoStopModes.Add(new AutoStopModeModel { AutoStopMode = AutoStopMode.GpiTrigger, Label = "GPI Trigger" });
                    _autoStopModes.Add(new AutoStopModeModel { AutoStopMode = AutoStopMode.None, Label = "None" });
                }
                return _autoStopModes;
            }
            set { }
        }
    }
}

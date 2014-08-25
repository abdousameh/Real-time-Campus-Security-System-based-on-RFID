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
    public class ReportModeModel
    {
        public ReportMode ReportMode { get; set; }
        public string Label { get; set; }

        private static ObservableCollection<ReportModeModel> _reportModes;

        public static ObservableCollection<ReportModeModel> ReportModes
        {
            get
            {
                if (null == _reportModes)
                {
                    _reportModes = new ObservableCollection<ReportModeModel>();

                   _reportModes.Add(new ReportModeModel { ReportMode = ReportMode.BatchAfterStop, Label = "Batch After Stop" });
                   _reportModes.Add(new ReportModeModel { ReportMode = ReportMode.Individual, Label = "Individual" });
                   _reportModes.Add(new ReportModeModel { ReportMode = ReportMode.IndividualUnbuffered, Label = "Individual Unbuffered" });
                   _reportModes.Add(new ReportModeModel { ReportMode = ReportMode.WaitForQuery, Label = "Wait For Query" });
                }

                return _reportModes;
            }
            set { }
        }
    }
}

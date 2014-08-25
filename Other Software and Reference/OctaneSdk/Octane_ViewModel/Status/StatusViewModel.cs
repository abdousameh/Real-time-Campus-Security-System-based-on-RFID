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
    public class StatusViewModel : ViewModelBase
    {
        public StatusViewModel()
        {
            Status = new Status();
            Test = "Test Label";
            AntennaStatuses = new ObservableCollection<AntennaStatus>();
            GpiStatuses = new ObservableCollection<GpiStatus>();
            GpoStatuses = new ObservableCollection<GpoStatus>();
        }

        public string Test { get; set; }

        public Status Status { get; set; }
        public ObservableCollection<AntennaStatus> AntennaStatuses { get; set; }
        public ObservableCollection<GpiStatus> GpiStatuses { get; set; }
        public ObservableCollection<GpoStatus> GpoStatuses { get; set; }

        public static StatusViewModel Factory(Status status)
        {
            var statusViewModel = new StatusViewModel();
            statusViewModel.Status = status;

            foreach (var s in status.Antennas.Elements)
            {
                statusViewModel.AntennaStatuses.Add(s);
            }

            foreach (var i in status.Gpis.Elements)
            {
                statusViewModel.GpiStatuses.Add(i);
            }

            foreach (var o in status.Gpos.Elements)
            {
                statusViewModel.GpoStatuses.Add(o);
            }

            return statusViewModel;
        }
    }
}

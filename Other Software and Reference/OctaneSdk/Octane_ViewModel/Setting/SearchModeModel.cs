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
    public class SearchModeModel
    {
        public SearchMode SearchMode { get; set; }
        public string Label { get; set; }

        private static ObservableCollection<SearchModeModel> _searchModes;

        public static ObservableCollection<SearchModeModel> SearchModes
        {
            get
            {
                if (null == _searchModes)
                {
                    _searchModes = new ObservableCollection<SearchModeModel>();
                    _searchModes.Add(new SearchModeModel
                        { SearchMode = SearchMode.DualTarget, Label = "Dual Target" }
                    );
                    _searchModes.Add(new SearchModeModel
                        { SearchMode = SearchMode.ReaderSelected, Label = "Reader Selected" }
                    );
                    _searchModes.Add(new SearchModeModel
                        { SearchMode = SearchMode.SingleTarget, Label = "Single Target" }
                    );
                    _searchModes.Add(new SearchModeModel
                        { SearchMode = SearchMode.SingleTargetWithSuppression, Label = "Single Target With Supression" }
                    );
                }
                return _searchModes;
            }
            set { }
        }
    }
}

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
    public class ReaderModeModel
    {
        public ReaderMode ReaderMode { get; set; }
        public string Label { get; set; }

        private static ObservableCollection<ReaderModeModel> _readerModes;

        public static ObservableCollection<ReaderModeModel> ReaderModes
        {
            get
            {
                if (null == _readerModes)
                {
                    _readerModes = new ObservableCollection<ReaderModeModel>();
                    _readerModes.Add(
                        new ReaderModeModel {
                            ReaderMode = ReaderMode.AutoSetDenseReader,
                            Label = "Auto Set Dense Reader" }
                        );
                    _readerModes.Add(
                        new ReaderModeModel
                        {
                            ReaderMode = ReaderMode.AutoSetSingleReader,
                            Label = "Auto Set Single Reader" }
                        );
                    _readerModes.Add(
                        new ReaderModeModel
                        {
                            ReaderMode = ReaderMode.DenseReaderM4,
                            Label = "Dense Reader M4" }
                        );
                    _readerModes.Add(
                        new ReaderModeModel
                        {
                            ReaderMode = ReaderMode.DenseReaderM8,
                            Label = "Dense Reader M8" }
                        );
                    _readerModes.Add(
                        new ReaderModeModel
                        {
                            ReaderMode = ReaderMode.Hybrid,
                            Label = "Hybrid" }
                        );
                    _readerModes.Add(
                        new ReaderModeModel
                        {
                            ReaderMode = ReaderMode.MaxMiller,
                            Label = "Max Miller" }
                        );
                    _readerModes.Add(
                        new ReaderModeModel
                        {
                            ReaderMode = ReaderMode.MaxThroughput,
                            Label = "Max Throughput" }
                        );
                }
                return _readerModes;
            }
            set
            {

            }
        }
    }
}

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
using System.ComponentModel;

namespace Octane_ViewModel
{
    public class FeatureSetViewModel : INotifyPropertyChanged
    {
        public FeatureSetViewModel()
        {
            FeatureSet = new FeatureSet();

            ReaderModes = new ObservableCollection<ReaderModeEntry>();
            ReducedPowerTxFrequencies = new ObservableCollection<TxFrequencyEntry>();
            RxSensitivities = new ObservableCollection<RxSensitivityEntry>();
            SearchModes = new ObservableCollection<SearchModeEntry>();
            TxFrequencies = new ObservableCollection<TxFrequencyEntry>();
            TxPowers = new ObservableCollection<TxPowerEntry>();

            TestLabel = "Test Label";
        }
        private string _testLabel;

        public string TestLabel
        {
            get { return _testLabel; }
            set
            {
                _testLabel = value;
                OnPropertyChanged("TestLabel");
            }
        }

        public ObservableCollection<ReaderModeEntry> ReaderModes { get; set; }
        public ObservableCollection<TxFrequencyEntry> ReducedPowerTxFrequencies { get; set; }
        public ObservableCollection<RxSensitivityEntry> RxSensitivities { get; set; }
        public ObservableCollection<SearchModeEntry> SearchModes { get; set; }
        public ObservableCollection<TxFrequencyEntry> TxFrequencies { get; set; }
        public ObservableCollection<TxPowerEntry> TxPowers { get; set; }

        private FeatureSet _featureSet;

        public FeatureSet FeatureSet
        {
            get { return _featureSet; }
            set
            {
                _featureSet = value;
                OnPropertyChanged("FeatureSet");
            }
        }

        internal static FeatureSetViewModel Factory(FeatureSet featureSet)
        {
            var success = false;
            var featureSetViewModel = new FeatureSetViewModel();

            featureSetViewModel._featureSet = featureSet;

            featureSetViewModel.ReaderModes.Clear();

            foreach (var mode in featureSet.ReaderModes.Entries)
            {
                featureSetViewModel.ReaderModes.Add(mode);
            }

            featureSetViewModel.ReducedPowerTxFrequencies.Clear();

            foreach (var frequency in featureSet.ReducedPowerTxFrequencies.Entries)
            {
                featureSetViewModel.ReducedPowerTxFrequencies.Add(frequency);
            }

            featureSetViewModel.RxSensitivities.Clear();

            foreach (var sensitivity in featureSet.RxSensitivities.Entries)
            {
                featureSetViewModel.RxSensitivities.Add(sensitivity);
            }

            featureSetViewModel.SearchModes.Clear();

            foreach (var mode in featureSet.SearchModes.Entries)
            {
                featureSetViewModel.SearchModes.Add(mode);
            }

            featureSetViewModel.TxFrequencies.Clear();
            
            foreach (var frequency in featureSet.TxFrequencies.Entries)
            {
                featureSetViewModel.TxFrequencies.Add(frequency);
            }

            featureSetViewModel.TxPowers.Clear();

            foreach (var power in featureSet.TxPowers.Entries)
            {
                featureSetViewModel.TxPowers.Add(power);
            }

            return featureSetViewModel;
        }

        #region INotifyPropertyChanged Members

        public void OnPropertyChanged(string propertyName)
        {
            if (null != PropertyChanged)
            {
                PropertyChangedEventArgs e = new PropertyChangedEventArgs(propertyName);
                PropertyChanged(this, e);
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion
    }
}

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
    public class SettingsViewModel : ViewModelBase
    {
        public SettingsViewModel()
        {
            Settings = new Settings();
            Antennas = new ObservableCollection<AntennaSettings>();
            Gpis = new ObservableCollection<GpiSettings>();
            ReducedPowerTxFrequencies = new ObservableCollection<double>();
            TxFrequencies = new ObservableCollection<double>();
        }

        private Settings _settings;
        public Settings Settings
        {
            get { return _settings; }
            set
            {
                _settings = value;
                OnPropertyChanged("Settings");
            }
        }
        public ObservableCollection<AntennaSettings> Antennas { get; set; }
        public ObservableCollection<GpiSettings> Gpis { get; set; }
        public ObservableCollection<double> ReducedPowerTxFrequencies { get; set; }
        public ObservableCollection<double> TxFrequencies { get; set; }

        internal static SettingsViewModel Factory(Settings settings, IMainWindowCallback _mainWindowCallback)
        {
            var success = false;

            var settingsViewModel = new SettingsViewModel();
            settingsViewModel.Settings = settings;

            settingsViewModel.Antennas.Clear();

            foreach (var antennaSetting in settings.Antennas.Elements)
            {
                settingsViewModel.Antennas.Add(antennaSetting);
            }

            settingsViewModel.Gpis.Clear();

            foreach (var gpiSetting in settings.Gpis.Elements)
            {
                settingsViewModel.Gpis.Add(gpiSetting);
            }

            settingsViewModel.ReducedPowerTxFrequencies.Clear();

            //settings.ReducedPowerTxFrequenciesInMhz.Add(12.45);
            //settings.ReducedPowerTxFrequenciesInMhz.Add(34.67);
            foreach (var mhz in settings.ReducedPowerTxFrequenciesInMhz)
            {
                settingsViewModel.ReducedPowerTxFrequencies.Add(mhz);
            }

            settingsViewModel.TxFrequencies.Clear();

            //settings.TxFrequenciesInMhz.Add(10.00);
            //settings.TxFrequenciesInMhz.Add(20.00);
            foreach (var mhz in settings.TxFrequenciesInMhz)
            {
                settingsViewModel.TxFrequencies.Add(mhz);
            }

            return settingsViewModel;
        }
    }
}

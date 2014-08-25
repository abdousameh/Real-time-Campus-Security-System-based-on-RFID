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
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Impinj.OctaneSdk;
using Octane_ViewModel;


namespace Template_Wpf
{
    /// <summary>
    /// Interaction logic for SpeedwayReaderView.xaml
    /// </summary>
    public partial class SpeedwayReaderView : UserControl
    {
        public SpeedwayReaderView()
        {
            InitializeComponent();
        }

        private SpeedwayReaderViewModel _viewModel
        {
            get
            {
                return DataContext as SpeedwayReaderViewModel;
            }
        }

        private void EditSettingsButton_Click(object sender, RoutedEventArgs e)
        {
            var editSettingsDialog = new EditSettingsWindow();
            var viewModel = new EditSettingsWindowViewModel();
            viewModel.ChangeSettingsClick += new EventHandler(viewModel_ChangeSettingsClick);

            if (null == _viewModel.Settings)
            {
                _viewModel.Settings = new SettingsViewModel();
                _viewModel.Settings.Settings = new Settings();
            }
            viewModel.Settings = _viewModel.Settings.Settings;

            editSettingsDialog.DataContext = viewModel;

            var result = editSettingsDialog.ShowDialog();

            if (result.Value)
            {
                _viewModel.Settings.Settings = ((EditSettingsWindowViewModel)editSettingsDialog.DataContext).Settings;
            }
        }

        void viewModel_ChangeSettingsClick(object sender, EventArgs e)
        {
            if (sender is Settings)
            {
                try
                {
                    var settings = sender as Settings;
                    _viewModel.ClearSettings();
                    _viewModel.ApplySettings(settings);
                }
                catch
                {
                }
            }
        }
    }
}

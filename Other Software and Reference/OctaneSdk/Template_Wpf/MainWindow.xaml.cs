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
using System.ComponentModel;
using Impinj.OctaneSdk;

using System.Timers;
using System.Threading;
using Octane_ViewModel;

namespace Template_Wpf
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            Loaded += new RoutedEventHandler(MainWindow_Loaded);
        }

        private MainWindowViewModel _mainWindowViewModel;

        void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            _mainWindowViewModel = new MainWindowViewModel();

            DataContext = _mainWindowViewModel;

            // set the label equal to the machine name
            var label = Environment.MachineName;
            _mainWindowViewModel.SetDefaults(Dispatcher, label);
            _mainWindowViewModel.Log(LogLevel.Debug, "ModelView found in MainWindow.Resources.");
            // get the default path out of the settings
            var autoStartPath = SettingsWpf.Default.AutoStartReadersPath;
            // execute the load readers command
            _mainWindowViewModel.LoadReadersAsyncCommand.Execute(autoStartPath);
        }

        public void Save()
        {
            // place to save the auto start xml
            _mainWindowViewModel.SaveAutoStartList();
        }
    }
}
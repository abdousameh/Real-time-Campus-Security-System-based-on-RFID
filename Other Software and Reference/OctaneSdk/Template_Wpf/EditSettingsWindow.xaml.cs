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
using System.Windows.Shapes;
using Impinj.OctaneSdk;
using Octane_ViewModel;


namespace Template_Wpf
{
    /// <summary>
    /// Interaction logic for EditSettingsWindow.xaml
    /// </summary>
    public partial class EditSettingsWindow : Window
    {
        public EditSettingsWindow()
        {
            InitializeComponent();
        }

        private void OpenSettings_Click(object sender, RoutedEventArgs e)
        {
            var path = Utility.GetFilePath();

            var settings = Settings.Load(path);
        }

        private void SaveAs_Click(object sender, RoutedEventArgs e)
        {
            string path = null;
            var dialog = new Microsoft.Win32.SaveFileDialog();
            dialog.Filter = @"Xml Documents (*.xml)|*.xml" + "|Text Documents (*.txt)|*.txt" + "|All Documents (*.*)|*.*";

            var editSettings = (EditSettingsWindowViewModel)DataContext;
            var settings = editSettings.Settings;

            var result = dialog.ShowDialog();

            if (result.Value)
            {
                path = dialog.FileName;
                settings.Save(path);
            }
        }

        private void OK_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
            Close();
        }

        private void ApplySettings_Click(object sender, RoutedEventArgs e)
        {
            if (DataContext is EditSettingsWindowViewModel)
            {
                var editSettings = (EditSettingsWindowViewModel)DataContext;
                editSettings.ChangeSettings();
            }
            Close();
        }
    }
}

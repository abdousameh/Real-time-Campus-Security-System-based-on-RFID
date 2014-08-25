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
using System.ComponentModel;
using System.IO;
using System.Windows.Forms;
using Octane_ViewModel;

namespace Template_Wpf
{
    /// <summary>
    /// Interaction logic for NewReaderWindow.xaml
    /// </summary>
    public partial class NewReaderWindow : Window
    {
        public NewReaderWindow()
        {
            InitializeComponent();
            Loaded += new RoutedEventHandler(NewReaderWindow_Loaded);
        }

        void NewReaderWindow_Loaded(object sender, RoutedEventArgs e)
        {
            NewReader = new NewReaderModel();
            DataContext = NewReader;
            ReaderNameTextBox.Focus();
        }

        public NewReaderModel NewReader { get; set; }

        private void OK_Button_Click(object sender, RoutedEventArgs e)
        {
            // the ok button was clicked
            DialogResult = true;
        }

        private void Browse_Button_Click(object sender, RoutedEventArgs e)
        {
            NewReader.SettingsFilePath = Utility.GetFilePath();
        }

        private void ColorPicker_Button_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new ColorDialog();

            System.Windows.Forms.DialogResult result = dialog.ShowDialog();

            if (result == System.Windows.Forms.DialogResult.OK)
            {
                var c = dialog.Color;
                var brushConverter = new BrushConverter();

                try
                {
                    NewReader.Color = (Brush)brushConverter.ConvertFromString("#" + dialog.Color.Name.Substring(2));
                }
                catch
                {
                    try
                    {
                        NewReader.Color = (Brush)brushConverter.ConvertFromString(dialog.Color.Name);
                    }
                    catch { }
                }

            }
        }
    }
}

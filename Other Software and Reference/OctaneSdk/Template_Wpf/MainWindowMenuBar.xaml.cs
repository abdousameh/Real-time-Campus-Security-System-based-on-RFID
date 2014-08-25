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
using Octane_ViewModel;

namespace Template_Wpf
{
    /// <summary>
    /// Interaction logic for MainWindowMenuBar.xaml
    /// </summary>
    public partial class MainWindowMenuBar : UserControl
    {
        public MainWindowMenuBar()
        {
            InitializeComponent();
        }

        private void NewMenuItem_Click(object sender, RoutedEventArgs e)
        {
            var newReaderDialog = new NewReaderWindow();

            var result = newReaderDialog.ShowDialog();
            var viewModel = DataContext as MainWindowViewModel;

            if (result.Value &&
                null != viewModel)
            {
                viewModel.AddNewReader(newReaderDialog.NewReader);
            }
        }
    }
}

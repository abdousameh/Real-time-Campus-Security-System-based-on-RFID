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
using System.Configuration;
using System.Data;
using System.Linq;
using System.Windows;

namespace Template_Wpf
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public App()
        {
            Exit += new ExitEventHandler(App_Exit);
            Startup += new StartupEventHandler(App_Startup);
        }

        /// <summary>
        /// This is the main windows.
        /// </summary>
        private MainWindow _mainWindow;

        void App_Startup(object sender, StartupEventArgs e)
        {
            // create the main window and start it "by hand"
            // need access to it on exit to be able to save state
            _mainWindow = new MainWindow();
            _mainWindow.Show();
        }

        /// <summary>
        /// When the application exits, allow the main window to save state.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void App_Exit(object sender, ExitEventArgs e)
        {
            _mainWindow.Save();
        }
    }
}

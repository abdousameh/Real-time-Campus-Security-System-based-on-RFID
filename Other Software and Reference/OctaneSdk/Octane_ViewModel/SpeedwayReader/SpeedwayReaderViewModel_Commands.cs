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
using System.Windows.Input;
using Impinj.OctaneSdk;
using System.ComponentModel;
using System.Threading;
using System.Windows.Controls;
using System.IO;
using Octane_ViewModel;


namespace Octane_ViewModel
{
    public partial class SpeedwayReaderViewModel
    {
        private void _initiliazeCommands()
        {
            TemplateCommand = new DelegateCommand(_templateExecute, _templateCanExecute);

            ChangeSettingsAsyncCommand = new DelegateCommand(_changeSettingsAsyncExecute, _changeSettingsAsyncCanExecute);
            ConnectAsyncCommand = new DelegateCommand(_connectAsyncExecute, _connectAsyncCanExecute);
            ClearSettingsAsyncCommand = new DelegateCommand(_clearSettingsAsyncExecute, _clearSettingsAsyncCanExecute);
            CloseAsyncCommand = new DelegateCommand(_closeAsyncCommandExecute, _closeAsyncCommandCanExecute);
            DisconnectAsyncCommand = new DelegateCommand(_disconnectAsyncExecute, _disconnectAsyncCanExecute);
            LoadSettingsFromFileCommand = new DelegateCommand(_loadSettingsFromFileExecute, _loadSettingsFromFileCanExecute);
            QueryFactorySettingsAsyncCommand = new DelegateCommand(_queryFactorySettingsAsyncExecute, _queryFactorySettingsAsyncCanExecute);
            QueryStatusAsyncCommand = new DelegateCommand(_queryStatusAsyncCommandExecute, _queryStatusAsyncCommandCanExecute);
            QueryTagsAsyncCommand = new DelegateCommand(_queryTagsAsyncExecute, _queryTagsAsyncCanExecute);
            ResumeAsyncCommand = new DelegateCommand(_resumeAsyncExecute, _resumeAsyncCanExecute);
            SaveSettingsAsync = new DelegateCommand(_saveSettingsAsyncExecute, _saveSettingsAsyncCanExecute);
            StartAsyncCommand = new DelegateCommand(_startAsyncExecute, _startAsyncCanExecute);
            StopAsyncCommand = new DelegateCommand(_stopAsyncExecute, _stopAsyncCanExecute);
        }

        #region // Load Settings From File Command

        public ICommand LoadSettingsFromFileCommand { get; set; }

        private void _loadSettingsFromFileExecute(object obj)
        {
            var path = Utility.GetFilePath();
            if(!string.IsNullOrEmpty(path) && File.Exists(path))
            {
                var settings = Impinj.OctaneSdk.Settings.Load(path);
                var fileInfo = new FileInfo(path);
                settings.Label = fileInfo.Name;
                Settings.Settings = settings;
            }
        }

        private bool _loadSettingsFromFileCanExecute(object obj)
        {
            return true;
        }

        #endregion

        #region // Template

        public ICommand TemplateCommand { get; set; }

        private void _templateExecute(object obj)
        {
            // put logic here
        }

        private bool _templateCanExecute(object obj)
        {
            return true;
        }

        #endregion

        #region // ConnectAsync

        public ICommand ConnectAsyncCommand { get; set; }

        private void _connectAsyncExecute(object obj)
        {
            _log(LogLevel.Verbose, "ConnectAsync to '{0}'", ReaderName);

            var backgroundWorker = new BackgroundWorker();
            backgroundWorker.WorkerReportsProgress = true;
            backgroundWorker.WorkerSupportsCancellation = true;
            backgroundWorker.DoWork += new DoWorkEventHandler(connectAsynchRequestDoWork);
            backgroundWorker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(connectAsyncRunWorkerCompleted);

            backgroundWorker.RunWorkerAsync(ReaderName);
        }

        private bool _connectAsyncCanExecute(object obj)
        {
            return true;
        }

        #endregion

        #region // DisconnectAsync

        public ICommand DisconnectAsyncCommand { get; set; }

        private void _disconnectAsyncExecute(object obj)
        {
            _log(LogLevel.Verbose, "DisconnectAsync to '{0}'", ReaderName);

            var backgroundWorker = new BackgroundWorker();
            backgroundWorker.WorkerReportsProgress = true;
            backgroundWorker.WorkerSupportsCancellation = true;
            backgroundWorker.DoWork += new DoWorkEventHandler(disconnectAsyncDoWork);
            backgroundWorker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(disconnectAsyncRunWorkerCompleted);

            backgroundWorker.RunWorkerAsync(ReaderName);
        }

        private bool _disconnectAsyncCanExecute(object obj)
        {
            return true;
        }

        #endregion


        #region // StartAsync

        public ICommand StartAsyncCommand { get; set; }

        private void _startAsyncExecute(object obj)
        {
            _log(LogLevel.Verbose, "StartAsync to '{0}'", ReaderName);

            var backgroundWorker = new BackgroundWorker();
            backgroundWorker.WorkerReportsProgress = true;
            backgroundWorker.WorkerSupportsCancellation = true;
            backgroundWorker.DoWork += new DoWorkEventHandler(startAsyncDoWork);
            backgroundWorker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(startAsyncRunWorkerCompleted);

            backgroundWorker.RunWorkerAsync(ReaderName);
        }

        private bool _startAsyncCanExecute(object obj)
        {
            return true;
        }

        #endregion

        #region // StopAsync

        public ICommand StopAsyncCommand { get; set; }

        private void _stopAsyncExecute(object obj)
        {
            _log(LogLevel.Verbose, "StopAsync to '{0}'", ReaderName);

            var backgroundWorker = new BackgroundWorker();
            backgroundWorker.WorkerReportsProgress = true;
            backgroundWorker.WorkerSupportsCancellation = true;
            backgroundWorker.DoWork += new DoWorkEventHandler(stopAsyncDoWork);
            backgroundWorker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(stopAsyncRunWorkerCompleted);

            backgroundWorker.RunWorkerAsync(ReaderName);
        }

        private bool _stopAsyncCanExecute(object obj)
        {
            return true;
        }

        #endregion

        #region // ConnectAsynch

        public ICommand CloseAsyncCommand { get; set; }
        public void Close()
        {
            _closeAsyncCommandExecute(null);
        }

        private void _closeAsyncCommandExecute(object obj)
        {
            _mainWindowCallback.RemoveReader(ReaderName);
        }

        private bool _closeAsyncCommandCanExecute(object obj)
        {
            return true;
        }

        #endregion

        #region // SaveSettingsAsync

        public ICommand SaveSettingsAsync { get; set; }

        private void _saveSettingsAsyncExecute(object obj)
        {
            _log(LogLevel.Verbose, "Save settings of '{0}'", ReaderName);

            var settings = obj as Settings;

            if (null != settings)
            {
                settings.Label = DisplayLabel;
                settings.Description = "this is the description";
                var saveAsDialog = new Microsoft.Win32.SaveFileDialog();

                var result = saveAsDialog.ShowDialog();

                if (result.Value)
                {
                    XmlHelper.SaveToXmlFile(saveAsDialog.FileName, obj);
                }
            }
        }

        private bool _saveSettingsAsyncCanExecute(object obj)
        {
            return true;
        }

        #endregion

    }
}

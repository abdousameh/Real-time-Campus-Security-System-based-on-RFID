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

using System.Timers;
using Impinj.OctaneSdk;

namespace Octane_ViewModel
{
    public partial class MainWindowViewModel
    {
        private void _initializeCommands()
        {
            TemplateCommand = new DelegateCommand(_templateExecute, _templateCanExecute);

            ClearReadersCommand = new DelegateCommand(_clearReadersExecute, _clearReadersCanExecute);
            LoadReadersAsyncCommand = new DelegateCommand(_loadReadersAsyncExecute, _loadReadersAsyncCanExecute);
            NewReadersCommand = new DelegateCommand(_newReadersExecute, _newReadersCanExecute);
            ConnectAllCommand = new DelegateCommand(_connectAllExecute, _connectAllCanExecute);
            DisconnectAllCommand = new DelegateCommand(_disconnectAllExecute, _disconnectAllCanExecute);
            StartAllCommand = new DelegateCommand(_startAllExecute, _startAllCanExecute);
            StopAllCommand = new DelegateCommand(_stopAllExecute, _stopAllCanExecute);
        }

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

        #region // New Readers

        public ICommand NewReadersCommand { get; set; }

        private void _newReadersExecute(object obj)
        {
            var path = Utility.GetFilePath();

            LoadReadersAsyncCommand.Execute(path);
        }

        private bool _newReadersCanExecute(object obj)
        {
            return true;
        }

        #endregion

        #region // Connect All

        public ICommand ConnectAllCommand { get; set; }

        private void _connectAllExecute(object obj)
        {
            foreach (var reader in Readers)
            {
                reader.ConnectAsyncCommand.Execute(null);
            }
        }

        private bool _connectAllCanExecute(object obj)
        {
            return true;
        }

        #endregion

        #region // Disconnect All

        public ICommand DisconnectAllCommand { get; set; }

        private void _disconnectAllExecute(object obj)
        {
            foreach (var reader in Readers)
            {
                reader.DisconnectAsyncCommand.Execute(null);
            }
        }

        private bool _disconnectAllCanExecute(object obj)
        {
            return true;
        }

        #endregion

        #region // Start All

        public ICommand StartAllCommand { get; set; }

        private void _startAllExecute(object obj)
        {
            foreach (var reader in Readers)
            {
                reader.StartAsyncCommand.Execute(null);
            }
        }

        private bool _startAllCanExecute(object obj)
        {
            return true;
        }

        #endregion

        #region // Stop All

        public ICommand StopAllCommand { get; set; }

        private void _stopAllExecute(object obj)
        {
            foreach (var reader in Readers)
            {
                reader.StopAsyncCommand.Execute(null);
            }
        }

        private bool _stopAllCanExecute(object obj)
        {
            return true;
        }

        #endregion
        
        #region // Clear Readers

        public ICommand ClearReadersCommand { get; set; }

        private void _clearReadersExecute(object obj)
        {
            _mainMutex.WaitOne();

            var listNames = new List<string>();

            foreach (var reader in Readers)
            {
                listNames.Add(reader.ReaderName);
            }

            foreach (var name in listNames)
            {
                RemoveReader(name);
            }

            _mainMutex.ReleaseMutex();
        }

        private bool _clearReadersCanExecute(object obj)
        {
            return true;
        }

        #endregion

        #region // Initialize Readers at Load

        public ICommand LoadReadersAsyncCommand { get; set; }

        private void _loadReadersAsyncExecute(object obj)
        {
            Log(LogLevel.Information, "LoadReaders from path: '{0}'.", obj);

            // the object is a string with a path to the initialization file
            if (null != obj && !string.IsNullOrEmpty(obj.ToString()))
            {
                var path = obj.ToString();
                // instantiate the new reader model from the file
                var store = NewReaderModel.LoadFromPath(path);

                if (store.Count == 0)
                {
                    Log(LogLevel.Debug, "No readers found at path: '{0}'.", obj);
                }
                else
                {
                    // add each found reader to the new reader queue
                    var index = 0;
                    foreach (var newReaderModel in store)
                    {
                        Log(LogLevel.Verbose, "Adding '{0}' to new reader queue at rank {1}.", newReaderModel.ReaderName, _newReaderQueue.Count);
                        _newReaderQueue.Enqueue(newReaderModel);
                    }
                    // start the timer that pops new reader models out of the queue and connects to them
                    // a timer is used so that each connection has 50 ms before the next reader connection
                    // is attempted
                    Log(LogLevel.Debug, "Starting CreateReaderTimer.");
                    _createReaderTimer.Start();
                }
            }
        }

        private bool _loadReadersAsyncCanExecute(object obj) { return true; }

        #endregion Initialize Readers at Load

        #region // Create Reader

        public ICommand CreateReadersAsyncCommand { get; set; }

        private void _createReadersAsyncExecute(object obj)
        {
            if (null != obj && !string.IsNullOrEmpty(obj.ToString()))
            {
                var path = obj.ToString();
                var store = NewReaderModel.LoadFromPath(path);

                foreach (var newReaderModel in store)
                {
                    Log(LogLevel.Verbose, "Adding '{0}' to new reader queue at rank {1}.", newReaderModel.ReaderName, _newReaderQueue.Count);
                    _newReaderQueue.Enqueue(newReaderModel);
                }

                _createReaderTimer.Start();
            }
        }

        private bool _createReadersAsyncCanExecute(object obj) { return true; }

        #endregion Create Readers
    }
}

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

using System.Timers;
using System.Windows.Media;
using System.Windows.Input;
using System.Windows.Controls;
using System.Windows;
using System.Windows.Threading;
using System.Threading;
using System.Windows.Data;

namespace Octane_ViewModel
{
    public partial class SpeedwayReaderViewModel : SpeedwayReader, INotifyPropertyChanged
    {
        public SpeedwayReaderViewModel()
        {
            _initializeProperties();
            _initiliazeCommands();
        }

        #region INotifyPropertyChanged Members

        public void OnPropertyChanged(string propertyName)
        {
            if(null != PropertyChanged)
            {
                PropertyChangedEventArgs e = new PropertyChangedEventArgs(propertyName);
                PropertyChanged(this, e);
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public Dispatcher Dispatcher { get; set; }

        #endregion

        internal static SpeedwayReaderViewModel Factory(NewReaderModel newReaderModel, IMainWindowCallback callback)
        {
            var speedwayReaderViewModel = new SpeedwayReaderViewModel();

            speedwayReaderViewModel.ReaderName = newReaderModel.ReaderName;
            speedwayReaderViewModel.ReaderIdentity = string.IsNullOrEmpty(newReaderModel.ReaderIdentity) ? newReaderModel.ReaderName : newReaderModel.ReaderIdentity;
            speedwayReaderViewModel.LogLevel = LogLevel.Verbose;
            speedwayReaderViewModel.Color = newReaderModel.Color;
            speedwayReaderViewModel._mainWindowCallback = callback;

            speedwayReaderViewModel._log(LogLevel.Verbose, "Creating ViewModel '{0}'.", newReaderModel.ReaderName);

            return speedwayReaderViewModel;
        }

        internal void TryDisconnect()
        {
            try
            {
                StatusLabel = "Disconnecting";
                Disconnect();
            }
            catch (OctaneSdkException octaneException)
            {
                _log(LogLevel.Error, octaneException.Message);
            }
            catch (Exception ex)
            {
                _log(LogLevel.Error, ex.Message);
            }
            finally
            {
                StatusLabel = "Disconnected";
            }
        }

        private void _log(LogLevel logLevel, string message, params object[] args)
        {
            var logEntry = new LogEntryWpf();
            logEntry.Message = string.Format(message, args);
            logEntry.Level = logLevel;
            logEntry.Timestamp = DateTime.Now;
            logEntry.ReaderIdentity = ReaderIdentity;
            logEntry.Origin = LogOrigin.Application;
            logEntry.Color = Color;
            logEntry.ManagedThreadId = Thread.CurrentThread.ManagedThreadId;

            _mainWindowCallback.AddLogEntry(logEntry);
        }

        internal void TryClearSettings()
        {
            try
            {
                StatusLabel = "Clearing Settings";
                ClearSettings();
            }
            catch (OctaneSdkException octaneException)
            {
                _log(LogLevel.Error, octaneException.Message);
            }
            catch (Exception ex)
            {
                _log(LogLevel.Error, ex.Message);
            }
            finally
            {
                StatusLabel = "Settings Cleared";
            }
        }

        internal void TryRebuild(Settings settings)
        {
            try
            {
                StatusLabel = "Applying Defaults";
                ApplySettings(settings);
            }
            catch (OctaneSdkException octaneException)
            {
                _log(LogLevel.Error, octaneException.Message);
            }
            catch (Exception ex)
            {
                _log(LogLevel.Error, ex.Message);
            }
            finally
            {
                StatusLabel = "Defaults Applied";
            }
        }

        internal void TryStop()
        {
            try
            {
                StatusLabel = "Stopping";
                Stop();
                _log(LogLevel.Error, "Reader stopped");
            }
            catch (OctaneSdkException octaneException)
            {
                _log(LogLevel.Error, octaneException.Message);
            }
            catch (Exception ex)
            {
                _log(LogLevel.Error, ex.Message);
            }
            finally
            {
                StatusLabel = "Stopped";
            }
        }
    }

    public delegate void AddLogDelegate(LoggingEventArgs e);
}

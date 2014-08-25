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
using System.IO;
using System.Collections.ObjectModel;
using System.Threading;

using System.Timers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Threading;
using System.Windows.Media;

namespace Octane_ViewModel
{
    /// <summary>
    /// This is the top level controller
    /// </summary>
    public partial class MainWindowViewModel : ViewModelBase, IMainWindowCallback
    {
        public MainWindowViewModel()
        {
            _initializeCommands();
            _initializeProperties();
        }

        /// <summary>
        /// Helper to inject WPF level log entries into log.
        /// </summary>
        /// <param name="logLevel">The level of the log entry.</param>
        /// <param name="messsage">The text message to include in the log.</param>
        /// <param name="args"></param>
        public void Log(LogLevel logLevel, string messsage, params object[] args)
        {
            var logEntry = new LogEntryWpf();
            logEntry.Message = string.Format(messsage, args);
            logEntry.Level = logLevel;
            logEntry.Timestamp = DateTime.Now;
            logEntry.ReaderIdentity = Label;
            logEntry.Origin = LogOrigin.Application;
            logEntry.Color = Brushes.Black;
            logEntry.ManagedThreadId = Thread.CurrentThread.ManagedThreadId;

            AddLogEntry(logEntry);
        }

        /// <summary>
        /// Used to "glue" the WPF Window to this view model. The dispatcher is needed in the ViewModel to
        /// allow the non-UI thread to marshal data to the UI thread.
        /// </summary>
        /// <param name="dispatcher">The instance of the dispatcher from the UI.</param>
        /// <param name="label"></param>
        public void SetDefaults(Dispatcher dispatcher, string label)
        {
            Label = label;
            _dispatcher = dispatcher;

            Log(LogLevel.Debug, "MainWindowModelView's Dispatcher set. Label set to '{0}'.", label);
        }

        #region ISpeedwayManagerCallback Members

        /// <summary>
        /// This is the callback used by Speedway Reader view models to add the log entry to the mail window.
        /// </summary>
        /// <param name="logEntryWpf"></param>
        public void AddLogEntry(LogEntryWpf logEntryWpf)
        {
            if (null != _dispatcher)
            {
                // begin invoke is needed to marshal logs from other threads into the main window thread
                _dispatcher.BeginInvoke(new LoggingDelegate(_loggingHandler),
                                    DispatcherPriority.Background,
                                    new object[] { logEntryWpf });
            }

            // if anyone is listening, forward the log entry to them
            if(null != LogCreated)
                LogCreated(logEntryWpf, new EventArgs());
        }

        /// <summary>
        /// Inserts the new log entry into the observable collection of log entries.
        /// </summary>
        /// <param name="logEntry"></param>
        private void _loggingHandler(LogEntryWpf logEntry)
        {
            LogViewModel.Insert(0, logEntry);
        }

        #endregion

        #region // AddReader

        public void AddReader(SpeedwayReaderViewModel viewModel)
        {
            Log(LogLevel.Verbose, "Dispatching {0}'s ViewModel to add to ReadersObservableCollection.", viewModel.ReaderName);

            if(null != _dispatcher)
                _dispatcher.BeginInvoke(new ReaderCreatedDelegate(_readerHandler), new object[] { viewModel });

            if (null != ReaderCreated)
            {
                try
                {
                    ReaderCreated(viewModel, new EventArgs());
                    Readers.Insert(0, viewModel);
                }
                catch
                {
                }
            }
        }

        public event EventHandler ReaderCreated;
        public event EventHandler ReaderRemoved;
        public event EventHandler TagsCreated;
        public event EventHandler LogCreated;

        private void _readerHandler(SpeedwayReaderViewModel viewModel)
        {
            Readers.Insert(0, viewModel);
            Log(LogLevel.Information, "Adding '{0}' ViewModel to ReadersObservableCollection, it should now be visible in the UI.", viewModel.ReaderName);
        }

        #endregion


        public void AddNewReader(NewReaderModel newReaderModel)
        {
            _newReaderQueue.Enqueue(newReaderModel);
            _createReaderTimer.Start();
        }

        public void SaveAutoStartList()
        {
            var path = SettingsUI.Default.AutoStartReadersPath;
            var list = new List<NewReaderModel>();

            foreach (var reader in Readers)
            {
                var newReader = NewReaderModel.CraeteFromReader(reader);
                list.Add(newReader);
            }

            XmlHelper.SaveToXmlFile(path, list);
        }
    }

    public delegate void LoggingDelegate(LogEntryWpf logEntry);
    public delegate void TagReportedDelegate(TagWpf tag);
    public delegate void TagsReportedDelegate(List<TagWpf> tag);
    public delegate void ReaderCreatedDelegate(SpeedwayReaderViewModel viewModel);
}

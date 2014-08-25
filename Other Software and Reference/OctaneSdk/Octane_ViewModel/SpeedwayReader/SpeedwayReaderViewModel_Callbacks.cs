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
using System.Threading;

namespace Octane_ViewModel
{
    public partial class SpeedwayReaderViewModel
    {
        public override void OnConnected(ConnectionChangedEventArgs args)
        {
            StatusLabel = "Connected";
            Progress = 0;
            _connectAsyncTimeoutTimer.Stop();
        }

        public override void OnDisconnected(ConnectionChangedEventArgs args)
        {
            StatusLabel = "Disconnected";
            Progress = 0;
            _connectAsyncTimeoutTimer.Stop();
        }

        public override void OnLogging(LoggingEventArgs args)
        {
            var logEntry = LogEntryWpf.Convert(args.Entry);
            logEntry.Color = Color;
            _mainWindowCallback.AddLogEntry(logEntry);

            base.OnLogging(args);
        }

        public override void OnStarted(StartedEventArgs args)
        {
            StatusLabel = "Started";
            _log(LogLevel.Verbose, "OnStarted on '{0}'", ReaderName);
        }

        public override void OnStopped(StoppedEventArgs args)
        {
            StatusLabel = "Stopped";
            _log(LogLevel.Verbose, "OnStopped on '{0}'", ReaderName);
        }

        public override void OnTagsReported(TagsReportedEventArgs args)
        {
            var locker = new object();

            lock (locker)
            {
                var list = new List<TagWpf>();

                foreach (var tag in args.TagReport.Tags)
                {
                    var tagWpf = TagWpf.Convert(tag);
                    tagWpf.Color = Color;
                    tagWpf.ManagedThreadId = Thread.CurrentThread.ManagedThreadId;
                    tagWpf.Timestamp = args.Timestamp;

                    list.Add(tagWpf);
                }
                _mainWindowCallback.AddTags(list);
            }

            base.OnTagsReported(args);
        }
    }
}


// Copyright (c) 2010 Impinj, Inc. All rights reserved.

//
// Example6 -- Disconnected Operation
//
// The reader can gather tag information while the application
// is not connected.
//
// Connect to the reader, prepare it, and start it. The reader
// is set to scan for three seconds at 10 second intervals.
// Then disconnect from the reader and let it work automatically.
// After a minute -- about six cycles -- reconnect to the reader
// and retrieve the tag report.
//

using System;
using System.Collections.Generic;
using System.Text;
using Impinj.OctaneSdk;

namespace Example6_DisconnectedOps
{
    class Ex6_DisconnectedOps
    {
        /// <summary>
        /// Static entry point for the example.
        /// </summary>
        /// <param name="args">Space delimited command line arguments.</param>
        static void Main(string[] args)
        {
            bool isInteractive = false;

            string readerName = null;

            if (args.Length == 0)
            {
                isInteractive = true;
                Console.Write("Example 6 Reader => ");
                readerName = Console.ReadLine();
            }
            else
            {
                readerName = args[0];
            }

            Example example = new Example();
            example.Run(readerName);

            if (isInteractive)
            {
                Console.Write("Done => ");
                Console.ReadLine();
            }
        }
    }

    class Example
    {
        public SpeedwayReader Reader = new SpeedwayReader();

        public void Run(string readerName)
        {
            try
            {
                // Change the level of logging detail. The default is Error.
                Reader.LogLevel = LogLevel.Error;

                // Attach to events
                Reader.Logging += new EventHandler<LoggingEventArgs>(LoggingHandler);
                Reader.Connected += new EventHandler<ConnectionChangedEventArgs>(ConnectedHandler);
                Reader.Disconnected += new EventHandler<ConnectionChangedEventArgs>(DisconnectedHandler);
                Reader.Started += new EventHandler<StartedEventArgs>(StartedHandler);
                Reader.Stopped += new EventHandler<StoppedEventArgs>(StoppedHandler);
                Reader.TagsReported += new EventHandler<TagsReportedEventArgs>(TagsReportedHandler);

                // Connect to the reader. The name is the host name
                // or IP address.
                Reader.Connect(readerName);

                // Clear the reader of any RFID operation and configuration.
                Reader.ClearSettings();

                // Query the default settings for the reader. Settings
                // include which antennas are enabled, when to report and
                // optional report fields. Typically you will prepare the
                // reader by getting the default settings and adjusting them.
                //
                // This example sets the reader to start once every ten
                // seconds. After it starts it stops after three seconds.
                // Note that this works out to on for 3, off for 7.
                // The tag report is maintained on the reader until
                // retrieved.
                Settings settings = Reader.QueryFactorySettings();
                settings.AutoStart.Mode = AutoStartMode.Periodic;
                settings.AutoStart.FirstDelayInMs = 0; // no initial delay
                settings.AutoStart.PeriodInMs = 10*1000; // 10 seconds
                settings.AutoStop.Mode = AutoStopMode.Duration;
                settings.AutoStop.DurationInMs = 3*1000; // 3 seconds
                settings.Report.Mode = ReportMode.WaitForQuery;
                settings.Report.IncludeFirstSeenTime = true;
                settings.Report.IncludeLastSeenTime = true;

                // Order the reader to change its settings to these.
                Reader.ApplySettings(settings);

                // The reader will start automatically.

                // Disconnect from the reader and let it work automatically.
                Reader.Disconnect();

                // Pause for a little over a minute
                System.Threading.Thread.Sleep(65*1000);

                // Reconnect to the reader.
                Reader.Connect(readerName);


                // The reader holds events and reports until it is told
                // to send them. This allows the application to connect
                // and prepare.
                //
                // This short pause allows you to see the connection event.
                // Notice, though, that there are no other events delivered
                // during this pause.
                System.Threading.Thread.Sleep(5*1000);

                // Allow the reader to send events and reports.
                // There will be a sudden burst of event reports.
                // Notice that their times are in the past.
                // Again, pause so that you can see events before the report.
                Reader.ResumeEventsAndReports();
                System.Threading.Thread.Sleep(5 * 1000);

                // Get the tag report. Note that the report might be empty.
                TagReport tagReport = Reader.QueryTagReport();

                // Show how many tags were seen. Then show each tag report
                // entry. The Tag.ToString() method takes care of including
                // optional information that is in the tag report.
                Console.WriteLine("First report {0} tags", tagReport.Tags.Count);
                foreach (Tag tag in tagReport.Tags)
                {
                    Console.WriteLine("{0}", tag);
                }

                // During this pause you will see the events reported
                // promptly rather than held by the reader.
                System.Threading.Thread.Sleep(25*1000);

                // Get the tag report. These are the tags that were
                // seen since the last QueryTags.
                tagReport = Reader.QueryTagReport();
                Console.WriteLine("Final report {0} tags", tagReport.Tags.Count);
                foreach (Tag tag in tagReport.Tags)
                {
                    Console.WriteLine("{0}", tag);
                }

                // Tidy up
                Reader.ClearSettings();
            }
            catch (OctaneSdkException ex)
            {
                // OctaneSdkExceptions are detected by the Octane SDK
                // library. These exceptions usually indicate either
                // something wrong with the reader or something wrong
                // with the applications request.
                Console.WriteLine("OctaneSdk detected {0}", ex);
            }
            catch (Exception ex)
            {
                // Any other kind of exception deteced by the system.
                Console.WriteLine("Exception {0}", ex);
            }

            // Safely disconnect from the reader.
            try
            {
                Reader.Disconnect();
            }
            catch (OctaneSdkException ex)
            {
                Console.WriteLine("OctaneSdk detected {0}", ex);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception {0}", ex);
            }
        }

        // The event handler methods below are invoked while running
        // on a callback thread. There is one callback thread per a
        // SpeedwayReader instance. Be sure to use thread safe techniques.

        /// <summary>
        /// Called each time the reader connects.
        /// </summary>
        /// <param name="sender">The reader that sent the event.</param>
        /// <param name="args">Contains information about the event; the reader, timestamp and new
        /// reader state are properties of this object.</param>
        public void ConnectedHandler(object sender, ConnectionChangedEventArgs args)
        {
            Console.WriteLine("Reader connected as of {0}", args.Timestamp);
        }

        /// <summary>
        /// Called each time the reader disconnects.
        /// </summary>
        /// <param name="sender">The reader that sent the event.</param>
        /// <param name="args">Contains information about the event; the reader, timestamp and new
        /// reader state are properties of this object.</param>
        public void DisconnectedHandler(object sender, ConnectionChangedEventArgs args)
        {
            Console.WriteLine("Reader disconnected as of {0}", args.Timestamp);
        }

        /// <summary>
        /// Called each time the reader starts.
        /// </summary>
        /// <param name="sender">The reader that sent the event.</param>
        /// <param name="args">Contains information about the event; the reader, timestamp and new
        /// operational state are properties of this object.</param>
        public void StartedHandler(object sender, StartedEventArgs args)
        {
            Console.WriteLine("Reader started as of {0}", args.Timestamp);
        }

        /// <summary>
        /// Called each time the reader stops.
        /// </summary>
        /// <param name="sender">The reader that sent the event.</param>
        /// <param name="args">Contains information about the event; the reader, timestamp and new
        /// operational state are properties of this object.</param>
        public void StoppedHandler(object sender, StoppedEventArgs args)
        {
            Console.WriteLine("Reader stopped as of {0}", args.Timestamp);
        }

        /// <summary>
        /// Called each time the reader singulates a tag, or a batch of tags.
        /// </summary>
        /// <param name="sender">The reader that sent the event.</param>
        /// <param name="args">Contains information about the event; the reader, timestamp and
        /// tag reports are properties of this object.</param>
        public void TagsReportedHandler(object sender, TagsReportedEventArgs args)
        {
            foreach (Tag tag in args.TagReport.Tags)
            {
                Console.WriteLine("Reader saw {0} on ant#{1}",
                    tag.Epc, tag.AntennaPortNumber);
            }
        }

        /// <summary>
        /// Called each time the library creates a log entry.
        /// </summary>
        /// <param name="sender">The object that originated the log entry.</param>
        /// <param name="args">Contains information about the event; has the log entry as a property.</param>
        public void LoggingHandler(object sender, LoggingEventArgs args)
        {
            LogEntry entry = args.Entry;
            Console.WriteLine("Log level={0} {1}", entry.Level, entry.Message);
        }
    }
}

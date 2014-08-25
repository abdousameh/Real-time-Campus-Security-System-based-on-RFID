
// Copyright (c) 2010 Impinj, Inc. All rights reserved.

//
// Example3 -- Async Tags
//
// This example shows how to hook events to your application
// and what to do with them.
//
// Connect to a Speedway reader, prepare it, and start it.
// Whenever tags enter the field of view of the reader's antennas
// the Octane SDK delivers reports.
//

using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using Impinj.OctaneSdk;

namespace Example3_AsyncTags
{
    class Ex3_AsyncTags
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
                Console.Write("Example 3 Reader => ");
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
                // This example sets the reader to send a tag report
                // immediately on every tag observation. Message buffering
                // improves overall efficiency but introduces a small
                // delay before the application is notified of a tag.
                // Message buffering is enabled by default.
                Settings settings = Reader.QueryFactorySettings();
                settings.Report.Mode = ReportMode.Individual;
                settings.Report.IncludeAntennaPortNumber = true;

                // Order the reader to change its settings to these.
                Reader.ApplySettings(settings);

                // Start the reader.
                Reader.Start();

                // Pause for 10 seconds
                System.Threading.Thread.Sleep(10*1000);

                // Stop the reader.
                Reader.Stop();

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
        // SpeedwayReader instance.
        //
        // Be sure to use thread safe techniques.

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


// Copyright (c) 2010 Impinj, Inc. All rights reserved.

//
// Example3A -- Async Tags
//
// This example shows how to establish override methods that
// receive events and what to do with them.
//
// Connect to a Speedway reader, prepare it, and start it.
// Whenever tags enter the field of view of the reader's antennas
// the OctaneSdk delivers results.
//
// Use this approach when prototyping on-reader applications.
// The C++ OctaneSdk used for on-reader applications only has
// override methods. Events are not available in C++ the way
// they are in C#.
//

using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using Impinj.OctaneSdk;

namespace Example3A_AsyncTags
{
    class Ex3A_AsyncTags
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
                Console.Write("Example 3A Reader => ");
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

    class Example : SpeedwayReader
    {
        // No need to create an instance. When this Example class
        // is instantiated so is the SpeedwayReader class.

        public void Run(string readerName)
        {
            try
            {
                // Change the level of logging detail. The default is Error.
                this.LogLevel = LogLevel.Error;

                // No need to attach to events. Just create an override below.

                // Connect to the reader. The name is the host name
                // or IP address.
                this.Connect(readerName);

                // Clear the reader of any RFID operation and configuration.
                this.ClearSettings();

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
                Settings settings = this.QueryFactorySettings();
                settings.Report.Mode = ReportMode.Individual;
                settings.Report.IncludeAntennaPortNumber = true;

                // Order the reader to change its settings to these.
                this.ApplySettings(settings);

                // Start the reader.
                this.Start();

                // Pause for 10 seconds
                System.Threading.Thread.Sleep(10*1000);

                // Stop the reader.
                this.Stop();

                // Tidy up
                this.ClearSettings();
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
                this.Disconnect();
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

        public override void OnConnected(ConnectionChangedEventArgs args)
        {
            Console.WriteLine("Reader connected as of {0}", args.Timestamp);
            base.OnConnected(args);
        }

        public override void OnDisconnected(ConnectionChangedEventArgs args)
        {
            Console.WriteLine("Reader disconnected as of {0}", args.Timestamp);
            base.OnDisconnected(args);
        }

        public override void OnStarted(StartedEventArgs args)
        {
            Console.WriteLine("Reader started as of {0}", args.Timestamp);
            base.OnStarted(args);
        }

        public override void OnStopped(StoppedEventArgs args)
        {
            Console.WriteLine("Reader stopped as of {0}", args.Timestamp);
            base.OnStopped(args);
        }

        public override void OnTagsReported(TagsReportedEventArgs args)
        {
            foreach (Tag tag in args.TagReport.Tags)
            {
                Console.WriteLine("Reader saw {0} on ant#{1}",
                    tag.Epc, tag.AntennaPortNumber);
            }
            base.OnTagsReported(args);
        }

        // The logging event handler can be invoked from a variety
        // of threads. Be sure to use thread safe techniques.

        public override void OnLogging(LoggingEventArgs args)
        {
            LogEntry entry = args.Entry;
            Console.WriteLine("Log level={0} {1}", entry.Level, entry.Message);
            base.OnLogging(args);
        }
    }
}

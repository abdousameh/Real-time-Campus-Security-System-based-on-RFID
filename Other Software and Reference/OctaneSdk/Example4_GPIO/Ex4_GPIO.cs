
// Copyright (c) 2010 Impinj, Inc. All rights reserved.

//
// Example4 -- General Purpose Input/Output ports
//
// Set the reader to automatically start/stop when GPI1 is turned on/off.
// While the reader is running a light connected to GPO1 is turned on.
//

using System;
using System.Collections.Generic;
using System.Text;
using Impinj.OctaneSdk;

namespace Example4_GPIO
{
    class Ex4_GPIO
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
                Console.Write("Example 4 Reader => ");
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
                Reader.Started += new EventHandler<StartedEventArgs>(StartedHandler);
                Reader.Stopped += new EventHandler<StoppedEventArgs>(StoppedHandler);
                Reader.Gpi1Changed += new EventHandler<GpiChangedEventArgs>(Gpi1ChangedHandler);
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
                // In this example the reader is set to automatically
                // start when GPI1 goes high and stop when GPI1 goes low.
                // GPI1 is essentially an on/off switch. Note that GPI1
                // must be explicitly enabled.
                //
                // The tags are reported in one batch when the reader stops.
                Settings settings = Reader.QueryFactorySettings();
                settings.Gpis[1].IsEnabled = true;
                settings.Gpis[1].DebounceInMs = 50;
                settings.AutoStart.Mode = AutoStartMode.GpiTrigger;
                settings.AutoStart.GpiPortNumber = 1;
                settings.AutoStart.GpiLevel = true;
                settings.AutoStop.Mode = AutoStopMode.GpiTrigger;
                settings.AutoStop.GpiPortNumber = 1;
                settings.AutoStop.GpiLevel = false;
                settings.Report.Mode = ReportMode.BatchAfterStop;

                // Order the reader to change its settings to these.
                Reader.ApplySettings(settings);

                // Example runs until Enter is pressed. Use GPI1
                // to start/stop the reader.
                Console.Write("Press enter when done --> ");
                Console.ReadLine();

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
        /// Called each time the reader starts.
        /// </summary>
        /// <param name="sender">The reader that sent the event.</param>
        /// <param name="args">Contains information about the event; the reader, timestamp and new
        /// operational state are properties of this object.</param>
        public void StartedHandler(object sender, StartedEventArgs args)
        {
            // Turn activity light on
            Reader.SetGpo(1, true);
        }

        /// <summary>
        /// Called each time the reader stops.
        /// </summary>
        /// <param name="sender">The reader that sent the event.</param>
        /// <param name="args">Contains information about the event; the reader, timestamp and new
        /// operational state are properties of this object.</param>
        public void StoppedHandler(object sender, StoppedEventArgs args)
        {
            // Turn activity light off
            Reader.SetGpo(1, false);
        }

        /// <summary>
        /// Called each time the reader singulates a tag, or a batch of tags.
        /// </summary>
        /// <param name="sender">The reader that sent the event.</param>
        /// <param name="args">Contains information about the event; the reader, timestamp and
        /// tag reports are properties of this object.</param>
        public void TagsReportedHandler(object sender, TagsReportedEventArgs args)
        {
            // Invoked each time the reader stops
            foreach (Tag tag in args.TagReport.Tags)
            {
                Console.WriteLine("Reader saw {0} on ant#{1}",
                    tag.Epc, tag.AntennaPortNumber);
            }
        }

        /// <summary>
        /// Called each time the state of GPI #1 changes.
        /// </summary>
        /// <param name="sender">The reader that sent the event.</param>
        /// <param name="args">Contains imfornation about hte event; the reader, timestamp and
        /// the new state of the GPI.</param>
        public void Gpi1ChangedHandler(object sender, GpiChangedEventArgs args)
        {
            Console.WriteLine("GPI1 now {0}", args.State);
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

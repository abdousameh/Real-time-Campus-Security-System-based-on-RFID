
// Copyright (c) 2010 Impinj, Inc. All rights reserved.

//
// Example5 -- Settings Files
//
// This example shows how settings files may be used.
//
// Connect to the reader, get the default settings and save them to a file.
// Give the user a chance to edit the file. Load the settings from the
// file, prepare the reader, and start it.
//
// This is an easy way to experiment with different settings.
//

using System;
using System.Collections.Generic;
using System.Text;
using Impinj.OctaneSdk;

namespace Example5_SettingsFiles
{
    class Ex5_SettingsFiles
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
                Console.Write("Example 5 Reader => ");
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
                // In this example, the settings are adjusted by the user
                // by editing a file rather than the more usual adjusting
                // the settings in code.
                Settings settings = Reader.QueryFactorySettings();

                // Save the settings.
                settings.Save("TheSettings.xml");

                // Give the user a chance to edit them.
                Console.Write("Edit TheSettings.xml file, then press Enter --> ");
                Console.ReadLine();

                // Load the settings from the file.
                settings = Settings.Load("TheSettings.xml");

                // Order the reader to change its settings to these.
                Reader.ApplySettings(settings);

                // If the reader isn't set to automatically start
                // explicitly start it now.
                if (AutoStartMode.None == settings.AutoStart.Mode)
                {
                    Reader.Start();
                }

                Console.Write("Press Enter when done --> ");
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
                Console.WriteLine("{0}", tag);
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

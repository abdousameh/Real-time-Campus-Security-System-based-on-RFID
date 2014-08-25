
// Copyright (c) 2010 Impinj, Inc. All rights reserved.

//
// Example2 -- Query Tags
//
// This is the simplest way to use the reader.
//
// Connect to a Speedway reader, prepare it, and look for tags in
// view of the reader's antennas. The request is synchronous.
// There are no events or callbacks.
//

using System;
using System.Collections.Generic;
using System.Text;
using Impinj.OctaneSdk;

namespace Example2_QueryTags
{
    class Ex2_QueryTags
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
                Console.Write("Example 2 Reader => ");
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
                // In this example we make sure the reader holds
                // tag reports until we query for them, and also
                // include which antenna the tag was seen on.
                Settings settings = Reader.QueryFactorySettings();
                settings.Report.Mode = ReportMode.WaitForQuery;
                settings.Report.IncludeAntennaPortNumber = true;

                // Order the reader to change its settings to these.
                Reader.ApplySettings(settings);

                // Order the reader to turn on for 7.250 seconds and
                // then report all tags seen.
                Console.WriteLine("Querying tags...");
                TagReport tagReport = Reader.QueryTags(7.250);

                // Show how many tags were seen. Then show each tag report
                // entry. The Tag.ToString() method takes care of including
                // optional information that is in the tag report.
                Console.WriteLine("{0} tags", tagReport.Tags.Count);
                foreach (Tag tag in tagReport.Tags)
                {
                    Console.WriteLine("{0}", tag);
                }
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

        // The logging event handler can be invoked from a variety
        // of threads. Be sure to use thread safe techniques.

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

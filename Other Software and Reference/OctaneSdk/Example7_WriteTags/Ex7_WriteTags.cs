
// Copyright (c) 2010 Impinj, Inc. All rights reserved.

//
// Example7 -- Write Tags
//
// Find all the tags in the field of view, then one-by-one change
// their EPC.
//

using System;
using System.Collections.Generic;
using System.Text;
using Impinj.OctaneSdk;

namespace Example7_WriteTags
{
    class Ex7_WriteTags
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
                Console.Write("Example 7 Reader => ");
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

    public class Example
    {
        public SpeedwayReader Reader = new SpeedwayReader();

        public void Run(string readerName)
        {
            // Change the level of logging detail. The default is Error.
            Reader.LogLevel = LogLevel.Error;

            // Attach to events
            Reader.Logging += new EventHandler<LoggingEventArgs>(LoggingHandler);

            try
            {
                Reader.Connect(readerName);
                Reader.ClearSettings();

                var settings = Reader.QueryFactorySettings();
                Reader.ApplySettings(settings);

                Console.WriteLine("Querying Tags");
                TagReport tagReport = Reader.QueryTags(5.000);

                Console.WriteLine("Found {0} tags", tagReport.Tags.Count);
                foreach (Tag tag in tagReport.Tags)
                {
                    Console.WriteLine("{0}", tag.Epc);
                }

                string NewEpcBase = "1111-2222-3333-4444-5555-";
                int index = 0;

                foreach (Tag tag in tagReport.Tags)
                {
                    ProgramEpcParams accessParams = new ProgramEpcParams();
                    ProgramEpcResult accessResult;

                    accessParams.TargetTag = tag.Epc;
                    accessParams.AccessPassword = null;
                    accessParams.TimeoutInMs = 5000;
                    accessParams.AntennaPortNumber = 1;
                    accessParams.NewEpc = NewEpcBase + index.ToString("x4");
                    accessParams.IsWriteVerified = true;
                    accessParams.LockType = LockType.Locked;

                    accessResult = Reader.ProgramEpc(accessParams);

                    Console.WriteLine("Programmed tag {0}: {1}",
                        accessResult.TagAccessed.Epc, accessResult.Result);

                    index++;
                }

                Console.WriteLine("Querying Tags");
                tagReport = Reader.QueryTags(5.000);

                Console.WriteLine("Found {0} tags", tagReport.Tags.Count);
                foreach (Tag tag in tagReport.Tags)
                {
                    Console.WriteLine("{0}", tag.Epc);
                }

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

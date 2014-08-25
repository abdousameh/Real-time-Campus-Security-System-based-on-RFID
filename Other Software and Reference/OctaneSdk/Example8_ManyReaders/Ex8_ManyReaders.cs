
// Copyright (c) 2010 Impinj, Inc. All rights reserved.

//
// Example8 -- Many Readers
//
// This example shows a way to manage multiple readers. Each
// reader is connected, prepared, and started. The tag reports
// are gathered into a Dictionary<>, which is NOT a thread safe
// structure, and this example shows how to lock it. After a
// ten second run all the readers are stopped and cleared.
// Finally the contents of the Dictionary<> are shown.
//
// The main concern with multiple readers is proper multi-threaded
// techniques. While this example introduces the concern, it should
// not be considered a complete solution. 
//

using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using Impinj.OctaneSdk;

namespace Example8_ManyReaders
{
    class Ex8_ManyReaders
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
                Console.Write("Example 8 Reader => ");
                readerName = Console.ReadLine();
            }
            else
            {
                readerName = args[0];
            }

            Example example = new Example();
            example.Run(readerName.Split(','));

            if (isInteractive)
            {
                Console.Write("Done => ");
                Console.ReadLine();
            }
        }
    }
    
    class Example
    {
        public List<string> ReaderNames = new List<string>();
        public List<SpeedwayReader> Readers = new List<SpeedwayReader>();
        public Dictionary<string,List<Tag>> TagTable = new Dictionary<string,List<Tag>>();

        public void Run(string[] args)
        {
            ReaderNames = new List<string>(args);

            ConnectAndPrepareAllReaders();
            StartAllReaders();
            System.Threading.Thread.Sleep(10*1000);    // 10 seconds
            StopAllReaders();
            System.Threading.Thread.Sleep(1*1000);     // 1 seconds
            ClearAndDisconnectAllReaders();

            foreach (KeyValuePair<string,List<Tag>> kvp in TagTable)
            {
                Console.WriteLine("EPC {0}", kvp.Key);
                foreach (Tag tag in kvp.Value)
                {
                    Console.WriteLine("  Reader {0} on ant#{1} at {2}",
                        tag.ReaderIdentity, tag.AntennaPortNumber, tag.FirstSeenTime);
                }
            }
        }
        
        public void ConnectAndPrepareAllReaders()
        {
            foreach (string readerName in ReaderNames)
            {
                SpeedwayReader reader = new SpeedwayReader();

                try
                {
                    // Set the reader identity. It can be any object.
                    // This is passed back with events and tag reports.
                    reader.ReaderIdentity = readerName;

                    // Change the level of logging detail. The default is Error.
                    reader.LogLevel = LogLevel.Error;

                    // Attach to events
                    reader.Logging += new EventHandler<LoggingEventArgs>(LoggingHandler);
                    reader.Started += new EventHandler<StartedEventArgs>(StartedHandler);
                    reader.Stopped += new EventHandler<StoppedEventArgs>(StoppedHandler);
                    reader.TagsReported += new EventHandler<TagsReportedEventArgs>(TagsReportedHandler);

                    reader.Connect(readerName);

                    Settings settings = reader.QueryFactorySettings();
                    settings.Report.Mode = ReportMode.Individual;
                    settings.Report.IncludeFirstSeenTime = true;
                    settings.Report.IncludeAntennaPortNumber = true;

                    reader.ApplySettings(settings);

                    // Add this reader to the list of readers.
                    Readers.Add(reader);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Exception {0} for reader {1}", ex, readerName);
                    try
                    {
                        reader.Disconnect();
                    }
                    catch (OctaneSdkException octaneSdkException)
                    {
                        Console.WriteLine("OctaneSdk detected {0}", octaneSdkException);
                    }
                    catch (Exception exception)
                    {
                        Console.WriteLine("Exception {0}", exception);
                    }
                }
            }
        }

        public void StartAllReaders()
        {
            foreach (SpeedwayReader reader in Readers)
            {
                try
                {
                    reader.Start();
                }
                catch (OctaneSdkException ex)
                {
                    Console.WriteLine("Reader {0} start: OctaneSdk detected {1}",
                        reader.ReaderIdentity, ex);
                }
                catch (Exception ex)
                {
                    // Any other kind of exception deteced by the system.
                    Console.WriteLine("Reader {0} start: Exception {1}",
                        reader.ReaderIdentity, ex);
                }
            }
        }

        public void StopAllReaders()
        {
            foreach (SpeedwayReader reader in Readers)
            {
                try
                {
                    reader.Stop();
                }
                catch (OctaneSdkException ex)
                {
                    Console.WriteLine("Reader {0} stop: OctaneSdk detected {1}",
                        reader.ReaderIdentity, ex);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Reader {0} stop: Exception {1}",
                        reader.ReaderIdentity, ex);
                }
            }
        }

        public void ClearAndDisconnectAllReaders()
        {
            foreach (SpeedwayReader reader in Readers)
            {
                try
                {
                    reader.ClearSettings();
                }
                catch (OctaneSdkException ex)
                {
                    Console.WriteLine("Reader {0} clear: OctaneSdk detected {1}",
                        reader.ReaderIdentity, ex);
                }
                catch (Exception ex)
                {
                    // Any other kind of exception deteced by the system.
                    Console.WriteLine("Reader {0} clear: Exception {1}",
                        reader.ReaderIdentity, ex);
                }

                try
                {
                    reader.Disconnect();
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
            Console.WriteLine("Reader {0} started", args.ReaderIdentity);
        }

        /// <summary>
        /// Called each time the reader stops.
        /// </summary>
        /// <param name="sender">The reader that sent the event.</param>
        /// <param name="args">Contains information about the event; the reader, timestamp and new
        /// operational state are properties of this object.</param>
        public void StoppedHandler(object sender, StoppedEventArgs args)
        {
            Console.WriteLine("Reader {0} stopped", args.ReaderIdentity);
        }

        /// <summary>
        /// Called each time the reader singulates a tag, or a batch of tags.
        /// </summary>
        /// <param name="sender">The reader that sent the event.</param>
        /// <param name="args">Contains information about the event; the reader, timestamp and
        /// tag reports are properties of this object.</param>
        public void TagsReportedHandler(object sender, TagsReportedEventArgs args)
        {
            // Lock TagTable to protect if from multiple thread concerns.
            // There is a thread per reader and reports might show up
            // at about the same time.
            lock (TagTable)
            {
                foreach (Tag tag in args.TagReport.Tags)
                {
                    string tagEpcStr = tag.Epc.ToString();

                    if (!TagTable.ContainsKey(tagEpcStr))
                    {
                        TagTable.Add(tagEpcStr, new List<Tag>());
                    }

                    TagTable[tagEpcStr].Add(tag);

                    //Console.WriteLine("Reader {0} saw {1} on ant#{2}",
                    //    args.ReaderIdentity, tag.Epc, tag.AntennaPortNumber);
                }
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


// Copyright (c) 2010 Impinj, Inc. All rights reserved.

//
// Example1 -- Query Features
//
// Connect to a Speedway reader and report its model name,
// version information, and the status of each antenna port.
//

using System;
using System.Collections.Generic;
using System.Text;
using Impinj.OctaneSdk;


namespace Example1_QueryFeatures
{
    class Ex1_QueryFeatures
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
                Console.Write("Example 1 Reader => ");
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

                // Query the features of the reader. This includes model
                // name, version information, subregion, transmit powers,
                // frequencies, sensitivities, modes, etc.
                FeatureSet featureSet = Reader.QueryFeatureSet();

                Console.WriteLine("Model              {0}", featureSet.ModelName);
                Console.WriteLine("Software Version   {0}", featureSet.SoftwareVersion);
                Console.WriteLine("Firmware Version   {0}", featureSet.FirmwareVersion);
                Console.WriteLine("PCBA Version       {0}", featureSet.PcbaVersion);
                Console.WriteLine("FPGA Version       {0}", featureSet.FpgaVersion);
                Console.WriteLine("Regulator Region   {0}", featureSet.Subregion);

                // Query the status of the reader. This includes the
                // connection state of each antenna, the state of each
                // general purpose input (GPI), the state of the RFID
                // operation, etc.
                Status status = Reader.QueryStatus(StatusRefresh.Everything);

                Console.Write("Antennas           ");
                foreach (AntennaStatus antStat in status.Antennas)
                {
                    string stateStr = String.Empty;

                    switch (antStat.State)
                    {
                        // The port present on the reader and connected
                        // to an antenna.
                        case AntennaConnectionState.Connected:
                            stateStr = "Connected";
                            break;

                        // The port is present on the reader but is not
                        // connected to an antenna, or the connection is bad.
                        case AntennaConnectionState.NotConnected:
                            stateStr = "Unconnected";
                            break;

                        // No such port on the reader.
                        case AntennaConnectionState.NotApplicable:
                            stateStr = "N/A";
                            break;

                        // Unknown state. This usually means something
                        // went wrong communicating with the reader.
                        case AntennaConnectionState.Unknown:
                            stateStr = "Unknown";
                            break;

                        // Should never happen.
                        default:
                            stateStr = "??";
                            break;
                    }
                    Console.Write(" {0}:{1}", antStat.PortNumber, stateStr);
                }
                Console.WriteLine();
            }
            catch (OctaneSdkException ex)
            {
                // OctaneSdkExceptions are detected by the Octane SDK
                // library. These exceptions usually indicate either
                // something wrong with the reader or something wrong
                // with the application's request.
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

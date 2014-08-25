using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Impinj.OctaneSdk;

namespace Template_Cons_CS
{
    public class ConsoleController
    {
        public ConsoleController()
        {
            _speedwayReader = new SpeedwayReader();
            _speedwayReader.LogLevel = LogLevel.Verbose;
            _speedwayReader.Logging += new EventHandler<LoggingEventArgs>(_speedwayReader_Logging);
            _speedwayReader.Gpi1Changed += new EventHandler<GpiChangedEventArgs>(_speedwayReader_Gpi1Changed);
            _speedwayReader.Gpi2Changed += new EventHandler<GpiChangedEventArgs>(_speedwayReader_Gpi2Changed);
            _speedwayReader.Gpi3Changed += new EventHandler<GpiChangedEventArgs>(_speedwayReader_Gpi3Changed);
            _speedwayReader.Gpi4Changed += new EventHandler<GpiChangedEventArgs>(_speedwayReader_Gpi4Changed);
        }

        void _speedwayReader_Gpi1Changed(object sender, GpiChangedEventArgs e)
        {
            _stdOut("Gpi 1 changed, it's new state is {0}", e.State);
        }

        void _speedwayReader_Gpi2Changed(object sender, GpiChangedEventArgs e)
        {
            _stdOut("Gpi 2 changed, it's new state is {0}", e.State);
        }

        void _speedwayReader_Gpi3Changed(object sender, GpiChangedEventArgs e)
        {
            _stdOut("Gpi 3 changed, it's new state is {0}", e.State);
        }

        void _speedwayReader_Gpi4Changed(object sender, GpiChangedEventArgs e)
        {
            _stdOut("Gpi 4 changed, it's new state is {0}", e.State);
        }

        void _speedwayReader_Logging(object sender, LoggingEventArgs e)
        {
            var message = string.Format("{0}\t{1}\t{2}",
                e.Entry.Origin,
                e.Entry.Level,
                e.Entry.Message);

            _stdOut(message);
        }

        internal bool ProcessCommand(string userInput)
        {
            var success = true;

            SpeedwayCommand command;

            try
            {
                if (!string.IsNullOrEmpty(userInput))
                {
                    var commandName = _parseCommand(userInput);
                    var args = _parseArgs(userInput);

                    command = (SpeedwayCommand)Enum.Parse(typeof(SpeedwayCommand), commandName, true);
                    success = _process(command, args);
                }
                else
                {
                }
            }
            catch (ArgumentNullException ex)
            {
                // they probably just hit enter, so reprint console prompt (do nothing)
            }
            catch (ArgumentException ex)
            {
                var message = string.Format("The command '{0}' was not found", userInput);
                _stdOut(message);
            }

            return success;
        }

        private SpeedwayReader _speedwayReader;

        private string _parseCommand(string userInput)
        {
            var command = string.Empty;

            if (!string.IsNullOrEmpty(userInput))
            {
                var array = userInput.Split(' ');
                command = array[0];
            }

            return command;
        }

        private string[] _parseArgs(string userInput)
        {
            var args = new string[0];

            if (!string.IsNullOrEmpty(userInput))
            {
                var list = userInput.Split(' ').ToList();
                // pluck the first one out of the list,
                // it is the command
                list.Remove(list[0]);
                args = list.ToArray();
            }

            return args;
        }

        private void _stdOut(string message, params object[] args)
        {
            Console.WriteLine(message, args);
        }

        private bool _process(SpeedwayCommand command, string[] args)
        {
            var success = true;

            switch (command)
            {
                case SpeedwayCommand.ChangeWithFactorySettings:
                    _stdOut("Change with Factory Settings...");
                    _changeWithFactorySettings();
                    _stdOut("... reader's settings changed.");
                    break;
                case SpeedwayCommand.Connect:
                    _stdOut("Connecting...");
                    _speedwayReader.Connect(args[0]);
                    _stdOut("Connected to '{0}", args[0]);
                    break;
                case SpeedwayCommand.Disconnect:
                    _stdOut("Disconnecting...");
                    _speedwayReader.Disconnect();
                    _stdOut("Disconnected");
                    break;
                case SpeedwayCommand.Initialize:
                    _stdOut("Ready...");
                    _stdOut(string.Empty);
                    break;
                case SpeedwayCommand.LogLevel:
                    _logLevelHandler(args[0]);
                    _stdOut("Log level set to '{0}'", args[0]);
                    break;
                case SpeedwayCommand.QueryFeatureSet:
                    _stdOut("Querying feature set...");
                    _queryFeatureSet();
                    break;
                case SpeedwayCommand.QueryStatus:
                    _stdOut("Querying status...");
                    _queryStatus();
                    break;
                case SpeedwayCommand.SetGpo:
                    _stdOut("Set gpo {0} to {1}", args[0], args[1]);
                    var portNumber = int.Parse(args[0].ToString());
                    var level = bool.Parse(args[1].ToString());
                    _setGpo(portNumber, level);
                    break;
                default:
                    break;
            }

            return success;
        }

        private void _setGpo(int portNumber, bool level)
        {
            _speedwayReader.SetGpo(portNumber, level);
        }

        private void _changeWithFactorySettings()
        {
            var settings = _speedwayReader.QueryDefaultSettings();
            
            settings.Gpis[1].IsEnabled = true;
            settings.Gpis[2].IsEnabled = true;
            settings.Gpis[3].IsEnabled = true;
            settings.Gpis[4].IsEnabled = true;

            _speedwayReader.ApplySettings(settings);
        }

        private void _logLevelHandler(string logLevel)
        {
            try
            {
                if (!string.IsNullOrEmpty(logLevel))
                {
                    var level = (LogLevel)Enum.Parse(typeof(LogLevel), logLevel, true);
                    _speedwayReader.LogLevel = level;
                }
                else
                {
                }
            }
            catch (ArgumentNullException ex)
            {
                // they probably just hit enter, so reprint console prompt (do nothing)
            }
            catch (ArgumentException ex)
            {
                var message = string.Format("The log level '{0}' was not found", logLevel);
                _stdOut(message);
            }
        }

        private void _queryStatus()
        {
            var status = _speedwayReader.QueryStatus(StatusRefresh.Everything);

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

        private void _queryFeatureSet()
        {
            var featureSet = _speedwayReader.QueryFeatureSet();

            Console.WriteLine("Model              {0}", featureSet.ModelName);
            Console.WriteLine("Software Version   {0}", featureSet.SoftwareVersion);
            Console.WriteLine("Firmware Version   {0}", featureSet.FirmwareVersion);
            Console.WriteLine("PCBA Version       {0}", featureSet.PcbaVersion);
            Console.WriteLine("FPGA Version       {0}", featureSet.FpgaVersion);
            Console.WriteLine("Regulator Region   {0}", featureSet.Subregion);
        }
    }
}

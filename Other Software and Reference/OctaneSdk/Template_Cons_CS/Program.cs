
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Impinj.OctaneSdk;

namespace Template_Cons_CS
{
    public class Program
    {
        #region public methods

        /// <summary>
        /// Constructor.
        /// </summary>
        public Program()
        {
            // have to instantiate the controller
            // it wraps the SpeedwayReader class
            _speedwayController = new SpeedwayController();
            _speedwayController.Logging += new EventHandler<LoggingEventArgs>(_controller_Logging);
        }

        /// <summary>
        /// Static entry point for a Console application.
        /// </summary>
        /// <param name="args">Command line arguements.</param>
        static void Main(string[] args)
        {
            // instantiate the program class
            var program = new Program();
            // ... and start it
            program._start();
        }

        /// <summary>
        /// This is the command processor. Each time a new line is typed into the
        /// console, it is processed by this method.
        /// </summary>
        /// <param name="userInput">The command, and arguments, the user entered.</param>
        /// <returns>Whether or not the user input was processed correctly.</returns>
        public bool ProcessCommand(string userInput)
        {
            // the return value, assume it is going to work
            var success = true;

            try
            {
                // if there is a value to process
                if (!string.IsNullOrEmpty(userInput))
                {
                    // parse out the command name
                    var commandName = _parseCommand(userInput);
                    // treat the remaining values as arguments
                    var args = _parseArgs(userInput);

                    // try and parse the command
                    SpeedwayCommand command = (SpeedwayCommand)Enum.Parse(typeof(SpeedwayCommand), commandName, true);
                    // then pass it off the the command router where it will be fulfilled
                    _process(command, args);
                }
            }
            catch (ArgumentNullException ex)
            {
                // they probably just hit enter, so reprint console prompt (do nothing)
            }
            catch (ArgumentException ex)
            {
                // if there was a bad argument, or the command was not parsed correctly
                // print out a helper message, and include the exception's message
                _stdOut("The command '{0}' was not found, an argument was invalid, or an argument was missing ({1})", userInput, ex.Message);
                success = false;
            }

            return success;
        }

        /// <summary>
        /// After commands are successfully parsed, this method is called where the arguments
        /// are formed up and then passed to the controller, where they are fulfilled. 
        /// </summary>
        /// <param name="command">The command to execute.</param>
        /// <param name="args">The args that were on the command line.</param>
        private void _process(SpeedwayCommand command, string[] args)
        {
            string path = null;

            switch (command)
            {
                case SpeedwayCommand.Connect:
                    _connectHandler(args[0]);
                    break;
                case SpeedwayCommand.Disconnect:
                    _disconnectHandler();
                    break;
                case SpeedwayCommand.Help:
                    _printHelp();
                    break;
                case SpeedwayCommand.LoadFactorySettings:
                    _loadFactorySettingsHandler();
                    break;
                case SpeedwayCommand.LoadSettings:
                    _loadSettingsHandler(args[0]);
                    break;
                case SpeedwayCommand.LogLevel:
                    _logLevelHandler(args[0]);
                    break;
                case SpeedwayCommand.QueryFactorySettings:
                    // set path to null if there was no argument passed in
                    path = (args.Length == 0) ? args[0] : null;
                    _queryFactorySettingsHandler(path);
                    break;
                case SpeedwayCommand.QueryFeatureSet:
                    // set path to null if there was no argument passed in
                    path = (args.Length == 0) ? null : args[0];
                    _queryFeatureSetHandler(path);
                    break;
                case SpeedwayCommand.QueryStatus:
                    // the first argument must match a status refresh value
                    if (args.Length > 0)
                    {
                        var refresh = (StatusRefresh)Enum.Parse(typeof(StatusRefresh), args[0], true);
                        // set path to null if there was no second argument passed in
                        path = (args.Length == 2) ? args[1] : null;
                        _queryStatusHandler(refresh, path);
                    }
                    else
                    {
                        throw new ArgumentException("Must have a status refresh type, try 'None', 'Everything', 'JustGpis', or 'JustAntennas'");
                    }
                    break;
                case SpeedwayCommand.QueryTags:
                    // first argument must be a double
                    var duration = double.Parse(args[0]);
                    // set path to null if there was no second argument passed in
                    path = (args.Length == 2) ? args[1] : null;
                    _queryTagsHandler(duration, path);
                    break;
                case SpeedwayCommand.SaveSettings:
                    _saveSettingsHandler(args[0]);
                    break;
                case SpeedwayCommand.SetGpo:
                    // first argument must be an integer
                    var portNumber = int.Parse(args[0].ToString());
                    // second argument must be a boolean
                    var level = bool.Parse(args[1].ToString());
                    _speedwayController.SetGpo(portNumber, level);
                    break;
                case SpeedwayCommand.UpdateTag:
                    if (2 == args.Length)
                    {
                        _updateTagHandler(args[0], args[1]);
                    }
                    else
                    {
                        throw new ArgumentException("Must have a target epc and the new epc's value");
                    }
                    break;
                default:
                    break;
            }
        }

        #endregion

        #region properties

        /// <summary>
        /// This is what is printed at the beginning of each line
        /// </summary>
        public const string Prompt = ":> ";
        /// <summary>
        /// Wraps the SpeedwayReader class, used to fulfill commands.
        /// </summary>
        private SpeedwayController _speedwayController;
        /// <summary>
        /// Console settings has the help text for each command, this is used as a wrapper to
        /// keep the line length down.
        /// </summary>
        private ConsoleSettings _settings
        {
            get { return ConsoleSettings.Default; }
        }

        #endregion

        #region private methods

        /// <summary>
        /// Formats and writes out the passed in log entry.
        /// </summary>
        /// <param name="logEntry">The log entry to format.</param>
        private void _log(LogEntry logEntry)
        {
            var message = string.Format("{0}\t{1}\t{2}",
                logEntry.Origin,
                logEntry.Level,
                logEntry.Message);

            _stdOut(message);
        }

        /// <summary>
        /// This is the entry point for the Program class. It loops until the command "quit" is entered.
        /// </summary>
        private void _start()
        {
            var userInput = string.Empty;

            while (!userInput.ToLower().Equals("quit"))
            {
                Console.Write(Prompt);
                userInput = Console.ReadLine();
                ProcessCommand(userInput);
            }
        }

        /// <summary>
        /// Strips off and passes back what is before the first space.
        /// </summary>
        /// <param name="userInput">The command line the user entered.</param>
        /// <returns>The first word of the line.</returns>
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

        /// <summary>
        /// Removed the command and passed back the rest of the user input as
        /// a string array that is split on spaces.
        /// </summary>
        /// <param name="userInput">The command line the user entered.</param>
        /// <returns>An array of args.</returns>
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

        /// <summary>
        /// Helper function used to write progress to the console.
        /// </summary>
        /// <param name="message">The message to write.</param>
        /// <param name="args">Optional parameters to use in case the message has indexes.</param>
        private void _stdOut(string message, params object[] args)
        {
            Console.WriteLine(message, args);
        }

        /// <summary>
        /// Helper function to print help to the console.
        /// </summary>
        private void _printHelp()
        {
            Console.WriteLine("Commands:" + Environment.NewLine);

            // pass each command name with its synopsis/description pair to the help line printer
            _printHelpLine("Connect", _settings.ConnectDescription, _settings.ConnectSynopsis);
            _printHelpLine("Disconnect", _settings.DisconnectDescription, _settings.DisconnectSynopsis);
            _printHelpLine("LoadFactorySettings", _settings.LoadFactorySetttingsDescription, _settings.LoadFactorySettingsSynopsis);
            _printHelpLine("LoadSettings", _settings.LoadSettingsDescription, _settings.LoadSettingsSynopsis);
            _printHelpLine("LogLevel", _settings.LoadSettingsDescription, _settings.LogLevelSynopsis);
            _printHelpLine("QueryFactorySettings", _settings.QueryFactorySettingsDescription, _settings.QueryFactorySettingsSynopsis);
            _printHelpLine("QueryFeatureSet", _settings.QueryFeatureSetDescription, _settings.QueryFeatureSetSynopsis);
            _printHelpLine("QueryStatus", _settings.QueryStatusDescription, _settings.QueryStatusSynopsis);
            _printHelpLine("QueryTags", _settings.QueryTagsDescription, _settings.QueryTagsSynopsis);
            _printHelpLine("Quit", _settings.QuitDescription, _settings.QuitSynopsis);
            _printHelpLine("SaveSettings", _settings.SaveSettingsDescription, _settings.SaveSettingsSynopsis);
            _printHelpLine("SetGpo", _settings.SetGpoDescription, _settings.SetGpoSynopsis);
            _printHelpLine("UpdateTag", _settings.UpdateTagDescription, _settings.UpdateTagSynopsis);
        }

        /// <summary>
        /// Helper function to format and write command help content.
        /// </summary>
        /// <param name="name">The name of the command.</param>
        /// <param name="synopsis">A conanical example of the command.</param>
        /// <param name="description">Verbose description of the command and its expected parameters.</param>
        private void _printHelpLine(string name, string synopsis, string description)
        {
            Console.WriteLine("{0}: {1}{2}  {3}{2}{2}", name, synopsis, Environment.NewLine, description);
        }

        #endregion

        #region event handlers

        private void _controller_Logging(object sender, LoggingEventArgs e)
        {
            _log(e.Entry);
        }

        #endregion

        #region command wrappers

        // command handlers wrap the controller's fulfillment methods, write out helper text to the console,
        // and handle any exceptions thrown by the controller.

        private void _connectHandler(string name)
        {
            try
            {
                _stdOut("Connecting...");
                _speedwayController.Connect(name);
                _stdOut("Connected to '{0}", name);
            }
            catch (OctaneSdkException ex)
            {
                Console.WriteLine("The following exception was thrown by the OctaneSdk Library: {0}", ex.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine("The following exception was thrown: {0}", ex.Message);
            }
        }

        private void _disconnectHandler()
        {
            try
            {
                _stdOut("Disconnecting...");
                _speedwayController.Disconnect();
                _stdOut("Disconnected");
            }
            catch (OctaneSdkException ex)
            {
                Console.WriteLine("The following exception was thrown by the OctaneSdk Library: {0}", ex.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine("The following exception was thrown: {0}", ex.Message);
            }
        }

        private void _loadFactorySettingsHandler()
        {
            try
            {
                _stdOut("Load Factory Settings...");
                _speedwayController.LoadFactorySettings();
                _stdOut("... reader's settings changed to factory defaults.");
            }
            catch (OctaneSdkException ex)
            {
                Console.WriteLine("The following exception was thrown by the OctaneSdk Library: {0}", ex.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine("The following exception was thrown: {0}", ex.Message);
            }
        }

        private void _loadSettingsHandler(string path)
        {
            try
            {
                _stdOut("Load Settings from {0}", path);
                _speedwayController.LoadSettings(path);
                _stdOut("... reader's settings loaded.");
            }
            catch (OctaneSdkException ex)
            {
                Console.WriteLine("The following exception was thrown by the OctaneSdk Library: {0}", ex.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine("The following exception was thrown: {0}", ex.Message);
            }
        }

        private void _logLevelHandler(string logLevel)
        {
            try
            {
                if (!string.IsNullOrEmpty(logLevel))
                {
                    var level = (LogLevel)Enum.Parse(typeof(LogLevel), logLevel, true);
                    _speedwayController.LogLevel = level;
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

        private void _queryFactorySettingsHandler(string path)
        {
            try
            {
                _stdOut("Query Factory Settings...");
                _speedwayController.QueryFactorySettings(path);
                _stdOut("... reader's factory settings query complete.");
            }
            catch (OctaneSdkException ex)
            {
                Console.WriteLine("The following exception was thrown by the OctaneSdk Library: {0}", ex.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine("The following exception was thrown: {0}", ex.Message);
            }
        }

        private void _queryFeatureSetHandler(string path)
        {
            try
            {
                _stdOut("Query Feature Set...");
                var featureSet = _speedwayController.QueryFeatureSet(path);

                Console.WriteLine("Model              {0}", featureSet.ModelName);
                Console.WriteLine("Software Version   {0}", featureSet.SoftwareVersion);
                Console.WriteLine("Firmware Version   {0}", featureSet.FirmwareVersion);
                Console.WriteLine("PCBA Version       {0}", featureSet.PcbaVersion);
                Console.WriteLine("FPGA Version       {0}", featureSet.FpgaVersion);
                Console.WriteLine("Regulatory Region   {0}", featureSet.Subregion);
            }
            catch (OctaneSdkException ex)
            {
                Console.WriteLine("The following exception was thrown by the OctaneSdk Library: {0}", ex.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine("The following exception was thrown: {0}", ex.Message);
            }
        }

        private void _queryStatusHandler(StatusRefresh refresh, string path)
        {
            try
            {
                _stdOut("Query Status...");
                var status = _speedwayController.QueryStatus(refresh, path);

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
                Console.WriteLine("The following exception was thrown by the OctaneSdk Library: {0}", ex.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine("The following exception was thrown: {0}", ex.Message);
            }
        }

        private void _queryTagsHandler(double duration, string path)
        {
            try
            {
                _stdOut("Querying tags for {0} seconds...", duration);
                var tagReport = _speedwayController.QueryTags(duration, path);
                var count = 1;
                
                foreach (var tag in tagReport.Tags)
                {
                    Console.WriteLine("{0,-3}: Found tag {1}", count++, tag.Epc);
                }
            }
            catch (OctaneSdkException ex)
            {
                Console.WriteLine("The following exception was thrown by the OctaneSdk Library: {0}", ex.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine("The following exception was thrown: {0}", ex.Message);
            }
        }

        private void _saveSettingsHandler(string path)
        {
            try
            {
                _stdOut("Saving settings to {0}...", path);
                _speedwayController.SaveSettings(path);
                _stdOut("... settings saved.");
            }
            catch (OctaneSdkException ex)
            {
                Console.WriteLine("The following exception was thrown by the OctaneSdk Library: {0}", ex.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine("The following exception was thrown: {0}", ex.Message);
            }
        }

        private void _setGpoHandler(int port, bool newLevel)
        {
            try
            {
                _stdOut("Setting port {0} to {1}...", port, newLevel);
                _speedwayController.SetGpo(port, newLevel);
                _stdOut("... set GPO complete.");
            }
            catch (OctaneSdkException ex)
            {
                Console.WriteLine("The following exception was thrown by the OctaneSdk Library: {0}", ex.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine("The following exception was thrown: {0}", ex.Message);
            }
        }

        private void _updateTagHandler(string match, string newEpc)
        {
            try
            {
                _stdOut("Updating tag {0} to {1}...", match, newEpc);
                var accessResult = _speedwayController.UpdateTag(match, newEpc);
                if (AccessResult.Success == accessResult)
                {
                    _stdOut("... tag with epc {0} updated to {1}.", match, newEpc);
                }
                else
                {
                    _stdOut("... tag with epc {0} was not found.", match);
                }
            }
            catch (OctaneSdkException ex)
            {
                Console.WriteLine("The following exception was thrown by the OctaneSdk Library: {0}", ex.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine("The following exception was thrown: {0}", ex.Message);
            }
        }

        #endregion
    }
}

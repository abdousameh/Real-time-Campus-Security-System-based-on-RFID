Imports System.Collections.Generic
Imports System.Linq
Imports System.Text
Imports Impinj.OctaneSdk

Module Module1

    ''' <summary>
    ''' Static entry point for a Console application.
    ''' </summary>
    ''' <param name="args">Command line arguements.</param>
    Sub Main(ByVal args As String())
        ' instantiate the program class
        Dim program = New Program()
        ' ... and start it
        program.Start()
    End Sub

    Public Class Program
#Region "public methods"

        ''' <summary>
        ''' Constructor.
        ''' </summary>
        Public Sub New()
            ' have to instantiate the controller
            ' it wraps the SpeedwayReader class
            _speedwayController = New SpeedwayController()
            '_speedwayController.Logging += New EventHandler(Of LoggingEventArgs)(AddressOf _controller_Logging)
        End Sub

        ''' <summary>
        ''' This is the command processor. Each time a new line is typed into the
        ''' console, it is processed by this method.
        ''' </summary>
        ''' <param name="userInput">The command, and arguments, the user entered.</param>
        ''' <returns>Whether or not the user input was processed correctly.</returns>
        Public Function ProcessCommand(ByVal userInput As String) As Boolean
            ' the return value, assume it is going to work
            Dim success = True

            Try
                ' if there is a value to process
                If Not String.IsNullOrEmpty(userInput) Then
                    ' parse out the command name
                    Dim commandName = _parseCommand(userInput)
                    ' treat the remaining values as arguments
                    Dim args = _parseArgs(userInput)

                    ' try and parse the command
                    Dim command As SpeedwayCommand = DirectCast([Enum].Parse(GetType(SpeedwayCommand), commandName, True), SpeedwayCommand)
                    ' then pass it off the the command router where it will be fulfilled
                    _process(command, args)
                End If
                ' they probably just hit enter, so reprint console prompt (do nothing)
            Catch ex As ArgumentNullException
            Catch ex As ArgumentException
                ' if there was a bad argument, or the command was not parsed correctly
                ' print out a helper message, and include the exception's message
                _stdOut("The command '{0}' was not found, an argument was invalid, or an argument was missing ({1})", userInput, ex.Message)
                success = False
            End Try

            Return success
        End Function

        ''' <summary>
        ''' After commands are successfully parsed, this method is called where the arguments
        ''' are formed up and then passed to the controller, where they are fulfilled. 
        ''' </summary>
        ''' <param name="command">The command to execute.</param>
        ''' <param name="args">The args that were on the command line.</param>
        Private Sub _process(ByVal command As SpeedwayCommand, ByVal args As String())
            Dim path As String = Nothing

            Select Case command
                Case SpeedwayCommand.Connect
                    _connectHandler(args(0))
                    Exit Select
                Case SpeedwayCommand.Disconnect
                    _disconnectHandler()
                    Exit Select
                Case SpeedwayCommand.LoadFactorySettings
                    _loadFactorySettingsHandler()
                    Exit Select
                Case SpeedwayCommand.LoadSettings
                    _loadSettingsHandler(args(0))
                    Exit Select
                Case SpeedwayCommand.LogLevel
                    _logLevelHandler(args(0))
                    Exit Select
                Case SpeedwayCommand.QueryFactorySettings
                    ' set path to null if there was no argument passed in
                    path = If((args.Length = 0), args(0), Nothing)
                    _queryFactorySettingsHandler(path)
                    Exit Select
                Case SpeedwayCommand.QueryFeatureSet
                    ' set path to null if there was no argument passed in
                    path = If((args.Length = 0), Nothing, args(0))
                    _queryFeatureSetHandler(path)
                    Exit Select
                Case SpeedwayCommand.QueryStatus
                    ' the first argument must match a status refresh value
                    If args.Length > 0 Then
                        Dim refresh = DirectCast([Enum].Parse(GetType(StatusRefresh), args(0), True), StatusRefresh)
                        ' set path to null if there was no second argument passed in
                        path = If((args.Length = 2), args(1), Nothing)
                        _queryStatusHandler(refresh, path)
                    Else
                        Throw New ArgumentException("Must have a status refresh type, try 'None', 'Everything', 'JustGpis', or 'JustAntennas'.")
                    End If
                    Exit Select
                Case SpeedwayCommand.QueryTags
                    ' first argument must be a double
                    Dim duration = Double.Parse(args(0))
                    ' set path to null if there was no second argument passed in
                    path = If((args.Length = 2), args(1), Nothing)
                    _queryTagsHandler(duration, path)
                    Exit Select
                Case SpeedwayCommand.SaveSettings
                    _saveSettingsHandler(args(0))
                    Exit Select
                Case SpeedwayCommand.SetGpo
                    ' first argument must be an integer
                    Dim portNumber = Integer.Parse(args(0).ToString())
                    ' second argument must be a boolean
                    Dim level = Boolean.Parse(args(1).ToString())
                    _speedwayController.SetGpo(portNumber, level)
                    Exit Select
                Case SpeedwayCommand.UpdateTag
                    If 2 = args.Length Then
                        _updateTagHandler(args(0), args(1))
                    Else
                        Throw New ArgumentException("Must have a target epc and the new epc's value")
                    End If
                    Exit Select
                Case Else
                    Exit Select
            End Select
        End Sub

#End Region

#Region "properties"

        ''' <summary>
        ''' This is what is printed at the beginning of each line
        ''' </summary>
        Public Const Prompt As String = ":> "
        ''' <summary>
        ''' Wraps the SpeedwayReader class, used to fulfill commands.
        ''' </summary>
        Private _speedwayController As SpeedwayController

#End Region

#Region "private methods"

        ''' <summary>
        ''' Formats and writes out the passed in log entry.
        ''' </summary>
        ''' <param name="logEntry">The log entry to format.</param>
        Private Sub _log(ByVal logEntry As LogEntry)
            Dim message = String.Format("{0}" & vbTab & "{1}" & vbTab & "{2}", logEntry.Origin, logEntry.Level, logEntry.Message)

            _stdOut(message)
        End Sub

        ''' <summary>
        ''' This is the entry point for the Program class. It loops until the command "quit" is entered.
        ''' </summary>
        Sub Start()
            Dim userInput = String.Empty

            While Not userInput.ToLower().Equals("quit")
                Console.Write(Prompt)
                userInput = Console.ReadLine()
                ProcessCommand(userInput)
            End While
        End Sub

        ''' <summary>
        ''' Strips off and passes back what is before the first space.
        ''' </summary>
        ''' <param name="userInput">The command line the user entered.</param>
        ''' <returns>The first word of the line.</returns>
        Private Function _parseCommand(ByVal userInput As String) As String
            Dim command = String.Empty

            If Not String.IsNullOrEmpty(userInput) Then
                Dim array = userInput.Split(" "c)
                command = array(0)
            End If

            Return command
        End Function

        ''' <summary>
        ''' Removed the command and passed back the rest of the user input as
        ''' a string array that is split on spaces.
        ''' </summary>
        ''' <param name="userInput">The command line the user entered.</param>
        ''' <returns>An array of args.</returns>
        Private Function _parseArgs(ByVal userInput As String) As String()
            Dim args = New String(-1) {}

            If Not String.IsNullOrEmpty(userInput) Then
                Dim list = userInput.Split(" "c).ToList()
                ' pluck the first one out of the list,
                ' it is the command
                list.Remove(list(0))
                args = list.ToArray()
            End If

            Return args
        End Function

        ''' <summary>
        ''' Helper function used to write progress to the console.
        ''' </summary>
        ''' <param name="message">The message to write.</param>
        ''' <param name="args">Optional parameters to use in case the message has indexes.</param>
        Private Sub _stdOut(ByVal message As String, ByVal ParamArray args As Object())
            Console.WriteLine(message, args)
        End Sub

        ''' <summary>
        ''' Helper function to format and write command help content.
        ''' </summary>
        ''' <param name="name">The name of the command.</param>
        ''' <param name="synopsis">A conanical example of the command.</param>
        ''' <param name="description">Verbose description of the command and its expected parameters.</param>
        Private Sub _printHelpLine(ByVal name As String, ByVal synopsis As String, ByVal description As String)
            Console.WriteLine("{0}: {1}{2}  {3}{2}{2}", name, synopsis, Environment.NewLine, description)
        End Sub

#End Region

#Region "event handlers"

        Private Sub _controller_Logging(ByVal sender As Object, ByVal e As LoggingEventArgs)
            _log(e.Entry)
        End Sub

#End Region

#Region "command wrappers"

        ' command handlers wrap the controller's fulfillment methods, write out helper text to the console,
        ' and handle any exceptions thrown by the controller.

        Private Sub _connectHandler(ByVal name As String)
            Try
                _stdOut("Connecting...")
                _speedwayController.Connect(name)
                _stdOut("Connected to '{0}", name)
            Catch ex As OctaneSdkException
                Console.WriteLine("The following exception was thrown by the OctaneSdk Library: {0}", ex.Message)
            Catch ex As Exception
                Console.WriteLine("The following exception was thrown: {0}", ex.Message)
            End Try
        End Sub

        Private Sub _disconnectHandler()
            Try
                _stdOut("Disconnecting...")
                _speedwayController.Disconnect()
                _stdOut("Disconnected")
            Catch ex As OctaneSdkException
                Console.WriteLine("The following exception was thrown by the OctaneSdk Library: {0}", ex.Message)
            Catch ex As Exception
                Console.WriteLine("The following exception was thrown: {0}", ex.Message)
            End Try
        End Sub

        Private Sub _loadFactorySettingsHandler()
            Try
                _stdOut("Load Factory Settings...")
                _speedwayController.LoadFactorySettings()
                _stdOut("... reader's settings changed to factory defaults.")
            Catch ex As OctaneSdkException
                Console.WriteLine("The following exception was thrown by the OctaneSdk Library: {0}", ex.Message)
            Catch ex As Exception
                Console.WriteLine("The following exception was thrown: {0}", ex.Message)
            End Try
        End Sub

        Private Sub _loadSettingsHandler(ByVal path As String)
            Try
                _stdOut("Load Settings from {0}", path)
                _speedwayController.LoadSettings(path)
                _stdOut("... reader's settings loaded.")
            Catch ex As OctaneSdkException
                Console.WriteLine("The following exception was thrown by the OctaneSdk Library: {0}", ex.Message)
            Catch ex As Exception
                Console.WriteLine("The following exception was thrown: {0}", ex.Message)
            End Try
        End Sub

        Private Sub _logLevelHandler(ByVal logLevel As String)
            Try
                If Not String.IsNullOrEmpty(logLevel) Then
                    Dim level = DirectCast([Enum].Parse(GetType(LogLevel), logLevel, True), LogLevel)
                    _speedwayController.LogLevel = level
                Else
                End If
                ' they probably just hit enter, so reprint console prompt (do nothing)
            Catch ex As ArgumentNullException
            Catch ex As ArgumentException
                Dim message = String.Format("The log level '{0}' was not found", logLevel)
                _stdOut(message)
            End Try
        End Sub

        Private Sub _queryFactorySettingsHandler(ByVal path As String)
            Try
                _stdOut("Query Factory Settings...")
                _speedwayController.QueryFactorySettings(path)
                _stdOut("... reader's factory settings query complete.")
            Catch ex As OctaneSdkException
                Console.WriteLine("The following exception was thrown by the OctaneSdk Library: {0}", ex.Message)
            Catch ex As Exception
                Console.WriteLine("The following exception was thrown: {0}", ex.Message)
            End Try
        End Sub

        Private Sub _queryFeatureSetHandler(ByVal path As String)
            Try
                _stdOut("Query Feature Set...")
                Dim featureSet = _speedwayController.QueryFeatureSet(path)

                Console.WriteLine("Model              {0}", featureSet.ModelName)
                Console.WriteLine("Software Version   {0}", featureSet.SoftwareVersion)
                Console.WriteLine("Firmware Version   {0}", featureSet.FirmwareVersion)
                Console.WriteLine("PCBA Version       {0}", featureSet.PcbaVersion)
                Console.WriteLine("FPGA Version       {0}", featureSet.FpgaVersion)
                Console.WriteLine("Regulatory Region   {0}", featureSet.Subregion)
            Catch ex As OctaneSdkException
                Console.WriteLine("The following exception was thrown by the OctaneSdk Library: {0}", ex.Message)
            Catch ex As Exception
                Console.WriteLine("The following exception was thrown: {0}", ex.Message)
            End Try
        End Sub

        Private Sub _queryStatusHandler(ByVal refresh As StatusRefresh, ByVal path As String)
            Try
                _stdOut("Query Status...")
                Dim status = _speedwayController.QueryStatus(refresh, path)

                Console.Write("Antennas           ")
                For Each antStat As AntennaStatus In status.Antennas
                    Dim stateStr As String = [String].Empty

                    Select Case antStat.State
                        ' The port present on the reader and connected
                        ' to an antenna.
                        Case AntennaConnectionState.Connected
                            stateStr = "Connected"
                            Exit Select

                            ' The port is present on the reader but is not
                            ' connected to an antenna, or the connection is bad.
                        Case AntennaConnectionState.NotConnected
                            stateStr = "Unconnected"
                            Exit Select

                            ' No such port on the reader.
                        Case AntennaConnectionState.NotApplicable
                            stateStr = "N/A"
                            Exit Select

                            ' Unknown state. This usually means something
                            ' went wrong communicating with the reader.
                        Case AntennaConnectionState.Unknown
                            stateStr = "Unknown"
                            Exit Select
                        Case Else

                            ' Should never happen.
                            stateStr = "??"
                            Exit Select
                    End Select
                    Console.Write(" {0}:{1}", antStat.PortNumber, stateStr)
                Next
                Console.WriteLine()
            Catch ex As OctaneSdkException
                Console.WriteLine("The following exception was thrown by the OctaneSdk Library: {0}", ex.Message)
            Catch ex As Exception
                Console.WriteLine("The following exception was thrown: {0}", ex.Message)
            End Try
        End Sub

        Private Sub _queryTagsHandler(ByVal duration As Double, ByVal path As String)
            Try
                _stdOut("Querying tags for {0} seconds...", duration)
                Dim tagReport = _speedwayController.QueryTags(duration, path)
                Dim count = 1

                For Each tag As Tag In tagReport.Tags
                    Console.WriteLine("{0,-3}: Found tag {1}", System.Math.Max(System.Threading.Interlocked.Increment(count), count - 1), tag.Epc)
                Next
            Catch ex As OctaneSdkException
                Console.WriteLine("The following exception was thrown by the OctaneSdk Library: {0}", ex.Message)
            Catch ex As Exception
                Console.WriteLine("The following exception was thrown: {0}", ex.Message)
            End Try
        End Sub

        Private Sub _saveSettingsHandler(ByVal path As String)
            Try
                _stdOut("Saving settings to {0}...", path)
                _speedwayController.SaveSettings(path)
                _stdOut("... settings saved.")
            Catch ex As OctaneSdkException
                Console.WriteLine("The following exception was thrown by the OctaneSdk Library: {0}", ex.Message)
            Catch ex As Exception
                Console.WriteLine("The following exception was thrown: {0}", ex.Message)
            End Try
        End Sub

        Private Sub _setGpoHandler(ByVal port As Integer, ByVal newLevel As Boolean)
            Try
                _stdOut("Setting port {0} to {1}...", port, newLevel)
                _speedwayController.SetGpo(port, newLevel)
                _stdOut("... set GPO complete.")
            Catch ex As OctaneSdkException
                Console.WriteLine("The following exception was thrown by the OctaneSdk Library: {0}", ex.Message)
            Catch ex As Exception
                Console.WriteLine("The following exception was thrown: {0}", ex.Message)
            End Try
        End Sub

        Private Sub _updateTagHandler(ByVal match As String, ByVal newEpc As String)
            Try
                _stdOut("Updating tag {0} to {1}...", match, newEpc)
                Dim accessResult__1 = _speedwayController.UpdateTag(match, newEpc)
                If AccessResult.Success = accessResult__1 Then
                    _stdOut("... tag with epc {0} updated to {1}.", match, newEpc)
                Else
                    _stdOut("... tag with epc {0} was not found.", match)
                End If
            Catch ex As OctaneSdkException
                Console.WriteLine("The following exception was thrown by the OctaneSdk Library: {0}", ex.Message)
            Catch ex As Exception
                Console.WriteLine("The following exception was thrown: {0}", ex.Message)
            End Try
        End Sub

#End Region
    End Class
End Module

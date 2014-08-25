Imports System.Collections.Generic
Imports System.Linq
Imports System.Text
Imports Impinj.OctaneSdk
Imports System.Threading
Imports Impinj.OctaneSdk.Internals

Public Class SpeedwayController
    ''' <summary>
    ''' Constructor.
    ''' </summary>
    Public Sub New()
        _speedwayReader = New SpeedwayReader()
        _speedwayReader.LogLevel = LogLevel.Information

        ' wire up all the events to their delegates
        ' the delegates re-fire the events
        '_speedwayReader.AntennaChanged += New EventHandler(Of AntennaChangedEventArgs)(AddressOf _speedwayReader_AntennaChanged)
        '_speedwayReader.Connected += New EventHandler(Of ConnectionChangedEventArgs)(AddressOf _speedwayReader_Connected)
        '_speedwayReader.ConnectionLost += New EventHandler(Of ConnectionChangedEventArgs)(AddressOf _speedwayReader_ConnectionLost)
        '_speedwayReader.Disconnected += New EventHandler(Of ConnectionChangedEventArgs)(AddressOf _speedwayReader_Disconnected)
        '_speedwayReader.Logging += New EventHandler(Of LoggingEventArgs)(AddressOf _speedwayReader_Logging)
        '_speedwayReader.Started += New EventHandler(Of StartedEventArgs)(AddressOf _speedwayReader_Started)
        '_speedwayReader.Stopped += New EventHandler(Of StoppedEventArgs)(AddressOf _speedwayReader_Stopped)
        '_speedwayReader.TagsReported += New EventHandler(Of TagsReportedEventArgs)(AddressOf _speedwayReader_TagsReported)

        ' each time a tag is read it is stored in this dictionary
        _tagDb = New Dictionary(Of String, List(Of Tag))()
    End Sub

#Region "properties"

    Private _speedwayReader As SpeedwayReader

    ''' <summary>
    ''' The last loaded setting is saved in this property. If it is null, it means settings have
    ''' not been loaded on the reader.
    ''' </summary>
    Private _settings As Settings

    ''' <summary>
    ''' Wrapper for log level.
    ''' </summary>
    Public Property LogLevel() As LogLevel
        Get
            Return _speedwayReader.LogLevel
        End Get
        Set(ByVal value As LogLevel)
            _speedwayReader.LogLevel = value
        End Set
    End Property

#End Region

#Region "events and event handlers"

    ' these events expose reader events to the owner of the controller

    Public Event AntennaChanged As EventHandler(Of AntennaChangedEventArgs)
    Public Event Connected As EventHandler(Of ConnectionChangedEventArgs)
    Public Event ConnectionLost As EventHandler(Of ConnectionChangedEventArgs)
    Public Event Disconnected As EventHandler(Of ConnectionChangedEventArgs)
    Public Event Logging As EventHandler(Of LoggingEventArgs)
    Public Event Started As EventHandler(Of StartedEventArgs)
    Public Event Stopped As EventHandler(Of StoppedEventArgs)
    Public Event TagReported As EventHandler(Of TagsReportedEventArgs)

    ' these delegates re-fire the events from the reader, routing them
    ' to the controller's events

    Private Sub _speedwayReader_AntennaChanged(ByVal sender As Object, ByVal e As AntennaChangedEventArgs)
        RaiseEvent AntennaChanged(sender, e)
    End Sub

    Private Sub _speedwayReader_Connected(ByVal sender As Object, ByVal e As ConnectionChangedEventArgs)
        RaiseEvent Connected(sender, e)
    End Sub

    Private Sub _speedwayReader_ConnectionLost(ByVal sender As Object, ByVal e As ConnectionChangedEventArgs)
        RaiseEvent ConnectionLost(sender, e)
    End Sub

    Private Sub _speedwayReader_Disconnected(ByVal sender As Object, ByVal e As ConnectionChangedEventArgs)
        RaiseEvent Disconnected(sender, e)
    End Sub

    Private Sub _speedwayReader_Logging(ByVal sender As Object, ByVal e As LoggingEventArgs)
        RaiseEvent Logging(sender, e)
    End Sub

    Private Sub _speedwayReader_Started(ByVal sender As Object, ByVal e As StartedEventArgs)
        RaiseEvent Started(sender, e)
    End Sub

    Private Sub _speedwayReader_Stopped(ByVal sender As Object, ByVal e As StoppedEventArgs)
        RaiseEvent Stopped(sender, e)
    End Sub

    Private Sub _speedwayReader_TagsReported(ByVal sender As Object, ByVal e As TagsReportedEventArgs)
        RaiseEvent TagReported(sender, e)

        SyncLock _tagDb
            For Each tag As Tag In e.TagReport.Tags
                Dim list As List(Of Tag) = Nothing
                If _tagDb.TryGetValue(tag.Epc, list) Then
                    list.Add(tag)
                Else
                    list = New List(Of Tag)()
                    list.Add(tag)
                    _tagDb.Add(tag.Epc, list)
                End If
            Next
        End SyncLock
    End Sub

#End Region

    ''' <summary>
    ''' Storage for tags as they are read.
    ''' </summary>
    Private _tagDb As Dictionary(Of String, List(Of Tag))

    ''' <summary>
    ''' Connect to reader.
    ''' </summary>
    ''' <param name="readerName"></param>
    Public Sub Connect(ByVal readerName As String)
        _speedwayReader.Connect(readerName)
    End Sub

    ''' <summary>
    ''' Disconnect from connected reader.
    ''' </summary>
    Public Sub Disconnect()
        _speedwayReader.Disconnect()
    End Sub

    ''' <summary>
    ''' Loads the factory (default) settings on the reader.
    ''' </summary>
    Public Sub LoadFactorySettings()
        Dim settings = _speedwayReader.QueryFactorySettings()
        _speedwayReader.ClearSettings()
        _speedwayReader.ApplySettings(settings)
        _settings = settings
    End Sub

    ''' <summary>
    ''' Attempts to open the specified path as a settings file and loads
    ''' them onto the reader.
    ''' </summary>
    ''' <param name="path">Path the the settings file to load onto the reader.</param>
    Friend Sub LoadSettings(ByVal path As String)
        Dim settings = DirectCast(XmlHelper.InstantiateFromXmlFile(path, GetType(Settings)), Settings)
        _speedwayReader.ClearSettings()
        _speedwayReader.ApplySettings(settings)
        _settings = settings
    End Sub

    ''' <summary>
    ''' Queries the reader for its factory settings.
    ''' </summary>
    ''' <param name="path">Path where to save the settings file.</param>
    ''' <returns>The settings object.</returns>
    Public Function QueryFactorySettings(ByVal path As String) As Settings
        Dim settings = _speedwayReader.QueryFactorySettings()

        ' save to the path that was passed in, if there is one
        If path IsNot Nothing Then
            XmlHelper.SaveToXmlFile(path, settings)
        End If

        Return settings
    End Function

    ''' <summary>
    ''' Queries the reader for its feature set.
    ''' </summary>
    ''' <param name="path">Path where to save the feature set.</param>
    ''' <returns>The reader's feature set.</returns>
    Public Function QueryFeatureSet(ByVal path As String) As FeatureSet
        Dim featureSet = _speedwayReader.QueryFeatureSet()

        ' save to the path that was passed in, if there is one
        If path IsNot Nothing Then
            XmlHelper.SaveToXmlFile(path, featureSet)
        End If

        Return featureSet
    End Function

    ''' <summary>
    ''' Queries the reader for its status.
    ''' </summary>
    ''' <param name="refresh">The type of status query to perform.</param>
    ''' <param name="path">Path where to save the status.</param>
    ''' <returns>The reader's status.</returns>
    Friend Function QueryStatus(ByVal refresh As StatusRefresh, ByVal path As String) As Status
        Dim status = _speedwayReader.QueryStatus(refresh)

        ' save to the path that was passed in, if there is one
        If path IsNot Nothing Then
            XmlHelper.SaveToXmlFile(path, status)
        End If

        Return status
    End Function

    ''' <summary>
    ''' Queries the reader for tags in its field of view.
    ''' </summary>
    ''' <param name="duration">Number of seconds for the reader to singulate.</param>
    ''' <param name="path">Path where to save the tag report.</param>
    ''' <returns>A tag report containing the tags.</returns>
    Friend Function QueryTags(ByVal duration As Double, ByVal path As String) As TagReport
        ' make sure that settings have been set on the reader
        If _settings Is Nothing Then
            ' have to have settings on the reader, so use factory
            ' since none have been set
            LoadFactorySettings()
        End If

        Dim tagReport = _speedwayReader.QueryTags(duration)

        ' save to the path that was passed in, if there is one
        If path IsNot Nothing Then
            XmlHelper.SaveToXmlFile(path, tagReport)
        End If

        Return tagReport
    End Function

    ''' <summary>
    ''' Saves the last known settings to the specified path.
    ''' </summary>
    ''' <param name="path">Path where to save the settings.</param>
    Public Sub SaveSettings(ByVal path As String)
        If _settings IsNot Nothing Then
            If Not String.IsNullOrEmpty(path) Then
                XmlHelper.SaveToXmlFile(path, _settings)
            Else
                Throw New Exception("The path was missing")
            End If
        Else
            Throw New Exception("No settings have been set")
        End If
    End Sub

    ''' <summary>
    ''' Controls the GPOs on the reader.
    ''' </summary>
    ''' <param name="portNumber">The port number to set.</param>
    ''' <param name="level">The new level to set the GPO port.</param>
    Public Sub SetGpo(ByVal portNumber As Integer, ByVal level As Boolean)
        _speedwayReader.SetGpo(portNumber, level)
    End Sub

    ''' <summary>
    ''' Searches for the match tag, and if found, updates its EPC with the new value.
    ''' </summary>
    ''' <param name="match">Look for a tag with this EPC.</param>
    ''' <param name="newEpc">Update a found tag with this value.</param>
    ''' <returns>The result of the operation.</returns>
    Public Function UpdateTag(ByVal match As String, ByVal newEpc As String) As AccessResult
        Dim programEpcParams As New ProgramEpcParams()
        Dim programEpcResult As ProgramEpcResult

        programEpcParams.TargetTag = match
        programEpcParams.AccessPassword = Nothing
        programEpcParams.TimeoutInMs = 5000
        programEpcParams.AntennaPortNumber = 1
        programEpcParams.NewEpc = newEpc
        programEpcParams.IsWriteVerified = True
        programEpcParams.LockType = LockType.Locked

        programEpcResult = _speedwayReader.ProgramEpc(programEpcParams)

        Return programEpcResult.Result
    End Function
End Class

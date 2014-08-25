using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Impinj.OctaneSdk;
using System.Threading;
using Octane_ViewModel;


namespace Template_Cons_CS
{
    public class SpeedwayController
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        public SpeedwayController()
        {
            _speedwayReader = new SpeedwayReader();
            _speedwayReader.LogLevel = LogLevel.Information;

            // wire up all the events to their delegates
            // the delegates re-fire the events
            _speedwayReader.AntennaChanged += new EventHandler<AntennaChangedEventArgs>(_speedwayReader_AntennaChanged);
            _speedwayReader.Connected += new EventHandler<ConnectionChangedEventArgs>(_speedwayReader_Connected);
            _speedwayReader.ConnectionLost += new EventHandler<ConnectionChangedEventArgs>(_speedwayReader_ConnectionLost);
            _speedwayReader.Disconnected += new EventHandler<ConnectionChangedEventArgs>(_speedwayReader_Disconnected);
            _speedwayReader.Logging += new EventHandler<LoggingEventArgs>(_speedwayReader_Logging);
            _speedwayReader.Started += new EventHandler<StartedEventArgs>(_speedwayReader_Started);
            _speedwayReader.Stopped += new EventHandler<StoppedEventArgs>(_speedwayReader_Stopped);
            _speedwayReader.TagsReported += new EventHandler<TagsReportedEventArgs>(_speedwayReader_TagsReported);

            // each time a tag is read it is stored in this dictionary
            _tagDb = new Dictionary<string, List<Tag>>();
        }

        #region properties

        private SpeedwayReader _speedwayReader;

        /// <summary>
        /// The last loaded setting is saved in this property. If it is null, it means settings have
        /// not been loaded on the reader.
        /// </summary>
        private Settings _settings;

        /// <summary>
        /// Wrapper for log level.
        /// </summary>
        public LogLevel LogLevel
        {
            get { return _speedwayReader.LogLevel; }
            set { _speedwayReader.LogLevel = value; }
        }

        #endregion

        #region events and event handlers

        // these events expose reader events to the owner of the controller

        public event EventHandler<AntennaChangedEventArgs> AntennaChanged;
        public event EventHandler<ConnectionChangedEventArgs> Connected;
        public event EventHandler<ConnectionChangedEventArgs> ConnectionLost;
        public event EventHandler<ConnectionChangedEventArgs> Disconnected;
        public event EventHandler<LoggingEventArgs> Logging;
        public event EventHandler<StartedEventArgs> Started;
        public event EventHandler<StoppedEventArgs> Stopped;
        public event EventHandler<TagsReportedEventArgs> TagReported;

        // these delegates re-fire the events from the reader, routing them
        // to the controller's events

        void _speedwayReader_AntennaChanged(object sender, AntennaChangedEventArgs e)
        {
            if (null != AntennaChanged)
                AntennaChanged(sender, e);
        }

        void _speedwayReader_Connected(object sender, ConnectionChangedEventArgs e)
        {
            if (null != Connected)
                Connected(sender, e);
        }

        void _speedwayReader_ConnectionLost(object sender, ConnectionChangedEventArgs e)
        {
            if (null != ConnectionLost)
                ConnectionLost(sender, e);
        }

        void _speedwayReader_Disconnected(object sender, ConnectionChangedEventArgs e)
        {
            if (null != Disconnected)
                Disconnected(sender, e);
        }

        void _speedwayReader_Logging(object sender, LoggingEventArgs e)
        {
            if (null != Logging)
                Logging(sender, e);
        }

        void _speedwayReader_Started(object sender, StartedEventArgs e)
        {
            if (null != Started)
                Started(sender, e);
        }

        void _speedwayReader_Stopped(object sender, StoppedEventArgs e)
        {
            if (null != Stopped)
                Stopped(sender, e);
        }

        void _speedwayReader_TagsReported(object sender, TagsReportedEventArgs e)
        {
            if (null != TagReported)
                TagReported(sender, e);

            lock (_tagDb)
            {
                foreach (var tag in e.TagReport.Tags)
                {
                    List<Tag> list = null;
                    if (_tagDb.TryGetValue(tag.Epc, out list))
                    {
                        list.Add(tag);
                    }
                    else
                    {
                        list = new List<Tag>();
                        list.Add(tag);
                        _tagDb.Add(tag.Epc, list);
                    }
                }
            }
        }

        #endregion

        /// <summary>
        /// Storage for tags as they are read.
        /// </summary>
        private Dictionary<string, List<Tag>> _tagDb;

        /// <summary>
        /// Connect to reader.
        /// </summary>
        /// <param name="readerName"></param>
        public void Connect(string readerName)
        {
            _speedwayReader.Connect(readerName);
        }

        /// <summary>
        /// Disconnect from connected reader.
        /// </summary>
        public void Disconnect()
        {   
            _speedwayReader.Disconnect();
        }

        /// <summary>
        /// Loads the factory (default) settings on the reader.
        /// </summary>
        public void LoadFactorySettings()
        {
            var settings = _speedwayReader.QueryFactorySettings();
            _speedwayReader.ClearSettings();
            _speedwayReader.ApplySettings(settings);
            _settings = settings;
        }

        /// <summary>
        /// Attempts to open the specified path as a settings file and loads
        /// them onto the reader.
        /// </summary>
        /// <param name="path">Path the the settings file to load onto the reader.</param>
        internal void LoadSettings(string path)
        {
            var settings = Settings.Load(path);
            _speedwayReader.ClearSettings();
            _speedwayReader.ApplySettings(settings);
            _settings = settings;
        }

        /// <summary>
        /// Queries the reader for its factory settings.
        /// </summary>
        /// <param name="path">Path where to save the settings file.</param>
        /// <returns>The settings object.</returns>
        public Settings QueryFactorySettings(string path)
        {
            var settings = _speedwayReader.QueryFactorySettings();

            // save to the path that was passed in, if there is one
            if (null != path)
            {
                XmlHelper.SaveToXmlFile(path, settings);
            }

            return settings;
        }

        /// <summary>
        /// Queries the reader for its feature set.
        /// </summary>
        /// <param name="path">Path where to save the feature set.</param>
        /// <returns>The reader's feature set.</returns>
        public FeatureSet QueryFeatureSet(string path)
        {
            var featureSet = _speedwayReader.QueryFeatureSet();

            // save to the path that was passed in, if there is one
            if (null != path)
            {
                XmlHelper.SaveToXmlFile(path, featureSet);
            }

            return featureSet;
        }

        /// <summary>
        /// Queries the reader for its status.
        /// </summary>
        /// <param name="refresh">The type of status query to perform.</param>
        /// <param name="path">Path where to save the status.</param>
        /// <returns>The reader's status.</returns>
        internal Status QueryStatus(StatusRefresh refresh, string path)
        {
            var status = _speedwayReader.QueryStatus(refresh);

            // save to the path that was passed in, if there is one
            if (null != path)
            {
                XmlHelper.SaveToXmlFile(path, status);
            }

            return status;
        }

        /// <summary>
        /// Queries the reader for tags in its field of view.
        /// </summary>
        /// <param name="duration">Number of seconds for the reader to singulate.</param>
        /// <param name="path">Path where to save the tag report.</param>
        /// <returns>A tag report containing the tags.</returns>
        internal TagReport QueryTags(double duration, string path)
        {
            // make sure that settings have been set on the reader
            if (null == _settings)
            {
                // have to have settings on the reader, so use factory
                // since none have been set
                LoadFactorySettings();
            }

            var tagReport = _speedwayReader.QueryTags(duration);

            // save to the path that was passed in, if there is one
            if (null != path)
            {
                XmlHelper.SaveToXmlFile(path, tagReport);
            }

            return tagReport;
        }

        /// <summary>
        /// Saves the last known settings to the specified path.
        /// </summary>
        /// <param name="path">Path where to save the settings.</param>
        public void SaveSettings(string path)
        {
            if (null != _settings)
            {
                if (!string.IsNullOrEmpty(path))
                {
                    XmlHelper.SaveToXmlFile(path, _settings);
                }
                else
                {
                    throw new Exception("The path was missing");
                }
            }
            else
            {
                throw new Exception("No settings have been set");
            }
        }

        /// <summary>
        /// Controls the GPOs on the reader.
        /// </summary>
        /// <param name="portNumber">The port number to set.</param>
        /// <param name="level">The new level to set the GPO port.</param>
        public void SetGpo(int portNumber, bool level)
        {
            _speedwayReader.SetGpo(portNumber, level);
        }

        /// <summary>
        /// Searches for the match tag, and if found, updates its EPC with the new value.
        /// </summary>
        /// <param name="match">Look for a tag with this EPC.</param>
        /// <param name="newEpc">Update a found tag with this value.</param>
        /// <returns>The result of the operation.</returns>
        public AccessResult UpdateTag(string match, string newEpc)
        {
            ProgramEpcParams programEpcParams = new ProgramEpcParams();
            ProgramEpcResult programEpcResult;

            programEpcParams.TargetTag = match;
            programEpcParams.AccessPassword = null;
            programEpcParams.TimeoutInMs = 5000;
            programEpcParams.AntennaPortNumber = 1;
            programEpcParams.NewEpc = newEpc;
            programEpcParams.IsWriteVerified = true;
            programEpcParams.LockType = LockType.Locked;

            programEpcResult = _speedwayReader.ProgramEpc(programEpcParams);

            return programEpcResult.Result;
        }
    }
}

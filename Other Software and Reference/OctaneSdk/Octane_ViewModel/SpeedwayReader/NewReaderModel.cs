/*
#############################################################################
#                                                                           #
#              IMPINJ CONFIDENTIAL AND PROPRIETARY                          #
#                                                                           #
# This source code is the sole property of Impinj, Inc. Reproduction or     #
# utilization of this source code in whole or in part is forbidden without  #
# the prior written consent of Impinj, Inc.                                 #
#                                                                           #
# (c) Copyright Impinj, Inc. 2010. All rights reserved.                     #
#                                                                           #
#############################################################################
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.IO;
using System.Windows.Media;
using System.Xml.Serialization;
using Octane_ViewModel;


namespace Octane_ViewModel
{
    public class NewReaderModel : INotifyPropertyChanged
    {
        public NewReaderModel()
        {
            _color = Brushes.White;
            IsAutoConnected = true;
            IsMemorable = true;
            IsIncluded = true;  
        }

        public string ReaderName { get; set; }
        public string ReaderIdentity { get; set; }
        public string SettingsFileName { get; set; }
        public bool IsAutoConnected { get; set; }
        public bool IsMemorable { get; set; }
        public bool IsIncluded { get; set; }

        [XmlIgnore()]
        private Brush _color;

        [XmlIgnore()]
        public Brush Color
        {
            get { return _color; }
            set
            {
                _color = value;
                OnPropertyChanged("Color");
            }
        }

        public string BrushColor
        {
            get
            {
                return Color.ToString();
            }
            set
            {
                var brushConverter = new BrushConverter();
                try
                {
                    Color = (Brush)brushConverter.ConvertFromString("#" + value);
                }
                catch
                {
                    try
                    {
                        Color = (Brush)brushConverter.ConvertFromString(value);
                    }
                    catch { }
                }
            }
        }

        private string _settingsFilePath;

        public string SettingsFilePath
        {
            get { return _settingsFilePath; }
            set
            {
                _settingsFilePath = value;
                if (File.Exists(value))
                {
                    var fileInfo = new FileInfo(SettingsFilePath);
                    SettingsFileName = fileInfo.Name;
                    OnPropertyChanged("SettingsFileName");
                }
            }
        }

        #region INotifyPropertyChanged Members

        public void OnPropertyChanged(string propertyName)
        {
            if (null != PropertyChanged)
            {
                var propertyChangedEventArgs = new PropertyChangedEventArgs(propertyName);
                PropertyChanged(this, propertyChangedEventArgs);
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion

        internal static List<NewReaderModel> LoadFromPath(string path)
        {
            var list = new List<NewReaderModel>();

            var obj = XmlHelper.InstantiateFromXmlFile(path, typeof(List<NewReaderModel>));
            if (null != obj)
            {
                var tryList = (List<NewReaderModel>)obj;
                if (null != tryList)
                    list = tryList;
            }

            return list;
        }

        /// <summary>
        /// Helper function that adds a new reader's model to the reader store, and
        /// saves it to disk.
        /// </summary>
        /// <param name="newReaderModel">The model to add and save.</param>
        /// <param name="path">The path to the store.</param>
        internal static void Save(NewReaderModel newReaderModel, string path)
        {
            bool alreadyPresent = false;

            var store = NewReaderModel.LoadFromPath(path);

            foreach (var s in store)
            {
                if (s.ReaderName.Equals(newReaderModel.ReaderName))
                {
                    s.Color = newReaderModel.Color;
                    s.IsAutoConnected = newReaderModel.IsAutoConnected;
                    s.ReaderIdentity = newReaderModel.ReaderIdentity;
                    s.SettingsFileName = newReaderModel.SettingsFileName;
                    s.SettingsFilePath = newReaderModel.SettingsFilePath;

                    alreadyPresent = true;
                    break;
                }
            }

            lock (store)
            {
                if (!alreadyPresent)
                {
                    store.Add(newReaderModel);
                }

                XmlHelper.SaveToXmlFile(path, store);
            }
        }

        public static NewReaderModel CraeteFromReader(SpeedwayReaderViewModel reader)
        {
            var newReaderModel = new NewReaderModel();
            newReaderModel.ReaderName = reader.ReaderName;
            newReaderModel.ReaderIdentity = reader.ReaderIdentity.ToString();
            newReaderModel.Color = reader.Color;

            return newReaderModel;
        }
    }
}

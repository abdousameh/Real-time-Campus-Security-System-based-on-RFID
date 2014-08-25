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
using System.Xml.Serialization;
using System.IO;
using System.Xml;

namespace Template_Wpf
{
    public class Utility
    {
        /// <summary>
        /// Opens a file dialog and passes back the user selected file.
        /// </summary>
        /// <returns>The full path to a file.</returns>
        public static string GetFilePath()
        {
            string path = null;
            var dialog = new Microsoft.Win32.OpenFileDialog();
            dialog.Multiselect = false;
            dialog.Filter = @"Xml Documents (*.xml)|*.xml" + "|Text Documents (*.txt)|*.txt" + "|All Documents (*.*)|*.*";

            var result = dialog.ShowDialog();

            if (result.Value)
            {
                path = dialog.FileName;
            }

            return path;
        }

        public static object InstantiateFromXmlString(string xml, Type type)
        {
            object result = null;

            try
            {
                // load from xml string
                var xmlSerializer = new XmlSerializer(type);
                var stringReader = new StringReader(xml);

                result = xmlSerializer.Deserialize(stringReader);
                stringReader.Close();
            }
            catch
            {
            }

            return result;
        }

        public static string DeserializeToXmlString(object toSerialize)
        {
            string xml = null;

            try
            {
                var type = toSerialize.GetType();
                var xmlSerializer = new XmlSerializer(type);
                var stringBuilder = new StringBuilder();
                var textWriter = new StringWriter(stringBuilder);
                xmlSerializer.Serialize(textWriter, toSerialize);
                textWriter.Close();

                xml = stringBuilder.ToString();
            }
            catch(Exception e)
            {
            }

            return xml;
        }
    }
}

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
using System.Text;
using System.IO;
using System.Xml.Serialization;

namespace Octane_ViewModel
{
    public class XmlHelper
    {
        public static object InstantiateFromXmlFile(string filePath, Type type)
        {
            object result = null;
            FileStream stream = null;

            try
            {
                // load from file
                if (File.Exists(filePath))
                {
                    var xmlSerializer = new XmlSerializer(type);
                    stream = new FileStream(filePath, FileMode.Open);

                    result = xmlSerializer.Deserialize(stream);
                }
            }
            catch
            {
                if (null != stream)
                    stream.Close();
            }
            finally
            {
                if (null != stream)
                    stream.Close();
            }

            return result;
        }

        public static bool SaveToXmlFile(string pathToSaveXml, object toSerialize)
        {
            bool ret = true;

            try
            {
                var type = toSerialize.GetType();
                var xmlSerializer = new XmlSerializer(type);
                var textWriter = new StreamWriter(pathToSaveXml);

                try
                {
                    xmlSerializer.Serialize(textWriter, toSerialize);
                }
                catch
                {
                    ret = false;
                }
                textWriter.Close();
            }
            catch
            {
                ret = false;
            }

            return ret;
        }
    }
}

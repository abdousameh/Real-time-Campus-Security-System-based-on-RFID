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
using System.Windows.Input;
using Impinj.OctaneSdk;
using System.ComponentModel;

namespace Octane_ViewModel
{
    public partial class SpeedwayReaderViewModel
    {
        public ICommand QueryTagsAsyncCommand { get; set; }

        private void _queryTagsAsyncExecute(object obj)
        {
            double d = 0;
            if (Double.TryParse(obj.ToString(), out d))
            {
                _log(LogLevel.Verbose, "QueryTagsAsync to '{0}'", ReaderName);

                var backgroundWorker = new BackgroundWorker();
                backgroundWorker.WorkerReportsProgress = true;
                backgroundWorker.WorkerSupportsCancellation = true;
                backgroundWorker.DoWork += new DoWorkEventHandler(_queryTagDoWork);
                backgroundWorker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(_queryTagRunWorkerCompleted);

                backgroundWorker.RunWorkerAsync(d);
            }
        }

        private bool _queryTagsAsyncCanExecute(object obj)
        {
            return true;
        }

        private void _queryTagRunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            StatusLabel = "Query Tags Completed";
        }

        private void _queryTagDoWork(object sender, DoWorkEventArgs e)
        {
            var d = Double.Parse(e.Argument.ToString());

            StatusLabel = "Querying Tags";

            try
            {
                var tags = QueryTags(d);

                var list = new List<TagWpf>();
                foreach (var tag in tags.Tags)
                {
                    var tagWpf = TagWpf.Convert(tag);
                    tagWpf.Timestamp = tags.Timestamp;
                    list.Add(tagWpf);
                }

                _mainWindowCallback.AddTags(list);
            }
            catch (Exception ex)
            {
                _log(LogLevel.Error, ex.Message);
            }
        }
    }
}

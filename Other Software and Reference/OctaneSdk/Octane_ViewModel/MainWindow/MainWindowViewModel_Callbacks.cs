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
using System.Threading;
using System.Windows.Threading;
using Impinj.OctaneSdk;

namespace Octane_ViewModel
{
    public partial class MainWindowViewModel
    {
        #region IMainWindowCallback Members


        public void AddTag(TagWpf tag)
        {
            _dispatcher.BeginInvoke(new TagReportedDelegate(_tagReportedHandler),
                DispatcherPriority.Background,
                new object[] { tag });
        }

        private int _tagRank = 0;
        private void _tagReportedHandler(TagWpf tag)
        {
            tag.Rank = _tagRank++;
            TagPaneViewModel.Insert(0, tag);

            if (TagPaneViewModel.Tags.Count > SettingsUI.Default.MaxTagCollectionCount)
            {
                var index = SettingsUI.Default.MaxTagCollectionCount - SettingsUI.Default.TagCollectionPruneCount;
                if (index > 0)
                {
                    for (; index < TagPaneViewModel.Tags.Count; index++)
                    {
                        TagPaneViewModel.Tags.RemoveAt(TagPaneViewModel.Tags.Count - 1);
                    }
                }
            }
        }

        public void AddTags(List<TagWpf> tags)
        {
            if (null != _dispatcher)
            {
                _dispatcher.BeginInvoke(new TagsReportedDelegate(_tagsReportedHandler),
                    DispatcherPriority.ContextIdle,
                    new object[] { tags });
            }

            if (null != TagsCreated)
                TagsCreated(tags, new EventArgs());
        }

        private void _tagsReportedHandler(List<TagWpf> tags)
        {
            foreach (var tag in tags)
            {
                TagPaneViewModel.Insert(0, tag);
            }

            if (TagPaneViewModel.Tags.Count > SettingsUI.Default.MaxTagCollectionCount)
            {
                var index = SettingsUI.Default.MaxTagCollectionCount - SettingsUI.Default.TagCollectionPruneCount;
                if (index > 0)
                {
                    for (; index < TagPaneViewModel.Tags.Count; index++)
                    {
                        TagPaneViewModel.Tags.RemoveAt(TagPaneViewModel.Tags.Count - 1);
                    }
                }
            }
        }

        public void ClearLogEntries()
        {
        }

        public void ClearTags()
        {
        }

        public void RemoveReader(string readerName)
        {
            SpeedwayReaderViewModel viewModel = null;

            foreach (var reader in Readers)
            {
                if (reader.ReaderName.Equals(readerName))
                {
                    viewModel = reader;
                    break;
                }
            }

            if (null != viewModel)
            {
                if (null != ReaderRemoved)
                    ReaderRemoved(viewModel, new EventArgs());

                viewModel.DisconnectAsyncCommand.Execute(null);
                Readers.Remove(viewModel);
                viewModel = null;
            }
        }

        public void StatusChangeNotification(string readerName, bool isConnected)
        {

        }

        #endregion
    }
}

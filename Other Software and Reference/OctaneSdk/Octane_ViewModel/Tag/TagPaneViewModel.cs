using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace Octane_ViewModel
{
    public class TagPaneViewModel : ViewModelBase
    {
        public TagPaneViewModel()
        {
            Tags = new ObservableCollection<TagWpf>();
            ClearTagsCommand = new DelegateCommand(_clearTagsExecute, _clearTagsCanExecute);
        }

        #region // Primary UI Display Properties

        public bool IsTagCountNonZero
        {
            get
            {
                return Tags.Count > 0;
            }
        }

        public ObservableCollection<TagWpf> Tags { get; set; }

        #endregion


        // Clear Tags Command

        public ICommand ClearTagsCommand { get; set; }

        private void _clearTagsExecute(object obj)
        {
            Tags.Clear();
            OnPropertyChanged("IsTagCountNonZero");
        }

        private bool _clearTagsCanExecute(object obj)
        {
            return true;
        }


        internal void Insert(int index, TagWpf tag)
        {
            Tags.Insert(index, tag);
            OnPropertyChanged("IsTagCountNonZero");
        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Octane_ViewModel;

namespace Template_WinForms
{
    public partial class SpeedwayReaderView : UserControl
    {
        public SpeedwayReaderView()
        {
            InitializeComponent();
        }

        private SpeedwayReaderViewModel _speedwayReaderViewModel;

        public SpeedwayReaderViewModel SpeedwayReaderViewModel
        {
            get { return _speedwayReaderViewModel; }
            set
            {
                _readerNameLabel.Text = value.ReaderName;
                _statusLabel.Text = value.StatusLabel;

                _speedwayReaderViewModel = value;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            _speedwayReaderViewModel.Close();
        }
    }
}

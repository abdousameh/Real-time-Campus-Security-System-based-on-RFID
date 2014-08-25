using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Impinj.OctaneSdk;
using System.Collections.ObjectModel;
using Octane_ViewModel;

namespace Template_WinForms
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();

            Load += new EventHandler(MainForm_Load);
            Application.ThreadExit += new EventHandler(Application_ThreadExit);
        }

        void Application_ThreadExit(object sender, EventArgs e)
        {
            _viewModel.SaveAutoStartList();
        }

        void MainForm_Load(object sender, EventArgs e)
        {
            _viewModel = new MainWindowViewModel();

            _viewModel.ReaderCreated += new EventHandler(_viewModel_ReaderCreated);
            _viewModel.ReaderRemoved += new EventHandler(_viewModel_ReaderRemoved);
            _viewModel.LogCreated += new EventHandler(_viewModel_LogCreated);
            _viewModel.TagsCreated += new EventHandler(_viewModel_TagCreated);

            _viewModel.Label = Environment.MachineName;

            var autoStartPath = "autoStartReaders.xml";
            _viewModel.LoadReaders(autoStartPath);
        }

        void _viewModel_ReaderRemoved(object sender, EventArgs e)
        {
            SpeedwayReaderView target = null;
            var viewModel = sender as SpeedwayReaderViewModel;

            foreach (var control in _readerFlowLayoutPanel.Controls)
            {
                var test = control as SpeedwayReaderView;
                if (null != test && test.SpeedwayReaderViewModel.ReaderName == viewModel.ReaderName)
                {
                    target = test;
                    break;
                }
            }

            if (null != target)
            {
                _readerFlowLayoutPanel.Controls.Remove(target);
            }
        }

        void _viewModel_TagCreated(object sender, EventArgs e)
        {
            var list = sender as List<TagWpf>;

            if (null != list)
            {
                _tagDataGridView.BeginInvoke(new TagsReportedDelegate(_addTags), new object[] { list });
            }
        }

        void _viewModel_LogCreated(object sender, EventArgs e)
        {
            var log = sender as LogEntryWpf;

            if (null != log)
            {
                _logDataGridView.BeginInvoke(new LoggingDelegate(_addLog), new object[] { log });
            }
        }

        void _viewModel_ReaderCreated(object sender, EventArgs e)
        {
            var reader = sender as SpeedwayReaderViewModel;

            if (null != reader)
            {
                _readerFlowLayoutPanel.BeginInvoke(new ReaderCreatedDelegate(_addReader), new object[] { reader });
            }
        }

        void _addReader(SpeedwayReaderViewModel reader)
        {
            var view = new SpeedwayReaderView();
            view.SpeedwayReaderViewModel = reader;

            _readerFlowLayoutPanel.Controls.Add(view);
        }

        private int _logCount = 0;

        void _addLog(LogEntryWpf log)
        {
            log.Rank = _logCount++;

            _logBindingSource.Insert(0, log);
        }

        private int _tagCount = 0;

        void _addTags(List<TagWpf> list)
        {
            foreach (var tag in list)
            {
                tag.Rank = _tagCount++;
                _tagBindingSource.Insert(0, tag);
            }
        }

        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var dialog = new NewReaderForm();
            var result = dialog.ShowDialog();

            if (result == DialogResult.OK)
            {
                _viewModel.AddNewReader(dialog.NewReaderModel);
            }
        }

        private MainWindowViewModel _viewModel;

        private void startAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _viewModel.StartAll();
        }

        private void stopAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _viewModel.StopAll();
        }

        private void connectAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _viewModel.ConnectAll();
        }

        private void disconnectAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _viewModel.DisconnectAll();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}

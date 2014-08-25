using Octane_ViewModel;
namespace Template_WinForms
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this._menuStrip = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.newToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.startAllToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.stopAllToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.connectAllToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.disconnectAllToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this._tagDataGridView = new System.Windows.Forms.DataGridView();
            this._logDataGridView = new System.Windows.Forms.DataGridView();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn7 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn8 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.rankDataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.readerIdentityAsStringDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.epcDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.isAntennaPortNumberPresentDataGridViewCheckBoxColumn = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.antennaPortNumberDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.isPeakRssiInDbmPresentDataGridViewCheckBoxColumn = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.peakRssiInDbmDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.isSeenCountPresentDataGridViewCheckBoxColumn = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.seenCountDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.isSerializedTidPresentDataGridViewCheckBoxColumn = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.serializedTidDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.isFirstSeenTimePresentDataGridViewCheckBoxColumn = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.firstSeenTimeDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.isLastSeenTimePresentDataGridViewCheckBoxColumn = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.lastSeenTimeDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.isChannelInMhzPresentDataGridViewCheckBoxColumn = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.channelInMhzDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.isPhaseAngleInRadiansPresentDataGridViewCheckBoxColumn = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.phaseAngleInRadiansDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this._tagBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.managedThreadIdDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colorDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.rankDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.timestampDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.levelDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.originDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.messageDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this._logBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.dataGridViewTextBoxColumn9 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn10 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this._readerFlowLayoutPanel = new System.Windows.Forms.FlowLayoutPanel();
            this.dataGridViewTextBoxColumn11 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn12 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.readerIdentityDataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.readerIdentityDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this._menuStrip.SuspendLayout();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this._tagDataGridView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this._logDataGridView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this._tagBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this._logBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // _menuStrip
            // 
            this._menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.toolsToolStripMenuItem});
            this._menuStrip.Location = new System.Drawing.Point(0, 0);
            this._menuStrip.Name = "_menuStrip";
            this._menuStrip.Size = new System.Drawing.Size(1123, 24);
            this._menuStrip.TabIndex = 0;
            this._menuStrip.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newToolStripMenuItem,
            this.toolStripSeparator2,
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "&File";
            // 
            // newToolStripMenuItem
            // 
            this.newToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("newToolStripMenuItem.Image")));
            this.newToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.newToolStripMenuItem.Name = "newToolStripMenuItem";
            this.newToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.N)));
            this.newToolStripMenuItem.Size = new System.Drawing.Size(141, 22);
            this.newToolStripMenuItem.Text = "&New";
            this.newToolStripMenuItem.Click += new System.EventHandler(this.newToolStripMenuItem_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(138, 6);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(141, 22);
            this.exitToolStripMenuItem.Text = "E&xit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // toolsToolStripMenuItem
            // 
            this.toolsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.startAllToolStripMenuItem,
            this.stopAllToolStripMenuItem,
            this.connectAllToolStripMenuItem,
            this.disconnectAllToolStripMenuItem});
            this.toolsToolStripMenuItem.Name = "toolsToolStripMenuItem";
            this.toolsToolStripMenuItem.Size = new System.Drawing.Size(48, 20);
            this.toolsToolStripMenuItem.Text = "&Tools";
            // 
            // startAllToolStripMenuItem
            // 
            this.startAllToolStripMenuItem.Name = "startAllToolStripMenuItem";
            this.startAllToolStripMenuItem.Size = new System.Drawing.Size(150, 22);
            this.startAllToolStripMenuItem.Text = "Start All";
            this.startAllToolStripMenuItem.Click += new System.EventHandler(this.startAllToolStripMenuItem_Click);
            // 
            // stopAllToolStripMenuItem
            // 
            this.stopAllToolStripMenuItem.Name = "stopAllToolStripMenuItem";
            this.stopAllToolStripMenuItem.Size = new System.Drawing.Size(150, 22);
            this.stopAllToolStripMenuItem.Text = "Stop All";
            this.stopAllToolStripMenuItem.Click += new System.EventHandler(this.stopAllToolStripMenuItem_Click);
            // 
            // connectAllToolStripMenuItem
            // 
            this.connectAllToolStripMenuItem.Name = "connectAllToolStripMenuItem";
            this.connectAllToolStripMenuItem.Size = new System.Drawing.Size(150, 22);
            this.connectAllToolStripMenuItem.Text = "Connect All";
            this.connectAllToolStripMenuItem.Click += new System.EventHandler(this.connectAllToolStripMenuItem_Click);
            // 
            // disconnectAllToolStripMenuItem
            // 
            this.disconnectAllToolStripMenuItem.Name = "disconnectAllToolStripMenuItem";
            this.disconnectAllToolStripMenuItem.Size = new System.Drawing.Size(150, 22);
            this.disconnectAllToolStripMenuItem.Text = "Disconnect All";
            this.disconnectAllToolStripMenuItem.Click += new System.EventHandler(this.disconnectAllToolStripMenuItem_Click);
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 24);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this._readerFlowLayoutPanel);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.splitContainer2);
            this.splitContainer1.Size = new System.Drawing.Size(1123, 554);
            this.splitContainer1.SplitterDistance = 236;
            this.splitContainer1.TabIndex = 1;
            // 
            // splitContainer2
            // 
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.Location = new System.Drawing.Point(0, 0);
            this.splitContainer2.Name = "splitContainer2";
            this.splitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this._tagDataGridView);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this._logDataGridView);
            this.splitContainer2.Size = new System.Drawing.Size(883, 554);
            this.splitContainer2.SplitterDistance = 333;
            this.splitContainer2.TabIndex = 0;
            // 
            // _tagDataGridView
            // 
            this._tagDataGridView.AllowUserToAddRows = false;
            this._tagDataGridView.AllowUserToDeleteRows = false;
            this._tagDataGridView.AutoGenerateColumns = false;
            this._tagDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this._tagDataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.rankDataGridViewTextBoxColumn1,
            this.readerIdentityAsStringDataGridViewTextBoxColumn,
            this.epcDataGridViewTextBoxColumn,
            this.isAntennaPortNumberPresentDataGridViewCheckBoxColumn,
            this.antennaPortNumberDataGridViewTextBoxColumn,
            this.isPeakRssiInDbmPresentDataGridViewCheckBoxColumn,
            this.peakRssiInDbmDataGridViewTextBoxColumn,
            this.isSeenCountPresentDataGridViewCheckBoxColumn,
            this.seenCountDataGridViewTextBoxColumn,
            this.isSerializedTidPresentDataGridViewCheckBoxColumn,
            this.serializedTidDataGridViewTextBoxColumn,
            this.isFirstSeenTimePresentDataGridViewCheckBoxColumn,
            this.firstSeenTimeDataGridViewTextBoxColumn,
            this.isLastSeenTimePresentDataGridViewCheckBoxColumn,
            this.lastSeenTimeDataGridViewTextBoxColumn,
            this.isChannelInMhzPresentDataGridViewCheckBoxColumn,
            this.channelInMhzDataGridViewTextBoxColumn,
            this.isPhaseAngleInRadiansPresentDataGridViewCheckBoxColumn,
            this.phaseAngleInRadiansDataGridViewTextBoxColumn,
            this.readerIdentityDataGridViewTextBoxColumn1});
            this._tagDataGridView.DataSource = this._tagBindingSource;
            this._tagDataGridView.Dock = System.Windows.Forms.DockStyle.Fill;
            this._tagDataGridView.Location = new System.Drawing.Point(0, 0);
            this._tagDataGridView.Name = "_tagDataGridView";
            this._tagDataGridView.ReadOnly = true;
            this._tagDataGridView.Size = new System.Drawing.Size(883, 333);
            this._tagDataGridView.TabIndex = 0;
            // 
            // _logDataGridView
            // 
            this._logDataGridView.AllowUserToAddRows = false;
            this._logDataGridView.AutoGenerateColumns = false;
            this._logDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this._logDataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.managedThreadIdDataGridViewTextBoxColumn,
            this.colorDataGridViewTextBoxColumn,
            this.rankDataGridViewTextBoxColumn,
            this.timestampDataGridViewTextBoxColumn,
            this.levelDataGridViewTextBoxColumn,
            this.originDataGridViewTextBoxColumn,
            this.messageDataGridViewTextBoxColumn,
            this.readerIdentityDataGridViewTextBoxColumn});
            this._logDataGridView.DataSource = this._logBindingSource;
            this._logDataGridView.Dock = System.Windows.Forms.DockStyle.Fill;
            this._logDataGridView.Location = new System.Drawing.Point(0, 0);
            this._logDataGridView.Name = "_logDataGridView";
            this._logDataGridView.ReadOnly = true;
            this._logDataGridView.Size = new System.Drawing.Size(883, 217);
            this._logDataGridView.TabIndex = 0;
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.DataPropertyName = "ReaderIdentity";
            this.dataGridViewTextBoxColumn1.HeaderText = "ReaderIdentity";
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            // 
            // dataGridViewTextBoxColumn2
            // 
            this.dataGridViewTextBoxColumn2.DataPropertyName = "ReaderIdentity";
            this.dataGridViewTextBoxColumn2.HeaderText = "ReaderIdentity";
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            this.dataGridViewTextBoxColumn2.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn3
            // 
            this.dataGridViewTextBoxColumn3.DataPropertyName = "ReaderIdentity";
            this.dataGridViewTextBoxColumn3.HeaderText = "ReaderIdentity";
            this.dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
            this.dataGridViewTextBoxColumn3.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn4
            // 
            this.dataGridViewTextBoxColumn4.DataPropertyName = "ReaderIdentity";
            this.dataGridViewTextBoxColumn4.HeaderText = "ReaderIdentity";
            this.dataGridViewTextBoxColumn4.Name = "dataGridViewTextBoxColumn4";
            this.dataGridViewTextBoxColumn4.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn5
            // 
            this.dataGridViewTextBoxColumn5.DataPropertyName = "ReaderIdentity";
            this.dataGridViewTextBoxColumn5.HeaderText = "ReaderIdentity";
            this.dataGridViewTextBoxColumn5.Name = "dataGridViewTextBoxColumn5";
            this.dataGridViewTextBoxColumn5.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn6
            // 
            this.dataGridViewTextBoxColumn6.DataPropertyName = "ReaderIdentity";
            this.dataGridViewTextBoxColumn6.HeaderText = "ReaderIdentity";
            this.dataGridViewTextBoxColumn6.Name = "dataGridViewTextBoxColumn6";
            this.dataGridViewTextBoxColumn6.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn7
            // 
            this.dataGridViewTextBoxColumn7.DataPropertyName = "ReaderIdentity";
            this.dataGridViewTextBoxColumn7.HeaderText = "ReaderIdentity";
            this.dataGridViewTextBoxColumn7.Name = "dataGridViewTextBoxColumn7";
            // 
            // dataGridViewTextBoxColumn8
            // 
            this.dataGridViewTextBoxColumn8.DataPropertyName = "ReaderIdentity";
            this.dataGridViewTextBoxColumn8.HeaderText = "ReaderIdentity";
            this.dataGridViewTextBoxColumn8.Name = "dataGridViewTextBoxColumn8";
            // 
            // rankDataGridViewTextBoxColumn1
            // 
            this.rankDataGridViewTextBoxColumn1.DataPropertyName = "Rank";
            this.rankDataGridViewTextBoxColumn1.HeaderText = "Rank";
            this.rankDataGridViewTextBoxColumn1.Name = "rankDataGridViewTextBoxColumn1";
            this.rankDataGridViewTextBoxColumn1.ReadOnly = true;
            // 
            // readerIdentityAsStringDataGridViewTextBoxColumn
            // 
            this.readerIdentityAsStringDataGridViewTextBoxColumn.DataPropertyName = "ReaderIdentityAsString";
            this.readerIdentityAsStringDataGridViewTextBoxColumn.HeaderText = "ReaderIdentityAsString";
            this.readerIdentityAsStringDataGridViewTextBoxColumn.Name = "readerIdentityAsStringDataGridViewTextBoxColumn";
            this.readerIdentityAsStringDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // epcDataGridViewTextBoxColumn
            // 
            this.epcDataGridViewTextBoxColumn.DataPropertyName = "Epc";
            this.epcDataGridViewTextBoxColumn.HeaderText = "Epc";
            this.epcDataGridViewTextBoxColumn.Name = "epcDataGridViewTextBoxColumn";
            this.epcDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // isAntennaPortNumberPresentDataGridViewCheckBoxColumn
            // 
            this.isAntennaPortNumberPresentDataGridViewCheckBoxColumn.DataPropertyName = "IsAntennaPortNumberPresent";
            this.isAntennaPortNumberPresentDataGridViewCheckBoxColumn.HeaderText = "IsAntennaPortNumberPresent";
            this.isAntennaPortNumberPresentDataGridViewCheckBoxColumn.Name = "isAntennaPortNumberPresentDataGridViewCheckBoxColumn";
            this.isAntennaPortNumberPresentDataGridViewCheckBoxColumn.ReadOnly = true;
            // 
            // antennaPortNumberDataGridViewTextBoxColumn
            // 
            this.antennaPortNumberDataGridViewTextBoxColumn.DataPropertyName = "AntennaPortNumber";
            this.antennaPortNumberDataGridViewTextBoxColumn.HeaderText = "AntennaPortNumber";
            this.antennaPortNumberDataGridViewTextBoxColumn.Name = "antennaPortNumberDataGridViewTextBoxColumn";
            this.antennaPortNumberDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // isPeakRssiInDbmPresentDataGridViewCheckBoxColumn
            // 
            this.isPeakRssiInDbmPresentDataGridViewCheckBoxColumn.DataPropertyName = "IsPeakRssiInDbmPresent";
            this.isPeakRssiInDbmPresentDataGridViewCheckBoxColumn.HeaderText = "IsPeakRssiInDbmPresent";
            this.isPeakRssiInDbmPresentDataGridViewCheckBoxColumn.Name = "isPeakRssiInDbmPresentDataGridViewCheckBoxColumn";
            this.isPeakRssiInDbmPresentDataGridViewCheckBoxColumn.ReadOnly = true;
            // 
            // peakRssiInDbmDataGridViewTextBoxColumn
            // 
            this.peakRssiInDbmDataGridViewTextBoxColumn.DataPropertyName = "PeakRssiInDbm";
            this.peakRssiInDbmDataGridViewTextBoxColumn.HeaderText = "PeakRssiInDbm";
            this.peakRssiInDbmDataGridViewTextBoxColumn.Name = "peakRssiInDbmDataGridViewTextBoxColumn";
            this.peakRssiInDbmDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // isSeenCountPresentDataGridViewCheckBoxColumn
            // 
            this.isSeenCountPresentDataGridViewCheckBoxColumn.DataPropertyName = "IsSeenCountPresent";
            this.isSeenCountPresentDataGridViewCheckBoxColumn.HeaderText = "IsSeenCountPresent";
            this.isSeenCountPresentDataGridViewCheckBoxColumn.Name = "isSeenCountPresentDataGridViewCheckBoxColumn";
            this.isSeenCountPresentDataGridViewCheckBoxColumn.ReadOnly = true;
            // 
            // seenCountDataGridViewTextBoxColumn
            // 
            this.seenCountDataGridViewTextBoxColumn.DataPropertyName = "SeenCount";
            this.seenCountDataGridViewTextBoxColumn.HeaderText = "SeenCount";
            this.seenCountDataGridViewTextBoxColumn.Name = "seenCountDataGridViewTextBoxColumn";
            this.seenCountDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // isSerializedTidPresentDataGridViewCheckBoxColumn
            // 
            this.isSerializedTidPresentDataGridViewCheckBoxColumn.DataPropertyName = "IsSerializedTidPresent";
            this.isSerializedTidPresentDataGridViewCheckBoxColumn.HeaderText = "IsSerializedTidPresent";
            this.isSerializedTidPresentDataGridViewCheckBoxColumn.Name = "isSerializedTidPresentDataGridViewCheckBoxColumn";
            this.isSerializedTidPresentDataGridViewCheckBoxColumn.ReadOnly = true;
            // 
            // serializedTidDataGridViewTextBoxColumn
            // 
            this.serializedTidDataGridViewTextBoxColumn.DataPropertyName = "SerializedTid";
            this.serializedTidDataGridViewTextBoxColumn.HeaderText = "SerializedTid";
            this.serializedTidDataGridViewTextBoxColumn.Name = "serializedTidDataGridViewTextBoxColumn";
            this.serializedTidDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // isFirstSeenTimePresentDataGridViewCheckBoxColumn
            // 
            this.isFirstSeenTimePresentDataGridViewCheckBoxColumn.DataPropertyName = "IsFirstSeenTimePresent";
            this.isFirstSeenTimePresentDataGridViewCheckBoxColumn.HeaderText = "IsFirstSeenTimePresent";
            this.isFirstSeenTimePresentDataGridViewCheckBoxColumn.Name = "isFirstSeenTimePresentDataGridViewCheckBoxColumn";
            this.isFirstSeenTimePresentDataGridViewCheckBoxColumn.ReadOnly = true;
            // 
            // firstSeenTimeDataGridViewTextBoxColumn
            // 
            this.firstSeenTimeDataGridViewTextBoxColumn.DataPropertyName = "FirstSeenTime";
            this.firstSeenTimeDataGridViewTextBoxColumn.HeaderText = "FirstSeenTime";
            this.firstSeenTimeDataGridViewTextBoxColumn.Name = "firstSeenTimeDataGridViewTextBoxColumn";
            this.firstSeenTimeDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // isLastSeenTimePresentDataGridViewCheckBoxColumn
            // 
            this.isLastSeenTimePresentDataGridViewCheckBoxColumn.DataPropertyName = "IsLastSeenTimePresent";
            this.isLastSeenTimePresentDataGridViewCheckBoxColumn.HeaderText = "IsLastSeenTimePresent";
            this.isLastSeenTimePresentDataGridViewCheckBoxColumn.Name = "isLastSeenTimePresentDataGridViewCheckBoxColumn";
            this.isLastSeenTimePresentDataGridViewCheckBoxColumn.ReadOnly = true;
            // 
            // lastSeenTimeDataGridViewTextBoxColumn
            // 
            this.lastSeenTimeDataGridViewTextBoxColumn.DataPropertyName = "LastSeenTime";
            this.lastSeenTimeDataGridViewTextBoxColumn.HeaderText = "LastSeenTime";
            this.lastSeenTimeDataGridViewTextBoxColumn.Name = "lastSeenTimeDataGridViewTextBoxColumn";
            this.lastSeenTimeDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // isChannelInMhzPresentDataGridViewCheckBoxColumn
            // 
            this.isChannelInMhzPresentDataGridViewCheckBoxColumn.DataPropertyName = "IsChannelInMhzPresent";
            this.isChannelInMhzPresentDataGridViewCheckBoxColumn.HeaderText = "IsChannelInMhzPresent";
            this.isChannelInMhzPresentDataGridViewCheckBoxColumn.Name = "isChannelInMhzPresentDataGridViewCheckBoxColumn";
            this.isChannelInMhzPresentDataGridViewCheckBoxColumn.ReadOnly = true;
            // 
            // channelInMhzDataGridViewTextBoxColumn
            // 
            this.channelInMhzDataGridViewTextBoxColumn.DataPropertyName = "ChannelInMhz";
            this.channelInMhzDataGridViewTextBoxColumn.HeaderText = "ChannelInMhz";
            this.channelInMhzDataGridViewTextBoxColumn.Name = "channelInMhzDataGridViewTextBoxColumn";
            this.channelInMhzDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // isPhaseAngleInRadiansPresentDataGridViewCheckBoxColumn
            // 
            this.isPhaseAngleInRadiansPresentDataGridViewCheckBoxColumn.DataPropertyName = "IsPhaseAngleInRadiansPresent";
            this.isPhaseAngleInRadiansPresentDataGridViewCheckBoxColumn.HeaderText = "IsPhaseAngleInRadiansPresent";
            this.isPhaseAngleInRadiansPresentDataGridViewCheckBoxColumn.Name = "isPhaseAngleInRadiansPresentDataGridViewCheckBoxColumn";
            this.isPhaseAngleInRadiansPresentDataGridViewCheckBoxColumn.ReadOnly = true;
            // 
            // phaseAngleInRadiansDataGridViewTextBoxColumn
            // 
            this.phaseAngleInRadiansDataGridViewTextBoxColumn.DataPropertyName = "PhaseAngleInRadians";
            this.phaseAngleInRadiansDataGridViewTextBoxColumn.HeaderText = "PhaseAngleInRadians";
            this.phaseAngleInRadiansDataGridViewTextBoxColumn.Name = "phaseAngleInRadiansDataGridViewTextBoxColumn";
            this.phaseAngleInRadiansDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // _tagBindingSource
            // 
            this._tagBindingSource.DataSource = typeof(Octane_ViewModel.TagWpf);
            // 
            // managedThreadIdDataGridViewTextBoxColumn
            // 
            this.managedThreadIdDataGridViewTextBoxColumn.DataPropertyName = "ManagedThreadId";
            this.managedThreadIdDataGridViewTextBoxColumn.HeaderText = "ManagedThreadId";
            this.managedThreadIdDataGridViewTextBoxColumn.Name = "managedThreadIdDataGridViewTextBoxColumn";
            this.managedThreadIdDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // colorDataGridViewTextBoxColumn
            // 
            this.colorDataGridViewTextBoxColumn.DataPropertyName = "Color";
            this.colorDataGridViewTextBoxColumn.HeaderText = "Color";
            this.colorDataGridViewTextBoxColumn.Name = "colorDataGridViewTextBoxColumn";
            this.colorDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // rankDataGridViewTextBoxColumn
            // 
            this.rankDataGridViewTextBoxColumn.DataPropertyName = "Rank";
            this.rankDataGridViewTextBoxColumn.HeaderText = "Rank";
            this.rankDataGridViewTextBoxColumn.Name = "rankDataGridViewTextBoxColumn";
            this.rankDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // timestampDataGridViewTextBoxColumn
            // 
            this.timestampDataGridViewTextBoxColumn.DataPropertyName = "Timestamp";
            this.timestampDataGridViewTextBoxColumn.HeaderText = "Timestamp";
            this.timestampDataGridViewTextBoxColumn.Name = "timestampDataGridViewTextBoxColumn";
            this.timestampDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // levelDataGridViewTextBoxColumn
            // 
            this.levelDataGridViewTextBoxColumn.DataPropertyName = "Level";
            this.levelDataGridViewTextBoxColumn.HeaderText = "Level";
            this.levelDataGridViewTextBoxColumn.Name = "levelDataGridViewTextBoxColumn";
            this.levelDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // originDataGridViewTextBoxColumn
            // 
            this.originDataGridViewTextBoxColumn.DataPropertyName = "Origin";
            this.originDataGridViewTextBoxColumn.HeaderText = "Origin";
            this.originDataGridViewTextBoxColumn.Name = "originDataGridViewTextBoxColumn";
            this.originDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // messageDataGridViewTextBoxColumn
            // 
            this.messageDataGridViewTextBoxColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.messageDataGridViewTextBoxColumn.DataPropertyName = "Message";
            this.messageDataGridViewTextBoxColumn.HeaderText = "Message";
            this.messageDataGridViewTextBoxColumn.Name = "messageDataGridViewTextBoxColumn";
            this.messageDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // _logBindingSource
            // 
            this._logBindingSource.DataSource = typeof(Octane_ViewModel.LogEntryWpf);
            // 
            // dataGridViewTextBoxColumn9
            // 
            this.dataGridViewTextBoxColumn9.DataPropertyName = "ReaderIdentity";
            this.dataGridViewTextBoxColumn9.HeaderText = "ReaderIdentity";
            this.dataGridViewTextBoxColumn9.Name = "dataGridViewTextBoxColumn9";
            this.dataGridViewTextBoxColumn9.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn10
            // 
            this.dataGridViewTextBoxColumn10.DataPropertyName = "ReaderIdentity";
            this.dataGridViewTextBoxColumn10.HeaderText = "ReaderIdentity";
            this.dataGridViewTextBoxColumn10.Name = "dataGridViewTextBoxColumn10";
            this.dataGridViewTextBoxColumn10.ReadOnly = true;
            // 
            // _readerFlowLayoutPanel
            // 
            this._readerFlowLayoutPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this._readerFlowLayoutPanel.Location = new System.Drawing.Point(0, 0);
            this._readerFlowLayoutPanel.Name = "_readerFlowLayoutPanel";
            this._readerFlowLayoutPanel.Size = new System.Drawing.Size(236, 554);
            this._readerFlowLayoutPanel.TabIndex = 0;
            // 
            // dataGridViewTextBoxColumn11
            // 
            this.dataGridViewTextBoxColumn11.DataPropertyName = "ReaderIdentity";
            this.dataGridViewTextBoxColumn11.HeaderText = "ReaderIdentity";
            this.dataGridViewTextBoxColumn11.Name = "dataGridViewTextBoxColumn11";
            this.dataGridViewTextBoxColumn11.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn12
            // 
            this.dataGridViewTextBoxColumn12.DataPropertyName = "ReaderIdentity";
            this.dataGridViewTextBoxColumn12.HeaderText = "ReaderIdentity";
            this.dataGridViewTextBoxColumn12.Name = "dataGridViewTextBoxColumn12";
            this.dataGridViewTextBoxColumn12.ReadOnly = true;
            // 
            // readerIdentityDataGridViewTextBoxColumn1
            // 
            this.readerIdentityDataGridViewTextBoxColumn1.DataPropertyName = "ReaderIdentity";
            this.readerIdentityDataGridViewTextBoxColumn1.HeaderText = "ReaderIdentity";
            this.readerIdentityDataGridViewTextBoxColumn1.Name = "readerIdentityDataGridViewTextBoxColumn1";
            this.readerIdentityDataGridViewTextBoxColumn1.ReadOnly = true;
            // 
            // readerIdentityDataGridViewTextBoxColumn
            // 
            this.readerIdentityDataGridViewTextBoxColumn.DataPropertyName = "ReaderIdentity";
            this.readerIdentityDataGridViewTextBoxColumn.HeaderText = "ReaderIdentity";
            this.readerIdentityDataGridViewTextBoxColumn.Name = "readerIdentityDataGridViewTextBoxColumn";
            this.readerIdentityDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1123, 578);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this._menuStrip);
            this.MainMenuStrip = this._menuStrip;
            this.Name = "MainForm";
            this.Text = "Speedway WinForm Template";
            this._menuStrip.ResumeLayout(false);
            this._menuStrip.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.ResumeLayout(false);
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            this.splitContainer2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this._tagDataGridView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this._logDataGridView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this._tagBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this._logBindingSource)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip _menuStrip;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem newToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toolsToolStripMenuItem;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.DataGridView _tagDataGridView;
        private System.Windows.Forms.DataGridView _logDataGridView;
        private System.Windows.Forms.BindingSource _logBindingSource;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.BindingSource _tagBindingSource;
        private System.Windows.Forms.DataGridViewTextBoxColumn rankDataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn readerIdentityDataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn readerIdentityAsStringDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn epcDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewCheckBoxColumn isAntennaPortNumberPresentDataGridViewCheckBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn antennaPortNumberDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewCheckBoxColumn isPeakRssiInDbmPresentDataGridViewCheckBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn peakRssiInDbmDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewCheckBoxColumn isSeenCountPresentDataGridViewCheckBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn seenCountDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewCheckBoxColumn isSerializedTidPresentDataGridViewCheckBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn serializedTidDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewCheckBoxColumn isFirstSeenTimePresentDataGridViewCheckBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn firstSeenTimeDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewCheckBoxColumn isLastSeenTimePresentDataGridViewCheckBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn lastSeenTimeDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewCheckBoxColumn isChannelInMhzPresentDataGridViewCheckBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn channelInMhzDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewCheckBoxColumn isPhaseAngleInRadiansPresentDataGridViewCheckBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn phaseAngleInRadiansDataGridViewTextBoxColumn;
        private System.Windows.Forms.ToolStripMenuItem startAllToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem stopAllToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem connectAllToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem disconnectAllToolStripMenuItem;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private System.Windows.Forms.DataGridViewTextBoxColumn managedThreadIdDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn colorDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn rankDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn timestampDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn levelDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn originDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn messageDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn readerIdentityDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn4;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn5;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn6;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn7;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn8;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn9;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn10;
        private System.Windows.Forms.FlowLayoutPanel _readerFlowLayoutPanel;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn11;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn12;
    }
}


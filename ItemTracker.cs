// reference:System.dll
// reference:System.Net.Http.dll

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Reflection;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.Threading;
using System.Xml;
using Advanced_Combat_Tracker;


namespace act_plugin_dkp
{
    /// <summary>   
    /// Loot Tracking ACT Plugin, modified from original LootTally plugin
    /// </summary>
    public class AphaLootTracker : UserControl, IActPluginV1
    {
        private HttpClient httpClient;
        bool cookiesInitialized = false;
        private bool refreshLvItems = false;
        private int lvChatSorting = 0;
        private bool lvChatReverse = true;
        private Label itemDataGridStatusLabel;
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }

            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.ItemsListViewContextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.copyItemLinkToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.copyItemNameToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.copyAsPlainTexToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.submitToDKPSite = new System.Windows.Forms.ToolStripMenuItem();
            this.ChatListView = new System.Windows.Forms.ListView();
            this.chatDateTimeColumnHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chatChannelColumnHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chatPlayerColumnHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chatTextColumnHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.ChatListViewContextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.copyActualLogLinesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.chatPreceedingItemGroupBox = new System.Windows.Forms.GroupBox();
            this.ChatPanel = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.lblCurrentItem = new System.Windows.Forms.Label();
            this.linkEQWire = new System.Windows.Forms.LinkLabel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.itemDataGridStatusLabel = new System.Windows.Forms.Label();
            this.itemGridView = new System.Windows.Forms.DataGridView();
            this.columnTime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.columnZone = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.columnPlayer = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.columnItemName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.columnContainer = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.columnDKP = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.columnDKPSubmitResult = new DataGridViewTextBoxColumn();
            this.tabs = new System.Windows.Forms.TabControl();
            this.itemsPage = new System.Windows.Forms.TabPage();
            this.splitter1 = new System.Windows.Forms.Splitter();
            this.copyTallyAsPlainTextToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.settingsPage = new System.Windows.Forms.TabPage();
            this.otherSettingsGroupBox = new System.Windows.Forms.GroupBox();
            this.skipLottoCheckBox = new System.Windows.Forms.CheckBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.label4 = new System.Windows.Forms.Label();
            this.GermanRadioButton = new System.Windows.Forms.RadioButton();
            this.EnglishRadioButton = new System.Windows.Forms.RadioButton();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.label3 = new System.Windows.Forms.Label();
            this.namedChatTextBox = new System.Windows.Forms.TextBox();
            this.removeNamedChatButton = new System.Windows.Forms.Button();
            this.addNamedChatButton = new System.Windows.Forms.Button();
            this.lbNamedChat = new System.Windows.Forms.ListBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.numberOfLinesToRetainPerChannelLabel = new System.Windows.Forms.Label();
            this.numberOfSecondsToRecordChatPrecedingItemLabel = new System.Windows.Forms.Label();
            this.numberOfChatLinesSelector = new System.Windows.Forms.NumericUpDown();
            this.numberOfChatSecondsSelector = new System.Windows.Forms.NumericUpDown();
            this.labeldkpSiteURL = new System.Windows.Forms.Label();
            this.labeldkpSiteUser = new System.Windows.Forms.Label();
            this.labeldkpSitePwd = new System.Windows.Forms.Label();
            this.dkpSiteURL = new System.Windows.Forms.TextBox();
            this.dkpSiteUser = new System.Windows.Forms.TextBox();
            this.dkpSitePwd = new System.Windows.Forms.TextBox();
            this.dkpDebugText = new System.Windows.Forms.TextBox();
            this.chatLinesCheckBox = new System.Windows.Forms.CheckBox();
            this.chatSecondsCheckBox = new System.Windows.Forms.CheckBox();
            this.HalfSecondTimer = new System.Windows.Forms.Timer(this.components);
            this.ClearButton = new System.Windows.Forms.Button();
            this.ItemsListViewContextMenuStrip.SuspendLayout();
            this.ChatListViewContextMenuStrip.SuspendLayout();
            this.chatPreceedingItemGroupBox.SuspendLayout();
            this.ChatPanel.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.itemGridView)).BeginInit();
            this.tabs.SuspendLayout();
            this.itemsPage.SuspendLayout();
            this.settingsPage.SuspendLayout();
            this.otherSettingsGroupBox.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numberOfChatLinesSelector)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numberOfChatSecondsSelector)).BeginInit();
            this.SuspendLayout();
            // 
            // ItemsListViewContextMenuStrip
            // 
            this.ItemsListViewContextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.copyItemLinkToolStripMenuItem,
            this.copyItemNameToolStripMenuItem,
            this.copyAsPlainTexToolStripMenuItem,
            this.submitToDKPSite});
            this.ItemsListViewContextMenuStrip.Name = "contextMenuStrip1";
            this.ItemsListViewContextMenuStrip.Size = new System.Drawing.Size(243, 280);
            // 
            // copyItemLinkToolStripMenuItem
            // 
            this.copyItemLinkToolStripMenuItem.Name = "copyItemLinkToolStripMenuItem";
            this.copyItemLinkToolStripMenuItem.Size = new System.Drawing.Size(242, 22);
            this.copyItemLinkToolStripMenuItem.Text = "Copy item link";
            this.copyItemLinkToolStripMenuItem.Click += new System.EventHandler(this.copyItemLinkToolStripMenuItem_Click);
            // 
            // copyItemNameToolStripMenuItem
            // 
            this.copyItemNameToolStripMenuItem.Name = "copyItemNameToolStripMenuItem";
            this.copyItemNameToolStripMenuItem.Size = new System.Drawing.Size(242, 22);
            this.copyItemNameToolStripMenuItem.Text = "Copy item name";
            this.copyItemNameToolStripMenuItem.Click += new System.EventHandler(this.copyItemNameToolStripMenuItem_Click);
            // 
            // copyAsPlainTexToolStripMenuItem
            // 
            this.copyAsPlainTexToolStripMenuItem.Name = "copyAsPlainTexToolStripMenuItem";
            this.copyAsPlainTexToolStripMenuItem.Size = new System.Drawing.Size(242, 22);
            this.copyAsPlainTexToolStripMenuItem.Text = "Copy as Plain Text";
            this.copyAsPlainTexToolStripMenuItem.Click += new System.EventHandler(this.copyAsPlainTexToolStripMenuItem_Click);
            // 
            // copyActualLogLinesToolStripMenuItem
            // 
            this.submitToDKPSite.Name = "submitToDKPSite";
            this.submitToDKPSite.Size = new System.Drawing.Size(242, 22);
            this.submitToDKPSite.Text = "Submit to DKP site";
            this.submitToDKPSite.Click += new System.EventHandler(this.submitToDKPSite_Click);
            // 
            // ChatListView
            // 
            this.ChatListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.chatDateTimeColumnHeader,
            this.chatChannelColumnHeader,
            this.chatPlayerColumnHeader,
            this.chatTextColumnHeader});
            this.ChatListView.ContextMenuStrip = this.ChatListViewContextMenuStrip;
            this.ChatListView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ChatListView.GridLines = true;
            this.ChatListView.Location = new System.Drawing.Point(3, 16);
            this.ChatListView.Name = "ChatListView";
            this.ChatListView.Size = new System.Drawing.Size(699, 110);
            this.ChatListView.TabIndex = 1;
            this.ChatListView.UseCompatibleStateImageBehavior = false;
            this.ChatListView.View = System.Windows.Forms.View.Details;
            this.ChatListView.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.lvChat_ColumnClick);
            this.ChatListView.Resize += new System.EventHandler(this.ChatListView_Resize);
            // 
            // chatDateTimeColumnHeader
            // 
            this.chatDateTimeColumnHeader.Text = "DateTime";
            // 
            // chatChannelColumnHeader
            // 
            this.chatChannelColumnHeader.Text = "Channel";
            // 
            // chatPlayerColumnHeader
            // 
            this.chatPlayerColumnHeader.Text = "Player";
            // 
            // chatTextColumnHeader
            // 
            this.chatTextColumnHeader.Text = "Text";
            // 
            // ChatListViewContextMenuStrip
            // 
            this.ChatListViewContextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.copyActualLogLinesToolStripMenuItem});
            this.ChatListViewContextMenuStrip.Name = "cmsLvChat";
            this.ChatListViewContextMenuStrip.Size = new System.Drawing.Size(185, 26);
            // 
            // copyActualLogLinesToolStripMenuItem1
            // 
            this.copyActualLogLinesToolStripMenuItem.Name = "copyActualLogLinesToolStripMenuItem1";
            this.copyActualLogLinesToolStripMenuItem.Size = new System.Drawing.Size(184, 22);
            this.copyActualLogLinesToolStripMenuItem.Text = "Copy actual log lines";
            this.copyActualLogLinesToolStripMenuItem.Click += new System.EventHandler(this.copyActualLogLinesToolStripMenuItem_Click);
            // 
            // chatPreceedingItemGroupBox
            // 
            this.chatPreceedingItemGroupBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.chatPreceedingItemGroupBox.Controls.Add(this.ChatListView);
            this.chatPreceedingItemGroupBox.Location = new System.Drawing.Point(3, 3);
            this.chatPreceedingItemGroupBox.Name = "chatPreceedingItemGroupBox";
            this.chatPreceedingItemGroupBox.Size = new System.Drawing.Size(705, 129);
            this.chatPreceedingItemGroupBox.TabIndex = 2;
            this.chatPreceedingItemGroupBox.TabStop = false;
            this.chatPreceedingItemGroupBox.Text = "Chat Preceeding Item";
            // 
            // ChatPanel
            // 
            this.ChatPanel.Controls.Add(this.panel2);
            this.ChatPanel.Controls.Add(this.chatPreceedingItemGroupBox);
            this.ChatPanel.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.ChatPanel.Location = new System.Drawing.Point(3, 794);
            this.ChatPanel.Name = "ChatPanel";
            this.ChatPanel.Size = new System.Drawing.Size(711, 152);
            this.ChatPanel.TabIndex = 4;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.lblCurrentItem);
            this.panel2.Controls.Add(this.linkEQWire);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel2.Location = new System.Drawing.Point(0, 129);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(711, 23);
            this.panel2.TabIndex = 3;
            // 
            // lblCurrentItem
            // 
            this.lblCurrentItem.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblCurrentItem.Location = new System.Drawing.Point(3, 0);
            this.lblCurrentItem.Name = "lblCurrentItem";
            this.lblCurrentItem.Size = new System.Drawing.Size(416, 23);
            this.lblCurrentItem.TabIndex = 1;
            this.lblCurrentItem.Text = "No Item";
            this.lblCurrentItem.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // linkEQ2Wire
            // 
            this.linkEQWire.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.linkEQWire.AutoSize = true;
            this.linkEQWire.Location = new System.Drawing.Point(643, 5);
            this.linkEQWire.Name = "linkLabel5";
            this.linkEQWire.Size = new System.Drawing.Size(50, 13);
            this.linkEQWire.TabIndex = 0;
            this.linkEQWire.TabStop = true;
            this.linkEQWire.Text = "EQ2Wire";
            this.linkEQWire.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel5_LinkClicked);
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.itemDataGridStatusLabel);
            this.panel3.Controls.Add(this.itemGridView);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(3, 3);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(711, 781);
            this.panel3.TabIndex = 5;
            // 
            // itemDataGridStatusLabel
            // 
            this.itemDataGridStatusLabel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.itemDataGridStatusLabel.BackColor = System.Drawing.Color.Transparent;
            this.itemDataGridStatusLabel.Location = new System.Drawing.Point(3, 758);
            this.itemDataGridStatusLabel.Name = "itemDataGridStatusLabel";
            this.itemDataGridStatusLabel.Padding = new System.Windows.Forms.Padding(0, 5, 0, 0);
            this.itemDataGridStatusLabel.Size = new System.Drawing.Size(702, 23);
            this.itemDataGridStatusLabel.TabIndex = 1;
            this.itemDataGridStatusLabel.Text = "Idle";
            // 
            // itemGridView
            // 
            this.itemGridView.AllowUserToAddRows = false;
            this.itemGridView.AllowUserToDeleteRows = false;
            this.itemGridView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.itemGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.itemGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.columnTime,
            this.columnZone,
            this.columnPlayer,
            this.columnItemName,
            this.columnContainer,
            this.columnDKP,
            this.columnDKPSubmitResult});
            this.itemGridView.ContextMenuStrip = this.ItemsListViewContextMenuStrip;
            this.itemGridView.Location = new System.Drawing.Point(4, 28);
            this.itemGridView.Name = "itemGridView";
            this.itemGridView.Size = new System.Drawing.Size(701, 727);
            this.itemGridView.TabIndex = 0;
            this.itemGridView.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.itemGridView_CellClick);
            this.itemGridView.CellEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.itemGridView_CellClick);
            this.itemGridView.CellValidating += new System.Windows.Forms.DataGridViewCellValidatingEventHandler(this.itemGridView_CellValidating);
            this.itemGridView.CellValidating += new System.Windows.Forms.DataGridViewCellValidatingEventHandler(this.itemGridView_CellValidated);
            // 
            // columnTime
            // 
            this.columnTime.HeaderText = "Time";
            this.columnTime.Name = "columnTime";
            this.columnTime.ReadOnly = true;
            // 
            // columnZone
            // 
            this.columnZone.HeaderText = "Zone";
            this.columnZone.Name = "columnZone";
            this.columnZone.ReadOnly = true;
            // 
            // columnPlayer
            // 
            this.columnPlayer.HeaderText = "Player";
            this.columnPlayer.Name = "columnPlayer";
            // 
            // columnItemName
            // 
            this.columnItemName.HeaderText = "Item Name";
            this.columnItemName.Name = "columnItemName";
            this.columnItemName.ReadOnly = true;
            // 
            // columnContainer
            // 
            this.columnContainer.HeaderText = "Container";
            this.columnContainer.Name = "columnContainer";
            this.columnContainer.ReadOnly = true;
            // 
            // columnDKP
            // 
            this.columnDKP.HeaderText = "DKP";
            this.columnDKP.Name = "columnDKP";
            // 
            // wasSubmitted
            // 
            this.columnDKPSubmitResult.HeaderText = "Submit Result";
            this.columnDKPSubmitResult.Name = "columnResult";
            // 
            // tabs
            // 
            this.tabs.Controls.Add(this.itemsPage);
            this.tabs.Controls.Add(this.settingsPage);
            this.tabs.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabs.Location = new System.Drawing.Point(0, 0);
            this.tabs.Name = "tabControl1";
            this.tabs.SelectedIndex = 0;
            this.tabs.Size = new System.Drawing.Size(725, 975);
            this.tabs.TabIndex = 1;
            // 
            // itemsPage
            // 
            this.itemsPage.Controls.Add(this.panel3);
            this.itemsPage.Controls.Add(this.splitter1);
            this.itemsPage.Controls.Add(this.ChatPanel);
            this.itemsPage.Location = new System.Drawing.Point(4, 22);
            this.itemsPage.Name = "tabPage1";
            this.itemsPage.Padding = new System.Windows.Forms.Padding(3);
            this.itemsPage.Size = new System.Drawing.Size(717, 949);
            this.itemsPage.TabIndex = 0;
            this.itemsPage.Text = "Session Loot Logs";
            this.itemsPage.UseVisualStyleBackColor = true;
            // 
            // splitter1
            // 
            this.splitter1.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.splitter1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.splitter1.Location = new System.Drawing.Point(3, 784);
            this.splitter1.Name = "splitter1";
            this.splitter1.Size = new System.Drawing.Size(711, 10);
            this.splitter1.TabIndex = 6;
            this.splitter1.TabStop = false;
            // 
            // settingsPage
            // 
            this.settingsPage.AutoScroll = true;
            this.settingsPage.Controls.Add(this.otherSettingsGroupBox);
            this.settingsPage.Controls.Add(this.groupBox4);
            this.settingsPage.Controls.Add(this.groupBox3);
            this.settingsPage.Controls.Add(this.groupBox2);
            this.settingsPage.Location = new System.Drawing.Point(4, 22);
            this.settingsPage.Name = "settingsPage";
            this.settingsPage.Padding = new System.Windows.Forms.Padding(3);
            this.settingsPage.Size = new System.Drawing.Size(717, 949);
            this.settingsPage.TabIndex = 1;
            this.settingsPage.Text = "Settings";
            this.settingsPage.UseVisualStyleBackColor = true;
            // 
            // otherSettingsGroupBox
            // 
            this.otherSettingsGroupBox.Controls.Add(this.skipLottoCheckBox);
            this.otherSettingsGroupBox.Controls.Add(this.labeldkpSiteURL);
            this.otherSettingsGroupBox.Controls.Add(this.labeldkpSiteUser);
            this.otherSettingsGroupBox.Controls.Add(this.labeldkpSitePwd);
            this.otherSettingsGroupBox.Controls.Add(this.dkpSiteURL);
            this.otherSettingsGroupBox.Controls.Add(this.dkpSiteUser);
            this.otherSettingsGroupBox.Controls.Add(this.dkpSitePwd);
            this.otherSettingsGroupBox.Controls.Add(this.dkpDebugText);
            this.otherSettingsGroupBox.Location = new System.Drawing.Point(7, 375);
            this.otherSettingsGroupBox.Name = "otherSettingsGroupBox";
            this.otherSettingsGroupBox.Size = new System.Drawing.Size(704, 300);
            this.otherSettingsGroupBox.TabIndex = 4;
            this.otherSettingsGroupBox.TabStop = false;
            this.otherSettingsGroupBox.Text = "Other Settings";
            // 
            // skipLottoCheckBox
            // 
            this.skipLottoCheckBox.AutoSize = true;
            this.skipLottoCheckBox.Checked = true;
            this.skipLottoCheckBox.CheckState = CheckState.Checked;
            this.skipLottoCheckBox.Location = new System.Drawing.Point(7, 20);
            this.skipLottoCheckBox.Name = "skipLottoCheckBox";
            this.skipLottoCheckBox.Size = new System.Drawing.Size(80, 17);
            this.skipLottoCheckBox.TabIndex = 0;
            this.skipLottoCheckBox.Text = "Skip Lotto?";
            this.skipLottoCheckBox.UseVisualStyleBackColor = true;
            // labels
            this.labeldkpSiteURL.AutoSize = true;
            this.labeldkpSiteURL.Location = new System.Drawing.Point(7, 50);
            this.labeldkpSiteURL.Name = "labeldkpSiteURL";
            this.labeldkpSiteURL.Size = new System.Drawing.Size(120, 20);
            this.labeldkpSiteURL.TabIndex = 1;
            this.labeldkpSiteURL.Text = "DKP Site URL";
            this.labeldkpSiteUser.AutoSize = true;
            this.labeldkpSiteUser.Location = new System.Drawing.Point(7, 80);
            this.labeldkpSiteUser.Name = "labeldkpSiteUser";
            this.labeldkpSiteUser.Size = new System.Drawing.Size(120, 20);
            this.labeldkpSiteUser.TabIndex = 1;
            this.labeldkpSiteUser.Text = "DKP Site Username";
            this.labeldkpSitePwd.AutoSize = true;
            this.labeldkpSitePwd.Location = new System.Drawing.Point(7, 110);
            this.labeldkpSitePwd.Name = "labeldkpSitePwd";
            this.labeldkpSitePwd.Size = new System.Drawing.Size(120, 20);
            this.labeldkpSitePwd.TabIndex = 1;
            this.labeldkpSitePwd.Text = "DKP Site Password";
            // 
            // dkpSiteURL
            // 
            this.dkpSiteURL.Location = new System.Drawing.Point(137, 50);
            this.dkpSiteURL.Name = "dkpSiteURL";
            this.dkpSiteURL.Size = new System.Drawing.Size(400, 20);
            this.dkpSiteURL.TabIndex = 2;
            this.dkpSiteURL.Text = "https://www.aphadrim.com/proc/raid_anwesenheit_tool_neu.php";
            // 
            // dkpSiteUser
            // 
            this.dkpSiteUser.Location = new System.Drawing.Point(137, 80);
            this.dkpSiteUser.Name = "dkpSiteUser";
            this.dkpSiteUser.Size = new System.Drawing.Size(200, 20);
            this.dkpSiteUser.TabIndex = 2;
            this.dkpSiteUser.Text = "";
            // 
            // dkpSitePwd
            // 
            this.dkpSitePwd.Location = new System.Drawing.Point(137, 110);
            this.dkpSitePwd.Name = "dkpSitePwd";
            this.dkpSitePwd.Size = new System.Drawing.Size(200, 20);
            this.dkpSitePwd.TabIndex = 2;
            this.dkpSitePwd.Text = "";
            // 
            // dkpDebugText
            // 
            this.dkpDebugText.Location = new System.Drawing.Point(7, 140);
            this.dkpDebugText.Name = "dkpDebugText";
            this.dkpDebugText.Size = new System.Drawing.Size(400, 150);
            this.dkpDebugText.TabIndex = 2;
            this.dkpDebugText.Text = "";
            this.dkpDebugText.Multiline = true;
            this.dkpDebugText.AcceptsReturn = true;
            this.dkpDebugText.ScrollBars = ScrollBars.Vertical;
            this.dkpDebugText.ReadOnly = true;
            // 
            // groupBox4
            // 
            this.groupBox4.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox4.Controls.Add(this.label4);
            this.groupBox4.Controls.Add(this.GermanRadioButton);
            this.groupBox4.Controls.Add(this.EnglishRadioButton);
            this.groupBox4.Location = new System.Drawing.Point(7, 275);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(704, 100);
            this.groupBox4.TabIndex = 2;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Log Input Language";
            // 
            // label4
            // 
            this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label4.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label4.Location = new System.Drawing.Point(377, 16);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(321, 78);
            this.label4.TabIndex = 1;
            this.label4.Text = @"Loot parsing speed can be greatly increased by temporarily disabling ACT's combat parsing." + Environment.NewLine + Environment.NewLine +
                               @"To do this, in the Plugins tab, uncheck ACT_English_Parser or similar.";
            // 
            // GermanRadioButton
            // 
            this.GermanRadioButton.AutoSize = true;
            this.GermanRadioButton.Location = new System.Drawing.Point(6, 38);
            this.GermanRadioButton.Margin = new System.Windows.Forms.Padding(3, 1, 3, 1);
            this.GermanRadioButton.Name = "GermanRadioButton";
            this.GermanRadioButton.Size = new System.Drawing.Size(181, 17);
            this.GermanRadioButton.TabIndex = 0;
            this.GermanRadioButton.TabStop = true;
            this.GermanRadioButton.Text = "German (Credit to Mayriia@Valor)";
            this.GermanRadioButton.UseVisualStyleBackColor = true;
            this.GermanRadioButton.CheckedChanged += new System.EventHandler(this.GermanRadioButton_CheckedChanged);
            // 
            // EnglishRadioButton
            // 
            this.EnglishRadioButton.AutoSize = true;
            this.EnglishRadioButton.Checked = true;
            this.EnglishRadioButton.Location = new System.Drawing.Point(6, 19);
            this.EnglishRadioButton.Margin = new System.Windows.Forms.Padding(3, 1, 3, 1);
            this.EnglishRadioButton.Name = "EnglishRadioButton";
            this.EnglishRadioButton.Size = new System.Drawing.Size(59, 17);
            this.EnglishRadioButton.TabIndex = 0;
            this.EnglishRadioButton.TabStop = true;
            this.EnglishRadioButton.Text = "English";
            this.EnglishRadioButton.UseVisualStyleBackColor = true;
            this.EnglishRadioButton.CheckedChanged += new System.EventHandler(this.EnglishRadioButton_CheckedChanged);
            // 
            // groupBox3
            // 
            this.groupBox3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox3.Controls.Add(this.label3);
            this.groupBox3.Controls.Add(this.namedChatTextBox);
            this.groupBox3.Controls.Add(this.removeNamedChatButton);
            this.groupBox3.Controls.Add(this.addNamedChatButton);
            this.groupBox3.Controls.Add(this.lbNamedChat);
            this.groupBox3.Location = new System.Drawing.Point(6, 132);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(705, 137);
            this.groupBox3.TabIndex = 1;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Preceeding Chat Channels";
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label3.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label3.Location = new System.Drawing.Point(378, 16);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(321, 115);
            this.label3.TabIndex = 5;
            this.label3.Text = @"Enter the chat channels to attach to a looted item." + Environment.NewLine +
                               @"These listings may be partial matches to the chat channel identifier." + Environment.NewLine + Environment.NewLine +
                               @"IE: ""raid"" will match identifiers, ""say to the raid party"" and ""says to the raid party""";
            // 
            // namedChatTextBox
            // 
            this.namedChatTextBox.Location = new System.Drawing.Point(207, 111);
            this.namedChatTextBox.Name = "namedChatTextBox";
            this.namedChatTextBox.Size = new System.Drawing.Size(165, 20);
            this.namedChatTextBox.TabIndex = 3;
            // 
            // removeNamedChatButton
            // 
            this.removeNamedChatButton.Location = new System.Drawing.Point(207, 44);
            this.removeNamedChatButton.Name = "removeNamedChatButton";
            this.removeNamedChatButton.Size = new System.Drawing.Size(165, 23);
            this.removeNamedChatButton.TabIndex = 4;
            this.removeNamedChatButton.Text = "Remove Channel";
            this.removeNamedChatButton.UseVisualStyleBackColor = true;
            this.removeNamedChatButton.Click += new System.EventHandler(this.btnRemoveNamedChat_Click);
            // 
            // addNamedChatButton
            // 
            this.addNamedChatButton.Location = new System.Drawing.Point(207, 19);
            this.addNamedChatButton.Name = "addNamedChatButton";
            this.addNamedChatButton.Size = new System.Drawing.Size(165, 23);
            this.addNamedChatButton.TabIndex = 4;
            this.addNamedChatButton.Text = "Add Channel";
            this.addNamedChatButton.UseVisualStyleBackColor = true;
            this.addNamedChatButton.Click += new System.EventHandler(this.btnAddNamedChat_Click);
            // 
            // lbNamedChat
            // 
            this.lbNamedChat.FormattingEnabled = true;
            this.lbNamedChat.IntegralHeight = false;
            this.lbNamedChat.Location = new System.Drawing.Point(6, 19);
            this.lbNamedChat.Name = "lbNamedChat";
            this.lbNamedChat.Size = new System.Drawing.Size(195, 112);
            this.lbNamedChat.TabIndex = 2;
            this.lbNamedChat.SelectedIndexChanged += new System.EventHandler(this.lbNamedChat_SelectedIndexChanged);
            this.lbNamedChat.Items.Add("RFApha");
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.numberOfLinesToRetainPerChannelLabel);
            this.groupBox2.Controls.Add(this.numberOfSecondsToRecordChatPrecedingItemLabel);
            this.groupBox2.Controls.Add(this.numberOfChatLinesSelector);
            this.groupBox2.Controls.Add(this.numberOfChatSecondsSelector);
            this.groupBox2.Controls.Add(this.chatLinesCheckBox);
            this.groupBox2.Controls.Add(this.chatSecondsCheckBox);
            this.groupBox2.Location = new System.Drawing.Point(6, 6);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(705, 120);
            this.groupBox2.TabIndex = 0;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Preceeding Chat Limits";
            // 
            // numberOfLinesToRetainPerChannelLabel
            // 
            this.numberOfLinesToRetainPerChannelLabel.AutoSize = true;
            this.numberOfLinesToRetainPerChannelLabel.Location = new System.Drawing.Point(88, 94);
            this.numberOfLinesToRetainPerChannelLabel.Name = "numberOfLinesToRetainPerChannelLabel";
            this.numberOfLinesToRetainPerChannelLabel.Size = new System.Drawing.Size(180, 13);
            this.numberOfLinesToRetainPerChannelLabel.TabIndex = 2;
            this.numberOfLinesToRetainPerChannelLabel.Text = "Number of lines to retain per channel";
            // 
            // numberOfSecondsToRecordChatPrecedingItemLabel
            // 
            this.numberOfSecondsToRecordChatPrecedingItemLabel.AutoSize = true;
            this.numberOfSecondsToRecordChatPrecedingItemLabel.Location = new System.Drawing.Point(88, 45);
            this.numberOfSecondsToRecordChatPrecedingItemLabel.Name = "numberOfSecondsToRecordChatPrecedingItemLabel";
            this.numberOfSecondsToRecordChatPrecedingItemLabel.Size = new System.Drawing.Size(255, 13);
            this.numberOfSecondsToRecordChatPrecedingItemLabel.TabIndex = 2;
            this.numberOfSecondsToRecordChatPrecedingItemLabel.Text = "Number of seconds to record chat preceding an item";
            // 
            // numberOfChatLinesSelector
            // 
            this.numberOfChatLinesSelector.Location = new System.Drawing.Point(27, 91);
            this.numberOfChatLinesSelector.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            this.numberOfChatLinesSelector.Name = "numberOfChatLinesSelector";
            this.numberOfChatLinesSelector.Size = new System.Drawing.Size(55, 20);
            this.numberOfChatLinesSelector.TabIndex = 1;
            this.numberOfChatLinesSelector.Value = new decimal(new int[] { 20, 0, 0, 0 });
            // 
            // numberOfChatSecondsSelector
            // 
            this.numberOfChatSecondsSelector.Location = new System.Drawing.Point(27, 42);
            this.numberOfChatSecondsSelector.Maximum = new decimal(new int[] { 600, 0, 0, 0 });
            this.numberOfChatSecondsSelector.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            this.numberOfChatSecondsSelector.Name = "numberOfChatSecondsSelector";
            this.numberOfChatSecondsSelector.Size = new System.Drawing.Size(55, 20);
            this.numberOfChatSecondsSelector.TabIndex = 1;
            this.numberOfChatSecondsSelector.Value = new decimal(new int[] { 60, 0, 0, 0 });
            // 
            // chatLinesCheckBox
            // 
            this.chatLinesCheckBox.AutoSize = true;
            this.chatLinesCheckBox.Checked = true;
            this.chatLinesCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chatLinesCheckBox.Location = new System.Drawing.Point(6, 68);
            this.chatLinesCheckBox.Name = "chatLinesCheckBox";
            this.chatLinesCheckBox.Size = new System.Drawing.Size(222, 17);
            this.chatLinesCheckBox.TabIndex = 0;
            this.chatLinesCheckBox.Text = "Keep chat by number of lines per channel";
            this.chatLinesCheckBox.UseVisualStyleBackColor = true;
            // 
            // chatSecondsCheckBox
            // 
            this.chatSecondsCheckBox.AutoSize = true;
            this.chatSecondsCheckBox.Checked = true;
            this.chatSecondsCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chatSecondsCheckBox.Location = new System.Drawing.Point(6, 19);
            this.chatSecondsCheckBox.Name = "chatSecondsCheckBox";
            this.chatSecondsCheckBox.Size = new System.Drawing.Size(111, 17);
            this.chatSecondsCheckBox.TabIndex = 0;
            this.chatSecondsCheckBox.Text = "Keep chat by time";
            this.chatSecondsCheckBox.UseVisualStyleBackColor = true;
            // 
            // HalfSecondTimer
            // 
            this.HalfSecondTimer.Enabled = true;
            this.HalfSecondTimer.Interval = 500;
            this.HalfSecondTimer.Tick += new System.EventHandler(this.tmrSec_Tick);
            // 
            // ClearButton
            // 
            this.ClearButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.ClearButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ClearButton.Location = new System.Drawing.Point(653, -1);
            this.ClearButton.Name = "ClearButton";
            this.ClearButton.Size = new System.Drawing.Size(68, 20);
            this.ClearButton.TabIndex = 1;
            this.ClearButton.Text = "Clear";
            this.ClearButton.UseVisualStyleBackColor = true;
            this.ClearButton.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // LootTally
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.ClearButton);
            this.Controls.Add(this.tabs);
            this.Name = "Aphadrim Loot Tracker";
            this.Size = new System.Drawing.Size(725, 975);
            this.ItemsListViewContextMenuStrip.ResumeLayout(false);
            this.ChatListViewContextMenuStrip.ResumeLayout(false);
            this.chatPreceedingItemGroupBox.ResumeLayout(false);
            this.ChatPanel.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.itemGridView)).EndInit();
            this.tabs.ResumeLayout(false);
            this.itemsPage.ResumeLayout(false);
            this.settingsPage.ResumeLayout(false);
            this.otherSettingsGroupBox.ResumeLayout(false);
            this.otherSettingsGroupBox.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numberOfChatLinesSelector)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numberOfChatSecondsSelector)).EndInit();
            this.ResumeLayout(false);

        }

        private ToolStripMenuItem copyAsPlainTexToolStripMenuItem;
        private ToolStripMenuItem submitToDKPSite;
        private System.Windows.Forms.GroupBox chatPreceedingItemGroupBox;
        private Panel ChatPanel;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.ColumnHeader chatDateTimeColumnHeader;
        private System.Windows.Forms.ColumnHeader chatChannelColumnHeader;
        private System.Windows.Forms.ColumnHeader chatPlayerColumnHeader;
        private System.Windows.Forms.TabControl tabs;
        private System.Windows.Forms.TabPage itemsPage;
        private Splitter splitter1;
        private System.Windows.Forms.TabPage settingsPage;
        private System.Windows.Forms.ListView ChatListView;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label numberOfSecondsToRecordChatPrecedingItemLabel;
        private System.Windows.Forms.Label numberOfLinesToRetainPerChannelLabel;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Button removeNamedChatButton;
        private System.Windows.Forms.Button addNamedChatButton;
        private System.Windows.Forms.TextBox namedChatTextBox;
        private System.Windows.Forms.NumericUpDown numberOfChatSecondsSelector;
        private System.Windows.Forms.CheckBox chatSecondsCheckBox;
        private System.Windows.Forms.NumericUpDown numberOfChatLinesSelector;
        private System.Windows.Forms.CheckBox chatLinesCheckBox;
        private System.Windows.Forms.ListBox lbNamedChat;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Timer HalfSecondTimer;
        private ContextMenuStrip ItemsListViewContextMenuStrip;
        private ContextMenuStrip ChatListViewContextMenuStrip;
        private ToolStripMenuItem copyActualLogLinesToolStripMenuItem;
        private GroupBox groupBox4;
        private Label label4;
        private RadioButton GermanRadioButton;
        private RadioButton EnglishRadioButton;
        private ToolStripMenuItem copyItemLinkToolStripMenuItem;
        private ToolStripMenuItem copyItemNameToolStripMenuItem;
        private LinkLabel linkEQWire;
        private Label lblCurrentItem;
        private Button ClearButton;
        private DataGridView itemGridView;
        private DataGridViewTextBoxColumn columnTime;
        private DataGridViewTextBoxColumn columnZone;
        private DataGridViewTextBoxColumn columnPlayer;
        private DataGridViewTextBoxColumn columnItemName;
        private DataGridViewTextBoxColumn columnContainer;
        private DataGridViewTextBoxColumn columnDKP;
        private DataGridViewTextBoxColumn columnDKPSubmitResult;
        private GroupBox otherSettingsGroupBox;
        private CheckBox skipLottoCheckBox;
        private Label labeldkpSiteURL;
        private Label labeldkpSiteUser;
        private Label labeldkpSitePwd;
        private TextBox dkpSiteURL;
        private TextBox dkpSiteUser;
        private TextBox dkpSitePwd;
        private TextBox dkpDebugText;
        private ToolStripMenuItem copyTallyAsPlainTextToolStripMenuItem;
        private System.Windows.Forms.ColumnHeader chatTextColumnHeader;

        public AphaLootTracker()
        {
            this.InitializeComponent();
        }

        /// <summary>String for a timestamp in the log, used in regular expression construction.</summary>
        private const string logTimeStampRegexStr = @"\(\d{10}\)\[.{24}\] ";
        private const string germanYou = "Ihr";
        private const string englishYou = "You";
        private string lootVerb;
        private string persona;
        private List<LootedItemData> lootedItems = new List<LootedItemData>();
        private Dictionary<string, List<ChatLine>> chatCollections = new Dictionary<string, List<ChatLine>>();
        public Label lblPluginStatus;

        /// <summary>Settings serializer, for storing plugin settings.</summary>
        private SettingsSerializer xmlSettings;

        /// <summary>Regular Expression for an item.</summary>
        private static Regex regexItem = new Regex(@"\\aITEM (?<itemid>-?\d+) [-\d ]+:(?<itemname>[^\\]+)\\/a", RegexOptions.Compiled);

        /// <summary>Regular Expression for looting - assigned a value from the language-specific regexLoot* fields.</summary>
        private Regex regexLoot;

        /// <summary>Regular Expression for looting - English language.</summary>
        private Regex regexLootEng = new Regex(logTimeStampRegexStr + @"(?<looter>\w+) (?<chance>loots?|wins? the lotto for) \\aITEM (?<itemid>-?\d+) [-\d ]+:(?<itemname>[^\\]+)\\/a from (?<container>.+)\.", RegexOptions.Compiled);

        /// <summary>Regular Expression for looting - German language.</summary>
        private Regex regexLootGer = new Regex(logTimeStampRegexStr + @"(?<looter>\w+) (?<chance>erbeutet:?|.+Lotterie gewonnen -) \\aITEM (?<itemid>-?\d+) [-\d ]+:(?<itemname>[^\\]+)\\/a( aus:| von:) (?<container>.+)\.", RegexOptions.Compiled);

        /// <summary>Regular Expression for zoning.</summary>
        private Regex regexZone;

        /// <summary>Regular Expression for zoning - English.</summary>
        private Regex regexZoneEng = new Regex(logTimeStampRegexStr + @"You have entered (?::.+?:)?(?<zone>.+)\.", RegexOptions.Compiled);

        /// <summary>Regular Expression for chat text.</summary>
        private Regex regexChat;

        /// <summary>Regular Expression for chat text - English.</summary>
        private Regex regexChatEng = new Regex(logTimeStampRegexStr + @"(?:(?<speaker>" + englishYou + @")|\\aPC -?\d+ (?<speaker>\w+):\w+\\/a) (?<channel>.+?), ?""(?<text>.+)""", RegexOptions.Compiled);

        /// <summary>Regular Expression for chat text - German.</summary>
        private Regex regexChatGer = new Regex(logTimeStampRegexStr + @"(?:(?<speaker>" + germanYou + @")|\\aPC -?\d+ (?<speaker>\w+):\w+\\.a) (?<channel>.+?), ?""(?<text>.+)""", RegexOptions.Compiled);

        public void InitPlugin(TabPage pluginScreenSpace, Label pluginStatusText)
        {
            this.lblPluginStatus = pluginStatusText;
            this.SetParseLang(0);
            this.xmlSettings = new SettingsSerializer(this);
            this.LoadSettings();
            pluginScreenSpace.Controls.Add(this);
            this.Dock = DockStyle.Fill;
            ActGlobals.oFormActMain.OnLogLineRead += new LogLineEventDelegate(this.oFormActMain_OnLogLineRead);
        }

        /// <summary>
        /// Loads Settings from configuration.
        /// </summary>
        private void LoadSettings()
        {
            // TODO remove or restore; for now settings are fixed
            // this.xmlSettings.AddControlSetting(this.chatLinesCheckBox.Name, this.chatLinesCheckBox);
            // this.xmlSettings.AddControlSetting(this.chatSecondsCheckBox.Name, this.chatSecondsCheckBox);
            // this.xmlSettings.AddControlSetting(this.numberOfChatLinesSelector.Name, this.numberOfChatLinesSelector);
            // this.xmlSettings.AddControlSetting(this.numberOfChatSecondsSelector.Name, this.numberOfChatSecondsSelector);
            this.xmlSettings.AddControlSetting(this.lbNamedChat.Name, this.lbNamedChat);
            this.xmlSettings.AddControlSetting(this.ChatPanel.Name, this.ChatPanel);
            this.xmlSettings.AddControlSetting(this.EnglishRadioButton.Name, this.EnglishRadioButton);
            this.xmlSettings.AddControlSetting(this.GermanRadioButton.Name, this.GermanRadioButton);
            this.xmlSettings.AddControlSetting(this.skipLottoCheckBox.Name, this.skipLottoCheckBox);
            this.xmlSettings.AddControlSetting(this.dkpSiteURL.Name, this.dkpSiteURL);
            this.xmlSettings.AddControlSetting(this.dkpSiteUser.Name, this.dkpSiteUser);
            this.xmlSettings.AddControlSetting(this.dkpSitePwd.Name, this.dkpSitePwd);
            DirectoryInfo appDataFolder = new DirectoryInfo(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\Advanced Combat Tracker");
            FileInfo configFile = new FileInfo(appDataFolder.FullName + "\\Config\\LootParsing.config.xml");
            if (configFile.Exists)
            {
                FileStream fs = new FileStream(configFile.FullName, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
                XmlTextReader xmlReader = new XmlTextReader(fs);
                while (xmlReader.Read())
                {
                    if (xmlReader.NodeType == XmlNodeType.Element && xmlReader.Name == "Config")
                    {
                        break;
                    }
                }
                this.xmlSettings.ImportFromXml(xmlReader);
                xmlReader.Close();
            }
        }

        public void DeInitPlugin()
        {
            ActGlobals.oFormActMain.OnLogLineRead -= this.oFormActMain_OnLogLineRead;
            this.SaveSettings();
        }

        /// <summary>
        /// Saves settings to configuration file.
        /// </summary>
        private void SaveSettings()
        {
            DirectoryInfo configFolder = new DirectoryInfo(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\Advanced Combat Tracker");
            FileInfo configFile = new FileInfo(configFolder.FullName + "\\Config\\LootParsing.config.xml");
            FileStream fs = new FileStream(configFile.FullName, FileMode.Create, FileAccess.Write, FileShare.ReadWrite);
            XmlTextWriter xmlWriter = new XmlTextWriter(fs, Encoding.UTF8);
            xmlWriter.Formatting = Formatting.Indented;
            xmlWriter.Indentation = 1;
            xmlWriter.IndentChar = '\t';
            xmlWriter.WriteStartDocument(true);
            xmlWriter.WriteStartElement("Config");
            this.xmlSettings.ExportToXml(xmlWriter);
            xmlWriter.WriteEndElement();
            xmlWriter.WriteEndDocument();
            xmlWriter.Flush();
            xmlWriter.Close();
        }

        public void SetParseLang(int language)
        {
            switch (language)
            {
                case 1: // German
                    this.regexChat = this.regexChatGer;
                    this.regexLoot = this.regexLootGer;
                    this.regexZone = this.regexZoneEng;
                    this.lootVerb = "erbeutet";
                    this.persona = germanYou;
                    this.lblPluginStatus.Text = "Language set to German.";
                    break;
                default: // English
                    this.regexChat = this.regexChatEng;
                    this.regexLoot = this.regexLootEng;
                    this.regexZone = this.regexZoneEng;
                    this.lootVerb = "loot";
                    this.persona = englishYou;
                    this.lblPluginStatus.Text = "Language set to English.";
                    break;
            }
        }

        void oFormActMain_OnLogLineRead(bool isImport, LogLineEventArgs logInfo)
        {
            if (this.regexZone.IsMatch(logInfo.logLine))
            {
                Match match = this.regexZone.Match(logInfo.logLine);
                ActGlobals.oFormActMain.CurrentZone = match.Groups["zone"].Value;
            }
            if (logInfo.detectedType != 0)
            {
                return;
            }
            if (this.regexChat.IsMatch(logInfo.logLine))
            {
                Match match = this.regexChat.Match(logInfo.logLine);
                string channel = match.Groups["channel"].Value;
                bool speakerIsSelf = false;
                string speaker = string.Empty;
                string text = string.Empty;

                foreach (string s in this.lbNamedChat.Items)
                {
                    if (channel.ToUpper().Contains(s.ToUpper()))
                    {
                        speakerIsSelf = match.Groups["speaker"].Value == this.persona;
                        speaker = speakerIsSelf ? ActGlobals.charName : match.Groups["speaker"].Value;
                        text = match.Groups["text"].Value;
                        if (!this.chatCollections.ContainsKey(s.ToUpper()))
                        {
                            this.chatCollections.Add(s.ToUpper(), new List<ChatLine>());
                        }
                        var chatline = new ChatLine(logInfo.detectedTime, logInfo.logLine, channel, speaker, text);
                        this.chatCollections[s.ToUpper()].Add(chatline);
                        break;
                    }
                }
            }
            else if (this.regexLoot.IsMatch(logInfo.logLine))
            {
                Match match = this.regexLoot.Match(logInfo.logLine);
                bool lottoWin = false;
                string container = string.Empty;
                if (match.Groups["chance"].Value.StartsWith(this.lootVerb))
                {
                    container = match.Groups["container"].Value;
                }
                else
                {
                    container = match.Groups["container"].Value + " (lotto)";
                    lottoWin = true;
                }
                if (skipLottoCheckBox.Checked && lottoWin)
                {
                    return;
                }
                if (container.Contains("corpse"))
                {
                    return;
                }
                string looter = match.Groups["looter"].Value == this.persona ? ActGlobals.charName : match.Groups["looter"].Value;
                LootedItemData item = new LootedItemData(logInfo.detectedTime, looter, match.Groups["itemname"].Value, match.Groups["itemid"].Value, container, logInfo.detectedZone, logInfo.logLine);

                if (this.chatLinesCheckBox.Checked)
                {
                    int count = (int)this.numberOfChatLinesSelector.Value;
                    foreach (KeyValuePair<string, List<ChatLine>> pair in this.chatCollections)
                    {
                        if (pair.Value.Count <= count)
                        {
                            item.Chat.AddRange(pair.Value);
                        }
                        else
                        {
                            for (int i = pair.Value.Count - 1; i >= pair.Value.Count - count; i--)
                            {
                                item.Chat.Add(pair.Value[i]);
                            }
                        }
                    }
                }
                if (this.chatSecondsCheckBox.Checked)
                {
                    int timeLimit = (int)this.numberOfChatSecondsSelector.Value;
                    foreach (KeyValuePair<string, List<ChatLine>> pair in this.chatCollections)
                    {
                        for (int i = pair.Value.Count - 1; i >= 0; i--)
                        {
                            ChatLine cl = pair.Value[i];
                            if (logInfo.detectedTime - cl.Time <= TimeSpan.FromSeconds(timeLimit))
                            {
                                item.Chat.Add(cl);
                            }
                            else
                            {
                                break;
                            }
                        }
                    }
                }
                item.Chat.Sort();
                for (int i = item.Chat.Count - 1; i > 0; i--)
                {
                    if (item.Chat[i].FullLine == item.Chat[i - 1].FullLine)
                    {
                        item.Chat.RemoveAt(i);
                    }
                }
                item.guessDKP();
                this.dkpDebugText.Text += "Auction: " + item.ItemName + " for " + item.DKP + Environment.NewLine;
                this.lootedItems.Add(item);
                this.refreshLvItems = true;
            }
        }

        #region Events
        private void lbNamedChat_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.lbNamedChat.SelectedIndex != -1)
            {
                this.namedChatTextBox.Text = (string)this.lbNamedChat.Items[this.lbNamedChat.SelectedIndex];
            }
        }

        private void btnAddNamedChat_Click(object sender, EventArgs e)
        {
            if (!this.lbNamedChat.Items.Contains(this.namedChatTextBox.Text) && !string.IsNullOrEmpty(this.namedChatTextBox.Text))
            {
                this.lbNamedChat.Items.Add(this.namedChatTextBox.Text);
            }
        }

        private void btnRemoveNamedChat_Click(object sender, EventArgs e)
        {
            if (this.lbNamedChat.SelectedIndex != -1)
            {
                this.lbNamedChat.Items.RemoveAt(this.lbNamedChat.SelectedIndex);
            }
        }

        private void itemGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (this.itemGridView.SelectedCells.Count > 0)
            {
                DataGridViewRow lvi = this.itemGridView.SelectedCells[0].OwningRow;
                LootedItemData item = (LootedItemData)lvi.Tag;
                this.lblCurrentItem.Text = item.ItemName;
            }
            else
            {
                this.lblCurrentItem.Text = "No Item";
            }
            this.SortChatListView();
            this.RepopulateChatListView();
        }

        private void itemGridView_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            if (this.itemGridView.SelectedCells.Count > 0)
            {
                string headerText = this.itemGridView.Columns[e.ColumnIndex].HeaderText;
                if (headerText == "DKP")
                {
                    int dkp = 0;
                    if (!int.TryParse(e.FormattedValue.ToString(), out dkp))
                    {
                        e.Cancel = true;
                        return;
                    }
                }
            }
        }

        private void itemGridView_CellValidated(object sender, DataGridViewCellValidatingEventArgs e)
        {
            if (this.itemGridView.SelectedCells.Count > 0)
            {
                string columnName = this.itemGridView.Columns[e.ColumnIndex].Name;
                DataGridViewRow lvi = this.itemGridView.SelectedCells[0].OwningRow;
                LootedItemData item = (LootedItemData)lvi.Tag;
                if (columnName == "columnDKP")
                {
                    int dkp = int.Parse(e.FormattedValue.ToString());
                    item.DKP = dkp.ToString();
                }
                else if (columnName == "columnPlayer")
                {
                    item.Player = e.FormattedValue.ToString();
                }
                else if (columnName == "columnResult")
                {
                    item.DKPSubmitResult = e.FormattedValue.ToString();
                }
            }
        }

        private void linkLabel5_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (itemGridView.SelectedCells.Count > 0)
            {
                DataGridViewRow itemRow = itemGridView.SelectedCells[0].OwningRow;
                LootedItemData item = (LootedItemData)itemRow.Tag;
                int itemIdForLink = 0;
                if (int.TryParse(item.ItemId, out itemIdForLink))
                {
                    Process.Start("http://u.eq2wire.com/item/index/" + ((uint)itemIdForLink).ToString());
                }
            }
        }

        private void lvChat_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            if (this.lvChatSorting == e.Column)
            {
                this.lvChatReverse = !this.lvChatReverse;
            }

            this.lvChatSorting = e.Column;
            this.SortChatListView();
            this.RepopulateChatListView();
        }

        private void tmrSec_Tick(object sender, EventArgs e)
        {
            if (this.refreshLvItems)
            {
                this.refreshLvItems = false;
                this.RepopulateItemDataGridview();
            }
        }

        private void submitToDKPSite_Click(object sender, EventArgs e)
        {
            if (this.dkpSiteURL.Text == "")
                throw new System.ArgumentException("Missing URL for DKP Website", "empty URL");
            if (!this.cookiesInitialized) {
                if (this.dkpSiteUser.Text == "")
                    throw new System.ArgumentException("Missing username for DKP Website", "empty username");
                if (this.dkpSitePwd.Text == "")
                    throw new System.ArgumentException("Missing password for DKP Website", "empty password");
                if (this.httpClient != null)
                    this.httpClient.Dispose();
                this.httpClient = new HttpClient();
                var websiteURI = new Uri(this.dkpSiteURL.Text);
                var baseUri = new Uri(websiteURI.GetLeftPart(System.UriPartial.Authority));
                this.httpClient.BaseAddress = baseUri;
                var login_values = new Dictionary<string, string> {
                    { "username", this.dkpSiteUser.Text },
                    { "password", this.dkpSitePwd.Text },
                    { "formular", "0" },
                    { "formular2", "0" },
                    { "formular_eb", "0" },
                    { "formular_rz", "0" },
                    { "formular_auth", "1" },
                    { "formular_boss", "0" },
                    { "formular_dkp", "0" },
                    { "formular_loot", "0" },
                    { "formular_progr", "0" },
                    { "login", "Anmelden" }
                    };
                var login_content = new FormUrlEncodedContent(login_values);
                var login_response = this.httpClient.PostAsync(this.dkpSiteURL.Text, login_content);
                login_response.Wait();
                var login_code = (int) login_response.Result.StatusCode;
                var login_result_str = login_code + " " + login_response.Result.StatusCode.ToString() + " (" + login_response.Result.ReasonPhrase + ")";
                this.dkpDebugText.Text += "Login result: " + login_result_str + Environment.NewLine;
                // TODO not sure if there is session timeout, so lets login each time for now
                //this.cookiesInitialized = true;
            }
            List<DataGridViewRow> selectedDataGridRows = GetSelectedItemRows();
            foreach (DataGridViewRow row in selectedDataGridRows)
            {
                LootedItemData item = (LootedItemData) row.Tag;
                byte[] encodedBytes = System.Text.ASCIIEncoding.ASCII.GetBytes(item.ItemLink);
                var ItemNameBase64 = Convert.ToBase64String(encodedBytes);
                var loot_values = new Dictionary<string, string> {
                    { "dkp_loot_name", item.ItemLink },
                    { "dkp_loot_name_base64", ItemNameBase64 },
                    { "name_user_loot", item.Player },
                    { "dkp_loot_wert", item.DKP },
                    { "loot_index", "0" },
                    { "formular2", "0" },
                    { "formular", "0" },
                    { "formular_eb", "0" },
                    { "formular_rz", "0" },
                    { "formular_boss", "0" },
                    { "formular_dkp", "0" },
                    { "formular_loot", "1" },
                    { "formular_progr", "0" },
                    { "dkp_loot_submit", "Loot/DKP-Eintragen" }
                    };
                var loot_content = new FormUrlEncodedContent(loot_values);
                var loot_response = this.httpClient.PostAsync(this.dkpSiteURL.Text, loot_content);
                loot_response.Wait();
                var loot_code = (int) loot_response.Result.StatusCode;
                var loot_result_str = loot_code + " " + loot_response.Result.StatusCode.ToString() + " (" + loot_response.Result.ReasonPhrase + ")";
                this.dkpDebugText.Text += "Loot submit result for [" + item.ItemName + "]: " + loot_result_str + Environment.NewLine;
                item.DKPSubmitResult = loot_response.Result.ReasonPhrase + " at " + DateTime.Now;
            }
            this.RepopulateItemDataGridview();
            this.RepopulateChatListView();
        }

        private void copyItemLinkToolStripMenuItem_Click(object sender, EventArgs e)
        {
            StringBuilder sb = new StringBuilder();
            List<DataGridViewRow> selectedDataGridRows = GetSelectedItemRows();
            foreach (DataGridViewRow row in selectedDataGridRows)
            {
                LootedItemData item = (LootedItemData)row.Tag;
                sb.AppendLine(item.ItemLink);
            }
            ActGlobals.oFormActMain.SendToClipboard(sb.ToString().Trim(new char[] { '\r', '\n' }), true);
        }

        private void copyItemNameToolStripMenuItem_Click(object sender, EventArgs e)
        {
            StringBuilder sb = new StringBuilder();
            List<DataGridViewRow> selectedDataGridRows = GetSelectedItemRows();
            foreach (DataGridViewRow row in selectedDataGridRows)
            {
                LootedItemData item = (LootedItemData)row.Tag;
                sb.AppendLine(item.ItemName);
            }
            ActGlobals.oFormActMain.SendToClipboard(sb.ToString().Trim(new char[] { '\r', '\n' }), true);
        }

        private void copyActualLogLinesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < ChatListView.Items.Count; i++)
            {
                if (ChatListView.SelectedIndices.Count == 0 | ChatListView.SelectedIndices.Contains(i))
                {
                    ChatLine cl = (ChatLine)ChatListView.Items[i].Tag;
                    sb.AppendLine(cl.FullLine);
                }
            }
            ActGlobals.oFormActMain.SendToClipboard(sb.ToString(), true);
        }

        private void EnglishRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            if (this.EnglishRadioButton.Checked)
            {
                this.SetParseLang(0);
            }
        }

        private void GermanRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            if (this.GermanRadioButton.Checked)
            {
                this.SetParseLang(1);
            }
        }

        private void ChatListView_Resize(object sender, EventArgs e)
        {
            ActGlobals.oFormActMain.ResizeLVCols(this.ChatListView);
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            this.lootedItems.Clear();
            this.RepopulateItemDataGridview();
        }

        private void copyAsPlainTexToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string result = GetPlainTextForDataGridSelection(itemGridView);
            ActGlobals.oFormActMain.SendToClipboard(result, true);
        }

        #endregion

        #region Internal Classes
        internal class ChatLine : IEquatable<ChatLine>, IComparable<ChatLine>
        {
            public DateTime Time { get; set; }
            public string FullLine { get; set; }
            public string Channel { get; set; }
            public string Player { get; set; }
            public string Text { get; set; }

            public ChatLine(DateTime time, string fullLine, string channel, string player, string text)
            {
                this.Time = time;
                this.FullLine = fullLine;
                this.Channel = channel;
                this.Text = regexItem.Replace(text, "$2");

                if (player == "You")
                {
                    player = ActGlobals.charName;
                }
                this.Player = player;
            }

            public override string ToString()
            {
                return this.FullLine;
            }

            public bool Equals(ChatLine other)
            {
                return this.ToString().Equals(other.ToString());
            }

            public override bool Equals(object obj)
            {
                ChatLine r = (ChatLine)obj;
                return this.ToString().Equals(r.ToString());
            }

            public override int GetHashCode()
            {
                return this.FullLine.GetHashCode();
            }

            public int CompareTo(ChatLine other)
            {
                return this.Time.CompareTo(other.Time);
            }

            public class Sorter : IComparer<ChatLine>
            {
                int sortColumn = 0;

                public Sorter(int SortColumn)
                {
                    this.sortColumn = SortColumn;
                }

                public int Compare(ChatLine x, ChatLine y)
                {
                    switch (this.sortColumn)
                    {
                        case 0: // Datetime
                            return x.Time.CompareTo(y.Time);
                        case 1: // Channel
                            return x.Channel.CompareTo(y.Channel);
                        case 2: // Player
                            return x.Player.CompareTo(y.Player);
                        case 3: // Text
                            return x.Text.CompareTo(y.Text);
                        default:
                            return 0;
                    }
                }
            }
        }

        internal class LootedItemData
        {
            private static Regex regexBid = new Regex(@"[^\d]*(?<bid>[\d,.]+)(?<platbid>[kK]+)?[^\d]*", RegexOptions.Compiled);

            public string Player { get; set; }

            public string ItemName { get; set; }

            public string ItemId { get; set; }

            public string Container { get; set; }

            public string Zone { get; set; }

            public string LogLine { get; set; }

            public string ItemLink { get; set; }

            public string DKP { get; set; }

            public string DKPSubmitResult { get; set; }

            public DateTime Time { get; set; }

            public List<ChatLine> Chat { get; set; }

            public LootedItemData(DateTime Time, string Player, string ItemName, string ItemId, string Container, string Zone, string LogLine)
            {
                this.Time = Time;
                this.Player = Player;
                this.ItemName = ItemName;
                this.ItemId = ItemId;
                this.Container = Container;
                this.Zone = Zone;
                this.Chat = new List<ChatLine>();
                this.LogLine = LogLine;
                if (!ItemName.StartsWith("["))
                    LogLine = LogLine.Replace(ItemName, "[" + ItemName + "]");
                this.ItemLink = regexItem.Match(LogLine).Value;
                this.DKP = "0";
                this.DKPSubmitResult = "-";
            }
            
            public void guessDKP()
            {
                var auctionstart = new DateTime(0);
                // find earliest occurence of item name in the chat
                foreach (var chatline in this.Chat) {
                    if (!chatline.Text.Contains(this.ItemName))
                        continue;
                    if (chatline.Time > auctionstart) {
                        auctionstart = chatline.Time;
                    }
                }
                string bidtext = null;
                var latestbid = new DateTime(0);
                // find lastest bid from looter
                foreach (var chatline in this.Chat) {
                    if (chatline.Time < auctionstart)
                        continue;
                    if (chatline.Player != this.Player)
                        continue;
                    if (latestbid == null || chatline.Time > latestbid) {
                        latestbid = chatline.Time;
                        bidtext = chatline.Text;
                    }
                }
                if (bidtext == null)
                    return;
                var multiplier = 1;
                var match = LootedItemData.regexBid.Match(bidtext);
                if (!match.Success) {
                    return;
                }
                if (match.Groups["platbid"].Success) {
                    if (match.Groups["platbid"].Value.Trim().ToLower() == "k") {
                        multiplier = 1000;
                    } else if (match.Groups["platbid"].Value.Trim().ToLower() == "kk") {
                        multiplier = 1000000;
                    }
                }
                string sdkp = match.Groups["bid"].Value.Trim().ToLower().Replace(",", ".");
                float fdkpraw = float.Parse(sdkp);
                float fdkp = fdkpraw * multiplier;
                if (fdkp >= 20000.0) {
                    // non-dkp bid
                    return;
                } else if (fdkp < 20.0) {
                    // probably missing multiplier
                    fdkp = fdkp * 1000;
                }
                int dkp = (int) fdkp;
                this.DKP = dkp.ToString();
            }

            public class Sorter : IComparer<LootedItemData>
            {
                int sortColumn = 0;

                public Sorter(int SortColumn)
                {
                    this.sortColumn = SortColumn;
                }

                public int Compare(LootedItemData x, LootedItemData y)
                {
                    switch (this.sortColumn)
                    {
                        case 0: // Datetime
                            return x.Time.CompareTo(y.Time);
                        case 1: // Zone
                            return x.Zone.CompareTo(y.Zone);
                        case 2: // Player
                            return x.Player.CompareTo(y.Player);
                        case 3: // Item Name
                            return x.ItemName.CompareTo(y.ItemName);
                        case 4: // Container
                            return x.Container.CompareTo(y.Container);
                        default:
                            return 0;
                    }
                }
            }
        }
        #endregion 

        private string GetPlainTextForDataGridSelection(DataGridView dataGridView)
        {
            int[] columnWidths = new int[dataGridView.Columns.Count];

            List<DataGridViewRow> selectedRows = GetSelectedRows(dataGridView);

            foreach (DataGridViewRow selectedRow in selectedRows)
            {
                foreach (DataGridViewCell cell in selectedRow.Cells)
                {
                    int columnIndex = cell.OwningColumn.Index;
                    int cellLength = cell.FormattedValue.ToString().Length;
                    if (columnWidths[columnIndex] < cellLength)
                    {
                        columnWidths[columnIndex] = cellLength;
                    }
                }
            }

            for (int i = 0; i < dataGridView.Columns.Count; i++)
            {
                columnWidths[i] = columnWidths[i] + 2;
            }

            StringBuilder sb = new StringBuilder();
            selectedRows.Reverse();
            foreach (DataGridViewRow selectedRow in selectedRows)
            {
                foreach (DataGridViewCell cell in selectedRow.Cells)
                {
                    sb.Append(cell.FormattedValue.ToString().PadRight(columnWidths[cell.OwningColumn.Index]));
                }

                sb.AppendLine();
            }

            string result = sb.ToString();
            return result;
        }

        private List<DataGridViewRow> GetSelectedItemRows()
        {
            return GetSelectedRows(itemGridView);
        }

        private List<DataGridViewRow> GetSelectedRows(DataGridView dataGridView)
        {
            int countOfSelectedCells = dataGridView.GetCellCount(DataGridViewElementStates.Selected);
            List<int> selectedRowIndices = new List<int>();
            List<DataGridViewRow> selectedDataGridRows = new List<DataGridViewRow>();

            for (int i = 0; i < countOfSelectedCells; i++)
            {
                int rowIndex = dataGridView.SelectedCells[i].RowIndex;
                if (!selectedRowIndices.Contains(rowIndex))
                {
                    selectedRowIndices.Add(rowIndex);
                    selectedDataGridRows.Add(dataGridView.Rows[rowIndex]);
                }
            }

            return selectedDataGridRows;
        }

        private void RepopulateChatListView()
        {
            this.ChatListView.BeginUpdate();
            this.ChatListView.Items.Clear();
            if (this.itemGridView.SelectedCells.Count > 0)
            {
                DataGridViewRow selectedRow = this.itemGridView.SelectedCells[0].OwningRow;
                ListViewItem lvi;

                LootedItemData item = (LootedItemData)selectedRow.Tag;
                for (int i = item.Chat.Count - 1; i >= 0; i--)
                {
                    ChatLine cl = item.Chat[i];
                    lvi = new ListViewItem(cl.Time.ToShortDateString() + " " + cl.Time.ToLongTimeString());
                    lvi.SubItems.Add(cl.Channel);
                    lvi.SubItems.Add(cl.Player);
                    lvi.SubItems.Add(cl.Text);
                    lvi.UseItemStyleForSubItems = false;
                    lvi.SubItems[this.lvChatSorting].BackColor = SystemColors.ControlLight;
                    lvi.Tag = cl;
                    this.ChatListView.Items.Add(lvi);
                }
            }

            ActGlobals.oFormActMain.ResizeLVCols(this.ChatListView);
            this.ChatListView.EndUpdate();
        }

        private void RepopulateItemDataGridview()
        {
            this.itemGridView.Rows.Clear();
            for (int i = 0; i < this.lootedItems.Count; i++)
            {
                LootedItemData item = this.lootedItems[i];
                int rowAdded = this.itemGridView.Rows.Add(
                    item.Time.ToShortDateString() + " " + item.Time.ToLongTimeString(),
                    item.Zone,
                    item.Player,
                    item.ItemName,
                    item.Container,
                    item.DKP,
                    item.DKPSubmitResult);
                this.itemGridView.Rows[rowAdded].Tag = item;
            }
        }

        private void SortChatListView()
        {
            if (this.itemGridView.SelectedCells.Count > 0)
            {
                DataGridViewRow selectedRow = this.itemGridView.SelectedCells[0].OwningRow;
                LootedItemData item = (LootedItemData)selectedRow.Tag;
                item.Chat.Sort(new ChatLine.Sorter(lvChatSorting));
                if (lvChatReverse)
                {
                    item.Chat.Reverse();
                }
            }
        }
    }
}

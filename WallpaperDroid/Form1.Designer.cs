
namespace WallpaperDroid
{
    partial class tagForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(tagForm));
            this.searchTextBox = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.notifyIcon1 = new System.Windows.Forms.NotifyIcon(this.components);
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.restoreMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.saveStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.nextStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.tagListBox = new System.Windows.Forms.ListBox();
            this.addTagButton = new System.Windows.Forms.Button();
            this.removeTagButton = new System.Windows.Forms.Button();
            this.screenResLabel = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // searchTextBox
            // 
            this.searchTextBox.Location = new System.Drawing.Point(6, 19);
            this.searchTextBox.Name = "searchTextBox";
            this.searchTextBox.Size = new System.Drawing.Size(313, 20);
            this.searchTextBox.TabIndex = 0;
            this.toolTip1.SetToolTip(this.searchTextBox, resources.GetString("searchTextBox.ToolTip"));
            this.searchTextBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.searchTextBox_KeyPress);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(262, 167);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(103, 23);
            this.button1.TabIndex = 1;
            this.button1.Text = "Apply";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.searchTextBox);
            this.groupBox1.Controls.Add(this.tagListBox);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(325, 141);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Search tag(s)";
            // 
            // notifyIcon1
            // 
            this.notifyIcon1.ContextMenuStrip = this.contextMenuStrip1;
            this.notifyIcon1.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIcon1.Icon")));
            this.notifyIcon1.Text = "notifyIcon1";
            this.notifyIcon1.Visible = true;
            this.notifyIcon1.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.notifyIcon1_MouseDoubleClick);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.restoreMenuItem,
            this.toolStripSeparator1,
            this.saveStripMenuItem,
            this.toolStripSeparator3,
            this.nextStripMenuItem,
            this.toolStripSeparator2,
            this.exitToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(165, 110);
            // 
            // restoreMenuItem
            // 
            this.restoreMenuItem.Name = "restoreMenuItem";
            this.restoreMenuItem.Size = new System.Drawing.Size(164, 22);
            this.restoreMenuItem.Text = "WallpaperDroid";
            this.restoreMenuItem.Click += new System.EventHandler(this.restoreStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(161, 6);
            // 
            // saveStripMenuItem
            // 
            this.saveStripMenuItem.Name = "saveStripMenuItem";
            this.saveStripMenuItem.Size = new System.Drawing.Size(164, 22);
            this.saveStripMenuItem.Text = "Revert to original";
            this.saveStripMenuItem.Click += new System.EventHandler(this.saveStripMenuItem_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(161, 6);
            // 
            // nextStripMenuItem
            // 
            this.nextStripMenuItem.Name = "nextStripMenuItem";
            this.nextStripMenuItem.Size = new System.Drawing.Size(164, 22);
            this.nextStripMenuItem.Text = "Next";
            this.nextStripMenuItem.Click += new System.EventHandler(this.nextStripMenuItem_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(161, 6);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(164, 22);
            this.exitToolStripMenuItem.Text = "Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // toolTip1
            // 
            this.toolTip1.AutomaticDelay = 1000;
            this.toolTip1.AutoPopDelay = 10000;
            this.toolTip1.InitialDelay = 200;
            this.toolTip1.ReshowDelay = 40;
            // 
            // tagListBox
            // 
            this.tagListBox.FormattingEnabled = true;
            this.tagListBox.Location = new System.Drawing.Point(6, 45);
            this.tagListBox.Name = "tagListBox";
            this.tagListBox.Size = new System.Drawing.Size(313, 82);
            this.tagListBox.Sorted = true;
            this.tagListBox.TabIndex = 4;
            this.toolTip1.SetToolTip(this.tagListBox, "Your applied tag(s) will show up here.");
            // 
            // addTagButton
            // 
            this.addTagButton.Location = new System.Drawing.Point(340, 28);
            this.addTagButton.Name = "addTagButton";
            this.addTagButton.Size = new System.Drawing.Size(25, 23);
            this.addTagButton.TabIndex = 5;
            this.addTagButton.Text = "+";
            this.toolTip1.SetToolTip(this.addTagButton, "Add tag.");
            this.addTagButton.UseVisualStyleBackColor = true;
            this.addTagButton.Click += new System.EventHandler(this.addTagButton_Click);
            // 
            // removeTagButton
            // 
            this.removeTagButton.Location = new System.Drawing.Point(340, 57);
            this.removeTagButton.Name = "removeTagButton";
            this.removeTagButton.Size = new System.Drawing.Size(26, 23);
            this.removeTagButton.TabIndex = 6;
            this.removeTagButton.Text = "-";
            this.toolTip1.SetToolTip(this.removeTagButton, "Remove selected tag.");
            this.removeTagButton.UseVisualStyleBackColor = true;
            this.removeTagButton.Click += new System.EventHandler(this.removeTagButton_Click);
            // 
            // screenResLabel
            // 
            this.screenResLabel.AutoSize = true;
            this.screenResLabel.Location = new System.Drawing.Point(15, 172);
            this.screenResLabel.Name = "screenResLabel";
            this.screenResLabel.Size = new System.Drawing.Size(118, 13);
            this.screenResLabel.TabIndex = 3;
            this.screenResLabel.Text = "Your screen resolution: ";
            // 
            // tagForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(377, 198);
            this.Controls.Add(this.removeTagButton);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.addTagButton);
            this.Controls.Add(this.screenResLabel);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.Name = "tagForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "WallpaperDroid";
            this.Load += new System.EventHandler(this.tagForm_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.NotifyIcon notifyIcon1;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem saveStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem nextStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem restoreMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.TextBox searchTextBox;
        private System.Windows.Forms.Label screenResLabel;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ListBox tagListBox;
        private System.Windows.Forms.Button addTagButton;
        private System.Windows.Forms.Button removeTagButton;
    }
}


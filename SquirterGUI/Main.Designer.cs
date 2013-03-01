namespace SquirterGUI
{
    sealed partial class Main
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
            this.settingsbox = new System.Windows.Forms.GroupBox();
            this.blockcountbox = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.logging = new System.Windows.Forms.CheckBox();
            this.modebox = new System.Windows.Forms.GroupBox();
            this.glitchmode = new System.Windows.Forms.RadioButton();
            this.rawmode = new System.Windows.Forms.RadioButton();
            this.sizebox = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.progressbar = new SquirterGUI.CToolStripProgressBar();
            this.statuslabel = new SquirterGUI.CToolStripLabel();
            this.dumpbtn = new System.Windows.Forms.Button();
            this.writebtn = new System.Windows.Forms.Button();
            this.erasebtn = new System.Windows.Forms.Button();
            this.flashconfigbox = new SquirterGUI.CTextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.infobox = new System.Windows.Forms.GroupBox();
            this.totalblockoutbox = new SquirterGUI.CTextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.startblockoutbox = new SquirterGUI.CTextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.errorbox = new System.Windows.Forms.RichTextBox();
            this.bw = new System.ComponentModel.BackgroundWorker();
            this.sfd = new System.Windows.Forms.SaveFileDialog();
            this.ofd = new System.Windows.Forms.OpenFileDialog();
            this.tooltip = new System.Windows.Forms.ToolTip(this.components);
            this.abortbtn = new System.Windows.Forms.Button();
            this.settingsbox.SuspendLayout();
            this.modebox.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.infobox.SuspendLayout();
            this.SuspendLayout();
            // 
            // settingsbox
            // 
            this.settingsbox.Controls.Add(this.blockcountbox);
            this.settingsbox.Controls.Add(this.label3);
            this.settingsbox.Controls.Add(this.textBox1);
            this.settingsbox.Controls.Add(this.label2);
            this.settingsbox.Controls.Add(this.logging);
            this.settingsbox.Controls.Add(this.modebox);
            this.settingsbox.Controls.Add(this.sizebox);
            this.settingsbox.Controls.Add(this.label1);
            this.settingsbox.Location = new System.Drawing.Point(12, 12);
            this.settingsbox.Name = "settingsbox";
            this.settingsbox.Size = new System.Drawing.Size(231, 117);
            this.settingsbox.TabIndex = 0;
            this.settingsbox.TabStop = false;
            this.settingsbox.Text = "Settings";
            // 
            // blockcountbox
            // 
            this.blockcountbox.Location = new System.Drawing.Point(140, 87);
            this.blockcountbox.Name = "blockcountbox";
            this.blockcountbox.Size = new System.Drawing.Size(85, 20);
            this.blockcountbox.TabIndex = 6;
            this.blockcountbox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.HexOnlyInput);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(92, 90);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(42, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "Blocks:";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(140, 61);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(85, 20);
            this.textBox1.TabIndex = 6;
            this.textBox1.Text = "0";
            this.textBox1.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.HexOnlyInput);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(76, 64);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(58, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "Startblock:";
            // 
            // logging
            // 
            this.logging.AutoSize = true;
            this.logging.Checked = true;
            this.logging.CheckState = System.Windows.Forms.CheckState.Checked;
            this.logging.Location = new System.Drawing.Point(120, 21);
            this.logging.Name = "logging";
            this.logging.Size = new System.Drawing.Size(105, 17);
            this.logging.TabIndex = 4;
            this.logging.Text = "Logging enabled";
            this.logging.UseVisualStyleBackColor = true;
            // 
            // modebox
            // 
            this.modebox.Controls.Add(this.glitchmode);
            this.modebox.Controls.Add(this.rawmode);
            this.modebox.Location = new System.Drawing.Point(6, 46);
            this.modebox.Name = "modebox";
            this.modebox.Size = new System.Drawing.Size(64, 65);
            this.modebox.TabIndex = 3;
            this.modebox.TabStop = false;
            this.modebox.Text = "Mode";
            // 
            // glitchmode
            // 
            this.glitchmode.AutoSize = true;
            this.glitchmode.Location = new System.Drawing.Point(6, 42);
            this.glitchmode.Name = "glitchmode";
            this.glitchmode.Size = new System.Drawing.Size(52, 17);
            this.glitchmode.TabIndex = 2;
            this.glitchmode.TabStop = true;
            this.glitchmode.Text = "Glitch";
            this.tooltip.SetToolTip(this.glitchmode, "Read without ECC data, Write with ECC data and recalculate ecc and block numbers");
            this.glitchmode.UseVisualStyleBackColor = true;
            // 
            // rawmode
            // 
            this.rawmode.AutoSize = true;
            this.rawmode.Checked = true;
            this.rawmode.Location = new System.Drawing.Point(6, 19);
            this.rawmode.Name = "rawmode";
            this.rawmode.Size = new System.Drawing.Size(51, 17);
            this.rawmode.TabIndex = 2;
            this.rawmode.TabStop = true;
            this.rawmode.Text = "RAW";
            this.tooltip.SetToolTip(this.rawmode, "Read/Write data in RAW mode means that it\'ll include ECC data");
            this.rawmode.UseVisualStyleBackColor = true;
            // 
            // sizebox
            // 
            this.sizebox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.sizebox.FormattingEnabled = true;
            this.sizebox.Location = new System.Drawing.Point(42, 19);
            this.sizebox.Name = "sizebox";
            this.sizebox.Size = new System.Drawing.Size(72, 21);
            this.sizebox.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(30, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Size:";
            // 
            // statusStrip1
            // 
            this.statusStrip1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.progressbar,
            this.statuslabel});
            this.statusStrip1.Location = new System.Drawing.Point(0, 254);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(461, 22);
            this.statusStrip1.SizingGrip = false;
            this.statusStrip1.TabIndex = 1;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // progressbar
            // 
            this.progressbar.Name = "progressbar";
            this.progressbar.Size = new System.Drawing.Size(100, 16);
            // 
            // statuslabel
            // 
            this.statuslabel.Name = "statuslabel";
            this.statuslabel.Size = new System.Drawing.Size(122, 17);
            this.statuslabel.Text = "Waiting for user input";
            // 
            // dumpbtn
            // 
            this.dumpbtn.Location = new System.Drawing.Point(12, 135);
            this.dumpbtn.Name = "dumpbtn";
            this.dumpbtn.Size = new System.Drawing.Size(231, 23);
            this.dumpbtn.TabIndex = 3;
            this.dumpbtn.Text = "Read";
            this.dumpbtn.UseVisualStyleBackColor = true;
            this.dumpbtn.Click += new System.EventHandler(this.DumpbtnClick);
            // 
            // writebtn
            // 
            this.writebtn.Location = new System.Drawing.Point(12, 164);
            this.writebtn.Name = "writebtn";
            this.writebtn.Size = new System.Drawing.Size(231, 23);
            this.writebtn.TabIndex = 3;
            this.writebtn.Text = "Write";
            this.writebtn.UseVisualStyleBackColor = true;
            this.writebtn.Click += new System.EventHandler(this.WritebtnClick);
            // 
            // erasebtn
            // 
            this.erasebtn.Location = new System.Drawing.Point(12, 193);
            this.erasebtn.Name = "erasebtn";
            this.erasebtn.Size = new System.Drawing.Size(231, 23);
            this.erasebtn.TabIndex = 3;
            this.erasebtn.Text = "Erase";
            this.erasebtn.UseVisualStyleBackColor = true;
            this.erasebtn.Click += new System.EventHandler(this.ErasebtnClick);
            // 
            // flashconfigbox
            // 
            this.flashconfigbox.Location = new System.Drawing.Point(81, 19);
            this.flashconfigbox.Name = "flashconfigbox";
            this.flashconfigbox.ReadOnly = true;
            this.flashconfigbox.Size = new System.Drawing.Size(113, 20);
            this.flashconfigbox.TabIndex = 4;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(11, 22);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(64, 13);
            this.label4.TabIndex = 5;
            this.label4.Text = "Flashconfig:";
            // 
            // infobox
            // 
            this.infobox.Controls.Add(this.totalblockoutbox);
            this.infobox.Controls.Add(this.label6);
            this.infobox.Controls.Add(this.startblockoutbox);
            this.infobox.Controls.Add(this.label5);
            this.infobox.Controls.Add(this.flashconfigbox);
            this.infobox.Controls.Add(this.label4);
            this.infobox.Location = new System.Drawing.Point(249, 12);
            this.infobox.Name = "infobox";
            this.infobox.Size = new System.Drawing.Size(200, 97);
            this.infobox.TabIndex = 6;
            this.infobox.TabStop = false;
            this.infobox.Text = "Information";
            // 
            // totalblockoutbox
            // 
            this.totalblockoutbox.Location = new System.Drawing.Point(81, 71);
            this.totalblockoutbox.Name = "totalblockoutbox";
            this.totalblockoutbox.ReadOnly = true;
            this.totalblockoutbox.Size = new System.Drawing.Size(113, 20);
            this.totalblockoutbox.TabIndex = 4;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(6, 74);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(69, 13);
            this.label6.TabIndex = 5;
            this.label6.Text = "Total Blocks:";
            // 
            // startblockoutbox
            // 
            this.startblockoutbox.Location = new System.Drawing.Point(81, 45);
            this.startblockoutbox.Name = "startblockoutbox";
            this.startblockoutbox.ReadOnly = true;
            this.startblockoutbox.Size = new System.Drawing.Size(113, 20);
            this.startblockoutbox.TabIndex = 4;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(17, 48);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(58, 13);
            this.label5.TabIndex = 5;
            this.label5.Text = "Startblock:";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(249, 112);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(37, 13);
            this.label7.TabIndex = 8;
            this.label7.Text = "Errors:";
            // 
            // errorbox
            // 
            this.errorbox.Location = new System.Drawing.Point(249, 128);
            this.errorbox.Name = "errorbox";
            this.errorbox.ReadOnly = true;
            this.errorbox.Size = new System.Drawing.Size(200, 123);
            this.errorbox.TabIndex = 9;
            this.errorbox.Text = "";
            // 
            // bw
            // 
            this.bw.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.BwRunWorkerCompleted);
            // 
            // sfd
            // 
            this.sfd.DefaultExt = "bin";
            this.sfd.FileName = "flashdmp.bin";
            this.sfd.Filter = "Xbox 360 NAND|*.bin";
            // 
            // ofd
            // 
            this.ofd.FileName = "openFileDialog1";
            this.ofd.Filter = "Xbox 360 NAND|*.bin|XeLL image|*.ecc|All Files|*.*";
            // 
            // abortbtn
            // 
            this.abortbtn.Enabled = false;
            this.abortbtn.Location = new System.Drawing.Point(12, 222);
            this.abortbtn.Name = "abortbtn";
            this.abortbtn.Size = new System.Drawing.Size(231, 23);
            this.abortbtn.TabIndex = 10;
            this.abortbtn.Text = "Abort";
            this.abortbtn.UseVisualStyleBackColor = true;
            this.abortbtn.Click += new System.EventHandler(this.AbortbtnClick);
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.ClientSize = new System.Drawing.Size(461, 276);
            this.Controls.Add(this.abortbtn);
            this.Controls.Add(this.errorbox);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.infobox);
            this.Controls.Add(this.erasebtn);
            this.Controls.Add(this.writebtn);
            this.Controls.Add(this.dumpbtn);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.settingsbox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "Main";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.MainLoad);
            this.settingsbox.ResumeLayout(false);
            this.settingsbox.PerformLayout();
            this.modebox.ResumeLayout(false);
            this.modebox.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.infobox.ResumeLayout(false);
            this.infobox.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox settingsbox;
        private System.Windows.Forms.ComboBox sizebox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private CToolStripProgressBar progressbar;
        private CToolStripLabel statuslabel;
        private System.Windows.Forms.CheckBox logging;
        private System.Windows.Forms.GroupBox modebox;
        private System.Windows.Forms.RadioButton glitchmode;
        private System.Windows.Forms.RadioButton rawmode;
        private System.Windows.Forms.TextBox blockcountbox;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button dumpbtn;
        private System.Windows.Forms.Button writebtn;
        private System.Windows.Forms.Button erasebtn;
        private CTextBox flashconfigbox;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.GroupBox infobox;
        private CTextBox totalblockoutbox;
        private System.Windows.Forms.Label label6;
        private CTextBox startblockoutbox;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.RichTextBox errorbox;
        private System.ComponentModel.BackgroundWorker bw;
        private System.Windows.Forms.SaveFileDialog sfd;
        private System.Windows.Forms.OpenFileDialog ofd;
        private System.Windows.Forms.ToolTip tooltip;
        private System.Windows.Forms.Button abortbtn;
    }
}


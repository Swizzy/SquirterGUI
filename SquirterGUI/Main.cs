using System.Windows.Forms;

namespace SquirterGUI
{
    using System;
    using System.ComponentModel;
    using System.Globalization;
    using System.IO;
    using System.Threading;
    using SquirterGUI.Properties;

    public partial class Main : Form
    {
        public static event EventHandler<StatusEventArgs> StatusUpdater;

        public Main()
        {
            InitializeComponent();
            StatusUpdater += StatusUpdate;
            sizebox.Items.Clear();
            sizebox.Items.Add(new ComboBoxItem("Auto", 0));
            sizebox.Items.Add(new ComboBoxItem("16MB", 0x8000));
            sizebox.Items.Add(new ComboBoxItem("64MB", 0x20000));
            sizebox.Items.Add(new ComboBoxItem("256MB", 0x80000));
            sizebox.Items.Add(new ComboBoxItem("512MB", 0x100000));
            sizebox.SelectedIndex = 0;
            
        }

        private void StatusUpdate(object sender, StatusEventArgs e)
        {
            if (InvokeRequired)
            {
                BeginInvoke(new EventHandler<StatusEventArgs>(StatusUpdate), new[] { sender, e });
                return;
            }
            progressbar.Value = e.Progress;
            statuslabel.Text = e.Status;
        }

        public static void SendStatus(int current, int max)
        {
            var msg = string.Format("Processing block 0x{0:X} of 0x{1:X}", current, max);
            if (StatusUpdater != null)
                StatusUpdater(null, new StatusEventArgs(msg, current));
        }

        private void SetButtonState(bool state) {
            dumpbtn.Enabled = state;
            erasebtn.Enabled = state;
            writebtn.Enabled = state;
        }

        bool BwInit(ref XNAND worker) {
            var config = XNAND.FlashDataInit();
            if (config > 0) {
                flashconfigbox.Text = string.Format("0x{0:X8}", config);
                worker.SetConfig(config);
                return true;
            }
            flashconfigbox.Text = "";
            return false;
        }

        static XNANDSettings BwFixArgs(ref BwArgs args, ref XNAND worker)
        {
            var nandopt = worker.GetSettings();
            if (nandopt == null)
                return null;
            if (args.BlockCount > nandopt.SizeBlocks - 1)
                args.BlockCount = 0;
            if (args.BlockCount == 0 || args.StartBlock + args.BlockCount > nandopt.SizeBlocks - 1)
                args.BlockCount = nandopt.SizeBlocks - args.StartBlock - 1;
            if (args.Pages == 0)
                args.Pages = nandopt.PagesInBlock * nandopt.SizeBlocks;
            return nandopt;
        }

        private void DumpbtnClick(object sender, EventArgs e)
        {
            if (sfd.ShowDialog() != DialogResult.OK)
                return;
            bw.DoWork += BwRead;
            SetButtonState(false);
            var args = new BwArgs {
                                      Filename = sfd.FileName,
                                      Pages =
                                          ((ComboBoxItem) sizebox.Items[sizebox.SelectedIndex]).Value
                                  };
            if (rawmode.Checked)
                args.Mode = (int)BwArgs.Modes.Raw;
            else if (glitchmode.Checked)
                args.Mode = (int)BwArgs.Modes.Glitch;
            if (!long.TryParse(startblockoutbox.Text, NumberStyles.HexNumber, null, out args.StartBlock))
                args.StartBlock = 0;
            if (!long.TryParse(blockcountbox.Text, NumberStyles.HexNumber, null, out args.BlockCount))
                args.StartBlock = 0;
            bw.RunWorkerAsync(args);
            sfd.FileName = Path.GetFileName(sfd.FileName);
        }

        private void BwRead(object sender, DoWorkEventArgs e)
        {
            e.Result = -1;
            var worker = new XNAND();
            if (!worker.IsOk)
                return;
            e.Result = -2;
            if (!BwInit(ref worker))
                return;
            e.Result = -3;
            BwArgs args;
            if (e.Argument is BwArgs)
                args = e.Argument as BwArgs;
            else
                return;
            var nandopt = BwFixArgs(ref args, ref worker);
            progressbar.Maximum = (int) (args.StartBlock + args.BlockCount);
            XNAND.Read(args.Filename, (int) args.StartBlock, (int) (args.StartBlock + args.BlockCount), args.Mode, ref nandopt);
            e.Result = 0;
        }

        private void WritebtnClick(object sender, EventArgs e)
        {
            bw.DoWork += BwWrite;
            SetButtonState(false);
            bw.RunWorkerAsync();
        }

        private void BwWrite(object sender, DoWorkEventArgs e)
        {
            e.Result = -1;
            var worker = new XNAND();
            if (!worker.IsOk)
                return;
            e.Result = -2;
            if (!BwInit(ref worker))
                return;
            e.Result = -3;
            BwArgs args;
            if (e.Argument is BwArgs)
                args = e.Argument as BwArgs;
            else
                return;
            var nandopt = BwFixArgs(ref args, ref worker);
            progressbar.Maximum = (int)(args.StartBlock + args.BlockCount);
            //TODO: Add write code
            for (var i = 0; i < 0x65; i++)
            {
                SendStatus(i, 0x64);
                Thread.Sleep(100);
            }
            e.Result = 0;
        }

        private void ErasebtnClick(object sender, EventArgs e)
        {
            bw.DoWork += BwErase;
            SetButtonState(false);
            bw.RunWorkerAsync();
        }

        private void BwErase(object sender, DoWorkEventArgs e)
        {
            e.Result = -1;
            var worker = new XNAND();
            if (!worker.IsOk)
                return;
            e.Result = -2;
            if (!BwInit(ref worker))
                return;
            e.Result = -3;
            BwArgs args;
            if (e.Argument is BwArgs)
                args = e.Argument as BwArgs;
            else
                return;
            var nandopt = BwFixArgs(ref args, ref worker);
            progressbar.Maximum = (int)(args.StartBlock + args.BlockCount);
            //TODO: Add erase code
            for (var i = 0; i < 0x65; i++)
            {
                SendStatus(i, 0x64);
                Thread.Sleep(100);
            }
            e.Result = 0;
        }

        private void BwRunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            bw.DoWork -= BwRead;
            bw.DoWork -= BwErase;
            bw.DoWork -= BwWrite;
            if (e.Result is int) {
                if (((int) e.Result) == -1)
                    MessageBox.Show(Resources.error_init_ftdi, Resources.error_title, MessageBoxButtons.OK, MessageBoxIcon.Error);
                else if (((int) e.Result) == -2) 
                    MessageBox.Show(Resources.error_bad_config, Resources.error_title, MessageBoxButtons.OK, MessageBoxIcon.Error);
                else if (((int) e.Result) < 0)
                    MessageBox.Show(Resources.error_unkown, Resources.error_title, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            //statuslabel.Text = Resources.bw_completemsg;
            SetButtonState(true);
        }

        private void HexOnlyInput(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != '\b')
                e.Handled = !Uri.IsHexDigit(e.KeyChar);
        }
    }

    internal class BwArgs {
        public string Filename;
        public long Pages;
        public long StartBlock;
        public long BlockCount;
        public int Mode;

        public enum Modes {
            Raw = 1,
            Glitch =2
        }
    }

    public class StatusEventArgs : EventArgs
    {
        public StatusEventArgs(string msg, int progress) { Status = msg; Progress = progress;}
        public readonly string Status;
        public readonly int Progress;
    }
}

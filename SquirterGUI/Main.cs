using System.Windows.Forms;

namespace SquirterGUI
{
    using System;
    using System.ComponentModel;
    using System.Globalization;
    using System.IO;
    using System.Reflection;
    using System.Threading;
    using SquirterGUI.Properties;

    public sealed partial class Main : Form
    {
        internal static string AppNameAndVersion;
        public static event EventHandler<StatusEventArgs> StatusUpdater;
        public static event EventHandler<StatusEventArgs> ErrorUpdater;

        public Main()
        {
            InitializeComponent();
            StatusUpdater += StatusUpdate;
            ErrorUpdater += AddError;
            sizebox.Items.Clear();
            sizebox.Items.Add(new ComboBoxItem("Auto", 0));
            sizebox.Items.Add(new ComboBoxItem("16MB", 0x8000));
            sizebox.Items.Add(new ComboBoxItem("64MB", 0x20000));
            sizebox.Items.Add(new ComboBoxItem("256MB", 0x80000));
            sizebox.Items.Add(new ComboBoxItem("512MB", 0x100000));
            sizebox.SelectedIndex = 0;
            var app = Assembly.GetExecutingAssembly();
            AppNameAndVersion = string.Format("Squirter GUI {3}-Bit v{0}.{1} (Build: {2}) by Swizzy", app.GetName().Version.Major, app.GetName().Version.Minor, app.GetName().Version.Build, IntPtr.Size * 8);
            Text = AppNameAndVersion;
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

        private void AddError(object sender, StatusEventArgs e) {
            if (InvokeRequired) {
                BeginInvoke(new EventHandler<StatusEventArgs>(AddError), new[] { sender, e});
                return;
            }
            errorbox.AppendText(e.Status);
            Logger.WriteLine2(e.Status);
        }

        public static void SendStatus(int current, int max)
        {
            if (StatusUpdater != null)
                StatusUpdater(null, new StatusEventArgs(string.Format("Processing block 0x{0:X} of 0x{1:X}", current, max), current));
        }

        public static void SendError(int block, uint status, string type) {
            if (ErrorUpdater != null)
                ErrorUpdater(null, new StatusEventArgs(string.Format("ERROR 0x{0:X} {2} block 0x{1:X}\n", status, block, type), 0));
        }

        public static void SendAbort() {
            if (ErrorUpdater != null)
                ErrorUpdater(null, new StatusEventArgs("Abort requested!", 0));
        }

        private void SetButtonState(bool state) {
            dumpbtn.Enabled = state;
            erasebtn.Enabled = state;
            writebtn.Enabled = state;
            settingsbox.Enabled = state;
            abortbtn.Enabled = !state;
        }

        private void SetLogState(string file) {
            errorbox.Text = "";
            Logger.Enabled = logging.Checked;
            Logger.LogPath = string.Format("{0}\\{1}.log", Path.GetDirectoryName(file), Path.GetFileNameWithoutExtension(file));
            if (Logger.LogPath.StartsWith("\\"))
                Logger.LogPath = string.Format("{0}\\{1}", Path.GetDirectoryName(Application.ExecutablePath), file);
        }

        private void SetInfo(long start, long count, int maxval)
        {
            progressbar.Maximum = maxval;
            startblockoutbox.Text = string.Format("0x{0:X}", start);
            totalblockoutbox.Text = string.Format("0x{0:X}", count);
        }

        bool BwInit(ref XNAND worker) {
            var config = XNAND.FlashDataInit();
            if (config > 0) {
                flashconfigbox.Text = string.Format("0x{0:X8}", config);
                XNAND.SetConfig(config);
                return true;
            }
            flashconfigbox.Text = "";
            return false;
        }

        static XNANDSettings BwFixArgs(ref BwArgs args, ref XNAND worker)
        {
            var nandopt = XNAND.GetSettings();
            if (nandopt == null)
                return null;
            if (args.BlockCount > nandopt.SizeBlocks - 1)
                args.BlockCount = 0;
            if (args.BlockCount == 0 || args.StartBlock + args.BlockCount > nandopt.SizeBlocks - 1)
                args.BlockCount = nandopt.SizeBlocks - args.StartBlock;
            if (args.Pages == 0)
                args.Pages = nandopt.PagesInBlock * nandopt.SizeBlocks;
            return nandopt;
        }

        private void DumpbtnClick(object sender, EventArgs e)
        {
            if (sfd.ShowDialog() != DialogResult.OK)
                return;
            SetLogState(sfd.FileName);
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
                args.BlockCount = 0;
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
            SetInfo(args.StartBlock, args.BlockCount, (int) (args.StartBlock + args.BlockCount - 1));
            XNAND.Read(args.Filename, (int) args.StartBlock, (int) (args.StartBlock + args.BlockCount - 1), args.Mode, ref nandopt);
            e.Result = 0;
        }

        private void WritebtnClick(object sender, EventArgs e)
        {
            if (ofd.ShowDialog() != DialogResult.OK)
                return;
            SetLogState(sfd.FileName);
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
            SetInfo(args.StartBlock, args.BlockCount, (int)(args.StartBlock + args.BlockCount - 1));
            XNAND.Write(args.Filename, (int)args.StartBlock, (int)(args.StartBlock + args.BlockCount - 1), args.Mode, ref nandopt);
            e.Result = 0;
        }

        private void ErasebtnClick(object sender, EventArgs e)
        {
            if (MessageBox.Show(Resources.AreYouSureErase, Resources.AreYouSure, MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                return;
            SetLogState("erase.log");
            bw.DoWork += BwErase;
            SetButtonState(false);
            var args = new BwArgs
            {
                Pages = ((ComboBoxItem)sizebox.Items[sizebox.SelectedIndex]).Value
            };
            if (!long.TryParse(startblockoutbox.Text, NumberStyles.HexNumber, null, out args.StartBlock))
                args.StartBlock = 0;
            if (!long.TryParse(blockcountbox.Text, NumberStyles.HexNumber, null, out args.BlockCount))
                args.BlockCount = 0;
            bw.RunWorkerAsync(args);
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
            SetInfo(args.StartBlock, args.BlockCount, (int)(args.StartBlock + args.BlockCount - 1));
            XNAND.Erase((int)args.StartBlock, (int)(args.StartBlock + args.BlockCount - 1), ref nandopt);
            e.Result = 0;
        }

        private void BwRunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            bw.DoWork -= BwRead;
            bw.DoWork -= BwErase;
            bw.DoWork -= BwWrite;
            if (!XNAND.Abort) {
                var msg = "";
                if (e.Result is int) {
                    switch (((int) e.Result)) {
                        case -1:
                            msg = Resources.error_init_ftdi;
                            break;
                        case -2:
                            msg = Resources.error_bad_config;
                            break;
                        default:
                            msg = Resources.error_unkown;
                            break;
                    }
                }
                if (((int) e.Result) < 0)
                    MessageBox.Show(msg,
                                    Resources.error_title,
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Error);
                statuslabel.Text = Resources.bw_completemsg;
            }
            else 
                statuslabel.Text = "Operation aborted!";
            SetButtonState(true);
        }

        private void HexOnlyInput(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != '\b')
                e.Handled = !Uri.IsHexDigit(e.KeyChar);
        }

        private void AbortbtnClick(object sender, EventArgs e)
        {
            if (MessageBox.Show(Resources.AreYouSureYouWantToAbort, Resources.AreYouSure, MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                return;
            XNAND.Abort = true;
            abortbtn.Enabled = false;
        }

        private void MainLoad(object sender, EventArgs e)
        {
            var fi = new FileInfo("FTCSPI.dll");
            var src = Assembly.GetExecutingAssembly().GetManifestResourceStream("FTCSPI.dll");
            if (src == null || fi.Exists && fi.Length == src.Length)
                return;
            var dest = fi.OpenWrite();
            const int size = 4096;
            var bytes = new byte[size];
            int numBytes;
            while ((numBytes = src.Read(bytes, 0, size)) > 0)
                dest.Write(bytes, 0, numBytes);
            dest.Close();
            src.Close();
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

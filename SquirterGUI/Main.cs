using System.Windows.Forms;

namespace SquirterGUI
{
    using System.Threading;

    public partial class Main : Form
    {
        public Main()
        {
            InitializeComponent();
            sizebox.Items.Clear();
            sizebox.Items.Add(new ComboBoxItem("Auto", 0));
            sizebox.Items.Add(new ComboBoxItem("16MB", 0x8000));
            sizebox.Items.Add(new ComboBoxItem("64MB", 0x20000));
            sizebox.Items.Add(new ComboBoxItem("256MB", 0x80000));
            sizebox.Items.Add(new ComboBoxItem("512MB", 0x100000));
            sizebox.SelectedIndex = 0;
            
        }

        private void SetButtonState(bool state) {
            dumpbtn.Enabled = state;
            erasebtn.Enabled = state;
            writebtn.Enabled = state;
        }

        private void SetProgress(int current, int max) {
            progressbar.Value = current;
            statuslabel.Text = string.Format("Processing block 0x{0:X} of 0x{1:X}", current, max);
        }

        private void DumpbtnClick(object sender, System.EventArgs e)
        {
            bw.DoWork += BwRead;
            bw.RunWorkerAsync();
            SetButtonState(false);
            bw.DoWork -= BwRead;
        }

        private void BwRead(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            e.Result = -1;
            var worker = new xSPI();
            if (!worker.IsOk)
                return;
            e.Result = 0;
            //TODO: Add read code
            for(var i = 0; i < 0x65; i++) {
                SetProgress(i, 0x64);
                Thread.Sleep(100);
            }
        }

        private void WritebtnClick(object sender, System.EventArgs e)
        {
            bw.DoWork += BwWrite;
            bw.RunWorkerAsync();
            SetButtonState(false);
            bw.DoWork -= BwWrite;
        }

        private void BwWrite(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            e.Result = -1;
            var worker = new xSPI();
            if (!worker.IsOk)
                return;
            e.Result = 0;
            //TODO: Add write code
            for (var i = 0; i < 0x65; i++)
            {
                SetProgress(i, 0x64);
                Thread.Sleep(100);
            }
        }

        private void ErasebtnClick(object sender, System.EventArgs e)
        {
            bw.DoWork += BwErase;
            bw.RunWorkerAsync();
            SetButtonState(false);
            bw.DoWork -= BwErase;
        }

        private void BwErase(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            e.Result = -1;
            var worker = new xSPI();
            if (!worker.IsOk)
                return;
            e.Result = 0;
            //TODO: Add erase code
            for (var i = 0; i < 0x65; i++)
            {
                SetProgress(i, 0x64);
                Thread.Sleep(100);
            }
        }

        private void BwRunWorkerCompleted(object sender, System.ComponentModel.RunWorkerCompletedEventArgs e)
        {
            if (e.Result is int)
                if (((int) e.Result) == -1)
                    MessageBox.Show("There was an error...");
            SetButtonState(true);
        }
    }
}

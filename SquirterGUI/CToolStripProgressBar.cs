namespace SquirterGUI {
    using System.Windows.Forms;

    public class CToolStripProgressBar : ToolStripProgressBar {
        private delegate void SetInt(int val);
        public new int Value
        {
            get {
                return base.Value;
            }
            set {
                if (Parent != null && Parent.InvokeRequired) {
                    try {
                        SetInt setDel = delegate { base.Value = value; };
                        Parent.Invoke(setDel, new object[] { value });
                    }
                    catch {}
                }
                else if (ProgressBar != null)
                    base.Value = value;
            }
        }

        public new int Maximum 
        {
            get
            {
                return base.Maximum;
            }
            set
            {
                if (Parent != null && Parent.InvokeRequired)
                {
                    try
                    {
                        SetInt setDel = delegate { base.Maximum = value; };
                        Parent.Invoke(setDel, new object[] { value });
                    }
                    catch { }
                }
                else if (ProgressBar != null)
                    base.Maximum = value;
            }
        }
    }
}
namespace SquirterGUI {
    using System;
    using System.Windows.Forms;

    public class CTextBox : TextBox {

        private delegate string GetString();

        private delegate void SetText(string text);

        public override string Text {
            get {
                if (Parent != null && Parent.InvokeRequired) {
                    GetString getTextDel = () => base.Text;
                    var text = String.Empty;
                    try {
                        text = (string) Parent.Invoke(getTextDel, null);
                    }
                    catch {
                    }
                    return text;
                }
                return base.Text;
            }

            set {
                if (Parent != null && Parent.InvokeRequired) {
                    SetText setTextDel = delegate(string text) {
                                             base.Text = text;
                                         };

                    try {
                        Parent.Invoke(setTextDel,
                                      new object[] {
                                                       value
                                                   });
                    }
                    catch {
                    }
                }
                else
                    base.Text = value;
            }
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Ookii.Dialogs.WinForms;

namespace FriishProduce
{
    public static class MessageBox
    {
        public static DialogResult Show(string mainText, string description, MessageBoxButtons buttons, TaskDialogIcon icon = 0, int dontShow = -1, bool isLinkStyle = false)
        {
            using (TaskDialog t = new TaskDialog()
            {
                WindowTitle = Language.AppTitle(),
                MainInstruction = mainText,
                Content = description,
                ButtonStyle = isLinkStyle ? TaskDialogButtonStyle.CommandLinks : TaskDialogButtonStyle.Standard,
                AllowDialogCancellation = false
            })
            {
                var lines = mainText.Replace("\r\n", "\n").Split('\n');
                if (lines.Length >= 2)
                {
                    var secondary = new List<string>();

                    for (int i = 1; i < lines.Length; i++)
                        secondary.Add(lines[i]);

                    t.MainInstruction = lines[0];
                    t.Content = string.Join("\n", secondary.ToArray());
                }

                var O = new TaskDialogButton { Text = Language.Get("B.OK") };
                var C = new TaskDialogButton { Text = Language.Get("B.Cancel") };
                var Y = new TaskDialogButton { Text = Language.Get("B.Yes") };
                var N = new TaskDialogButton { Text = Language.Get("B.No") };

                switch (buttons)
                {
                    default:
                    case MessageBoxButtons.OK:
                    case MessageBoxButtons.AbortRetryIgnore:
                    case MessageBoxButtons.RetryCancel:
                        t.Buttons.Add(O);
                        break;

                    case MessageBoxButtons.OKCancel:
                        t.Buttons.Add(O);
                        t.Buttons.Add(C);
                        break;

                    case MessageBoxButtons.YesNo:
                        t.Buttons.Add(Y);
                        t.Buttons.Add(N);
                        break;

                    case MessageBoxButtons.YesNoCancel:
                        t.Buttons.Add(Y);
                        t.Buttons.Add(N);
                        t.Buttons.Add(C);
                        break;
                }

                switch (icon)
                {
                    case 0:
                        break;

                    default:
                        t.MainIcon = icon;
                        break;
                }

                if (dontShow >= 0) { t.VerificationText = Language.Get("DoNotShow"); }

                var text = t.ShowDialog().Text;

                if (t.IsVerificationChecked && dontShow >= 0) { Properties.Settings.Default[$"DoNotShow_{dontShow:000}"] = true; Properties.Settings.Default.Save(); }

                if (text == Language.Get("B.Cancel")) return DialogResult.Cancel;
                if (text == Language.Get("B.Yes")) return DialogResult.Yes;
                if (text == Language.Get("B.No")) return DialogResult.No;
                return DialogResult.OK;
            }
        }

        public static DialogResult Show(string mainText, string description, MessageBoxButtons buttons, TaskDialogIcon icon, bool isLinkStyle) => Show(mainText, description, buttons, icon, -1, isLinkStyle);

        public static DialogResult Show(string mainText, MessageBoxButtons buttons, TaskDialogIcon icon, bool isLinkStyle) => Show(mainText, null, buttons, icon, -1, isLinkStyle);

        public static DialogResult Show(string mainText, MessageBoxButtons buttons = MessageBoxButtons.OK, TaskDialogIcon icon = 0, int dontShow = -1) => Show(mainText, null, buttons, icon, dontShow);

        public static void Show(string mainText, string description, int dontShow = -1) => Show(mainText, description, MessageBoxButtons.OK, 0, dontShow);

        public static void Show(string mainText, int dontShow) => Show(mainText, null, MessageBoxButtons.OK, 0, dontShow);
    }
}

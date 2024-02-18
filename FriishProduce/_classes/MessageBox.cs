using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.WindowsAPICodePack.Dialogs;

namespace FriishProduce
{
    public static class MessageBox
    {
        public static DialogResult Show(string mainText, string description, MessageBoxButtons buttons, MessageBoxIcon icon)
        {
            TaskDialog t = new TaskDialog()
            {
                Caption = Language.Get("_AppTitle"),
                InstructionText = mainText,
                Text = description,
                Cancelable = false,
                OwnerWindowHandle = Program.Handle,
            };

            var lines = mainText.Replace("\r\n", "\n").Split('\n');
            if (lines.Length >= 2)
            {
                var secondary = new List<string>();

                for (int i = 1; i < lines.Length; i++)
                    secondary.Add(lines[i]);

                t.InstructionText = lines[0];
                t.Text = string.Join("\n", secondary.ToArray());
            }

            switch (buttons)
            {
                default:
                case MessageBoxButtons.OK:
                case MessageBoxButtons.AbortRetryIgnore:
                case MessageBoxButtons.RetryCancel:
                    t.StandardButtons = TaskDialogStandardButtons.Ok;
                    break;

                case MessageBoxButtons.OKCancel:
                    t.StandardButtons = TaskDialogStandardButtons.Ok | TaskDialogStandardButtons.Cancel;
                    break;

                case MessageBoxButtons.YesNo:
                    t.StandardButtons = TaskDialogStandardButtons.Yes | TaskDialogStandardButtons.No;
                    break;

                case MessageBoxButtons.YesNoCancel:
                    t.StandardButtons = TaskDialogStandardButtons.Yes | TaskDialogStandardButtons.No | TaskDialogStandardButtons.Cancel;
                    break;
            }

            switch (icon)
            {
                case MessageBoxIcon.None:
                case MessageBoxIcon.Question:
                    t.Icon = TaskDialogStandardIcon.None;

                    break;
                case MessageBoxIcon.Hand:
                    t.Icon = TaskDialogStandardIcon.Error;
                    break;

                case MessageBoxIcon.Exclamation:
                    t.Icon = TaskDialogStandardIcon.Warning;
                    break;

                case MessageBoxIcon.Asterisk:
                    t.Icon = TaskDialogStandardIcon.Information;
                    break;
            }

            switch (t.Show())
            {
                default:
                case TaskDialogResult.Ok:
                    return DialogResult.OK;

                case TaskDialogResult.Yes:
                    return DialogResult.Yes;

                case TaskDialogResult.No:
                    return DialogResult.No;

                case TaskDialogResult.Cancel:
                    return DialogResult.Cancel;
            }
        }

        public static DialogResult Show(string mainText, MessageBoxButtons buttons, MessageBoxIcon icon) => Show(mainText, null, buttons, icon);

        public static void Show(string mainText, string description = null) => Show(mainText, description, MessageBoxButtons.OK, MessageBoxIcon.None);
    }
}

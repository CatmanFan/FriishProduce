using Ookii.Dialogs.WinForms;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace FriishProduce
{
    public static class MessageBox
    {
        public enum Result
        {
            Option1 = 0,
            Option2 = 1,
            Option3 = 2,
            Option4 = 3,
            Option5 = 4,
            Yes = 5,
            No = 6,
            Ok = 7,
            Cancel = 8,
            Close = 9
        }

        public static Result Show(string mainText, string description, MessageBoxButtons buttons, TaskDialogIcon icon = 0, int dontShow = -1, bool isLinkStyle = false)
        {
            List<string> Buttons = new List<string>();

            switch (buttons)
            {
                default:
                case MessageBoxButtons.OK:
                case MessageBoxButtons.AbortRetryIgnore:
                case MessageBoxButtons.RetryCancel:
                    Buttons.Add(Language.Get("B.OK"));
                    break;

                case MessageBoxButtons.OKCancel:
                    Buttons.Add(Language.Get("B.OK"));
                    Buttons.Add(Language.Get("B.Cancel"));
                    break;

                case MessageBoxButtons.YesNo:
                    Buttons.Add(Language.Get("B.Yes"));
                    Buttons.Add(Language.Get("B.No"));
                    break;

                case MessageBoxButtons.YesNoCancel:
                    Buttons.Add(Language.Get("B.Yes"));
                    Buttons.Add(Language.Get("B.No"));
                    Buttons.Add(Language.Get("B.Cancel"));
                    break;
            }

            return Show(mainText, description, Buttons.ToArray(), icon, dontShow, isLinkStyle);
        }

        public static Result Show(string mainText, string description, string[] buttons, TaskDialogIcon icon = 0, int dontShow = -1, bool isLinkStyle = false)
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
                if (string.IsNullOrWhiteSpace(description))
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
                }

                foreach (var item in buttons)
                {
                    t.Buttons.Add(new TaskDialogButton() { Text = item });
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

                var clicked = t.ShowDialog();

                if (t.IsVerificationChecked && dontShow >= 0) { Properties.Settings.Default[$"DoNotShow_{dontShow:000}"] = true; Properties.Settings.Default.Save(); }

                if (clicked.Text == Language.Get("B.Yes")) return Result.Yes;
                if (clicked.Text == Language.Get("B.No")) return Result.No;
                if (clicked.Text == Language.Get("B.OK")) return Result.Ok;
                if (clicked.Text == Language.Get("B.Cancel")) return Result.Cancel;
                if (clicked.Text == Language.Get("B.Close")) return Result.Close;

                for (int i = 0; i < Math.Max(t.Buttons.Count, 5); i++)
                {
                    if (clicked.Text == t.Buttons[i].Text) return (Result)Enum.ToObject(typeof(Result), i);
                }

                return Result.Ok;
            }
        }

        public static Result Show(string mainText, string description, MessageBoxButtons buttons, TaskDialogIcon icon, bool isLinkStyle) => Show(mainText, description, buttons, icon, -1, isLinkStyle);

        public static Result Show(string mainText, MessageBoxButtons buttons, TaskDialogIcon icon, bool isLinkStyle) => Show(mainText, null, buttons, icon, -1, isLinkStyle);

        public static Result Show(string mainText, MessageBoxButtons buttons = MessageBoxButtons.OK, TaskDialogIcon icon = 0, int dontShow = -1) => Show(mainText, null, buttons, icon, dontShow);

        public static void Show(string mainText, string description, int dontShow = -1) => Show(mainText, description, MessageBoxButtons.OK, 0, dontShow);

        public static void Show(string mainText, int dontShow) => Show(mainText, null, MessageBoxButtons.OK, 0, dontShow);
    }
}

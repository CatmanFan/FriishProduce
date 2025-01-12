using Ookii.Dialogs.WinForms;
using System;
using System.Collections.Generic;

namespace FriishProduce
{
    public static class MessageBox
    {
        public enum Buttons
        {
            Ok,
            OkCancel,
            Cancel,
            YesNo,
            YesNoCancel,
            Close,
            Custom
        }

        public enum Icons
        {
            None = 0,
            Error = 1,
            Information = 2,
            Shield = 3,
            Warning = 4
        }

        public enum Result
        {
            Button1 = 0,
            Button2 = 1,
            Button3 = 2,
            Button4 = 3,
            Button5 = 4,
            Yes = 5,
            No = 6,
            Ok = 7,
            Cancel = 8,
            Close = 9
        }

        public static Result Show(string mainText, Buttons buttons, Icons icon) => Show(mainText, null, buttons, icon);

        public static Result Show(string mainText, string description, Buttons buttons, System.Drawing.Icon icon, int dontShow = -1, bool isLinkStyle = false) => Show(mainText, description, buttons, 0, dontShow, isLinkStyle, icon);

        public static Result Show(string mainText, string description, Buttons buttons, Icons icon = 0, int dontShow = -1, bool isLinkStyle = false, System.Drawing.Icon ico = null)
        {
            List<string> b = new List<string>();

            switch (buttons)
            {
                default:
                case Buttons.Ok:
                    b.Add(Program.Lang.String("b_ok"));
                    break;

                case Buttons.OkCancel:
                    b.Add(Program.Lang.String("b_ok"));
                    b.Add(Program.Lang.String("b_cancel"));
                    break;

                case Buttons.YesNo:
                    b.Add(Program.Lang.String("b_yes"));
                    b.Add(Program.Lang.String("b_no"));
                    break;

                case Buttons.YesNoCancel:
                    b.Add(Program.Lang.String("b_yes"));
                    b.Add(Program.Lang.String("b_no"));
                    b.Add(Program.Lang.String("b_cancel"));
                    break;

                case Buttons.Custom:
                    for (int i = 0; i < 100; i++)
                    {
                        string s = Program.Lang.String(i.ToString("000"), "messages");
                        bool valid = !string.IsNullOrWhiteSpace(description) ? s.Contains(mainText) || s.Contains(description) : s.Contains(mainText);

                        if (valid)
                            for (int j = 0; j < 5; j++)
                            {
                                string code = i.ToString("000") + "_" + (char)(j + 97);
                                if (Program.Lang.StringCheck(code)) b.Add(Program.Lang.String(code, "messages"));
                            }

                        if (b.Count > 0) break;
                    }

                    if (b.Count == 0) b.Add(Program.Lang.String("b_ok"));
                    break;

            }

            return Show(mainText, description, b.ToArray(), icon, isLinkStyle, dontShow, ico);
        }

        public static Result Show(string mainText, string description, string[] buttons, Icons icon = 0, bool isLinkStyle = false, int dontShow = -1, System.Drawing.Icon ico = null)
        {
            using (TaskDialog t = new TaskDialog()
            {
                WindowTitle = Program.Lang.ApplicationTitle,
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
                        t.Content = string.Join("\n", secondary.ToArray()).Trim();
                    }

                    else
                    {
                        t.MainInstruction = null;
                        t.Content = lines[0].Trim();
                    }
                }

                foreach (var item in buttons)
                {
                    t.Buttons.Add(new TaskDialogButton() { Text = item });
                }

                if (ico != null)
                {
                    t.MainIcon = TaskDialogIcon.Custom;
                    t.CustomMainIcon = ico;
                }

                else
                {
                    t.MainIcon = icon switch
                    {
                        Icons.Error => TaskDialogIcon.Error,
                        Icons.Information => TaskDialogIcon.Information,
                        Icons.Shield => TaskDialogIcon.Shield,
                        Icons.Warning => TaskDialogIcon.Warning,
                        _ => TaskDialogIcon.Custom
                    };
                }

                if (dontShow >= 0) { t.VerificationText = Program.Lang.String("do_not_show"); }

                Language.ScriptType script = Program.Lang.GetScript(mainText);
                t.RightToLeft = script == Language.ScriptType.RTL;
                if (script == Language.ScriptType.CJK)
                {
                    bool hasTitle = !string.IsNullOrEmpty(t.MainInstruction);
                    using System.Drawing.Font f = new(System.Drawing.FontFamily.GenericSansSerif, hasTitle ? 6.5f : 5.5f, System.Drawing.FontStyle.Regular);
                    t.Width = 250; // Math.Min(450, System.Windows.Forms.TextRenderer.MeasureText(hasTitle ? t.MainInstruction : mainText, f).Width + (hasTitle ? 0 : 5));
                }

                var clicked = t.ShowDialog();

                if (t.IsVerificationChecked && dontShow >= 0)
                {
                    var setting = Program.Config.application.GetType().GetField($"donotshow_{dontShow:000}");

                    if (setting != null)
                        setting.SetValue(typeof(bool), true);

                    Program.Config.Save();
                }

                if (clicked?.Text == Program.Lang.String("b_yes")) return Result.Yes;
                if (clicked?.Text == Program.Lang.String("b_no")) return Result.No;
                if (clicked?.Text == Program.Lang.String("b_ok")) return Result.Ok;
                if (clicked?.Text == Program.Lang.String("b_cancel")) return Result.Cancel;
                if (clicked?.Text == Program.Lang.String("b_close")) return Result.Close;

                for (int i = 0; i < Math.Max(t.Buttons.Count, 5); i++)
                {
                    if (clicked?.Text == t.Buttons[i].Text) return (Result)Enum.ToObject(typeof(Result), i);
                }

                return Result.Ok;
            }
        }

        public static Result Show(string mainText, string description, Buttons buttons, Icons icon, bool isLinkStyle) => Show(mainText, description, buttons, icon, -1, isLinkStyle);

        public static Result Show(string mainText, Buttons buttons, Icons icon, bool isLinkStyle) => Show(mainText, null, buttons, icon, -1, isLinkStyle);

        public static Result Show(string mainText, Buttons buttons, System.Drawing.Icon icon, bool isLinkStyle = false) => Show(mainText, null, buttons, icon, -1, isLinkStyle);

        public static Result Show(string mainText, Buttons buttons = Buttons.Ok, Icons icon = 0, int dontShow = -1) => Show(mainText, null, buttons, icon, dontShow);

        public static void Show(string mainText, string description, int dontShow = -1) => Show(mainText, description, Buttons.Ok, 0, dontShow);

        public static void Show(string mainText, int dontShow) => Show(mainText, null, Buttons.Ok, 0, dontShow);

        public static void Show(string mainText) => Show(mainText, null, Buttons.Ok, 0, -1);

        public static void Error(int msg) => Error(Program.Lang.Msg(msg, true));

        public static void Error(string msg)
        {
            System.Media.SystemSounds.Beep.Play();
            Show(Program.Lang.String("error", "messages"), msg, Buttons.Ok, Properties.Resources.brick);
        }
    }
}

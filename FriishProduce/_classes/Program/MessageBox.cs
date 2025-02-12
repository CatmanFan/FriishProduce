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
            Warning = 4,
            Custom = 5
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
            List<string> b = new();

            switch (buttons)
            {
                default:
                case Buttons.Ok:
                    b.Add("b_ok");
                    break;

                case Buttons.OkCancel:
                    b.Add("b_ok");
                    b.Add("b_cancel");
                    break;

                case Buttons.YesNo:
                    b.Add("b_yes");
                    b.Add("b_no");
                    break;

                case Buttons.YesNoCancel:
                    b.Add("b_yes");
                    b.Add("b_no");
                    b.Add("b_cancel");
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

                    if (b.Count == 0) b.Add("b_ok");
                    break;

            }

            return Show(mainText, description, b.ToArray(), icon, isLinkStyle, dontShow, ico);
        }

        public static Result Show(string mainText, string description, string[] buttons, Icons icon = 0, bool isLinkStyle = false, int dontShow = -1, System.Drawing.Icon ico = null)
        {
            Result? result = null;
            if (Program.GUI)
                Program.MainForm.Invoke(new System.Windows.Forms.MethodInvoker(() =>
                {
                    using (Msg d = new()
                    {
                        MsgText = mainText,
                        Description = description,
                        Buttons = buttons,
                        IconType = icon,
                        Icon = ico,
                        DoNotShow_Index = dontShow
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

                                d.MsgText = lines[0];
                                d.Description = string.Join("\n", secondary.ToArray()).Trim();
                            }

                            else
                            {
                                d.MsgText = null;
                                d.Description = lines[0].Trim();
                            }
                        }

                        Language.ScriptType script = Program.Lang.GetScript(mainText);
                        d.RightToLeft = script == Language.ScriptType.RTL ? System.Windows.Forms.RightToLeft.Yes : System.Windows.Forms.RightToLeft.No;

                        d.ShowDialog(Program.MainForm);

                        if (d.DoNotShow_Clicked)
                        {
                            var setting = Program.Config.application.GetType().GetField($"donotshow_{d.DoNotShow_Index:000}");

                            if (setting != null)
                                setting.SetValue(typeof(bool), true);

                            Program.Config.Save();
                        }

                        result = d.Result;
                    }
                }));

            return result ?? Result.Cancel;
        }

        public static Result Show(string mainText, string description, Buttons buttons, Icons icon, bool isLinkStyle) => Show(mainText, description, buttons, icon, -1, isLinkStyle);

        public static Result Show(string mainText, Buttons buttons, Icons icon, bool isLinkStyle) => Show(mainText, null, buttons, icon, -1, isLinkStyle);

        public static Result Show(string mainText, Buttons buttons, System.Drawing.Icon icon, bool isLinkStyle = false) => Show(mainText, null, buttons, icon, -1, isLinkStyle);

        public static Result Show(string mainText, Buttons buttons = Buttons.Ok, Icons icon = 0, int dontShow = -1) => Show(mainText, null, buttons, icon, dontShow);

        public static void Show(string mainText, string description, int dontShow = -1) => Show(mainText, description, Buttons.Ok, 0, dontShow);

        public static void Show(string mainText, int dontShow) => Show(mainText, null, Buttons.Ok, 0, dontShow);

        public static void Show(string mainText) => Show(mainText, null, Buttons.Ok, 0, -1);

        public static void Error(int msg) => Error(Program.Lang.Msg(msg, 1));

        public static void Error(string msg)
        {
            System.Media.SystemSounds.Beep.Play();
            Show(Program.Lang.String("error", "messages"), msg, Buttons.Ok, Properties.Resources.brick);
        }
    }
}

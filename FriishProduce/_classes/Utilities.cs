using ICSharpCode.SharpZipLib.Zip;
using SharpCompress.Archives.Zip;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace FriishProduce
{
    public enum ExportResult
    {
        SUCCESS,
        BIOS_NOT_FOUND,
        FILE_NOT_FOUND,
        FOLDER_NOT_FOUND,
        ROM_SIZE,
        FAILED_INTERNET,
        FAILED_INJECTION,
        FAILED_DOWNLOADED_WAD
    }
    public static class Logger
    {
        private static string Text { get; set; }

        public static void Log(string msg)
        {
            try { Text = File.ReadAllText(Paths.Log); } catch { }

            if (!string.IsNullOrWhiteSpace(Text))
                Text += Environment.NewLine;
            Text += $"[{DateTime.Now.Year}-{DateTime.Now.Month:D2}-{DateTime.Now.Day:D2} {DateTime.Now.Hour:D2}:{DateTime.Now.Minute:D2}:{DateTime.Now.Second:D2}] {msg}";

            // File.WriteAllText(Paths.Log, Text);
        }
    }

    public static class HTML
    {
        public static string BaseStylesheet
        {
            get
            {
                string value = string.Join("\n",
                "div { font-size: 12px !important; line-height: 0.2 !important; }",
                "ul li, h1, h2, h3, h4, h5 { margin-top: 5px !important; margin-bottom: 5px !important; }",
                "hr { border-top: 1px solid black; }");

                if (Theme.Active)
                    value += "\ndiv { color: " + System.Drawing.ColorTranslator.ToHtml(Theme.Colors.Text).ToLower() + " !important }"
                           + "\na { color: " + System.Drawing.ColorTranslator.ToHtml(Theme.Colors.Headline).ToLower() + " !important }";
                return value;
            }
        }

        public static TheArtOfDev.HtmlRenderer.WinForms.HtmlToolTip CreateToolTip() => new()
        {
            BaseStylesheet = BaseStylesheet + "\n" +
                "b { font-weight: 450 !important; }\n" +
                "div { font-family: \"Segoe UI\", sans-serif !important; }",
            StripAmpersands = false,
            InitialDelay = 300,
            AutoPopDelay = 12000,
            TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit,
            UseGdiPlusTextRendering = true,
            UseFading = true,
            UseAnimation = true,
            MaximumSize = new System.Drawing.Size(375, 0),
        };

        public static string MarkdownToHTML(string[] input)
        {
            for (int i = 0; i < input.Length; i++)
            {
                string line = input[i];
                string placeholder = "[`0/[REPLACEME!!!! NO.{0}]/0`]";

                if (line == null) line = "<br />";

                line = line.Replace("\r", "");
                line = line.Replace("<br>", "\n");
                line = line.Replace("<br/>", "\n");
                line = line.Replace("\n", "<br />");

                if (string.IsNullOrWhiteSpace(line)) line = "";

                // Get URLs
                // ****************
                var URL = new Regex(@"\[(.*?)\]\((.*?)\)");
                List<string> urls = new();
                if (URL.Match(line).Success)
                {
                    var matches = URL.Matches(line);
                    for (int x = 0; x < matches.Count; x++)
                    {
                        urls.Add(matches[x].Value);
                        line = line.Replace(urls[x], string.Format(placeholder, x));
                    }
                }

                // Bold
                // ****************
                line = new Regex(@"\*\*(.*?)\*\*").Replace(line, "<b>$1</b>");

                // Italic
                // ****************
                line = new Regex(@"\*(.*?)\*").Replace(line, "<i>$1</i>");
                line = new Regex(@"_(.*?)_").Replace(line, "<i>$1</i>");

                // Replace URLs
                // ****************
                for (int x = 0; x < urls.Count; x++)
                {
                    line = line.Replace(string.Format(placeholder, x), URL.Replace(urls[x], "<a href=\"$2\">$1</a>"));
                }

                if (line.StartsWith("#####")) line = "<h5>" + line.Remove(0, 5).TrimStart() + "</h5>";
                if (line.StartsWith("####")) line = "<h4>" + line.Remove(0, 4).TrimStart() + "</h4>";
                if (line.StartsWith("###")) line = "<h3>" + line.Remove(0, 3).TrimStart() + "</h3>";
                if (line.StartsWith("##")) line = "<h2>" + line.Remove(0, 2).TrimStart() + "</h2>";
                if (line.StartsWith("#")) line = "<h1>" + line.Remove(0, 1).TrimStart() + "</h1>";

                if (line.StartsWith("* ") || line.StartsWith("- ")) line = "<li>" + line.Remove(0, 2) + "</li>";

                input[i] = line;
            }

            var output = new List<string>();
            bool list = false;

            foreach (var line in input)
            {
                if (line?.StartsWith("<li>") == true)
                {
                    if (!list)
                    {
                        output.Add("<ul>");
                        list = true;
                    }
                }
                else
                {
                    if (list)
                    {
                        output.Add("</ul>");
                        list = false;
                    }
                }

                output.Add(line);
            }

            try { output[0] = "<div>" + output[0]; } catch { }
            try { output[output.Count - 1] = output[output.Count - 1] + "</div>"; } catch { }

            return string.Join("\n", output.ToArray());
        }
    }

    public static class Web
    {
        private static bool _compatibilityMode;
        private static bool CompatibilityMode
        {
            get => _compatibilityMode;
            set
            {
                _compatibilityMode = value;

                if (value)
                {
                    ServicePointManager.Expect100Continue = true;
                    ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                }

                else
                {
                    ServicePointManager.SecurityProtocol = SecurityProtocolType.SystemDefault;
                }
            }
        }

        public static bool InternetTest(string URL = null, bool showDialog = true)
        {
            if (showDialog)
                Program.MainForm?.Wait(true, true, false, 0, 2);

            Start:
            int timeout = 30;
            int region = System.Globalization.CultureInfo.InstalledUICulture.Name.StartsWith("fa") ? 2
                       : System.Globalization.CultureInfo.InstalledUICulture.Name.Contains("zh-CN") ? 1
                       : -1;
            if (string.IsNullOrWhiteSpace(URL)) URL =
                region == 2 ? "https://www.aparat.com/" :
                region == 1 ? "http://www.baidu.com/" :
                region == 0 ? "https://www.google.com/" :
                "https://www.example.com/";

            try
            {
                Request:
                if (!URL.StartsWith("https://") && !URL.StartsWith("http://")) URL = "https://" + URL;
                if (!URL.EndsWith("/")) URL += "/";

                Logger.Log($"Sending initial Web request to URL: {URL}");
                var request = (HttpWebRequest)WebRequest.Create(URL);

                URL = request.Address.Authority;
                request.Method = "HEAD";
                request.KeepAlive = false;
                request.Timeout = timeout * 1000;
                request.UserAgent = "Mozilla/4.0 (compatible; MSIE 8.0; Windows NT 6.0; WOW64; " +
                                    "Trident/4.0; SLCC1; .NET CLR 2.0.50727; Media Center PC 5.0; " +
                                    ".NET CLR 3.5.21022; .NET CLR 3.5.30729; .NET CLR 3.0.30618; " +
                                    "InfoPath.2; OfficeLiveConnector.1.3; OfficeLivePatch.0.0)";

                bool result = false;

                if (CheckDomain(URL, timeout))
                {
                    WebResponse response = null;
                    try { response = request.GetResponse(); }
                    catch (Exception ex)
                    {
                        if (region < 0 && region > 2)
                        {
                            region = 0;
                            goto Request;
                        }
                        else { throw ex; }
                    }

                    for (int i = 0; i < 2; i++)
                    {
                        char x = response.ResponseUri.ToString()[i];
                    }

                    result = true;
                }

                if (showDialog)
                    Program.MainForm?.Wait(false, false, false);
                return result;
            }

            catch (Exception ex)
            {
                // Go back to beginning and set compatibility mode to true in event of SSL/TLS secure channel failure (Windows 7)
                // ****************
                if (ex.GetType() == typeof(WebException) && (ex as WebException).Status == WebExceptionStatus.SecureChannelFailure)
                {
                    if (!CompatibilityMode)
                    {
                        Logger.Log("Failed to send initial Web request. Starting over in compatibility mode.");
                        CompatibilityMode = true;
                        goto Start;
                    }
                }

                if (showDialog)
                    Program.MainForm?.Wait(false, false, false);

                // Automatically return true in event of 429: Too many requests
                // ****************
                if (ex.GetType() == typeof(WebException) && (ex as WebException).Status == WebExceptionStatus.ProtocolError)
                    return true;

                Logger.Log("Failed to send initial Web request. Process halted.");
                throw new Exception(string.Format(Program.Lang.Msg(0, 1), Message(ex, URL)));
            }
        }

        public static string Message(Exception ex, string url)
        {
            string msg = _message(ex.Message, url);

            if (ex.InnerException != null)
            {
                Exception inner = ex.InnerException;
                msg += "\n\n- " + _message(inner.Message, url);

                if (inner.InnerException != null)
                {
                    Exception inner1 = ex.InnerException;
                    msg += "\n  - " + _message(inner1.Message, url);

                    if (inner1.InnerException != null)
                    {
                        Exception inner2 = inner1.InnerException;
                        msg += "\n    - " + _message(inner2.Message, url);
                    }
                }
            }

            return msg;
        }

        private static string _message(string msg, string url)
        {
            int colon = msg.IndexOf(':');
            char dot = Program.Lang.GetScript(msg) == Language.ScriptType.CJK && Program.Lang.GetRegion() is not Language.Region.Korea ? '。' : '.';

            if (!string.IsNullOrWhiteSpace(url) && msg.Contains(url) && colon > 0) msg = msg.Substring(0, colon);
            if (msg[msg.Length - 1] != dot) msg += dot;

            return msg;
        }

        private static bool CheckDomain(string URL, int timeout)
        {
            string host;
            try { host = new Uri(URL).Host; } catch { host = URL; }

            using (var tcp = new TcpClient())
            {
                var result = tcp.BeginConnect(host, 80, null, null);
                if (!result.AsyncWaitHandle.WaitOne(TimeSpan.FromSeconds(timeout)))
                {
                    throw new WebException("The domain is not available.", WebExceptionStatus.ConnectFailure);
                }

                tcp.EndConnect(result);
                return true;
            }
        }

        public static byte[] Get(string URL, int timeout = 200)
        {
            Start:
            Logger.Log("Downloading from URL: " + URL);

            // Actual web connection is done here
            // ****************
            using (MemoryStream ms = new())
            using (WebClient x = new())
            {
                try
                {
                    if (Task.WaitAny
                    (new Task[]{Task.Run(() => { try { x.OpenRead(URL).CopyTo(ms); return ms; } catch { return null; } }
                    )}, timeout * 1000) == -1)
                        throw new TimeoutException();

                    if (ms.ToArray().Length == 0) throw new FileNotFoundException();
                    else if (ms.ToArray().Length > 75) return ms.ToArray();

                    throw new WebException();
                }

                catch (Exception ex)
                {
                    // Go back to beginning and set compatibility mode to true in event of SSL/TLS secure channel failure (Windows 7)
                    // ****************
                    if (!CompatibilityMode && ex.GetType() == typeof(WebException) && (ex as WebException).Status == WebExceptionStatus.SecureChannelFailure)
                    {
                        Logger.Log("Failed to download from URL. Starting over in compatibility mode.");
                        CompatibilityMode = true;
                        goto Start;
                    }

                    else if (ex.GetType() == typeof(FileNotFoundException))
                        throw ex;

                    else
                        throw new Exception(string.Format(Program.Lang.Msg(0, 1), Message(ex, URL)));
                }
            }
        }
    }

    public static class Byte
    {
        public static readonly byte[] Dummy = new byte[] { 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF };

        public static string GetPattern(byte[] source, int index = 0, int length = 16) => Encoding.GetEncoding(1252).GetString(source.Skip(index).Take(length).ToArray());

        public static int PatternAt(byte[] source, byte[] pattern)
        {
            for (int i = 0; i < source.Length; i++)
                if (source.Skip(i).Take(pattern.Length).SequenceEqual(pattern))
                    return i;
            return -1;
        }

        public static int PatternAt(byte[] source, string text)
        {
            byte[] pattern = Encoding.ASCII.GetBytes(text);
            for (int i = 0; i < source.Length; i++)
                if (source.Skip(i).Take(pattern.Length).SequenceEqual(pattern))
                    return i;
            return -1;
        }

        public static byte[] FromHex(string hex)
        {
            hex = hex.ToUpper().Replace("-", "").Replace(" ", "");
            return Enumerable.Range(0, hex.Length)
                         .Where(x => x % 2 == 0)
                         .Select(x => Convert.ToByte(hex.Substring(x, 2), 16))
                         .ToArray();
        }

        public static int IndexOf(byte[] source, byte[] pattern, int start = 0, int end = -1)
        {
            if (start < 0 || start > end - pattern.Length) start = 0;
            if (end > source.Length || end < 0) end = source.Length - pattern.Length;

            for (int i = start; i < end; i++)
            {
                if (source[i] != pattern[0]) continue;

                for (int x = pattern.Length - 1; x >= 1; x--)
                {
                    try { if (source[i + x] != pattern[x]) break; } catch { }
                    if (x == 1) return i;
                }
            }

            return -1;
        }

        public static int IndexOf(byte[] source, string pattern, int start = 0, int end = -1)
        {
            var pArray = pattern.Split(' ');
            var pBytes = new byte[pArray.Length];
            try
            {
                for (int i = 0; i < pArray.Length; i++)
                    pBytes[i] = Convert.ToByte(pArray[i], 16);
            }
            catch
            {
                pBytes = Encoding.GetEncoding(1252).GetBytes(pattern);
            }

            if (end > source.Length || end < 0) end = source.Length - pBytes.Length;
            return IndexOf(source, pBytes, start, end);
        }

        public static bool IsSame(byte[] first, byte[] second)
        {
            return first.Length == second.Length && System.Collections.StructuralComparisons.StructuralEqualityComparer.Equals(first, second);
        }
    }

    public static class Utils
    {
        public static string Run(byte[] app, string appName, string arguments, bool showWindow = false, bool redirectOutput = true)
        {
            string targetPath = Paths.WorkingFolder + Path.GetFileNameWithoutExtension(appName) + ".exe";

            File.WriteAllBytes(targetPath, app);
            string value = Run(targetPath, Paths.WorkingFolder, arguments, showWindow, redirectOutput);
            try { File.Delete(targetPath); } catch { }

            return value;
        }

        public static string Run(string app, string arguments, bool showWindow = false, bool redirectOutput = true) => Run(app, Paths.Tools, arguments, showWindow, redirectOutput);

        public static string Run(string app, string workingFolder, string arguments, bool showWindow = false, bool redirectOutput = true)
        {
            var appPath = Path.Combine(Paths.Tools, app.Replace(Paths.Tools, "").Contains('\\') ? app.Replace(Paths.Tools, "") : Path.GetFileName(app));

            if (!appPath.EndsWith(".exe")) appPath += ".exe";

            if (!File.Exists(appPath)) throw new Exception(string.Format(Program.Lang.Msg(6, 1), app));

            using Process p = Process.Start(new ProcessStartInfo
            {
                FileName = appPath,
                WorkingDirectory = workingFolder,
                Arguments = arguments,
                UseShellExecute = false,
                CreateNoWindow = !showWindow,
                RedirectStandardOutput = redirectOutput
            });

            p.WaitForExit();
            return redirectOutput ? p.StandardOutput.ReadToEnd() : null;
        }

        public static (bool Compressed, byte[] Data) ExtractContent1(byte[] input)
        {
            // Create temporary files at working folder
            // ****************
            File.WriteAllBytes(Paths.WorkingFolder + "main.dol", input);

            // Decompress
            // ****************
            Run
            (
                FileDatas.Apps.wwcxtool,
                "wwcxtool.exe",
                "/u main.dol main.dec.dol"
            );

            bool compressed = File.Exists(Paths.WorkingFolder + "main.dec.dol");
            return (compressed, compressed ? File.ReadAllBytes(Paths.WorkingFolder + "main.dec.dol") : input);
        }

        public static byte[] PackContent1(byte[] input, bool? compressed = null)
        {
            if (compressed == null)
                compressed = File.Exists(Paths.WorkingFolder + "main.dec.dol");

            if (compressed == true)
            {
                // Write new to original decompressed file
                // ****************
                File.WriteAllBytes(Paths.WorkingFolder + "main.dec.dol", input);

                // Pack
                // ****************
                Run
                (
                    FileDatas.Apps.wwcxtool,
                    "wwcxtool.exe",
                    "/cr main.dol main.dec.dol main.new.dol"
                );

                if (!File.Exists(Paths.WorkingFolder + "main.new.dol"))
                    throw new Exception(Program.Lang.Msg(2, 1));

                var output = File.ReadAllBytes(Paths.WorkingFolder + "main.new.dol");

                try { File.Delete(Paths.WorkingFolder + "main.new.dol"); } catch { }
                try { File.Delete(Paths.WorkingFolder + "main.dec.dol"); } catch { }
                try { File.Delete(Paths.WorkingFolder + "main.dol"); } catch { }

                return output;
            }

            else return input;
        }

        private enum VideoModes
        {
            NTSC_Interl,
            NTSC_NonInt,
            NTSC_Prgsiv,

            MPAL_Interl,
            MPAL_NonInt,
            MPAL_Prgsiv,

            PAL60_Interl,
            PAL60_NonInt,
            PAL60_Prgsiv,

            PAL50_Interl,
            PAL50_NonInt,
            PAL50_Prgsiv
        }

        private static void copyVideoMode(byte[] dol, int index, string code)
        {
            string[] array = code.Split(' ');

            byte?[] converted = new byte?[array.Length];

            for (int i = 0; i < converted.Length; i++)
                if (array[i].ToLower() != "xx")
                    converted[i] = Convert.ToByte(array[i], 16);

            byte[] target = dol.Skip(index).Take(converted.Length).ToArray();

            for (int i = 0; i < converted.Length; i++)
                if (converted[i].HasValue)
                    target[i] = converted[i].Value;

            target.CopyTo(dol, index);
        }

        private static SortedDictionary<int, VideoModes> parseVideoModes(byte[] dol, Dictionary<VideoModes, string> modes, int start = 0, int end = -1)
        {
            SortedDictionary<int, VideoModes> value = new();
            foreach (var mode in modes)
                for (int index = start; (index = Byte.IndexOf(dol, mode.Value.Substring(0, 23), index, end)) > 0;)
                    if (!value.ContainsKey(index))
                    {
                        value.Add(index, mode.Key);
                        index += 1;
                    }

            return value;
        }

        public static void ChangeVideoMode(libWiiSharp.WAD wad, int mode = 0, int wiiu = 0)
        {
            // Modes:
            // 0 = None
            // 1 = NTSC
            // 2 = MPAL
            // 3 = PAL60
            // 4 = PAL50
            // 5 = NTSC/MPAL
            // 7 = PAL60/50
            // 6 = NTSC/PAL60
            // 8 = MPAL/PAL50

            wiiu = 0;

            bool VideoModeChanger = mode > 0;
            bool WiiUAspectRatio = wiiu > 0;

            if (VideoModeChanger || WiiUAspectRatio)
            {
                var content1 = ExtractContent1(wad.Contents[1]);

                if (VideoModeChanger)
                {
                    #region List of byte patterns and corresponding video modes
                    /// NTSC & PAL60: 60Hz ///
                    // NTSC (interlaced)   / 480i       00 02 80 01 E0 01 E0 00 28 00 00 02 80 01 E0
                    // NTSC (non interlaced)            01 02 80 01 E0 01 E0 00 28 00 0B 02 80 01 E0
                    // NTSC (progressive)  / 480p       02 02 80 01 E0 01 E0 00 28 00 00 02 80 01 E0
                    // PAL60 (interlaced)  / 480i       14 02 80 01 E0 01 E0 00 28 00 00 02 80 01 E0
                    // PAL60 (non interlaced)           15 02 80 01 E0 01 E0 00 28 00 00 02 80 01 E0
                    // PAL60 (progressive) / 480p       16 02 80 01 E0 01 E0 00 28 00 00 02 80 01 E0

                    /// MPAL: 50Hz for American region ///
                    // MPAL (interlaced)                08 02 80 01 E0 01 E0 00 28 00 00 02 80 01 E0
                    // MPAL (non interlaced)            09 02 80 01 E0 01 E0 00 28 00 00 02 80 01 E0
                    // MPAL (progressive)               0A 02 80 01 E0 01 E0 00 28 00 00 02 80 01 E0
                    /// PAL: replace 01 at end with 02 ///
                    // PAL (interlaced)    / 576i       04 02 80 02 10 02 10 00 28 00 17 02 80 02 10
                    // PAL (non interlaced)             05 02 80 01 08 01 08 00 28 00 0B 02 80 02 10
                    // PAL (progressive)                06 02 80 02 10 02 10 00 28 00 17 02 80 02 10
                    /// ONLY PAL50 is different in byte composition !                 ^^ This byte seems to vary ///
                    // PAL (progressive/alt)          = 06 02 80 01 08 02 0C 00 28 00 17 02 80 02 0C
                    #endregion

                    int start = 0x13F000;
                    int end = 0x1FFFFF;

                    // 0-2: NTSC/PAL
                    // 3-5: MPAL/PAL
                    // 6: Backup for PAL (progressive)
                    Dictionary<VideoModes, string> ModeCodes = new()
                    {
                        { VideoModes.NTSC_Interl, "00 02 80 01 E0 01 E0 00 28 00 xx 02 80 01 E0" },
                        { VideoModes.NTSC_NonInt, "01 02 80 01 E0 01 E0 00 28 00 xx 02 80 01 E0" },
                        { VideoModes.NTSC_Prgsiv, "02 02 80 01 E0 01 E0 00 28 00 xx 02 80 01 E0" },
                        { VideoModes.MPAL_Interl, "08 02 80 01 E0 01 E0 00 28 00 xx 02 80 01 E0" },
                        { VideoModes.MPAL_NonInt, "09 02 80 01 E0 01 E0 00 28 00 xx 02 80 01 E0" },
                        { VideoModes.MPAL_Prgsiv, "0A 02 80 01 E0 01 E0 00 28 00 xx 02 80 01 E0" },
                        { VideoModes.PAL60_Interl, "14 02 80 01 E0 01 E0 00 28 00 xx 02 80 01 E0" },
                        { VideoModes.PAL60_NonInt, "15 02 80 01 E0 01 E0 00 28 00 xx 02 80 01 E0" },
                        { VideoModes.PAL60_Prgsiv, "16 02 80 01 E0 01 E0 00 28 00 xx 02 80 01 E0" },
                        { VideoModes.PAL50_Interl, "04 02 80 02 10 02 10 00 28 00 xx 02 80 02 10" },
                        { VideoModes.PAL50_NonInt, "05 02 80 01 08 01 08 00 28 00 xx 02 80 02 10" },
                        { VideoModes.PAL50_Prgsiv, "06 02 80 02 10 02 10 00 28 00 xx 02 80 02 10" },
                    };

                    int alt = 0;

                    Search:
                    SortedDictionary<int, VideoModes> Indexes = parseVideoModes(content1.Data, ModeCodes, start, end);

                    if (Indexes.Count == 0)
                    {
                        alt++;

                        switch (alt)
                        {
                            case 1:
                                // Offsets found in Super Mario RPG
                                start = 0x54A000;
                                end = 0x561000;
                                goto Search;

                            default:
                                // Some other offsets are still not known, should be added later when I do more research
                                MessageBox.Show(Program.Lang.Msg(20, 1));
                                goto Second;
                        }
                    }

                    switch (mode)
                    {
                        default:
                            int TargetMode_index = mode switch
                            {
                                1 => 0, // NTSC
                                2 => 3, // MPAL
                                3 => 6, // PAL60
                                4 => 9, // PAL50
                                _ => 0
                            };

                            foreach (var Index in Indexes)
                            {
                                int offset = Index.Value is VideoModes.NTSC_Interl
                                                         or VideoModes.MPAL_Interl
                                                         or VideoModes.PAL60_Interl
                                                         or VideoModes.PAL50_Interl ? 0
                                           : Index.Value is VideoModes.NTSC_NonInt
                                                         or VideoModes.MPAL_NonInt
                                                         or VideoModes.PAL60_NonInt
                                                         or VideoModes.PAL50_NonInt ? 1
                                           : 2;

                                var TargetMode = ModeCodes.ElementAt(TargetMode_index + offset);

                                if (Index.Value != TargetMode.Key)
                                    copyVideoMode(content1.Data, Index.Key, TargetMode.Value);
                            }
                            break;

                        case 5:
                        case 6:
                        case 7:
                        case 8:
                            (VideoModes[] outputs, VideoModes[] inputs) = mode switch
                            {
                                // **** NTSC/MPAL configuration **** //
                                5 =>
                                (
                                    // Outputs
                                    new VideoModes[]
                                    {
                                    VideoModes.NTSC_Interl,     VideoModes.NTSC_NonInt,     VideoModes.NTSC_Prgsiv,
                                    VideoModes.MPAL_Interl,     VideoModes.MPAL_NonInt,     VideoModes.MPAL_Prgsiv
                                    },

                                    // To be converted
                                    new VideoModes[]
                                    {
                                    VideoModes.PAL60_Interl,    VideoModes.PAL60_NonInt,    VideoModes.PAL60_Prgsiv,
                                    VideoModes.PAL50_Interl,    VideoModes.PAL50_NonInt,    VideoModes.PAL50_Prgsiv
                                    }
                                ),

                                // **** PAL60/50 configuration **** //
                                6 =>
                                (
                                    // Outputs
                                    new VideoModes[]
                                    {
                                    VideoModes.PAL60_Interl,    VideoModes.PAL60_NonInt,    VideoModes.PAL60_Prgsiv,
                                    VideoModes.PAL50_Interl,    VideoModes.PAL50_NonInt,    VideoModes.PAL50_Prgsiv
                                    },

                                    // To be converted
                                    new VideoModes[]
                                    {
                                    VideoModes.NTSC_Interl,     VideoModes.NTSC_NonInt,     VideoModes.NTSC_Prgsiv,
                                    VideoModes.MPAL_Interl,     VideoModes.MPAL_NonInt,     VideoModes.MPAL_Prgsiv
                                    }
                                ),

                                // **** NTSC/PAL60 configuration **** //
                                7 =>
                                (
                                    // Outputs
                                    new VideoModes[]
                                    {
                                    VideoModes.NTSC_Interl,     VideoModes.NTSC_NonInt,     VideoModes.NTSC_Prgsiv,
                                    VideoModes.PAL60_Interl,    VideoModes.PAL60_NonInt,    VideoModes.PAL60_Prgsiv
                                    },

                                    // To be converted
                                    new VideoModes[]
                                    {
                                    VideoModes.MPAL_Interl,     VideoModes.MPAL_NonInt,     VideoModes.MPAL_Prgsiv,
                                    VideoModes.PAL50_Interl,    VideoModes.PAL50_NonInt,    VideoModes.PAL50_Prgsiv
                                    }
                                ),

                                // **** MPAL/PAL50 configuration **** //
                                _ =>
                                (
                                    // Outputs
                                    new VideoModes[]
                                    {
                                    VideoModes.MPAL_Interl,     VideoModes.MPAL_NonInt,     VideoModes.MPAL_Prgsiv,
                                    VideoModes.PAL50_Interl,    VideoModes.PAL50_NonInt,    VideoModes.PAL50_Prgsiv
                                    },

                                    // To be converted
                                    new VideoModes[]
                                    {
                                    VideoModes.NTSC_Interl,     VideoModes.NTSC_NonInt,     VideoModes.NTSC_Prgsiv,
                                    VideoModes.PAL60_Interl,    VideoModes.PAL60_NonInt,    VideoModes.PAL60_Prgsiv
                                    }
                                ),
                            };

                            for (int i = 0; i < inputs.Length; i++)
                            {
                                if (inputs[i] != outputs[i])
                                {
                                    foreach (var Index in Indexes.Where(x => x.Value == inputs[i]))
                                    {
                                        copyVideoMode(content1.Data, Index.Key, ModeCodes[outputs[i]]);
                                    }
                                }
                            }
                            break;
                    }

                    // Reparse list so that we can see the changed values
                    Indexes = parseVideoModes(content1.Data, ModeCodes, start, end);
                }

                Second:
                /* Force 4:3 aspect ratio display on Wii U (NOT WORKING) */
                // *************************
                if (WiiUAspectRatio)
                {
                    switch (wiiu)
                    {
                        default:
                        case 0:
                            break;

                        case 1:
                            File.WriteAllBytes(Paths.WorkingFolder + "main.dec.dol", content1.Data);

                            string output = Run
                            (
                                "wstrt\\wstrt.exe",
                                $"patch \"{Path.GetFullPath(Paths.WorkingFolder + "main.dec.dol")}\" --add-section \"{Path.GetFullPath(Paths.Tools + "wstrt\\Force43.gct")}\""
                            );

                            bool isModified = !content1.Data.SequenceEqual(File.ReadAllBytes(Paths.WorkingFolder + "main.dec.dol"));

                            if (isModified) content1.Data = File.ReadAllBytes(Paths.WorkingFolder + "main.dec.dol");
                            break;
                    }
                }

                var newContent1 = PackContent1(content1.Data, content1.Compressed);

                wad.Contents[1] = newContent1;
                if (!Byte.IsSame(wad.Contents[1], newContent1))
                {
                    wad.Unpack(Paths.WAD);
                    File.WriteAllBytes(Paths.WAD + "00000001.app", newContent1);
                    wad.CreateNew(Paths.WAD);
                    Directory.Delete(Paths.WAD, true);
                }
            }
        }
    }

    public static class RTP
    {
        public static bool IsValid(string path)
        {
            if (!Directory.Exists(path)) return false;
            int isValid = 0;

            string[] folders = new string[]
                {
                    "Battle",
                    "CharSet",
                    "ChipSet",
                    "FaceSet",
                    "GameOver",
                    "Music",
                    "Title",
                    "Sound",
                    "System",
                    "Title"
                };

            try
            {
                foreach (var item in folders)
                    if (Directory.Exists(Path.Combine(path, item)))
                        if (Directory.GetFiles(Path.Combine(path, item)).Length > 0)
                            isValid++;
            }
            catch { }

            return isValid >= 10;
        }
    }

    public static class Zip
    {
        /// <summary>
        /// Used to convert any ZIP file entry stream to a usable byte array.
        /// </summary>
        private static byte[] toByteArray(Stream entry)
        {
            try
            {
                List<byte> bytes = new();

                using (var s = entry)
                {
                    int curByte = s.ReadByte();
                    while (curByte != -1)
                    {
                        bytes.Add(Convert.ToByte(curByte));
                        curByte = s.ReadByte();
                    }
                }

                try { entry.Close(); }
                catch { }
                try { entry.Dispose(); }
                catch { }
                return bytes.ToArray();
            }

            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Gets a specific file from the archive using its name. (SharpCompress)
        /// </summary>
        public static byte[] Extract(ZipArchive zip, string file)
        {
            foreach (var entry in zip.Entries.Where(entry => !entry.IsDirectory && entry.Key != null))
            {
                if (Path.GetFileName(entry.Key).ToLower() == file.ToLower())
                {
                    return Extract(entry);
                }
            }

            return null;
        }

        /// <summary>
        /// Gets a specific file from the archive. (SharpCompress)
        /// </summary>
        public static byte[] Extract(ZipArchiveEntry file) => toByteArray(file.OpenEntryStream());

        /// <summary>
        /// Gets a specific file from the archive using its name. (SharpZipLib)
        /// </summary>
        public static byte[] Extract(ZipFile zip, string file)
        {
            var entry = zip.GetEntry(file);

            if (entry != null && entry.IsFile)
            {
                return Extract(zip, entry);
            }

            return null;
        }

        /// <summary>
        /// Gets a specific file from the archive. (SharpZipLib)
        /// </summary>
        public static byte[] Extract(ZipFile zip, ZipEntry entry) => toByteArray(zip.GetInputStream(entry));
    }
}
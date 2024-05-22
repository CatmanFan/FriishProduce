using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace FriishProduce
{
    public static class Web
    {
        public static bool InternetTest(string URL = null, int timeout = 30)
        {
            try
            {
                var request = (HttpWebRequest)WebRequest.Create
                (
                    !string.IsNullOrWhiteSpace(URL) ? URL :
                    (
                        System.Globalization.CultureInfo.InstalledUICulture.Name.StartsWith("fa") ? "https://www.aparat.com/" :
                        System.Globalization.CultureInfo.InstalledUICulture.Name.Contains("zh-CN") ? "http://www.baidu.com/" :
                        "https://www.google.com/"
                    )
                );

                URL = request.Address.Authority;
                request.Method = "HEAD";
                request.KeepAlive = false;
                request.Timeout = timeout * 500;

                if (CheckDomain(URL, timeout))
                {
                    var response = request.GetResponse();

                    for (int i = 0; i < 2; i++)
                    {
                        char x = response.ResponseUri.ToString()[i];
                    }

                    return true;
                }

                return false;
            }

            catch (Exception ex)
            {
                string message = (ex.Message.Contains(URL) ? ex.Message.Substring(0, ex.Message.IndexOf(':')) : ex.Message) + (ex.Message[ex.Message.Length - 1] != '.' ? "." : string.Empty);
                throw new Exception(string.Format(Program.Lang.Msg(0, true), message));
            }
        }

        private static bool CheckDomain(string URL, int timeout)
        {
            string host = URL;
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

        public static byte[] Get(string URL)
        {
            // Actual web connection is done here
            // ****************

            using (MemoryStream ms = new MemoryStream())
            using (WebClient x = new WebClient())
            {
                if (Task.WaitAny
                    (new Task[]{Task.Run(() => { try { x.OpenRead(URL).CopyTo(ms); return ms; } catch { return null; } }
                    )}, 100 * 1000) == -1)
                    throw new TimeoutException();

                if (ms.ToArray().Length > 75) return ms.ToArray();

                throw new WebException();
            }
        }
    }

    public static class Byte
    {
        public static readonly byte[] Dummy = new byte[] { 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF };

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

        public static int IndexOf(byte[] source, byte[] pattern, int start, int end)
        {
            if (start < 0) start = 0;
            if (end > source.Length) end = source.Length - pattern.Length;

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
            for (int i = 0; i < pArray.Length; i++)
                pBytes[i] = Convert.ToByte(pArray[i], 16);

            if (end == -1) end = source.Length - pBytes.Length;
            return IndexOf(source, pBytes, start, end);
        }

        public static bool IsSame(byte[] first, byte[] second)
        {
            return first.Length == second.Length && System.Collections.StructuralComparisons.StructuralEqualityComparer.Equals(first, second);
        }
    }

    public static class Utils
    {
        public static void Run(byte[] app, string appName, string arguments, bool showWindow = false)
        {
            string targetPath = Paths.WorkingFolder + Path.GetFileNameWithoutExtension(appName) + ".exe";
            File.WriteAllBytes(targetPath, app);
            Run(targetPath, Paths.WorkingFolder, arguments, showWindow);

            if (File.Exists(targetPath)) File.Delete(targetPath);
        }

        public static void Run(string app, string arguments, bool showWindow = false) => Run(app, Paths.Tools, arguments, showWindow);

        public static void Run(string app, string workingFolder, string arguments, bool showWindow = false)
        {
            var appPath = Path.Combine(Paths.Tools, app.Replace(Paths.Tools, "").Contains('\\') ? app.Replace(Paths.Tools, "") : Path.GetFileName(app));

            if (!appPath.EndsWith(".exe")) appPath += ".exe";

            if (!File.Exists(appPath)) throw new Exception(string.Format(Program.Lang.Msg(5, true), app));

            using (Process p = Process.Start(new ProcessStartInfo
            {
                FileName = appPath,
                WorkingDirectory = workingFolder,
                Arguments = arguments,
                UseShellExecute = false,
                CreateNoWindow = !showWindow
            }))
                p.WaitForExit();
        }
    }
}

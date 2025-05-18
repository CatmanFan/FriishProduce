using ICSharpCode.SharpZipLib.Zip;
using System.Collections.Generic;
using System.IO;

namespace FriishProduce
{
    public class RPGM : ROM
    {
        public override bool CheckValidity(string path)
        {
            bool isZip = true;
            try { ZIP = new(path); }
            catch { /* Not a ZIP file */ ZIP = null; isZip = false; }

            if (isZip)
                return CheckZIPValidity(new string[] { "ldb" }, true, true, path);
            else
                return Path.GetExtension(path).ToLower() == ".ldb";
        }

        public string GetTitle(string path = null)
        {
            if (path == null) return null;

            string f = null;
            ZipEntry entry = null;
            string[] ini = null;
            
            if (ZIP != null)
            {
                entry = GetZIPEntry("rpg_rt.ini", true, true, true);

                if (entry != null)
                {
                    ini = System.Text.Encoding.Default.GetString(Zip.Extract(ZIP, entry)).Split('\n');
                }
            }

            else
            {
                foreach (var file in Directory.EnumerateFiles(Path.GetDirectoryName(path), "*.*", SearchOption.AllDirectories))
                {
                    if (Path.GetFileName(file).ToLower() == "rpg_rt.ini")
                    {
                        f = file;
                        ini = File.ReadAllLines(f);
                    }
                }
            }

            if (ini?.Length > 0)
            {
                for (int l = 0; l < ini.Length; l++)
                {
                    if (ini[l].ToLower().StartsWith("gametitle="))
                    {
                        string output = ini[l];

                        List<string> encodings = new();
                        foreach (var encoding in Encodings)
                        {
                            try { encodings.Add(entry != null ? encoding.GetString(Zip.Extract(ZIP, entry)).Split('\n')[l] : File.ReadAllLines(f, encoding)[l]); }
                            catch { }
                        }

                        // Convert to CJK if found
                        foreach (var encoding in encodings)
                        {
                            if (Program.Lang.GetScript(encoding) == Language.ScriptType.CJK)
                            {
                                output = encoding;
                                goto End;
                            }
                        }

                        End:
                        return output.Substring("GameTitle=".Length).Replace("\r", null);
                    }
                }
            }

            return null;
        }
    }
}

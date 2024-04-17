using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization.Metadata;
using libWiiSharp;

namespace FriishProduce
{
    public class Database
    {
        protected class NUSD
        {
            private static string Secret(int start, int len)
            {
                string ret = "";
                int add = start + len;
                for (int i = 0; i < len; i++)
                {
                    ret += BitConverter.ToString(new byte[] { (byte)(start & 0xFF) }).Replace("-", "");
                    int next = start + add;
                    add = start;
                    start = next;
                }
                return ret;
            }

            private static byte[] GenerateKey(string titleId, string password)
            {
                byte[] secret = MD5.Create().ComputeHash(Encoding.UTF8.GetBytes(Secret(-2, 9) + titleId.Substring(2)));
                byte[] key = new Rfc2898DeriveBytes(password, secret, 20).GetBytes(16); // new PBKDF2(password, hashedSecret, 20).GetBytes(16);
                return key;
            }

            private static byte[] EncryptTitleKey(ulong titleId, byte[] titleKey, byte[] commonKey)
            {
                byte[] iv = BitConverter.GetBytes(Shared.Swap(titleId));
                Array.Resize(ref iv, 16);

                RijndaelManaged rm = new RijndaelManaged();
                rm.Mode = CipherMode.CBC;
                rm.Padding = PaddingMode.None;
                rm.KeySize = 128;
                rm.BlockSize = 128;
                rm.Key = commonKey;
                rm.IV = iv;

                ICryptoTransform encryptor = rm.CreateEncryptor();

                MemoryStream ms = new MemoryStream(titleKey);
                CryptoStream cs = new CryptoStream(ms, encryptor, CryptoStreamMode.Read);

                var encTitleKey = new byte[titleKey.Length];
                cs.Read(encTitleKey, 0, titleKey.Length);
                return encTitleKey;
            }

            public static WAD GetWAD(string tid, string password, CommonKeyType cKeyType)
            {
                WAD w = null;
                TMD tmd = new TMD();
                Ticket tik = Ticket.Load(Byte.FromHex("0001000163C7CA3E6D51EF91B637251DF8A1887B12F222400D5DE7F4740343A0069EE091B0442B7E69C52259624D6471ABB421AC1884DF4D9BC516CC6940AA5BED4A653D196880D6E93F16BF3AA818DAF0EAD11D845B072C8AF5130858D08CDD26CDE82813170C18B09EF309CAD298C94B5E17F50E7E9AC9A858D7578E931806CB187EEA9C847D4B417B431C2A4A8D03EB218410D0E9622A3D46CFCD05C56F7113AA392A009034801B4F240F487F4FBF22F9A0CBDAFEF04E3293E4FE616C6BCA7659AE681F57BF7F32ACC5EDC123BA58BD67425EDC1B60B5766A4175A63CCED67C1DB6B1449532F075440C6E6B0DA338C797917AAAD8801BFC3339F09E4DCC2C0F5D8E3D000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000526F6F742D434130303030303030312D58533030303030303033000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000041414141414141414141414141414141000000000000000000000000005959595959595959FFFF393900000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000001FFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFF0000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000100017D9D5EBA5281DCA7065D2F0868DB8AC73ACE7EA991F1969FE1D0F2C11FAEC0C3F01ADCB446ADE5CA03B625219462C6E1410DB9E63FDE98D1AF263B4CB28784278272EF27134B87C258D67B62F2B5BF9CB6BA8C89192EC50689AC7424A022094003EE98A4BD2F013B593FE5666CD5EB5AD7A49310F34EFBB43D46CBF1B523CF82F68EB56DB904A7C2A82BE11D78D39BA20D90D30742DB5E7AC1EFF221510962CFA914A880DCF417BA99930AEE08B0B0E51A3E9FAFCDC2D7E3CBA12F3AC00790DE447AC3C538A8679238078BD4C4B245AC2916886D2A0E594EED5CC835698B4D6238DF05724DCCF681808A7074065930BFF8514137E815FABAA172B8E0696C61E4000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000526F6F742D43413030303030303031000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000158533030303030303033000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000F1B89FD1AD07A9378A7B100C7DC739BE9EDDB7320089AB25B1F871AF5AA9F4589ED18302328E811A1FEFD009C8063643F854B9E13BBB613A7ACF8714856BA45BAAE7BBC64EB2F75D87EBF267ED0FA441A933665E577D5ADEABFB462E7600CA9CE94DC4CB983992AB7A2FB3A39EA2BF9C53ECD0DCFA6B8B5EB2CBA40FFA4075F8F2B2DE973811872DF5E2A6C38B2FDC8E57DDBD5F46EB27D61952F6AEF862B7EE9AC682A2B19AA9B558FBEBB3892FBD50C9F5DC4A6E9C9BFE458034A942182DDEB75FE0D1B3DF0E97E39980877018C2B283F135757C5A30FC3F3084A49AAAC01EE706694F8E1448DA123ACC4FFA26AA38F7EFBF278F369779775DB7C5ADC78991DCF8438D000100010000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000010000B3ADB3226B3C3DFF1B4B407716FF4F7AD76486C895AC562D21F10601D4F66428191C07768FDF1AE2CE7B27C90FBC0AD0312578EC0779B657D4372413A7F86F0C14C0EF6E0941ED2B05EC3957360789004A878D2E9DF8C7A5A9F8CAB311B1187957BBF898E2A25402CF5439CF2BBFA0E1F85C066E839AE094CA47E01558F56E6F34E92AA2DC38937E37CD8C5C4DFD2F114FE868C9A8D9FED86E0C2175A2BD7E89B9C7B513F41A7961443910EFF9D7FE572218D56DFB7F497AA4CB90D4F1AEB176E4685DA7944060982F0448401FCFC6BAEBDA1630B473B415233508070A9F4F8978E62CEC5E9246A5A8BDA0857868750C3A112FAF95E838C8990E87B162CD10DAB3319665EF889B541BB336BB67539FAFC2AE2D0A2E75C02374EA4EAC8D99507F59B95377305F2635C608A99093AC8FC6DE23B97AEA70B4C4CF66B30E58320EC5B6720448CE3BB11C531FCB70287CB5C27C674FBBFD8C7FC94220A473231D587E5A1A1A82E37579A1BB826ECE0171C97563474B1D46E679B282376211CDC7002F4687C23C6DC0D5B5786EE1F273FF0192500FF4C7506AEE72B6F43DF608FEA583A1F9860F87AF524454BB47C3060C94E99BF7D632A7C8AB4B4FF535211FC18047BB7AFA5A2BD7B884AD8E564F5B89FF379737F1F5013B1F9EC4186F922AD5C4B3C0D5870B9C04AF1AB5F3BC6D0AF17D4708E443E973F7B7707754BAF3ECD2AC49000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000526F6F7400000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000001434130303030303030310000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000005BFA7D5CB279C9E2EEE121C6EAF44FF639F88F078B4B77ED9F9560B0358281B50E55AB721115A177703C7A30FE3AE9EF1C60BC1D974676B23A68CC04B198525BC968F11DE2DB50E4D9E7F071E562DAE2092233E9D363F61DD7C19FF3A4A91E8F6553D471DD7B84B9F1B8CE7335F0F5540563A1EAB83963E09BE901011F99546361287020E9CC0DAB487F140D6626A1836D27111F2068DE4772149151CF69C61BA60EF9D949A0F71F5499F2D39AD28C7005348293C431FFBD33F6BCA60DC7195EA2BCC56D200BAF6D06D09C41DB8DE9C720154CA4832B69C08C69CD3B073A0063602F462D338061A5EA6C915CD5623579C3EB64CE44EF586D14BAAA8834019B3EEBEED3790001000100000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000"));

                using (NusClient nus = new NusClient())
                {
                    nus.UseLocalFiles = true;
                    nus.ContinueWithoutTicket = true;
                    tmd = nus.DownloadTMD(tid, "");
                    nus.DownloadTitle(tid, "", Paths.WAD, new StoreType[] { StoreType.EncryptedContent });
                }

                tik.FakeSign = tmd.FakeSign;
                tik.TitleID = tmd.TitleID;
                tik.CommonKeyIndex = cKeyType;
                tik.NumOfDLC = 0;
                tik.ConsoleID = 0;
                tik.TicketID = 471578341952616; // TO-DO: Generate algorithm for this instead of hardcoding it
                var cKey = tik.CommonKeyIndex == CommonKeyType.vWii ? CommonKey.GetvWiiKey() : tik.CommonKeyIndex == CommonKeyType.Korean ? CommonKey.GetKoreanKey() : CommonKey.GetStandardKey();

                // Title key
                // ****************
                var tKey = EncryptTitleKey(tik.TitleID, GenerateKey(tid, password), cKey);
                tik.SetTitleKey(tKey);

                // Others
                // ****************
                /*if (vc)
                {
                    var temp = tik.ToByteArray();
                    var buffer = new byte[48];
                    buffer[47] = 1;
                    buffer.CopyTo(temp, 0x1F2);
                    BitConverter.GetBytes(tmd.TitleVersion).Take(2).ToArray().CopyTo(temp, 0x1E6);
                    tik = Ticket.Load(temp);
                }*/

                for (int i = 0; i < tmd.Contents.Length; i++)
                {
                    var item = Paths.WAD + i.ToString("D8");

                    if (File.Exists(item))
                    {
                        // COPIED FROM NUSCLIENT.CS (LIBWIISHARP)
                        // ****************
                        
                        var content = File.ReadAllBytes(item);
                        Array.Resize(ref content, Shared.AddPadding(content.Length, 16));
                        byte[] numArray = new byte[16];
                        byte[] bytes = BitConverter.GetBytes(tmd.Contents[i].Index);
                        numArray[0] = bytes[1];
                        numArray[1] = bytes[0];

                        using (RijndaelManaged rijndaelManaged = new RijndaelManaged
                        {
                            Mode = CipherMode.CBC,
                            Padding = PaddingMode.None,
                            KeySize = 128,
                            BlockSize = 128,
                            Key = tKey,
                            IV = numArray
                        })
                        using (ICryptoTransform decryptor = rijndaelManaged.CreateDecryptor())
                        using (MemoryStream memoryStream = new MemoryStream(content))
                        using (CryptoStream cryptoStream = new CryptoStream(memoryStream, decryptor, CryptoStreamMode.Read))
                        {
                            byte[] buffer = new byte[content.Length];
                            cryptoStream.Read(buffer, 0, buffer.Length);
                            File.WriteAllBytes(item + ".app", buffer);
                        }
                    }
                }

                // Save ticket file
                // ****************
                string path = Paths.WAD + tid;
                if (!Directory.Exists(Path.GetDirectoryName(path))) Directory.CreateDirectory(Path.GetDirectoryName(path));
                tik.Save(path + ".tik");
                tmd.Save(path + ".tmd");
                tik.Dispose();
                tmd.Dispose();

                w = WAD.Create(Paths.WAD);

                Directory.Delete(Paths.WAD, true);
                return w;
            }
        }

        public class DatabaseEntry
        {
            public string ID { get; set; }
            public List<int> Regions = new List<int>();
            public List<string> Titles = new List<string>();
            public List<int> EmuRevs = new List<int>();

            public string GetID(int index, bool raw = false)
            {
                string r = "41";

                switch (Regions[index])
                {
                    default:
                    case 0:
                        r = "4a";
                        break;

                    case 1:
                        r = "45";
                        break;

                    case 2:
                        r = "4e";
                        break;

                    case 3:
                        r = "50";
                        break;

                    case 4:
                        r = "4c";
                        break;

                    case 5:
                        r = "4d";
                        break;

                    case 6:
                        r = "51";
                        break;

                    case 7:
                        r = "54";
                        break;
                }

                return raw ? ID.Replace("__", r).Replace("-", "").ToLower() : ID.Replace("__", r).ToLower();
            }

            public string GetUpperID(int index)
            {
                var hex = GetID(index).Substring(ID.Length - 8);
                var ascii = string.Empty;


                for (int i = 0; i < hex.Length; i += 2)
                {
                    var hs = hex.Substring(i, 2);
                    char character = Convert.ToChar(Convert.ToInt64(hs, 16));
                    ascii += character;
                }

                return ascii.ToUpper();
            }

            /// <summary>
            /// Gets a WAD file from the entry corresponding to the index entered and loads it to memory.
            /// </summary>
            public WAD GetWAD(int index)
            {
                var cKey = Regions[index] >= 6 ? CommonKeyType.Korean : CommonKeyType.Standard;

                WAD w = NUSD.GetWAD(GetID(index, true), "mypass", cKey);
                if (w == null) w = new WAD();

                return w;
            }
        }

        public List<DatabaseEntry> Entries { get; private set; }

        /// <summary>
        /// Loads a database of WADs for a selected console/platform.
        /// </summary>
        public Database(Console c)
        {
            try
            {
                using (MemoryStream ms = new MemoryStream(Properties.Resources.Database))
                using (StreamReader sr = new StreamReader(ms, Encoding.Unicode))
                using (var doc = JsonDocument.Parse(sr.ReadToEnd(), new JsonDocumentOptions() { AllowTrailingCommas = true, CommentHandling = JsonCommentHandling.Skip }))
                {
                    var x = doc.Deserialize<JsonElement>(new JsonSerializerOptions() { AllowTrailingCommas = true, ReadCommentHandling = JsonCommentHandling.Skip }).GetProperty(c.ToString().ToLower());
                    if (x.ValueKind != JsonValueKind.Array) throw new Exception("The database format or styling is not valid.");

                    sr.Dispose();
                    ms.Dispose();

                    Entries = new List<DatabaseEntry>();
                    foreach (var item in x.EnumerateArray())
                    {
                        var y = new DatabaseEntry() { ID = item.GetProperty("id").GetString() };
                        var reg = item.GetProperty("region");

                        for (int i = 0; i < reg.GetArrayLength(); i++)
                        {
                            y.Regions.Add(reg[i].GetInt32());
                            try { y.Titles.Add(item.GetProperty("titles")[i].GetString()); } catch { y.Titles.Add(item.GetProperty("titles")[Math.Max(0, item.GetProperty("titles").GetArrayLength() - 1)].GetString()); }
                            try { y.EmuRevs.Add(item.GetProperty("emu_ver")[i].GetInt32()); } catch { y.EmuRevs.Add(0); }
                        }

                        Entries.Add(y);
                    }
                }
            }

            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show($"A fatal error occurred retrieving the {c} WADs database.\n\nException: {ex.GetType().FullName}\nMessage: {ex.Message}\n\nThe application will now shut down.", "Halt", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Hand);
                Environment.FailFast("Database initialization failed.");
            }
        }
    }
}

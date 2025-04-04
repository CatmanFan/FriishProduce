﻿using Octokit;
using SharpCompress.Archives;
using SharpCompress.Common;
using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace FriishProduce
{
    public static class Updater
    {
        public static string VerName = FileVersionInfo.GetVersionInfo(Assembly.GetExecutingAssembly().Location).ProductVersion;
        private static Version current;
        public static Version Current
        {
            get
            {
                if (current == null)
                {
                    string verName = VerName.IndexOf('-') > 0 ? VerName.Substring(0, VerName.IndexOf('-')) : VerName;
                    current = new Version(verName);
                }

                return current;
            }
        }

        public static GitHubClient Client;

        private static bool _IsLatest = false;
        public static bool IsLatest { get => _IsLatest; private set { _IsLatest = value; } }

        private static readonly string temp = Paths.EnvironmentFolder + "update\\";

#nullable enable
        private static bool? NeedsUpdate(string? file, DateTime? date)
#nullable disable
        {
            // Auto-return true on debug mode
            // ****************
            if (Program.Config.application.force_update) return true;

            // Check if any values are null
            // ****************
            if (file == null || !date.HasValue || !File.Exists(Paths.EnvironmentFolder + file))
                return null;

            // Compare dates
            // ****************
            (DateTime Old, DateTime New) = (File.GetLastWriteTimeUtc(Paths.EnvironmentFolder + file), date.Value.ToUniversalTime());

            return Old < New;
        }

        public static void ClearFiles(bool keepFolder = false, bool keepArchive = false)
        {
            if (!keepFolder)
                try { Directory.Delete(temp, true); } catch { }

            if (!keepArchive)
                try { File.Delete(Paths.Update); } catch { }
        }

        /// <summary>
        /// Overwrites original files with updated ones. This is temporarily set to extract the updated files to a subdirectory for now.
        /// </summary>
        public static void Extract()
        {
            Exception ex = null;

            // Create temp directory
            // ****************
            ClearFiles(false, true);
            Directory.CreateDirectory(temp);

            try
            {
                try
                {
                    // Extract ZIP
                    // ****************
                    using (var r = SharpCompress.Archives.Zip.ZipArchive.Open(Paths.Update))
                    {
                        foreach (var entry in r.Entries.Where(entry => !entry.IsDirectory))
                        {
                            // Check if needs update, then write
                            // ****************
                            if (NeedsUpdate(entry.Key, entry.LastModifiedTime) == true)
                            {
                                entry.WriteToDirectory(temp, new ExtractionOptions()
                                {
                                    ExtractFullPath = true,
                                    Overwrite = true
                                });
                            }
                        }
                    }
                }

                catch
                {
                    // Extract RAR
                    // ****************
                    using (var r = SharpCompress.Archives.Rar.RarArchive.Open(Paths.Update))
                    {
                        foreach (var entry in r.Entries.Where(entry => !entry.IsDirectory))
                        {
                            // Check if needs update, then write
                            // ****************
                            if (NeedsUpdate(entry.Key, entry.LastModifiedTime) == true)
                            {
                                entry.WriteToDirectory(temp, new ExtractionOptions()
                                {
                                    ExtractFullPath = true,
                                    Overwrite = true
                                });
                            }
                        }
                    }
                }

                // Check if there are any updated files, then notify
                // ****************
                if (Directory.EnumerateFiles(temp, "*.*", SearchOption.AllDirectories).Count() > 0)
                {
                    File.WriteAllText(Path.Combine(temp, "_Copy and overwrite original files.txt"), "");
                    System.Diagnostics.Process.Start("explorer.exe", $"\"{temp}\"");
                }
                else
                    ClearFiles(false, true);
            }

            catch (Exception Ex)
            {
                ex = Ex;
            }

            finally
            {
                ClearFiles(ex == null);

                if (ex != null)
                    throw ex;
                else
                    Environment.Exit(0);
            }
        }

        public static async Task<bool> GetLatest()
        {
            try
            {
                if (Client == null) Client = new(new ProductHeaderValue("FriishProduce"));

                Release Latest = await Client.Repository.Release.GetLatest("CatmanFan", "FriishProduce");
                Version LatestVersion = Latest.TagName.ToLower() == "latest" && !Latest.Prerelease && !Latest.Draft ? new Version("9.99") : new Version(Latest.TagName.Substring(1).Replace("-beta", ""));

                IsLatest = Current.CompareTo(LatestVersion) >= 0 && !Latest.Prerelease && !Program.Config.application.force_update;

                if (!IsLatest)
                    new UpdaterForm(Latest, LatestVersion).ShowDialog();
            }
            catch { }

            return IsLatest;
        }
    }
}

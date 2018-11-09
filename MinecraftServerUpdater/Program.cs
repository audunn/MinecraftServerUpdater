using McMaster.Extensions.CommandLineUtils;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Cryptography;

namespace MinecraftServerUpdater
{
    public class Program
    {
        [Option("-s|--snapshot", Description = "Get latest shapshot version instead of relase verson")]
        public bool shapshot { get; } = false;

        [Option("-d|--directory", Description = "Set working directory where Minecraft server is installed, default is current directory")]
        public string WorkingDirectory { get; } 
        static int Main(string[] args)        
            => CommandLineApplication.Execute<Program>(args);


        private void OnExecute()
        {
            Console.WriteLine("Minecraft Server Updater");
            WebClient wc = new WebClient();
            string versionsLink = "https://launchermeta.mojang.com/mc/game/version_manifest.json";            
            string fileName = "server.jar"; 
            string versionFileName = "minecraftversion.txt";
            string downloadPath = Directory.GetCurrentDirectory();
            if (!string.IsNullOrWhiteSpace(WorkingDirectory))
            {
                downloadPath = WorkingDirectory;
            }
            string verisonToGet = "release";
            if (shapshot)
            {
                verisonToGet = "snapshot";
            }
            try
            {
                Console.WriteLine("Check latest versions.json for latest {verisonToGet} version");
                var jsonFile = wc.DownloadString(versionsLink);
                try
                {
                    var versions = JObject.Parse(jsonFile);
                    JObject relaseVersion = versions["versions"].Values<JObject>()
                        .Where(m => m["type"].Value<string>() == verisonToGet)
                        .FirstOrDefault();
                    if (relaseVersion == null)
                    {
                        Console.WriteLine($"No relase found in json:");
                        return;
                    }
                    Console.WriteLine($"Found version in json: {relaseVersion["id"]}, {relaseVersion["type"]} {relaseVersion["url"]}");
                    var latestVersion = relaseVersion["id"].ToString();
                    var detailJsonUrl = relaseVersion["url"].ToString();
                    var downloadLastVersion = false;
                    Console.WriteLine($"Parsed Json, latest {verisonToGet} version: {latestVersion}.");

                    Console.WriteLine("Getting version details: ");
                    var versionDetailsJson = wc.DownloadString(detailJsonUrl); //downloads the versions details json file

                    var versionsDetails = JObject.Parse(versionDetailsJson);
                    string serverUrl = (string)versionsDetails["downloads"]["server"]["url"];
                    string serverSha1 = (string)versionsDetails["downloads"]["server"]["sha1"];
                    string serverSize = (string)versionsDetails["downloads"]["server"]["size"];
                    string versionId = (string)versionsDetails["downloads"]["id"];
                    string serverFileFullPath = Path.Combine(downloadPath, fileName);
                    versionFileName = Path.Combine(downloadPath, versionFileName);
                    if (!File.Exists(serverFileFullPath)) //if server jar doesn't exist
                    {
                        downloadLastVersion = true;
                        File.WriteAllText(versionFileName, ""); 
                    }
                    string localSha1 = GetShaCheckSum(serverFileFullPath);
                    if (!downloadLastVersion && !serverSha1.Equals(localSha1))
                    {
                        Console.WriteLine($"Server.jar Checksum does not match");
                        Console.WriteLine($"local  server.jar - {localSha1}");
                        Console.WriteLine($"online server.jar - {serverSha1}");
                        Console.WriteLine($"Starting update to version {latestVersion}.");
                        try
                        {
                            Console.WriteLine($"Starting update to {verisonToGet} version {latestVersion}");
                            File.WriteAllText(versionFileName, latestVersion);           
                            wc.DownloadFile(serverUrl, serverFileFullPath);
                            Console.WriteLine($"Updated to {verisonToGet} version {latestVersion}");
                        }
                        catch(Exception ex)
                        {
                            Console.WriteLine($"Error when downloading or saving file. Check file link and directory permissions. {ex.Message}");
                        }
                    }
                    else
                    {
                        Console.WriteLine($"Minecraft server is already up to date at {verisonToGet} version {latestVersion}.");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error while parsing json.  {ex.Message}");
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine($"Error while downloading versions.json file");
                Console.WriteLine("{ex.Message}");
            }
        }

        private static string GetShaCheckSum(string filename)
        {
            string checkSum = string.Empty;
            using (FileStream stream = File.OpenRead(filename))
            {
                using (SHA1Managed sha = new SHA1Managed())
                {
                    byte[] checksum = sha.ComputeHash(stream);
                    checkSum = BitConverter.ToString(checksum)
                        .Replace("-", string.Empty).ToLower();
                }
            }
            return checkSum;
        }
    }
}

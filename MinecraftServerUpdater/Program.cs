// See https://aka.ms/new-console-template for more information
using MinecraftServerUpdater;
using System.CommandLine;
using System.CommandLine.Invocation;
using System.Linq;
using System.Security.Cryptography;
using System.Text.Json;
using System.Text.Json.Serialization;

// Create a root command with some options
var rootCommand = new RootCommand
{
    new Option<bool>(
        "--snapshot",
        getDefaultValue: () => false,
        description: "Get latest shapshot version instead of relase verson"),
    new Option<string>(
        "--directory",
        "Set working directory where Minecraft server is installed, default is current directory")
};

rootCommand.Description = "Minecraft Server Updater";
// Note that the parameters of the handler method are matched according to the names of the options
rootCommand.Handler = CommandHandler.Create<bool, string>(async (snapshot, directory) =>
{
    Console.WriteLine($"The value for --snapshot is: {snapshot}");
    Console.WriteLine($"The value for --directory is: {directory}");
    await OnExecute(snapshot, directory);
});
return rootCommand.InvokeAsync(args).Result;


async Task OnExecute(bool snapshot, string directory)
{    
    HttpClient httpClient = new();
    string versionsLink = "https://launchermeta.mojang.com/mc/game/version_manifest.json";
    string fileName = "server.jar";
    string versionFileName = "minecraftversion.txt";
    string downloadPath = Directory.GetCurrentDirectory();
    if (!string.IsNullOrWhiteSpace(directory))
    {
        downloadPath = directory;
    }
    string verisonToGet = "release";
    if (snapshot)
    {
        verisonToGet = "snapshot";
    }
    try
    {
        Console.WriteLine("Check latest versions.json for latest {verisonToGet} version");
        //var jsonFile = wc.DownloadString(versionsLink);
        var response = await httpClient.GetStringAsync(versionsLink);
        var options = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        };
        try
        {
            //var versions = JObject.Parse(jsonFile);
            var versions = JsonSerializer.Deserialize<MinecraftVersions>(response, options);
            
            if (versions?.Latest == null)
            {
                Console.WriteLine($"No relase found in json:");
                return;
            }
            Console.WriteLine($"Latest online versions release: {versions.Latest.Release} snapshot: {versions.Latest.Snapshot}");

            var getVersion = snapshot ? versions.Latest.Release : versions.Latest.Snapshot;
            var latestVersionDetails = versions?.Versions?.FirstOrDefault(v => v.Id == getVersion);
            if (latestVersionDetails == null)
            {
                Console.WriteLine($"No relase found in json:");
                return;
            }            
            Console.WriteLine($"Parsed Json, latest {verisonToGet} version: {getVersion}.");

            Console.WriteLine($"Getting version details from {latestVersionDetails.Url}: ");
            var versionDetailsJson = await httpClient.GetStringAsync(latestVersionDetails.Url);  //downloads the versions details json file
            if(string.IsNullOrWhiteSpace(versionDetailsJson))
            {
                Console.WriteLine($"No version details found in {latestVersionDetails.Url}:");
                return;
            }
            var versionsDetails = JsonSerializer.Deserialize<VersionDetails>(versionDetailsJson, options);
            if (versionsDetails == null)
            {
                Console.WriteLine($"No version details found in json:");
                return;
            }
            string serverUrl = versionsDetails.Downloads.Server.Url;
            string serverSha1 = versionsDetails.Downloads.Server.Sha1;
            int serverSize = versionsDetails.Downloads.Server.Size;
            string versionId = versionsDetails.Id;
            string serverFileFullPath = Path.Combine(downloadPath, fileName);
            string tempServerFileFullPath = serverFileFullPath + "temp";
            versionFileName = Path.Combine(downloadPath, versionFileName);
            string localSha1 = String.Empty;
            if (!System.IO.File.Exists(serverFileFullPath)) //if server jar doesn't exist
            {
                System.IO.File.WriteAllText(versionFileName, "");
            }
            else
            {
                localSha1 = GetShaCheckSum(serverFileFullPath);
            }
            if (string.IsNullOrWhiteSpace(localSha1) || !serverSha1.Equals(localSha1))
            {
                Console.WriteLine($"Server.jar Checksum does not match, online version has changed");
                Console.WriteLine($"local  server.jar - {localSha1}");
                Console.WriteLine($"online server.jar - {serverSha1}");
                Console.WriteLine($"Starting update to version {versionId}.");
                try
                {
                    Console.WriteLine($"Starting update to {verisonToGet} version {versionId}");                    
                    //wc.DownloadFile(serverUrl, serverFileFullPath);
                    byte[] fileBytes = await httpClient.GetByteArrayAsync(serverUrl);
                    System.IO.File.WriteAllBytes(tempServerFileFullPath, fileBytes);
                    string newLocalSha1 = GetShaCheckSum(tempServerFileFullPath);
                    if (!serverSha1.Equals(newLocalSha1))
                    {
                        Console.WriteLine($"Downloaded Server.jar Checksum does not match, manifest {serverSha1} SHA {newLocalSha1}");
                        Console.WriteLine($"reverting");
                        System.IO.File.Delete(tempServerFileFullPath);
                    }
                    else
                    {
                        System.IO.File.Move(tempServerFileFullPath,serverFileFullPath, true);
                        System.IO.File.WriteAllText(versionFileName, versionId);
                        Console.WriteLine($"Updated to {verisonToGet} version {versionId}");
                    }                   
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error when downloading or saving file. Check file link and directory permissions. {ex.Message}");
                }
            }
            else
            {
                Console.WriteLine($"Minecraft server is already up to date at {verisonToGet} version {getVersion}.");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error while parsing json.  {ex.Message}");
        }
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Error while downloading versions.json file");
        Console.WriteLine($"{ex.Message}");
    }
}


static string GetShaCheckSum(string filename)
{
    string checkSum = string.Empty;
    using (FileStream stream = System.IO.File.OpenRead(filename))
    {
        using (HashAlgorithm sha = SHA1.Create())
        {
            byte[] checksum = sha.ComputeHash(stream);
            checkSum = BitConverter.ToString(checksum)
                .Replace("-", string.Empty).ToLower();
        }
    }
    return checkSum;
}
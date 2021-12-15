using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinecraftServerUpdater
{

    public class VersionDetails
    {
        public Arguments Arguments { get; set; }
        public Assetindex AassetIndex { get; set; }
        public string Assets { get; set; }
        public int ComplianceLevel { get; set; }
        public Downloads Downloads { get; set; }
        public string Id { get; set; }
        public Javaversion JavaVersion { get; set; }
        public Library[] Libraries { get; set; }
        public Logging Logging { get; set; }
        public string MainClass { get; set; }
        public int MinimumLauncherVersion { get; set; }
        public DateTime ReleaseTime { get; set; }
        public DateTime Time { get; set; }
        public string Type { get; set; }
    }

    public class Arguments
    {
        public object[] Game { get; set; }
        public object[] Jvm { get; set; }
    }

    public class Assetindex
    {
        public string Id { get; set; }
        public string Sha1 { get; set; }
        public int Size { get; set; }
        public int TotalSize { get; set; }
        public string Url { get; set; }
    }

    public class Downloads
    {
        public Client Client { get; set; }
        public Client_Mappings Client_Mappings { get; set; }
        public Server Server { get; set; }
        public Server_Mappings Server_Mappings { get; set; }
    }

    public class Client
    {
        public string Sha1 { get; set; }
        public int Size { get; set; }
        public string Url { get; set; }
    }

    public class Client_Mappings
    {
        public string Sha1 { get; set; }
        public int Size { get; set; }
        public string Url { get; set; }
    }

    public class Server
    {
        public string Sha1 { get; set; }
        public int Size { get; set; }
        public string Url { get; set; }
    }

    public class Server_Mappings
    {
        public string Sha1 { get; set; }
        public int Size { get; set; }
        public string Url { get; set; }
    }

    public class Javaversion
    {
        public string Component { get; set; }
        public int MajorVersion { get; set; }
    }

    public class Logging
    {
        public Client1 Client { get; set; }
    }

    public class Client1
    {
        public string Argument { get; set; }
        public File File { get; set; }
        public string Type { get; set; }
    }

    public class File
    {
        public string Id { get; set; }
        public string Sha1 { get; set; }
        public int Size { get; set; }
        public string Url { get; set; }
    }

    public class Library
    {
        public Downloads1 Downloads { get; set; }
        public string Name { get; set; }
        public Rule[] Rules { get; set; }
        public Natives Natives { get; set; }
        public Extract Extract { get; set; }
    }

    public class Downloads1
    {
        public Artifact Artifact { get; set; }
        public Classifiers Classifiers { get; set; }
    }

    public class Artifact
    {
        public string Path { get; set; }
        public string Sha1 { get; set; }
        public int Size { get; set; }
        public string Url { get; set; }
    }

    public class Classifiers
    {
        public Javadoc Javadoc { get; set; }
        public NativesLinux Nativeslinux { get; set; }
        public NativesMacos Nativesmacos { get; set; }
        public NativesWindows Nativeswindows { get; set; }
        public Sources Sources { get; set; }
        public NativesOsx Nativesosx { get; set; }
    }

    public class Javadoc
    {
        public string Path { get; set; }
        public string Sha1 { get; set; }
        public int Size { get; set; }
        public string Url { get; set; }
    }

    public class NativesLinux
    {
        public string Path { get; set; }
        public string Sha1 { get; set; }
        public int Size { get; set; }
        public string Url { get; set; }
    }

    public class NativesMacos
    {
        public string Path { get; set; }
        public string Sha1 { get; set; }
        public int Size { get; set; }
        public string Url { get; set; }
    }

    public class NativesWindows
    {
        public string Path { get; set; }
        public string Sha1 { get; set; }
        public int Size { get; set; }
        public string Url { get; set; }
    }

    public class Sources
    {
        public string Path { get; set; }
        public string Sha1 { get; set; }
        public int Size { get; set; }
        public string Url { get; set; }
    }

    public class NativesOsx
    {
        public string Path { get; set; }
        public string Sha1 { get; set; }
        public int Size { get; set; }
        public string Url { get; set; }
    }

    public class Natives
    {
        public string Osx { get; set; }
        public string Linux { get; set; }
        public string Windows { get; set; }
    }

    public class Extract
    {
        public string[] Exclude { get; set; }
    }

    public class Rule
    {
        public string Action { get; set; }
        public Os Os { get; set; }
    }

    public class Os
    {
        public string Name { get; set; }
    }

}

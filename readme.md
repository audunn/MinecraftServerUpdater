# Minecraft Server Updater

## About
Simple Minecraft Server updater. Checks for minerafts server version here https://launchermeta.mojang.com/mc/game/version_manifest.json and download if needed.
### Why
Me and my son run a [Minecraft](https://minecraft.net) server at home for fun and he sometimes gets into trouble running it when the client has updated but the server has not. He is still young so he does not know yet how to update it himself.
So we made this to be used in Minecraft servers startup script to update the server automaticaly. As a learning experience we made it using .net core 2.1

### Prerequisites

You will need to have .NET Core

### Usage
To run the application, copy to the Minecraft server directory and type the following command:
```
dotnet minecraftserverupdater.dll
```
This will update to lastest relaes version by default but to update to latest shapshot version type the following command:
```
dotnet minecraftserverupdater.dll -s
```

## Built With

* [.netcore](https://dotnet.github.io/) - The general-purpose development platform used
* [json.net](https://www.newtonsoft.com/json) - JSON framework for .NET
* [CommandLineUtils](https://github.com/natemcmaster/CommandLineUtils) - Command line parsing and utilities for .NET Core and .NET Framework

## Authors

* **Auðunn Baldvinsson** - *Initial work* - [audunn](https://github.com/audunn)

See also the list of [contributors](https://github.com/audunn/MinecraftServerUpdater/graphs/contributors) who participated in this project.

## License

This project is licensed under the MIT License - see the [LICENSE.txt](LICENSE.txt) file for details

## Acknowledgments

* Inspiration by https://github.com/aveao/McSvSnapshotUpdater



# Minecraft Server Updater

## Getting Started
Simple Minecraft Server updater. Checks for minerafts server version here https://launchermeta.mojang.com/mc/game/version_manifest.json and download if needed.
Me and my son run a minecraft server at home for fun and he frequently gets into trouble with it when the Client has updated but the server has not, he does not know yet how to update it himself.
So we made this to be used in startup script to update the server automaticaly. As a learning experience we made it in .net core 2.1

## Getting Started

These instructions will get you a copy of the project up and running on your local machine for development and testing purposes. See deployment for notes on how to deploy the project on a live system.

### Prerequisites

You will need to have .NET Core

### Usage
To build and run the application, copy to the minecraft server directory and type the following command:
```
dotnet minecraftserverupdater.dll
```
To update to latest shapshot version type the following command:
```
dotnet minecraftserverupdater.dll -s
```

## Built With

* [.netcore](https://dotnet.github.io/) - The web framework used
* [json.net](https://www.newtonsoft.com/json) - JSON framework for .NET

## Authors

* **Auðunn Baldvinsson** - *Initial work* - [audunn](https://github.com/audunn)

See also the list of [contributors](https://github.com/your/project/contributors) who participated in this project.

## License

This project is licensed under the MIT License - see the [LICENSE.md](LICENSE.md) file for details

## Acknowledgments

* Inspiration by https://github.com/aveao/McSvSnapshotUpdater



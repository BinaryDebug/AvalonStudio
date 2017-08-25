[![CodeFactor](https://www.codefactor.io/repository/github/vitalelement/avalonstudio/badge)](https://www.codefactor.io/repository/github/vitalelement/avalonstudio)
[![Build status](https://ci.appveyor.com/api/projects/status/l2k85kekoby4tb4j/branch/master?svg=true)](https://ci.appveyor.com/project/danwalmsley/avalonstudio/branch/master)
[![Build Status](https://travis-ci.org/VitalElement/AvalonStudio.svg?branch=master)](https://travis-ci.org/VitalElement/AvalonStudio)
[![Gitter](https://badges.gitter.im/VitalElement/AvalonStudio.svg)](https://gitter.im/VitalElement/AvalonStudio?utm_source=badge&utm_medium=badge&utm_campaign=pr-badge)

<img src="AvalonStudio/AvalonStudio/Assets/icon.png" width="50" height="50" />
<img src="https://cloud.githubusercontent.com/assets/4672627/21014496/36454f48-bd56-11e6-8e00-6cf8f878e9a6.png" />
<img src="https://files.gitter.im/AvaloniaUI/Avalonia/LoHY/Screenshot-from-2017-01-27-00-51-26.png" />

# AvalonStudio

An extensible, cross platform IDE written in C#

## Try the bleeding-edge builds?

| Version | Installers |
|---------|------------|
|**Windows 7 8.1 10 x64**|[Download](https://ci.appveyor.com/api/projects/danwalmsley/AvalonStudio/artifacts/artifacts/zip/AvalonStudio-win7-x64.zip?branch=master)|
|**MacOSX 10.12 x64**|[Download](https://ci.appveyor.com/api/projects/danwalmsley/AvalonStudio/artifacts/artifacts/zip/AvalonStudio-osx.10.12-x64.zip?branch=master)|
|**Ubuntu 14.04+ x64**|[Download](https://ci.appveyor.com/api/projects/danwalmsley/AvalonStudio/artifacts/artifacts/zip/AvalonStudio-ubuntu.14.04-x64.zip?branch=master)|
|**Debian 8 x64**|[Download](https://ci.appveyor.com/api/projects/danwalmsley/AvalonStudio/artifacts/artifacts/zip/AvalonStudio-debian.8-x64.zip?branch=master)|

## Building from source

### Getting the code

Clone the repo.
```
git clone https://github.com/VitalElement/AvalonStudio --recursive

cd AvalonStudio
```

### Install Dependencies
Install Dotnet Core 2 Preview from: https://www.microsoft.com/net/core/preview

### Building the project
These instructions are roughly the same for all platforms:

cd into the repo directory `/AvalonStudio/AvalonStudio`

`dotnet restore`
`dotnet build`

### Running locally built binaries

cd to /AvalonStudio/AvalonStudio/bin/Debug/netcoreapp2.0/
`dotnet ./AvalonStudio.dll`


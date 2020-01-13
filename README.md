# Gear Language · ![GitHub license](https://img.shields.io/badge/license-MIT-brightgreen.svg) [![Build Status](https://travis-ci.com/kimon0202/GearLanguage.svg?branch=master)](https://travis-ci.com/kimon0202/GearLanguage)

GearLang is a interpreted programming language that offers scripting features to process plain text and serialized files, or manage system tasks. It is simple and straightforward.

Windows Version: Available
MacOS Version: Not Available Yet;
Linux Version: Not Available Yet;

## Features of Gear

* Simple Syntax
* Declarative functional paradigm

## Environmet Setup

GearLang depends on .NET Core 3.1.100 and .NET SDK 3.1.100. [Download](https://dotnet.microsoft.com/download) dependencies.

After downloading the project run

``` bash
  dotnet restore
```

on your terminal.

## Download (Only available for x64 architecture)

Click on the download link of your OS and extract the files.

*[Linux](https://github.com/kimon0202/GearLanguage/releases/download/alpha-1.0.0/linux-x64.rar)
*[MacOS](https://github.com/kimon0202/GearLanguage/releases/download/alpha-1.0.0/osx-x64.rar)
*[Windows](https://github.com/kimon0202/GearLanguage/releases/download/alpha-1.0.0/win-x64.rar)

After extracting the files, you should add the extracted folder to your PATH Environmental Variable.

### Linux Only

And then update user permissions to Write/Read the folder's content, using the command:
``` bash
  sudo chmod -R 700 /path/to/the/directory
```

### Adding PATH Environmetal Variable

#### Linux and MacOS

Open a new terminal and type:

``` bash
  cd ~
```

to open root directory. Then type the following command to edit your .bash_profile (using nano text-editor):

``` bash
  nano .bach_profile
```

Then add the following line to your .bash_profile, save the file and close the editor:

``` bash
  export PATH=$PATH:/path/to/the/directory
```

Finally, type:

``` bash
  source ~/.bash_profile
```
to update your terminal or close the terminal window and open it again.

### Windows

Open **Control Panel**, click at **System** and the on **Advanced System Settings**. Now click on **Environment Variables**, and under the System Variables category look for a PATH variable. Double-click it and add ```C:\Users\gusta\Desktop\Workspace\GearLanguage\GearLanguage\bin\Release\netcoreapp3.1\win-x64\``` to a new line. Click **OK** to close the opened windows.

## Running the application

### Development

At the folder the project(.csprof file) is located open a new terminal and run:

``` bash
  dotnet run _pathOfFileToRun_
```

### Pre-Release

Run using the command: 

``` bash
  gear _pathOfFileToRun_
```

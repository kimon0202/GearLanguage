# Gear · Language ![GitHub license](https://img.shields.io/badge/license-MIT-green.svg) [![Build Status](https://travis-ci.com/kimon0202/GearLanguage.svg?branch=master)](https://travis-ci.com/kimon0202/GearLanguage)

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

## Running the application

### Development

At the folder the project(.csprof file) is located open a new terminal and run:

``` bash
  dotner run _pathOfFileToRun_
```

### Production (Only on Windows)

1. Add ```C:\Users\gusta\Desktop\Workspace\GearLanguage\GearLanguage\bin\Release\netcoreapp3.1\win-x64\``` to your PATH System Environmental Variable

2. Run

``` bash
  gear _pathOfFileToRun_
```

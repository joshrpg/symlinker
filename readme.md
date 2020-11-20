# Symlinker.exe

## How to use

```Batch
symlinker:
  This is to give symlinking an actual exe rather than trying to use the embedded mklink command embedded into Windows CMD.

Usage:
  symlinker [options]

Options:
  -v, --verbose                       Shows debug information.
  -f, --force                         Forces symlinker to override an existing link. This will also override an existing file or folder. I'm too lazy to check if it is a link. ^_^
  -l, --link <link> (REQUIRED)        Specifies the new symbolic link name.
  -t, --target <target> (REQUIRED)    Specifies the path (relative or absolute) that the new link refers to.
  --version                           Show version information
  -?, -h, --help                      Show help and usage information
```

Example:

`symlinker --link C:\Path\To\Link --target C:\Path\To\Link_Target`

## How to build

### Prerec

* [VS Code](https://code.visualstudio.com/Download)
* [C# Extension](https://marketplace.visualstudio.com/items?itemName=ms-dotnettools.csharp)
* [.NET Core](https://dotnet.microsoft.com/download)

### Build

1. Clone the repo
2. Use cmd to navigate to the src folder in the repo folder.
3. Run the following `dotnet run`.

```Batch
git clone https://github.com/joshrpg/symlinker.git
cd symlinker\src
dotnet run
```

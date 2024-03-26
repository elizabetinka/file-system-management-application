using System;
using System.Collections.Generic;
using System.IO;
using Itmo.ObjectOrientedProgramming.Lab4.Models;
using Itmo.ObjectOrientedProgramming.Lab4.Models.GetTreeStrategies;
using Itmo.ObjectOrientedProgramming.Lab4.Services.Command;
using Directory = Itmo.ObjectOrientedProgramming.Lab4.Models.Directory;
using File = Itmo.ObjectOrientedProgramming.Lab4.Models.File;

namespace Itmo.ObjectOrientedProgramming.Lab4.Entities.FileSystem;

public class LocalFileSystem : IFileSystem
{
    private IGetTreeStrategyForSystemWithoutICollections _getTreeStrategy = new ConsoleGetTreeStrategy();

    public LocalFileSystem(string rootAdresse, IGetTreeStrategyForSystemWithoutICollections getTreeStrategy)
    {
        // ToDo если такого пути нет
        RootAdresse = rootAdresse;
        _getTreeStrategy = getTreeStrategy;
    }

    public LocalFileSystem(string rootAdresse)
    {
        // ToDo если такого пути нет
        RootAdresse = rootAdresse;
    }

    public string RootAdresse { get; init; }
    public FyleSystemMode Mode { get; init; } = FyleSystemMode.Console;

    public ICollection<IFileSystemObject> GetTree(int depth, string currentDirectory)
    {
        _getTreeStrategy.Path = RootAdresse + currentDirectory;
        return _getTreeStrategy.GetTree(depth);
    }

    public string? GetFileRepresentation(string path, string currentDirectory)
    {
        if (path is null)
        {
            return null;
        }

        path = PathToFull(currentDirectory + "/" + path);

        try
        {
            return System.IO.File.ReadAllText(path);
        }
        catch (Exception e) when (e is SystemException)
        {
            return null;
        }
    }

    public bool Move(string from, string toPlace)
    {
        if (from is null || toPlace is null)
        {
            return false;
        }

        try
        {
            var fileInfo = new FileInfo(from);
            fileInfo.MoveTo(toPlace);
        }
        catch (Exception e) when (e is SystemException)
        {
            return false;
        }

        return true;
    }

    public bool Copy(string from, string toPlace)
    {
        if (from is null || toPlace is null)
        {
            return false;
        }

        try
        {
            var fileInfo = new FileInfo(from);
            fileInfo.CopyTo(toPlace);
        }
        catch (Exception e) when (e is SystemException)
        {
            return false;
        }

        return true;
    }

    public bool Delete(string path, string currentDirectory)
    {
        var directoryInfo = new DirectoryInfo(RootAdresse + currentDirectory);
        foreach (FileInfo file in directoryInfo.GetFiles())
        {
            if (file.FullName == (RootAdresse + currentDirectory + path))
            {
                file.Delete();
                return true;
            }
        }

        return false;
    }

    public bool Rename(string path, string name, string currentDirectory)
    {
        if (path is null)
        {
            return false;
        }

        var directoryInfo = new DirectoryInfo(RootAdresse + currentDirectory);
        path = PathToFull(path);
        foreach (FileInfo file in directoryInfo.GetFiles())
        {
            if (file.FullName == path)
            {
                file.MoveTo(RootAdresse + currentDirectory + name);
                return true;
            }
        }

        return false;
    }

    private string PathToFull(string path)
    {
        return path.StartsWith(RootAdresse, StringComparison.Ordinal) ? path : RootAdresse + path;
    }

    private ICollection<IFileSystemObject> GetTreePrivate(int depth, string path)
    {
        if (depth <= 0)
        {
            return new List<IFileSystemObject>();
        }

        ICollection<IFileSystemObject> res = new List<IFileSystemObject>();
        var directoryInfo = new DirectoryInfo(path);

        foreach (FileInfo file in directoryInfo.GetFiles())
        {
            res.Add(new File(file.Name));
        }

        foreach (DirectoryInfo directory in directoryInfo.GetDirectories())
        {
            res.Add(new Directory(directory.Name, GetTreePrivate(depth - 1, directory.FullName)));
        }

        return res;
    }
}
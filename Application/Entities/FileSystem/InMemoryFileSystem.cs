using System;
using System.Collections.Generic;
using System.Linq;
using Itmo.ObjectOrientedProgramming.Lab4.Models;
using Itmo.ObjectOrientedProgramming.Lab4.Models.GetTreeStrategies;
using Itmo.ObjectOrientedProgramming.Lab4.Services.Command;
using File = Itmo.ObjectOrientedProgramming.Lab4.Models.File;
using Directory = Itmo.ObjectOrientedProgramming.Lab4.Models.Directory;

namespace Itmo.ObjectOrientedProgramming.Lab4.Entities.FileSystem;

public class InMemoryFileSystem : IFileSystem
{
    private ICollection<IFileSystemObject> _fileSystemObjects;
    private IGetStrategyFroSystemWithCollections _getTreeStrategy = new InMemoryTreeStratagy();

    public InMemoryFileSystem(string rootAdresse, ICollection<IFileSystemObject> fileSystemObjects, IGetStrategyFroSystemWithCollections getTreeStrategy)
    {
        _getTreeStrategy = getTreeStrategy;
        RootAdresse = rootAdresse;
        _fileSystemObjects = fileSystemObjects;
    }

    public InMemoryFileSystem(string rootAdresse, ICollection<IFileSystemObject> fileSystemObjects)
    {
        RootAdresse = rootAdresse;
        _fileSystemObjects = fileSystemObjects;
    }

    public string RootAdresse { get; init; }
    public FyleSystemMode Mode { get; init; } = FyleSystemMode.InMemory;

    public ICollection<IFileSystemObject> GetTree(int depth, string currentDirectory)
    {
        _getTreeStrategy.SetCollection(_fileSystemObjects);
        return _getTreeStrategy.GetTree(depth);
    }

    public string? GetFileRepresentation(string path, string currentDirectory)
    {
        path = path ?? throw new ArgumentNullException(nameof(path));

        if (!path.StartsWith(currentDirectory, StringComparison.Ordinal))
        {
            path = currentDirectory + path;
        }

        IFileSystemObject? obj = FindFileSystemObject(path);
        if (obj is File f)
        {
            return f.Name + "\n Содержимое файла";
        }

        return null;
    }

    public bool Move(string from, string toPlace)
    {
        from = from ?? throw new ArgumentNullException(nameof(from));
        toPlace = toPlace ?? throw new ArgumentNullException(nameof(toPlace));

        IFileSystemObject? obj = FindFileSystemObject(from);
        IFileSystemObject? obj2 = FindFileSystemObject(toPlace);
        if (obj is File f && obj2 is Directory d)
        {
            if (!d.FileSystemObjects.Any(ob => ob.Name.Equals(f.Name, StringComparison.Ordinal)))
            {
                if (InFirstDepth(from))
                {
                    _fileSystemObjects.Remove(f);
                    return true;
                }

                Directory? dir = FindDirectoryFileSystemObject(from);
                if (dir is Directory d2)
                {
                    d2.FileSystemObjects.Remove(f);
                }

                d.FileSystemObjects.Add(f.Clone());
                return true;
            }
        }

        return false;
    }

    public bool Copy(string from, string toPlace)
    {
        from = from ?? throw new ArgumentNullException(nameof(from));
        toPlace = toPlace ?? throw new ArgumentNullException(nameof(toPlace));

        IFileSystemObject? obj = FindFileSystemObject(from);
        IFileSystemObject? obj2 = FindFileSystemObject(toPlace);
        if (obj is File f && obj2 is Directory d)
        {
            if (!d.FileSystemObjects.Any(ob => ob.Name.Equals(f.Name, StringComparison.Ordinal)))
            {
                d.FileSystemObjects.Add(f.Clone());
                return true;
            }
        }

        return false;
    }

    public bool Delete(string path, string currentDirectory)
    {
        path = path ?? throw new ArgumentNullException(nameof(path));

        if (!path.StartsWith(currentDirectory, StringComparison.Ordinal))
        {
            path = currentDirectory + path;
        }

        IFileSystemObject? obj = FindFileSystemObject(path);
        if (obj is File f)
        {
            if (InFirstDepth(path))
            {
                _fileSystemObjects.Remove(f);
                return true;
            }

            Directory? dir = FindDirectoryFileSystemObject(path);
            if (dir is Directory d)
            {
                d.FileSystemObjects.Remove(f);
            }
        }

        return false;
    }

    public bool Rename(string path, string name, string currentDirectory)
    {
        path = path ?? throw new ArgumentNullException(nameof(path));
        name = name ?? throw new ArgumentNullException(nameof(name));

        if (!path.StartsWith(currentDirectory, StringComparison.Ordinal))
        {
            path = currentDirectory + path;
        }

        IFileSystemObject? obj = FindFileSystemObject(path);
        if (obj is File f)
        {
            if (FindFileSystemObject(ChangeName(path, name)) is null)
            {
                f.Name = name;
                return true;
            }
        }

        return false;
    }

    private IFileSystemObject? FindFileSystemObject(string path)
    {
        string[] str = path.Split('/');
        return FindFileSystemObjectByArray(str);
    }

    private IFileSystemObject? FindFileSystemObjectByArray(string[] str)
    {
        ICollection<IFileSystemObject> fileSystemObjectsTemp = _fileSystemObjects;

        foreach (string fileObject in str)
        {
            IFileSystemObject? elem = fileSystemObjectsTemp.FirstOrDefault(obj => obj.Name.Equals(fileObject, StringComparison.Ordinal));
            if (elem is null || (elem is File && !fileObject.Equals(str[str.Length - 1], StringComparison.Ordinal)))
            {
                return null;
            }

            if (elem is Directory d)
            {
                if (fileObject.Equals(str[str.Length - 1], StringComparison.Ordinal))
                {
                    return elem;
                }

                fileSystemObjectsTemp = d.FileSystemObjects;
            }
            else
            {
                return elem;
            }
        }

        return null;
    }

    private Directory? FindDirectoryFileSystemObject(string path)
    {
        string[] str = path.Split('/');
        if (str.Length == 1)
        {
            return null;
        }

        string[] str2 = new string[str.Length - 1];
        for (int i = 0; i < str.Length - 1; i++)
        {
            str2[i] = str[i];
        }

        IFileSystemObject? res = FindFileSystemObjectByArray(str2);
        if (res is Directory d)
        {
            return d;
        }

        return null;
    }

    private string ChangeName(string path, string name)
    {
        int pos = path.LastIndexOf('/');
        if (pos == -1)
        {
            return name;
        }

        return string.Concat(path.AsSpan(0, pos), name);
    }

    private bool AreWeHaveThisPath(string path)
    {
        string[] str = path.Split('/');
        ICollection<IFileSystemObject> fileSystemObjectsTemp = _fileSystemObjects;

        foreach (string fileObject in str)
        {
            if (fileObject.Equals(str[str.Length - 1], StringComparison.Ordinal))
            {
                break;
            }

            IFileSystemObject? elem = _fileSystemObjects.FirstOrDefault(obj => obj.Name.Equals(fileObject, StringComparison.Ordinal));
            if (elem is null || (elem is File && !fileObject.Equals(str[str.Length - 1], StringComparison.Ordinal)))
            {
                return false;
            }

            if (elem is Directory d)
            {
                _fileSystemObjects = d.FileSystemObjects;
            }
        }

        if (_fileSystemObjects.Any(obj => obj.Name.Equals(str[str.Length - 1], StringComparison.Ordinal)))
        {
            return true;
        }

        return false;
    }

    private bool InFirstDepth(string path)
    {
        string[] str = path.Split('/');
        return str.Length == 1;
    }
}
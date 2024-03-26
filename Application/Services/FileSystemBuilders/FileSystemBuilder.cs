using System;
using System.Collections.Generic;
using Itmo.ObjectOrientedProgramming.Lab4.Entities.FileSystem;
using Itmo.ObjectOrientedProgramming.Lab4.Models;
using Itmo.ObjectOrientedProgramming.Lab4.Services.Command;
using Itmo.ObjectOrientedProgramming.Lab4.Services.FileSystemBuilders;

namespace Itmo.ObjectOrientedProgramming.Lab4.Services;

public class FileSystemBuilder : IFileSystemBuilder, IInMemoryFileSystem
{
    private string? _adresse;
    private FyleSystemMode _mode;
    private ICollection<IFileSystemObject> _fileSystemObjects = new List<IFileSystemObject>();

    public IFileSystemBuilder WithAdresse(string adresse)
    {
        adresse = adresse ?? throw new ArgumentNullException(nameof(adresse));
        _adresse = adresse;
        return this;
    }

    public IFileSystemBuilder WithMode(FyleSystemMode mode)
    {
        _mode = mode;
        return this;
    }

    public IInMemoryFileSystem WithInMemoryMode()
    {
        _mode = FyleSystemMode.InMemory;
        return this;
    }

    public IInMemoryFileSystem WithFileSystemObject(IFileSystemObject fileSystemObject)
    {
        _fileSystemObjects.Add(fileSystemObject);
        return this;
    }

    public IFileSystem Build()
    {
        _adresse = _adresse ?? throw new ArgumentNullException(nameof(_adresse));
        return _mode == FyleSystemMode.Console ? new LocalFileSystem(_adresse) : new InMemoryFileSystem(_adresse, _fileSystemObjects);
    }
}
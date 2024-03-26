using System;
using System.Collections.Generic;
using System.Linq;
using Itmo.ObjectOrientedProgramming.Lab4.Entities.FileSystem;
using Itmo.ObjectOrientedProgramming.Lab4.Services.Command;

namespace Itmo.ObjectOrientedProgramming.Lab4.Services;

public class FileSystemRepository : IFIleSystemRepository
{
    private readonly ICollection<IFileSystem> _fileSystems;

    public FileSystemRepository(ICollection<IFileSystem> fileSystems)
    {
        _fileSystems = fileSystems;
    }

    public void Add(IFileSystem fileSystem)
    {
        IFileSystem? var = _fileSystems.FirstOrDefault(obj => obj.RootAdresse.Equals(fileSystem.RootAdresse, StringComparison.OrdinalIgnoreCase));
        if (var is null)
        {
            _fileSystems.Add(fileSystem);
        }
    }

    public void Delete(IFileSystem fileSystem)
    {
        IFileSystem? var = _fileSystems.FirstOrDefault(obj => obj.RootAdresse.Equals(fileSystem.RootAdresse, StringComparison.OrdinalIgnoreCase));
        if (var is null)
        {
            _fileSystems.Remove(fileSystem);
        }
    }

    public IFileSystem? FindByRootAdresseAndMode(string rootAdresse, FyleSystemMode mode)
    {
        return _fileSystems.FirstOrDefault(obj => obj.RootAdresse.Equals(rootAdresse, StringComparison.OrdinalIgnoreCase) && obj.Mode.Equals(mode));
    }
}
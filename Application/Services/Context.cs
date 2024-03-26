using System;
using System.IO;
using Itmo.ObjectOrientedProgramming.Lab4.Entities.FileSystem;
using Itmo.ObjectOrientedProgramming.Lab4.Services;
using Itmo.ObjectOrientedProgramming.Lab4.Services.Command;

namespace Itmo.ObjectOrientedProgramming.Lab4.Models;

public class Context
{
    private IFIleSystemRepository _fileSystemRepository;
    private IPrinterForShowRepository _printerForShowRepository;
    private ITreeRepresentationMaster _treeRepresentationMaster;

    public Context(IFIleSystemRepository fileSystemRepository, IPrinterForShowRepository printerForShowRepository, ITreeRepresentationMaster treeRepresentationMaster)
    {
        _fileSystemRepository = fileSystemRepository;
        _printerForShowRepository = printerForShowRepository;
        _treeRepresentationMaster = treeRepresentationMaster;
    }

    public IFileSystem? FileSystem { get; set; }
    public string CurrentDirectoryRelative { get; set; } = string.Empty; // c /

    public bool TryConnectToFileSystem(string adresse, FyleSystemMode mode)
    {
        _fileSystemRepository = _fileSystemRepository ?? throw new ArgumentException(nameof(_fileSystemRepository));
        FileSystem = _fileSystemRepository.FindByRootAdresseAndMode(adresse, mode);
        return FileSystem is not null;
    }

    public bool TryMoveFile(string from, string to)
    {
        if (FileSystem is null)
        {
            return false;
        }

        return FileSystem.Move(from, to);
    }

    public bool TryCopyFile(string from, string to)
    {
        if (FileSystem is null)
        {
            return false;
        }

        return FileSystem.Copy(from, to);
    }

    public bool TryDeleteFile(string path)
    {
        if (FileSystem is null)
        {
            return false;
        }

        return FileSystem.Delete(path, CurrentDirectoryRelative);
    }

    public bool TryShowFile(FileShowMode mode, string path)
    {
        if (FileSystem is null)
        {
            return false;
        }

        string? file = FileSystem.GetFileRepresentation(path, CurrentDirectoryRelative);
        if (file is null)
        {
            return false;
        }

        IPrinterForShow? printerForShow = _printerForShowRepository.FindByMode(mode);
        if (printerForShow is null)
        {
            return false;
        }

        return printerForShow.TryDraw(file);
    }

    public bool TryPrintList(int depth = 1)
    {
        if (FileSystem is null)
        {
            return false;
        }

        IPrinterForShow? printerForShowConsole = _printerForShowRepository.FindByMode(FileShowMode.Console);
        if (printerForShowConsole is null)
        {
            return false;
        }

        printerForShowConsole.TryDraw(
            _treeRepresentationMaster.TreeRepresentation(FileSystem.GetTree(depth, CurrentDirectoryRelative)));

        return true;
    }

    public bool TryRenameFile(string path, string name)
    {
        if (FileSystem is null)
        {
            return false;
        }

        return FileSystem.Rename(path, name, CurrentDirectoryRelative);
    }

    public bool TryGoTo(string path)
    {
        path = path ?? throw new ArgumentNullException(nameof(path));

        if (FileSystem is null)
        {
            return false;
        }

        if (IsFullPath(path))
        {
            int found = path.IndexOf(FileSystem + CurrentDirectoryRelative, StringComparison.Ordinal);
            CurrentDirectoryRelative = path.Substring(found + (FileSystem + CurrentDirectoryRelative).Length);
            return true;
        }

        var directoryInfo = new DirectoryInfo(FileSystem.RootAdresse + CurrentDirectoryRelative);

        foreach (DirectoryInfo directory in directoryInfo.GetDirectories())
        {
            if (path.StartsWith(directory.Name, StringComparison.Ordinal))
            {
                CurrentDirectoryRelative = "/" + path;
                return true;
            }
        }

        return false;
    }

    public void Disconnect()
    {
        FileSystem = null;
        CurrentDirectoryRelative = string.Empty;
    }

    private bool IsFullPath(string path)
    {
        if (FileSystem is null)
        {
            return false;
        }

        return path.StartsWith(FileSystem.RootAdresse + CurrentDirectoryRelative, StringComparison.Ordinal);
    }
}
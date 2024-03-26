using System.Collections.Generic;
using Itmo.ObjectOrientedProgramming.Lab4.Models;
using Itmo.ObjectOrientedProgramming.Lab4.Services.Command;

namespace Itmo.ObjectOrientedProgramming.Lab4.Entities.FileSystem;

public interface IFileSystem
{
    string RootAdresse { get; }
    FyleSystemMode Mode { get; }

    ICollection<IFileSystemObject> GetTree(int depth, string currentDirectory);

    public string? GetFileRepresentation(string path, string currentDirectory);
    bool Move(string from, string toPlace);

    bool Copy(string from, string toPlace);
    bool Delete(string path, string currentDirectory);
    public bool Rename(string path, string name, string currentDirectory);
}
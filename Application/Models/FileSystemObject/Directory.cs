using System;
using System.Collections.Generic;

namespace Itmo.ObjectOrientedProgramming.Lab4.Models;

public class Directory : IFileSystemObject
{
    public Directory(string name, ICollection<IFileSystemObject> fileSystemObjects)
    {
        Name = "/" + name;
        FileSystemObjects = fileSystemObjects;
    }

    public ICollection<IFileSystemObject> FileSystemObjects { get; }
    public string Name { get; init; }

    public void AcceptVisitor(IFileSystemObjectVisitor fileSystemObjectVisitor, int i)
    {
        fileSystemObjectVisitor =
            fileSystemObjectVisitor ?? throw new ArgumentNullException(nameof(fileSystemObjectVisitor));
        fileSystemObjectVisitor.VisitDirectory(this, i);
    }

    public IFileSystemObject Clone()
    {
        return new Directory(Name, new List<IFileSystemObject>(FileSystemObjects));
    }
}
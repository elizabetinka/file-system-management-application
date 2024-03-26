using System;

namespace Itmo.ObjectOrientedProgramming.Lab4.Models;

public class File : IFileSystemObject
{
    public File(string name)
    {
        Name = name;
    }

    public string Name { get; set; }
    public void AcceptVisitor(IFileSystemObjectVisitor fileSystemObjectVisitor, int i)
    {
        fileSystemObjectVisitor =
            fileSystemObjectVisitor ?? throw new ArgumentNullException(nameof(fileSystemObjectVisitor));
        fileSystemObjectVisitor.VisitFile(this, i);
    }

    public IFileSystemObject Clone()
    {
        return new File(Name);
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Itmo.ObjectOrientedProgramming.Lab4.Models;

public class TreeRepresentationMaster : IFileSystemObjectVisitor, ITreeRepresentationMaster
{
    private StringBuilder _stringBuilder = new StringBuilder();
    public string Result { get => _stringBuilder.ToString(); }

    public string TreeRepresentation(ICollection<IFileSystemObject> fileSystemObjects, int i = 0)
    {
        fileSystemObjects = fileSystemObjects ?? throw new ArgumentNullException(nameof(fileSystemObjects));
        foreach (IFileSystemObject fileObject in fileSystemObjects)
        {
            fileObject.AcceptVisitor(this, i);
        }

        return Result;
    }

    public void VisitDirectory(Directory directory, int i = 0)
    {
        directory = directory ?? throw new ArgumentNullException(nameof(directory));
        _stringBuilder.Append(string.Concat(Enumerable.Repeat("-",  (i * 3) + 1)) + directory.Name + '\n');
        foreach (IFileSystemObject obj in directory.FileSystemObjects)
        {
            obj.AcceptVisitor(this, i + 1);
        }
    }

    public void VisitFile(File file, int i = 0)
    {
        file = file ?? throw new ArgumentNullException(nameof(file));
        _stringBuilder.Append(string.Concat(Enumerable.Repeat("---", i)) + file.Name + '\n');
    }
}
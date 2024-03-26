using System.Collections.Generic;

namespace Itmo.ObjectOrientedProgramming.Lab4.Models.GetTreeStrategies;

public class InMemoryTreeStratagy : IGetStrategyFroSystemWithCollections
{
    public ICollection<IFileSystemObject> MyCollection { get; private set; } = new List<IFileSystemObject>();

    public void SetCollection(ICollection<IFileSystemObject> collection)
    {
        MyCollection = collection;
    }

    public ICollection<IFileSystemObject> GetTree(int depth)
    {
        if (depth <= 0)
        {
            return new List<IFileSystemObject>();
        }

        ICollection<IFileSystemObject> res = new List<IFileSystemObject>();
        foreach (IFileSystemObject obj in MyCollection)
        {
            if (obj is Directory d)
            {
                res.Add(CuttedDirectory(depth - 1, d));
            }
            else
            {
                res.Add(obj);
            }
        }

        return res;
    }

    private Directory CuttedDirectory(int depth, Directory directory)
    {
        if (depth <= 0)
        {
            return new Directory(directory.Name, new List<IFileSystemObject>());
        }

        ICollection<IFileSystemObject> res = new List<IFileSystemObject>();
        foreach (IFileSystemObject obj in directory.FileSystemObjects)
        {
            if (obj is Directory d)
            {
                res.Add(CuttedDirectory(depth - 1, d));
            }
            else
            {
                res.Add(obj);
            }
        }

        return new Directory(directory.Name, res);
    }
}
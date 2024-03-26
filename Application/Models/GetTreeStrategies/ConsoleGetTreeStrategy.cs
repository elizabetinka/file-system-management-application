using System.Collections.Generic;
using System.IO;

namespace Itmo.ObjectOrientedProgramming.Lab4.Models.GetTreeStrategies;

public class ConsoleGetTreeStrategy : IGetTreeStrategyForSystemWithoutICollections
{
    public string Path { get; set; } = string.Empty;
    public ICollection<IFileSystemObject> GetTree(int depth)
    {
        return GetTreePrivate(depth, Path);
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
using System.Collections.Generic;
using Itmo.ObjectOrientedProgramming.Lab4.Entities.FileSystem;
using Itmo.ObjectOrientedProgramming.Lab4.Models;
using File = Itmo.ObjectOrientedProgramming.Lab4.Models.File;
using Directory = Itmo.ObjectOrientedProgramming.Lab4.Models.Directory;

namespace Itmo.ObjectOrientedProgramming.Lab3.Tests;

public class DataBase
{
    public DataBase()
    {
        ICollection<IFileSystemObject> objects = new List<IFileSystemObject>
        {
            new File("Скрин"),
            new Directory(
                "Фотки2004",
                new List<IFileSystemObject> { new File("фото1"), new File("фото2"), new File("фото3") }),
            new Directory(
                "Фотки2005",
                new List<IFileSystemObject> { new File("фото1"), new File("фото2"), new File("фото3") }),
            new Directory(
                "Фотки2006",
                new List<IFileSystemObject> { new File("фото1"), new File("фото2"), new File("фото3") }),
        };

        FileSystemList = new List<IFileSystem>(new List<IFileSystem> { new InMemoryFileSystem("Альбом", objects) });
    }

    public ICollection<IFileSystem> FileSystemList { get; private set; }
}
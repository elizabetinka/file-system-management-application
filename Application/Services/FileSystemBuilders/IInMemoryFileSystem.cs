using Itmo.ObjectOrientedProgramming.Lab4.Entities.FileSystem;
using Itmo.ObjectOrientedProgramming.Lab4.Models;

namespace Itmo.ObjectOrientedProgramming.Lab4.Services.FileSystemBuilders;

public interface IInMemoryFileSystem
{
    public IInMemoryFileSystem WithFileSystemObject(IFileSystemObject fileSystemObject);

    IFileSystem Build();
}
using Itmo.ObjectOrientedProgramming.Lab4.Entities.FileSystem;
using Itmo.ObjectOrientedProgramming.Lab4.Services.Command;

namespace Itmo.ObjectOrientedProgramming.Lab4.Services.FileSystemBuilders;

public interface IFileSystemBuilder
{
    IFileSystemBuilder WithAdresse(string adresse);

    IFileSystemBuilder WithMode(FyleSystemMode mode);

    public IInMemoryFileSystem WithInMemoryMode();

    IFileSystem Build();
}

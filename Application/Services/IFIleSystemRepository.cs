using Itmo.ObjectOrientedProgramming.Lab4.Entities.FileSystem;
using Itmo.ObjectOrientedProgramming.Lab4.Services.Command;

namespace Itmo.ObjectOrientedProgramming.Lab4.Services;

public interface IFIleSystemRepository
{
    void Add(IFileSystem fileSystem);

    void Delete(IFileSystem fileSystem);

    IFileSystem? FindByRootAdresseAndMode(string rootAdresse, FyleSystemMode mode);
}
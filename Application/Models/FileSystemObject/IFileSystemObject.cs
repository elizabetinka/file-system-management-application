namespace Itmo.ObjectOrientedProgramming.Lab4.Models;

public interface IFileSystemObject : IPrototype<IFileSystemObject>
{
    string Name { get; }

    void AcceptVisitor(IFileSystemObjectVisitor fileSystemObjectVisitor, int i);
}
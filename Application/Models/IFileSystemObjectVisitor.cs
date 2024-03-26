namespace Itmo.ObjectOrientedProgramming.Lab4.Models;

public interface IFileSystemObjectVisitor
{
    void VisitDirectory(Directory directory, int i = 0);

    void VisitFile(File file, int i = 0);
}
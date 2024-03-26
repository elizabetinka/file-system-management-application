using System.Collections.Generic;

namespace Itmo.ObjectOrientedProgramming.Lab4.Models.GetTreeStrategies;

public interface IGetTreeStrategy
{
    ICollection<IFileSystemObject> GetTree(int depth);
}
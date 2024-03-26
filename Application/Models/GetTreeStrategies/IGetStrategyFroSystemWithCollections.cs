using System.Collections.Generic;

namespace Itmo.ObjectOrientedProgramming.Lab4.Models.GetTreeStrategies;

public interface IGetStrategyFroSystemWithCollections : IGetTreeStrategy
{
    ICollection<IFileSystemObject> MyCollection { get; }
    void SetCollection(ICollection<IFileSystemObject> collection);
}
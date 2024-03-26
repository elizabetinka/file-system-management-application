namespace Itmo.ObjectOrientedProgramming.Lab4.Models.GetTreeStrategies;

public interface IGetTreeStrategyForSystemWithoutICollections : IGetTreeStrategy
{
    string Path { get; set; }
}
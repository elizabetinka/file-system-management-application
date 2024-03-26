namespace Itmo.ObjectOrientedProgramming.Lab4.Models;

public interface IPrototype<T>
{
    T Clone();
}
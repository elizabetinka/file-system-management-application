using System;

namespace Itmo.ObjectOrientedProgramming.Lab4.Services.Command.Listeners;

public class StartConsoleListener : ICommandEventLiteners
{
    private string _reaction;

    public StartConsoleListener(string reaction)
    {
        _reaction = reaction;
    }

    public void Reaction()
    {
        Console.WriteLine(_reaction);
    }
}
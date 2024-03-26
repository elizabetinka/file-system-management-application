using System;
using System.Collections.Generic;
using Itmo.ObjectOrientedProgramming.Lab4.Services.Command;
using Itmo.ObjectOrientedProgramming.Lab4.Services.Handlers;

namespace Itmo.ObjectOrientedProgramming.Lab4.Services;

public class ArgsParser
{
    private IHandler _rootHandler;

    public ArgsParser(IHandler rootHandler)
    {
        _rootHandler = rootHandler;
    }

    public ICommand? Parse(string consoleString)
    {
        consoleString = consoleString ?? throw new ArgumentNullException(nameof(consoleString));
        IList<string> strings = new List<string>();
        foreach (string str in consoleString.Split(" "))
        {
            if (!string.IsNullOrEmpty(str))
            {
                strings.Add(str);
            }
        }

        return _rootHandler.Handle(strings);
    }
}
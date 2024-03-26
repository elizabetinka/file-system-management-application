using System;
using System.Collections.Generic;
using Itmo.ObjectOrientedProgramming.Lab4.Services.Command;

namespace Itmo.ObjectOrientedProgramming.Lab4.Services.Handlers;

public class MoveHandler : IHandler
{
    public IHandler? NextHandler { get; set; }

    public ICommand? Handle(IList<string> parseString)
    {
        parseString = parseString ?? throw new ArgumentNullException(nameof(parseString));
        if (parseString.Count == 4 && parseString[0] == "file" && parseString[1] == "move")
        {
            return new MoveCommand(parseString[2], parseString[3]);
        }

        return NextHandler?.Handle(parseString);
    }
}
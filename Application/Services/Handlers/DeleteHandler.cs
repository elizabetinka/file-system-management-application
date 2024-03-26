using System;
using System.Collections.Generic;
using Itmo.ObjectOrientedProgramming.Lab4.Services.Command;

namespace Itmo.ObjectOrientedProgramming.Lab4.Services.Handlers;

public class DeleteHandler : IHandler
{
    public IHandler? NextHandler { get; set; }

    public ICommand? Handle(IList<string> parseString)
    {
        parseString = parseString ?? throw new ArgumentNullException(nameof(parseString));
        if (parseString.Count == 3 && parseString[0] == "file" && parseString[1] == "delete")
        {
            return new DeleteCommand(parseString[2]);
        }

        return NextHandler?.Handle(parseString);
    }
}
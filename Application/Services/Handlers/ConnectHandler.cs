using System;
using System.Collections.Generic;
using Itmo.ObjectOrientedProgramming.Lab4.Services.Command;

namespace Itmo.ObjectOrientedProgramming.Lab4.Services.Handlers;

public class ConnectHandler : IHandler
{
    public IHandler? NextHandler { get; set; }

    public ICommand? Handle(IList<string> parseString)
    {
        parseString = parseString ?? throw new ArgumentNullException(nameof(parseString));
        if (parseString.Count == 4 && parseString[0] == "connect" && parseString[2] == "-m")
        {
            if (parseString[3] == "local")
            {
                return new ConnectComand(parseString[1], FyleSystemMode.Console);
            }

            if (parseString[3] == "in-memory")
            {
                return new ConnectComand(parseString[1], FyleSystemMode.InMemory);
            }
        }

        return NextHandler?.Handle(parseString);
    }
}
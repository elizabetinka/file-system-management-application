using System;
using System.Collections.Generic;
using System.Globalization;
using Itmo.ObjectOrientedProgramming.Lab4.Services.Command;

namespace Itmo.ObjectOrientedProgramming.Lab4.Services.Handlers;

public class TreeListHandler : IHandler
{
    public IHandler? NextHandler { get; set; }

    public ICommand? Handle(IList<string> parseString)
    {
        parseString = parseString ?? throw new ArgumentNullException(nameof(parseString));
        if (parseString.Count >= 2 && parseString[0] == "tree" && parseString[1] == "list")
        {
            int depth = 1;
            if (parseString.Count == 4 && parseString[2] == "-d")
            {
                try
                {
                    depth = int.Parse(parseString[3], new NumberFormatInfo());
                    if (depth <= 0)
                    {
                        return NextHandler?.Handle(parseString);
                    }
                }
                catch (Exception e) when (e is SystemException)
                {
                    return NextHandler?.Handle(parseString);
                }
            }

            return new TreeListCommand(depth);
        }

        return NextHandler?.Handle(parseString);
    }
}
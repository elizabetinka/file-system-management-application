using System;
using System.Collections.Generic;
using Itmo.ObjectOrientedProgramming.Lab4.Services.Command;

namespace Itmo.ObjectOrientedProgramming.Lab4.Services.Handlers;

public class FileShowHandler : IHandler
{
    public IHandler? NextHandler { get; set; }

    public ICommand? Handle(IList<string> parseString)
    {
        parseString = parseString ?? throw new ArgumentNullException(nameof(parseString));
        if (parseString.Count >= 3 && parseString[0] == "file" && parseString[1] == "show")
        {
            FileShowMode mode_ = FileShowMode.Console;
            if (parseString.Count == 5 && parseString[3] == "-m")
            {
                if (parseString[4] == "console")
                {
                    mode_ = FileShowMode.Console;
                }
            }

            return new FileShowCommand(parseString[2], mode_);
        }

        return NextHandler?.Handle(parseString);
    }
}
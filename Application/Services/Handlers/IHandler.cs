using System.Collections.Generic;
using Itmo.ObjectOrientedProgramming.Lab4.Services.Command;

namespace Itmo.ObjectOrientedProgramming.Lab4.Services.Handlers;

public interface IHandler
{
    public IHandler? NextHandler { get; set; }

    ICommand? Handle(IList<string> parseString);
}
using Itmo.ObjectOrientedProgramming.Lab4.Models;
using Itmo.ObjectOrientedProgramming.Lab4.Services.Command.Listeners;

namespace Itmo.ObjectOrientedProgramming.Lab4.Services.Command;

public enum CommandStatus
{
    Start = 0,
    Error = 1,
    Success = 2,
}

public interface ICommand
{
    public EventManager Events { get; }
    void Execute(Context context);
}
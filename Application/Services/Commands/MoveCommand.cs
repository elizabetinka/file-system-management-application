using System;
using Itmo.ObjectOrientedProgramming.Lab4.Models;
using Itmo.ObjectOrientedProgramming.Lab4.Services.Command.Listeners;

namespace Itmo.ObjectOrientedProgramming.Lab4.Services.Command;

public class MoveCommand : ICommand
{
    private string _from;
    private string _to;

    public MoveCommand(string from, string to)
    {
        _from = from;
        _to = to;
    }

    public EventManager Events { get; } = new EventManager();

    public void Execute(Context context)
    {
        context = context ?? throw new ArgumentNullException(nameof(context));
        Events.Notify(CommandStatus.Start);

        if (!context.TryMoveFile(_from, _to))
        {
            Events.Notify(CommandStatus.Error);
            return;
        }

        Events.Notify(CommandStatus.Success);
    }
}
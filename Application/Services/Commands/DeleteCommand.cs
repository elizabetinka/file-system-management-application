using System;
using Itmo.ObjectOrientedProgramming.Lab4.Models;
using Itmo.ObjectOrientedProgramming.Lab4.Services.Command.Listeners;

namespace Itmo.ObjectOrientedProgramming.Lab4.Services.Command;

public class DeleteCommand : ICommand
{
    private string _path;

    public DeleteCommand(string path)
    {
        _path = path;
    }

    public EventManager Events { get; } = new EventManager();

    public void Execute(Context context)
    {
        context = context ?? throw new ArgumentNullException(nameof(context));
        Events.Notify(CommandStatus.Start);

        if (!context.TryDeleteFile(_path))
        {
            Events.Notify(CommandStatus.Error);
            return;
        }

        Events.Notify(CommandStatus.Success);
    }
}
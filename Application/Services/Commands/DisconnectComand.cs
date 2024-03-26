using System;
using Itmo.ObjectOrientedProgramming.Lab4.Models;
using Itmo.ObjectOrientedProgramming.Lab4.Services.Command.Listeners;

namespace Itmo.ObjectOrientedProgramming.Lab4.Services.Command;

public class DisconnectComand : ICommand
{
    public EventManager Events { get; } = new EventManager();

    public void Execute(Context context)
    {
        context = context ?? throw new ArgumentNullException(nameof(context));
        Events.Notify(CommandStatus.Start);

        context.Disconnect();

        Events.Notify(CommandStatus.Success);
    }
}
using System;
using Itmo.ObjectOrientedProgramming.Lab4.Models;
using Itmo.ObjectOrientedProgramming.Lab4.Services.Command.Listeners;

namespace Itmo.ObjectOrientedProgramming.Lab4.Services.Command;

public enum FyleSystemMode
{
    Console,
    InMemory,
}

public class ConnectComand : ICommand
{
    private string _adresse;
    private FyleSystemMode _mode;

    public ConnectComand(string adresse, FyleSystemMode mode)
    {
        _adresse = adresse;
        _mode = mode;
    }

    public EventManager Events { get; } = new EventManager();

    public void Execute(Context context)
    {
        context = context ?? throw new ArgumentNullException(nameof(context));
        Events.Notify(CommandStatus.Start);

        if (!context.TryConnectToFileSystem(_adresse, _mode))
        {
            Events.Notify(CommandStatus.Error);
            return;
        }

        Events.Notify(CommandStatus.Success);
    }
}
using System;
using Itmo.ObjectOrientedProgramming.Lab4.Models;
using Itmo.ObjectOrientedProgramming.Lab4.Services.Command.Listeners;

namespace Itmo.ObjectOrientedProgramming.Lab4.Services.Command;

public enum FileShowMode
{
    Console,
    File,
}

public class FileShowCommand : ICommand
{
    private string _adresse;
    private FileShowMode _mode;

    public FileShowCommand(string adresse, FileShowMode mode)
    {
        _adresse = adresse;
        _mode = mode;
    }

    public EventManager Events { get; } = new EventManager();

    public void Execute(Context context)
    {
        context = context ?? throw new ArgumentNullException(nameof(context));
        Events.Notify(CommandStatus.Start);

        // ToDo
        if (context.TryShowFile(_mode, _adresse))
        {
            Events.Notify(CommandStatus.Error);
            return;
        }

        Events.Notify(CommandStatus.Success);
    }
}
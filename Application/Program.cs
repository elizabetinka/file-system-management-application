using System;
using System.Collections.Generic;
using Itmo.ObjectOrientedProgramming.Lab4.Entities.FileSystem;
using Itmo.ObjectOrientedProgramming.Lab4.Models;
using Itmo.ObjectOrientedProgramming.Lab4.Services;
using Itmo.ObjectOrientedProgramming.Lab4.Services.Command;
using Itmo.ObjectOrientedProgramming.Lab4.Services.Command.Listeners;
using Itmo.ObjectOrientedProgramming.Lab4.Services.Handlers;

namespace Itmo.ObjectOrientedProgramming.Lab4;

public static class Program
{
    public static void Main()
    {
        var fileSys = new LocalFileSystem("/Users/indo.evri.momint/file-system-management-application/Application/Services");
        var context = new Context(new FileSystemRepository(new List<IFileSystem>()), new PrinterForShowRepository(new List<IPrinterForShow> { new PrinterForShowConsole() }), new TreeRepresentationMaster());
        context.FileSystem = fileSys;

        // context.PrinterForList.Draw(context.FileSystem.GetTree(2));
        // context.TryGoTo("Handlers");

        // context.TryShowFile(FileShowMode.Console, "IHandler.cs");
        var handler1 = new ConnectHandler();
        var handler2 = new DisconnectHandler();
        var handler3 = new FileShowHandler();
        var handler4 = new TreeListHandler();
        handler3.NextHandler = handler4;
        handler2.NextHandler = handler3;
        handler1.NextHandler = handler2;
        var parser = new ArgsParser(handler1);

        // context = new Context(new FileSystemRepository(new List<IFileSystem>()), new PrinterForTree(), new PrinterForShow());
        ICommand? command = parser.Parse("tree list -d 2");
        command = command ?? throw new ArgumentNullException(nameof(command));
        command.Events.Subscribe(CommandStatus.Start, new StartConsoleListener("Огоооо, что-то напечаталось"));
        command.Execute(context);
    }
}
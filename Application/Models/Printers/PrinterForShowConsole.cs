using System;
using Itmo.ObjectOrientedProgramming.Lab4.Services.Command;

namespace Itmo.ObjectOrientedProgramming.Lab4.Models;

public class PrinterForShowConsole : IPrinterForShow
{
    public FileShowMode ShowMode { get; } = FileShowMode.Console;
    public bool TryDraw(string file)
    {
        Console.WriteLine(file);
        return true;
    }
}
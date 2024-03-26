using System.Collections.Generic;
using Itmo.ObjectOrientedProgramming.Lab4.Services.Command;

namespace Itmo.ObjectOrientedProgramming.Lab4.Models;

public class PrinterForShowFile : IPrinterForShow
{
    private string _path = "../../../labworkINFO.txt";

    public PrinterForShowFile(string path)
    {
        _path = path;
    }

    public PrinterForShowFile()
    {
    }

    public FileShowMode ShowMode { get; } = FileShowMode.File;
    public bool TryDraw(string file)
    {
        System.IO.File.AppendAllLines(_path, new List<string> { file });
        return true;
    }
}
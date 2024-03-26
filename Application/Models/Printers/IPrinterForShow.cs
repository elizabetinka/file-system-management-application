using Itmo.ObjectOrientedProgramming.Lab4.Services.Command;

namespace Itmo.ObjectOrientedProgramming.Lab4.Models;

public interface IPrinterForShow
{
    public FileShowMode ShowMode { get; }
    bool TryDraw(string file);
}
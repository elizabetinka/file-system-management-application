using Itmo.ObjectOrientedProgramming.Lab4.Models;
using Itmo.ObjectOrientedProgramming.Lab4.Services.Command;

namespace Itmo.ObjectOrientedProgramming.Lab4.Services;

public interface IPrinterForShowRepository
{
    void Add(IPrinterForShow printerForShow);

    void Delete(IPrinterForShow printerForShow);

    IPrinterForShow? FindByMode(FileShowMode mode);
}
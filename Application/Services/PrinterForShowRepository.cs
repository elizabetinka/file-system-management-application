using System.Collections.Generic;
using System.Linq;
using Itmo.ObjectOrientedProgramming.Lab4.Models;
using Itmo.ObjectOrientedProgramming.Lab4.Services.Command;

namespace Itmo.ObjectOrientedProgramming.Lab4.Services;

public class PrinterForShowRepository : IPrinterForShowRepository
{
    private readonly ICollection<IPrinterForShow> _printersForShow;

    public PrinterForShowRepository(ICollection<IPrinterForShow> printersForShow)
    {
        _printersForShow = printersForShow;
    }

    public void Add(IPrinterForShow printerForShow)
    {
        IPrinterForShow? var = _printersForShow.FirstOrDefault(obj => obj.ShowMode.Equals(printerForShow.ShowMode));
        if (var is null)
        {
            _printersForShow.Add(printerForShow);
        }
    }

    public void Delete(IPrinterForShow printerForShow)
    {
        IPrinterForShow? var = _printersForShow.FirstOrDefault(obj => obj.ShowMode.Equals(printerForShow.ShowMode));
        if (var is null)
        {
            _printersForShow.Remove(printerForShow);
        }
    }

    public IPrinterForShow? FindByMode(FileShowMode mode)
    {
        return _printersForShow.FirstOrDefault(obj => obj.ShowMode.Equals(mode));
    }
}
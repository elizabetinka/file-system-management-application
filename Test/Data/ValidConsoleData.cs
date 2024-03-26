using System.Collections;
using System.Collections.Generic;
using Itmo.ObjectOrientedProgramming.Lab4.Services.Command;

namespace Itmo.ObjectOrientedProgramming.Lab3.Tests;

public class ValidConsoleData : IEnumerable<object[]>
{
    private readonly List<object[]> _data = new List<object[]>
    {
        new object[] { "tree list -d 2", new TreeListCommand(2) },
        new object[] { "  tree    list -d     2 ", new TreeListCommand(2) },
        new object[] { "connect Альбом -m in-memory", new ConnectComand("Альбом", FyleSystemMode.InMemory) },
        new object[] { "disconnect", new DisconnectComand() },
        new object[] { "tree goto Фотки2004 ", new GoToCommand("Фотки2004") },
        new object[] { "file show Скрин ", new FileShowCommand("Скрин", FileShowMode.Console) },
        new object[] { "file move Скрин  Фотки2005", new MoveCommand("Скрин", "Фотки2005") },
        new object[] { "file copy Фотки2005/Скрин  Фотки2004", new CopyCommand("Фотки2005/Скрин", "Фотки2004") },
        new object[] { "file delete Фотки2005/Скрин", new DeleteCommand("Фотки2005/Скрин") },
        new object[] { "file rename Фотки2005/Скрин фото4", new RenameCommand("Фотки2005/Скрин", "фото4") },
    };

    public IEnumerator<object[]> GetEnumerator() => _data.GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}
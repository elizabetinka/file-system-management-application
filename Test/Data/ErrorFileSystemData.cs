using System.Collections;
using System.Collections.Generic;

namespace Itmo.ObjectOrientedProgramming.Lab3.Tests;

public class ErrorFileSystemData : IEnumerable<object[]>
{
    private readonly List<object[]> _data = new List<object[]>
    {
        new object[] { "connect Альбомы -m in-memory" },
        new object[] { "connect Альбом -m local" },
        new object[] { "tree goto Фотки2010 " },
        new object[] { "file show Скрины " },
        new object[] { "file move Фотки2004/фото1  Фотки2005" },
        new object[] { "file copy Фотки2005/Скрин  Фотки2004" },
        new object[] { "file delete Фотки2005/Скрин" },
        new object[] { "file rename Фотки2005/Скрин фото4" },
    };

    public IEnumerator<object[]> GetEnumerator() => _data.GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}
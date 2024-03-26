using System.Collections;
using System.Collections.Generic;

namespace Itmo.ObjectOrientedProgramming.Lab3.Tests;

public class SuccessFileSystemData : IEnumerable<object[]>
{
    private readonly List<object[]> _data = new List<object[]>
    {
        new object[] { "tree list -d 2 " },
        new object[] { "connect Альбом -m in-memory" },
        new object[] { "disconnect " },
        new object[] { "tree goto Фотки2004 " },
        new object[] { "file show Скрин " },
        new object[] { "file move Скрин  Фотки2005" },
        new object[] { "file copy Фотки2005/Скрин  Фотки2004" },
        new object[] { "file delete Фотки2005/Скрин" },
        new object[] { "file rename Фотки2005/Скрин фото4" },
    };

    public IEnumerator<object[]> GetEnumerator() => _data.GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}
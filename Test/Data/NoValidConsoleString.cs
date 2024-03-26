using System.Collections;
using System.Collections.Generic;

namespace Itmo.ObjectOrientedProgramming.Lab3.Tests;

public class NoValidConsoleString : IEnumerable<object[]>
{
    private readonly List<object[]> _data = new List<object[]>
    {
        new object[] { "connect - m Mode " },
        new object[] { "connect - m         " },
        new object[] { "connect -m local disconnect " },
        new object[] { "tree GoTo path " },
        new object[] { "tree list -d -1 " },
    };

    public IEnumerator<object[]> GetEnumerator() => _data.GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}
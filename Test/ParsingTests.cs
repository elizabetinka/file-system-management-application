using System.Collections.Generic;
using Itmo.ObjectOrientedProgramming.Lab3.Tests;
using Itmo.ObjectOrientedProgramming.Lab4.Models;
using Itmo.ObjectOrientedProgramming.Lab4.Services;
using Itmo.ObjectOrientedProgramming.Lab4.Services.Command;
using Itmo.ObjectOrientedProgramming.Lab4.Services.Handlers;
using Xunit;

namespace Itmo.ObjectOrientedProgramming.Lab4.Tests;

public class ParsingTests
{
    private ArgsParser _parser;
    private IFIleSystemRepository _fileSystemRepository;
    private IPrinterForShowRepository _printerForShowRepository;
    private Context _context;
    public ParsingTests()
    {
        var h1 = new ConnectHandler();
        var h2 = new DisconnectHandler();
        var h3 = new GoToHandler();
        var h4 = new TreeListHandler();
        var h5 = new MoveHandler();
        var h6 = new FileShowHandler();
        var h7 = new CopyHandler();
        var h8 = new DeleteHandler();
        var h9 = new RenameHandler();
        h8.NextHandler = h9;
        h7.NextHandler = h8;
        h6.NextHandler = h7;
        h5.NextHandler = h6;
        h4.NextHandler = h5;
        h3.NextHandler = h4;
        h2.NextHandler = h3;
        h1.NextHandler = h2;
        _parser = new ArgsParser(h1);
        var dataBase = new DataBase();
        _fileSystemRepository = new FileSystemRepository(dataBase.FileSystemList);
        _printerForShowRepository = new PrinterForShowRepository(new List<IPrinterForShow>
            { new PrinterForShowConsole(), new PrinterForShowFile() });
        _context = new Context(_fileSystemRepository, _printerForShowRepository, new TreeRepresentationMaster());
    }

    [Theory]
    [ClassData(typeof(NoValidConsoleString))]
    public void NoValidTest(string consoleString)
    {
        Assert.Null(_parser.Parse(consoleString));
    }

    [Theory]
    [ClassData(typeof(ValidConsoleData))]
    public void ValidTest(string consoleString, ICommand exCommand)
    {
        ICommand? command = _parser.Parse(consoleString);
        Assert.NotNull(command);
        Assert.Equivalent(exCommand, command);
    }
}
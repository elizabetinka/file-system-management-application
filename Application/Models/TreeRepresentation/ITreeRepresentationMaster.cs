using System.Collections.Generic;
using Itmo.ObjectOrientedProgramming.Lab4.Models;

namespace Itmo.ObjectOrientedProgramming.Lab4;

public interface ITreeRepresentationMaster
{
    public string Result { get; }
    public string TreeRepresentation(ICollection<IFileSystemObject> fileSystemObjects, int i = 0);
}
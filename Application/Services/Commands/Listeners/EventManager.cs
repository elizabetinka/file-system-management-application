using System.Collections.Generic;

namespace Itmo.ObjectOrientedProgramming.Lab4.Services.Command.Listeners;

public class EventManager
{
    private IDictionary<int, List<ICommandEventLiteners>> _listeners =
        new Dictionary<int, List<ICommandEventLiteners>>();

    public void Subscribe(CommandStatus eventMoment, ICommandEventLiteners liteners)
    {
        if (!_listeners.ContainsKey((int)eventMoment))
        {
            _listeners.Add((int)eventMoment, new List<ICommandEventLiteners>());
        }

        _listeners[(int)eventMoment].Add(liteners);
    }

    public void Unsbscribe(CommandStatus eventMoment, ICommandEventLiteners liteners)
    {
        if (!_listeners.ContainsKey((int)eventMoment))
        {
            _listeners.Add((int)eventMoment, new List<ICommandEventLiteners>());
        }

        _listeners[(int)eventMoment].Remove(liteners);
    }

    public void Notify(CommandStatus eventMoment)
    {
        if (!_listeners.ContainsKey((int)eventMoment))
        {
            _listeners.Add((int)eventMoment, new List<ICommandEventLiteners>());
        }

        foreach (ICommandEventLiteners listener in _listeners[(int)eventMoment])
        {
            listener.Reaction();
        }
    }
}
﻿using CQRS.Core.Messages;

namespace CQRS.Core.Events;

public class BaseEvent : Message
{
    protected BaseEvent(string type)
    {
        Type = type;
    }

    public string Type { get; set; }
    public int Version { get; set; }

    public Guid ProductId { get; set; }
}
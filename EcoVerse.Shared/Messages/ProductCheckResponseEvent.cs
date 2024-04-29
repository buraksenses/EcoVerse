﻿namespace EcoVerse.Shared.Messages;

public class ProductCheckResponseEvent
{
    public Guid CorrelationId { get; set; }
    
    public string UserId { get; set; }
    
    public Guid ProductId { get; set; }

    public decimal Price { get; set; }

    public int Quantity { get; set; }

    public bool Exists { get; set; }
}
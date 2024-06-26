﻿namespace EcoVerse.StockManagement.Query.Domain.Entities;

public class InventoryItemEntity
{
    public Guid Id { get; set; }

    public Guid ProductId { get; set; }

    public string Name { get; set; }

    public string Description { get; set; }

    public decimal Price { get; set; }

    public int Quantity { get; set; }
}
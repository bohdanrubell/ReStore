﻿namespace API.Features.Products.CreateProduct;

public class Request
{
    public string Name { get; set; }
    public string Description { get; set; } 
    public long Price { get; set; } 
    public string Type { get; set; }
    public string Brand { get; set; }
    public int QuantityInStock { get; set; }
}
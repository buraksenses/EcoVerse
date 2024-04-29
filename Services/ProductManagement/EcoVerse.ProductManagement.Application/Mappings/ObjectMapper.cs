﻿using AutoMapper;

namespace EcoVerse.ProductManagement.Application.Mappings;

public class ObjectMapper
{
    private static readonly Lazy<IMapper> lazy = new Lazy<IMapper>(() =>
    {
        var config = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile<CustomMapper>();
        });
        return config.CreateMapper();
    });

    public static IMapper Mapper => lazy.Value;
}
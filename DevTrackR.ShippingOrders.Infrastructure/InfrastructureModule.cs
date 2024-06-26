﻿using DevTrackR.ShippingOrders.Core.Repositories;
using DevTrackR.ShippingOrders.Infrastructure.Messaging;
using DevTrackR.ShippingOrders.Infrastructure.Persistence;
using DevTrackR.ShippingOrders.Infrastructure.Persistence.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Bson;
using MongoDB.Driver;

namespace DevTrackR.ShippingOrders.Infrastructure;
public static class InfrastructureModule
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.AddMongo();
        services.AddServiceRepository();
        services.AddServiceRabbitMq();
        return services;
    }

    public static IServiceCollection AddMongo(this IServiceCollection services)
    {
        services.AddSingleton<MongoDbOptions>(sp =>
        {
            var configuration = sp.GetService<IConfiguration>();
            var options = new MongoDbOptions();

            configuration.GetSection("Mongo").Bind(options);

            return options;
        });

        services.AddSingleton<IMongoClient>(sp =>
        {
            var configuration = sp.GetService<IConfiguration>();
            var options = sp.GetService<MongoDbOptions>();

            var client = new MongoClient(options.ConnectionString);
            var db = client.GetDatabase(options.Database);

            var dbSeed = new DbSeed(db);
            dbSeed.Populate();

            return client;
        });

        services.AddTransient(sp =>
        {
            BsonDefaults.GuidRepresentation = GuidRepresentation.Standard;

            var options = sp.GetService<MongoDbOptions>();
            var mongoClient = sp.GetService<IMongoClient>();

            var db = mongoClient.GetDatabase(options.Database);

            return db;
        });

        return services;
    }

    private static IServiceCollection AddServiceRepository(this IServiceCollection services)
    {
        services.AddScoped<IShippingServiceRepository, ShippingServiceRepository>();
        services.AddScoped<IShippingOrderRepository, ShippingOrderRepository>();
        return services;
    }

    private static IServiceCollection AddServiceRabbitMq( this IServiceCollection services)
    {
        services.AddScoped<IMessageBusService, RabbitMqService>();
        return services;
    }
}

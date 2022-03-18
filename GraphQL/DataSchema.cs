using System;
using GraphQL.Types;
using Microsoft.Extensions.DependencyInjection;

namespace GraphQLProductApp.GraphQL;

public class DataSchema : Schema
{
    public DataSchema(IServiceProvider resolver) : base(resolver)
    {
        Query = resolver.GetRequiredService<Query>();
        Mutation = resolver.GetRequiredService<ComponentMutation>();
    }
}
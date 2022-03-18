using System.Collections.Generic;
using GraphQL;
using GraphQL.Types;
using GraphQLProductApp.Data;
using GraphQLProductApp.Repository;

namespace GraphQLProductApp.GraphQL;

public class ProductType : ObjectGraphType<Product>
{
    public ProductType(IComponentRepository componentRepository)
    {
        Field(x => x.ProductId).Description("The id of the product.");
        Field(x => x.Name).Description("The name of the product.");
        Field(x => x.Price).Description("The price of the product.");
        Field(x => x.Description).Description("The description of the product.");
        Field<ListGraphType<ComponentType>>(
            "components",
            resolve: context => componentRepository.GetComponentsById(context.Source.ProductId));

        //Bit more tricky to have this in place, since it doesnt makes more sense in this context, due to the way Data is designed
        Field<ComponentType>("component",
            arguments: new QueryArguments(
                new List<QueryArgument>
                {
                    //By Id
                    new QueryArgument<IdGraphType> { Name = "id" },
                    //By Name
                    new QueryArgument<StringGraphType> { Name = "name" }
                }),
            resolve: context =>
            {
                var result = componentRepository;

                var componentId = context.GetArgument<int?>("id");
                if (componentId.HasValue)
                    return result
                        .GetComponentById(componentId.Value, context.Source.ProductId);
                var componentName = context.GetArgument<string>("name");
                if (!string.IsNullOrEmpty(componentName))
                    return result.GetComponentByName(componentName);
                return result.GetComponents();
            });
    }
}
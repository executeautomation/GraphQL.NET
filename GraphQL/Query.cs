using System.Collections.Generic;
using GraphQL;
using GraphQL.Types;
using GraphQLProductApp.Repository;

namespace GraphQLProductApp.GraphQL;

public class Query : ObjectGraphType
{
    public Query(
        IProductRepository productRepository,
        IComponentRepository componentRepository
    )
    {
        Field<ListGraphType<ProductType>>("products",
            resolve: context => productRepository.GetAllProducts());

        Field<ProductType>("product",
            arguments: new QueryArguments(new List<QueryArgument>
            {
                new QueryArgument<IdGraphType> { Name = "id" },
                new QueryArgument<StringGraphType> { Name = "name" }
            }),
            resolve: context =>
            {
                var result = productRepository;

                var id = context.GetArgument<int?>("id");
                if (id.HasValue) return result.GetProductById(id.Value);

                var name = context.GetArgument<string>("name");
                if (!string.IsNullOrEmpty(name))
                    return result.GetProductByName(name);

                return result.GetAllProducts();
            });

        Field<ListGraphType<ComponentType>>("components",
            resolve: context => componentRepository.GetComponents());

        //Field to get component based on arguments given below
        Field<ComponentType>("component",
            arguments: new QueryArguments(new List<QueryArgument>
            {
                //By Id
                new QueryArgument<IdGraphType> { Name = "id" }
            }),
            resolve: context =>
            {
                var result = componentRepository;

                var id = context.GetArgument<int?>("id");
                if (id.HasValue) return result.GetComponentById(id.Value);
                return result.GetComponents();
            });
    }
}
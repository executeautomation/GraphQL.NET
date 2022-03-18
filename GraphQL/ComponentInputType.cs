using GraphQL.Types;

namespace GraphQLProductApp.GraphQL;

public class ComponentInputType : InputObjectGraphType
{
    public ComponentInputType()
    {
        Name = "ComponentInput";
        Field<NonNullGraphType<StringGraphType>>("name");
        Field<StringGraphType>("description");
        Field<NonNullGraphType<IntGraphType>>("productId");
    }
}
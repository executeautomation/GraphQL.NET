using GraphQL.Types;
using GraphQLProductApp.Data;

namespace GraphQLProductApp.GraphQL;

internal class ComponentType : ObjectGraphType<Components>
{
    public ComponentType()
    {
        Field(x => x.Id).Description("Id of the component");
        Field(x => x.Name).Description("Name of the component");
        Field(x => x.Description).Description("Description of the component");
    }
}
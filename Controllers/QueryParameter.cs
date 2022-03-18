using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace GraphQLProductApp.Controllers;

public class QueryParameter
{
    public int Id { get; set; }

    [BindRequired] public string Name { get; set; }
}
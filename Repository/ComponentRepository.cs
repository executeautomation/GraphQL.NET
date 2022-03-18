using System.Collections.Generic;
using System.Linq;
using GraphQLProductApp.Data;

namespace GraphQLProductApp.Repository;

public class ComponentRepository : IComponentRepository
{
    private readonly ProductDbContext productContext;

    public ComponentRepository(ProductDbContext productContext)
    {
        this.productContext = productContext;
    }


    public List<Components> GetComponents()
    {
        return productContext.Components.ToList();
    }

    public List<Components> GetComponentsById(int id)
    {
        return productContext.Components.Where(x => x.ProductId == id).ToList();
    }

    public Components GetComponentById(int componentId, int productId)
    {
        return productContext.Components.Where(x => x.Id == componentId && x.ProductId == productId).FirstOrDefault();
    }

    public Components GetComponentById(int componentId)
    {
        return productContext.Components.Where(x => x.Id == componentId).FirstOrDefault();
    }

    public Components GetComponentByName(string componentName)
    {
        return productContext.Components.Where(x => x.Name == componentName).FirstOrDefault();
    }

    public Components CreateComponent(Components components)
    {
        productContext.Components.Add(components);
        productContext.SaveChanges();
        return components;
    }
}
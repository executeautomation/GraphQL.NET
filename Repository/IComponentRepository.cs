using System.Collections.Generic;
using GraphQLProductApp.Data;

namespace GraphQLProductApp.Repository;

public interface IComponentRepository
{
    Components CreateComponent(Components components);
    Components GetComponentById(int componentId);
    Components GetComponentById(int componentId, int productId);
    List<Components> GetComponents();
    List<Components> GetComponentsById(int id);
    Components GetComponentByName(string componentName);
}
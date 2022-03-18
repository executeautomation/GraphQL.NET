using System.Collections.Generic;
using GraphQLProductApp.Data;
using GraphQLProductApp.Repository;
using Microsoft.AspNetCore.Mvc;

namespace GraphQLProductApp.Controllers;

[ApiController]
[Route("[controller]")]
public class ComponentsController
{
    private readonly IComponentRepository _componentRepository;

    public ComponentsController(IComponentRepository componentRepository)
    {
        _componentRepository = componentRepository;
    }

    [HttpGet]
    [Route("/[controller]/[action]/{id}")]
    public List<Components> GetComponentByProductId(int id)
    {
        return _componentRepository.GetComponentsById(id);
    }

    [HttpGet]
    [Route("/[controller]/[action]/{id}")]
    public List<Components> GetComponentsByProductId(int id)
    {
        return _componentRepository.GetComponentsById(id);
    }


    [HttpPost]
    [Route("/[controller]/[action]")]
    public Components CreateComponent(Components components)
    {
        return _componentRepository.CreateComponent(components);
    }

    [HttpGet]
    [Route("/[controller]/[action]")]
    public List<Components> GetAllComponents()
    {
        return _componentRepository.GetComponents();
    }
}
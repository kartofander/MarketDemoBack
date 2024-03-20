using System.ComponentModel.DataAnnotations;
using Data.Commands.StoreItems;
using Data.Queries;
using MainApi.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MainApi.Endpoints;

[ApiController]
[Route("[controller]")]
public class StoreItemsController
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly StoreItemsQuery _storeItemsQuery;
    private readonly CreateStoreItemCommand _createStoreItemsCommand;
    private readonly DeleteStoreItemCommand _deleteStoreItemsCommand;
    private readonly UpdateStoreItemCommand _updateStoreItemsCommand;

    public StoreItemsController(
        IHttpContextAccessor httpContextAccessor,
        StoreItemsQuery storeItemsQuery,
        CreateStoreItemCommand createStoreItemsCommand,
        DeleteStoreItemCommand deleteStoreItemsCommand,
        UpdateStoreItemCommand updateStoreItemsCommand) 
    {
        _httpContextAccessor = httpContextAccessor;
        _storeItemsQuery = storeItemsQuery;
        _createStoreItemsCommand = createStoreItemsCommand;
        _deleteStoreItemsCommand = deleteStoreItemsCommand;
        _updateStoreItemsCommand = updateStoreItemsCommand;
    }

    [HttpGet("all")]
    public async Task<dynamic> GetStoreItems([Required] int page, [Required] int pageSize)
    {
        return await _storeItemsQuery.GetAvailableStoreItems(page, pageSize, x => x.Name, false);
    }

    [HttpPost]
    [Authorize]
    public async Task<dynamic> CreateStoreItem([FromBody] StoreItemDto itemDto)
    {
        var userLogin = _httpContextAccessor.HttpContext.User.Identity.Name;
        var result = await _createStoreItemsCommand.Execute(userLogin, itemDto.Name, itemDto.Cost, itemDto.Description);
        return new
        {
            result.id,
            result.validation
        };
    }

    [HttpPut]
    [Authorize]
    public async Task<IResult> UpdateStoreItem([FromBody] StoreItemDto itemDto)
    {
        var userLogin = _httpContextAccessor.HttpContext.User.Identity.Name;
        try
        {
            await _updateStoreItemsCommand.Execute(userLogin, itemDto.ItemId, itemDto.Name, itemDto.Cost, itemDto.Description);
        }
        catch (InvalidOperationException)
        {
            return Results.Forbid();
        }

        return Results.Ok();
    }

    [HttpDelete]
    [Authorize]
    public async Task DeleteStoreItem([Required] long itemId)
    {
        await _deleteStoreItemsCommand.Execute(itemId);
    }
}
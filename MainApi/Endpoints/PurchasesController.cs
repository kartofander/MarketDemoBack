using System.ComponentModel.DataAnnotations;
using Data.Queries;
using MainApi.Dtos;
using MainApi.Services.Purchases;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MainApi.Endpoints;

[ApiController]
[Route("[controller]")]
public class PurchasesController
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IPurchaseService _purchaseService;
    private readonly PurchasesQuery _purchaseQuery;

    public PurchasesController(
        IHttpContextAccessor httpContextAccessor,
        PurchasesQuery purchaseQuery,
        IPurchaseService purchaseService)
    {
        _httpContextAccessor = httpContextAccessor;
        _purchaseQuery = purchaseQuery;
        _purchaseService = purchaseService;
    }

    [HttpGet("history")]
    [Authorize]
    public async Task<dynamic> GetPurchaseHistory([Required] int page, [Required] int pageSize)
    {
        var userLogin = _httpContextAccessor.HttpContext.User.Identity.Name;
        var purchases = await _purchaseQuery.GetUserPurchases(userLogin, page, pageSize);

        return purchases
            .Select(x => new PurchaseResponseDto(x))
        .ToArray();
    }

    // I am assuming that the payment process takes place on the front end using some third-party payment services
    // and it sends data about the completed transaction here (as a receipt id number, for example),
    // so I can verify its authenticity and get its content by sending request to the same payment service from here.
    // But as part of this simplified project, I get the purchase data directly
    [HttpPost]
    public async Task<long> Purchase([FromBody] PurchaseRequestDto purchaseDto)
    {
        var userLogin = _httpContextAccessor.HttpContext.User?.Identity?.Name;

        if (userLogin != null)
        {
            return await _purchaseService.Purchase(purchaseDto, userLogin);
        }

        return await _purchaseService.Purchase(purchaseDto);
    }
}
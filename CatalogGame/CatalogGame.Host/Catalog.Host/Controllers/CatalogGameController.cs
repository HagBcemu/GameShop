using System.Net;
using Microsoft.AspNetCore.Mvc;
using CatalogGame.Host.Models.Dtos;
using CatalogGame.Host.Models.Requests;
using CatalogGame.Host.Models.Response;
using CatalogGame.Host.Services.Interfaces;
using Infrastructure;
using Microsoft.AspNetCore.Authorization;
using Infrastructure.Identity;

namespace Catalog.Host.Controllers
{
    [ApiController]
    [Authorize(Policy = AuthPolicy.AllowEndUserPolicy)]
    [Route(ComponentDefaults.DefaultRoute)]
    public class CatalogGameController : ControllerBase
    {
        private readonly ILogger<CatalogGameController> _logger;
        private readonly ICatalogGameService _catalogGameService;
        public CatalogGameController(
            ILogger<CatalogGameController> logger,
            ICatalogGameService catalogGameService)
        {
            _logger = logger;
            _catalogGameService = catalogGameService;
        }

        [HttpPost]
        [ProducesResponseType(typeof(AddGameResponce<int?>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Add(CreateGameRequest request)
        {
            var result = await _catalogGameService.AddAsync(request.Name, request.Description, request.Price, request.PictureFileName, request.CompanyName);
            return Ok(new AddGameResponce<int?>() { Id = result });
        }

        [HttpPost]
        [AllowAnonymous]
        [ProducesResponseType(typeof(PaginatedItemsResponse<CatalogGameItemDto>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Items(PaginatedItemsRequest request)
        {
            var result = await _catalogGameService.GetCatalogItemsAsync(request.PageSize, request.PageIndex);
            return Ok(result);
        }

        [HttpPost]
        [ProducesResponseType(typeof(StatusOperationResponce<bool?>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GameDelete(RequstsId requstsIdGame)
        {
            var result = await _catalogGameService.DeleteGame(requstsIdGame.Id);
            return Ok(new StatusOperationResponce<bool?>() { Success = result });
        }

        [HttpPost]
        [ProducesResponseType(typeof(StatusOperationResponce<bool?>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GameUpdate(CatalogGameItemDto request)
        {
            var result = await _catalogGameService.UpdateGameAsync(request.Id, request.Name, request.Description, request.Price, request.PictureFileName, request.CompanyName);
            return Ok(new StatusOperationResponce<bool?>() { Success = result });
        }

        [HttpPost]
        [ProducesResponseType(typeof(CatalogGameItemDto), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GameGetbyId(RequstsId requstsIdGame)
        {
            var result = await _catalogGameService.GetCatalogbyId(requstsIdGame.Id);
            return Ok(result);
        }
    }
}

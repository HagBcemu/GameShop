using MVC.Services.Interfaces;
using MVC.ViewModels;
using MVC.ViewModels.CatalogViewModels;
using MVC.ViewModels.Pagination;
namespace MVC.Controllers;

public class CatalogController : Controller
{
    private  readonly ICatalogService _catalogService;

    public CatalogController(ICatalogService catalogService)
    {
        _catalogService = catalogService;

		catalog = _catalogService.GetCatalogItems(0, 100, null, null).Result;
	}

    private Catalog catalog { get; set; }

    public async Task<IActionResult> Index(int? brandFilterApplied, int? typesFilterApplied, int? page, int? itemsPage)
    {
        return RedirectToAction("Index3");
    }

    public async Task<IActionResult> Index3()
    {
		
        var Basket = await _catalogService.GetBasketItem();

        var vm = new IndexViewModel()
		{
			CatalogItems = catalog.Data,
            BasketItems = Basket,
		};
        
		ViewData["SumBasket"] = Basket?.Sum(x => x.Price * x.Count);
		return View(vm);
    }

    public async Task<IActionResult> OrderPage()
    {
        var orders = await _catalogService.GetOrders();

        List<OrdersPage> userPage = new List<OrdersPage>();

        if (orders==null)
        {
			return View(userPage);
		}

        for (int i = 0; i < orders.Count; i++)
        {
            OrdersPage order = new OrdersPage()
            {
                Id = i + 1,
                Price = orders[i].Price,
                Items = new List<GameItemPage>(),
            };

            for (int j = 0; j < orders[i].Items.Count; j++)
            {
                GameItemPage gameItemPage = new GameItemPage()
                {
                    Count = orders[i].Items[j].Count,
                    Name = catalog.Data.Where(x => x.Id == orders[i].Items[j].GameId).First().Name,
                    PicturePath = catalog.Data.Where(x => x.Id == orders[i].Items[j].GameId).First().PictureFileName,
                };
                order.Items.Add(gameItemPage);
			}
			userPage.Add(order);
		}

		return View(userPage);
	}
    public async Task<IActionResult> CreateOrder()
    {
        var basket = _catalogService.GetBasketItem().Result;

        if (basket.Count() == 0)
        {
            return RedirectToAction("Index3");
        }

        await _catalogService.CreateOrder(basket);

        await _catalogService.RemoveBasket();

        return RedirectToAction("Index3");
    }


    public async Task<IActionResult> Basket2()
    {
        var Basket = await _catalogService.GetBasketItem();

        if (Basket == null)
        {
            Basket = new List<ItemGame>();
        }

        return View(Basket);
    }

    public async Task<IActionResult> AddItemBasket(int id)
    {
        var item = catalog.Data.Where(x => x.Id == id).FirstOrDefault();
        if (item!= null)
        {
			ItemGame itemGame = new ItemGame()
			{
				Id = id,
				Count = 1,
				Name = item.Name,
				PictureFileName = item.PictureFileName,
				Price = item.Price,
			};
			await _catalogService.AddToBasket(itemGame);
		}
        var Basket = await _catalogService.GetBasketItem();

        var vm = new IndexViewModel()
        {
            CatalogItems = catalog.Data,
            BasketItems = Basket,
        };

		ViewData["SumBasket"] = Basket?.Sum(x => x.Price * x.Count);

		return View("Index3", vm);
    }

	public async Task<IActionResult> RemoveBasket()
	{
		await _catalogService.RemoveBasket();

        var Basket = new List<ItemGame>();

		var vm = new IndexViewModel()
		{
			CatalogItems = catalog.Data,
			BasketItems = Basket,
		};

        ViewData["SumBasket"] = 0;

		return View("Index3", vm);
	}


	public async Task<IActionResult> GetBasket()
    {        
		var vm = new IndexViewModel()
		{
			CatalogItems = catalog.Data,
		};
        return PartialView("basket", vm);
    }

	public async Task<IActionResult> RemoveOneItemInBasket(int id)
	{

		await _catalogService.RemoveOneItemInBasket(id);

		var Basket = await _catalogService.GetBasketItem();

		var vm = new IndexViewModel()
		{
			CatalogItems = catalog.Data,
			BasketItems = Basket,
		};

		ViewData["SumBasket"] = Basket?.Sum(x => x.Price * x.Count);

		return View("Index3", vm);
	}



}
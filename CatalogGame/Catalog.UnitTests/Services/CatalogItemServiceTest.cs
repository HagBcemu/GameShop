using System.Threading;
using CatalogGame.Data.Entities;
using CatalogGame.Host.Models.Dtos;
using CatalogGame.Host.Models.Response;
using CatalogGame.Host.Repositories;
using Microsoft.VisualStudio.TestPlatform.ObjectModel;

namespace Catalog.UnitTests.Services;

public class CatalogItemServiceTest
{
    private readonly ICatalogGameService _catalogService;

    private readonly Mock<ICatalogGameItemRepository> _catalogItemRepository;
    private readonly Mock<IDbContextWrapper<ApplicationDbContext>> _dbContextWrapper;
    private readonly Mock<ILogger<CatalogGameService>> _logger;
    private readonly Mock<IMapper> _mapper;

    private readonly CatalogGameItem _testItem = new CatalogGameItem()
    {
        Id = 1,
        Name = "Name",
        Description = "Description",
        Price = 1000,
        CompanyName = "Steam",
        PictureFileName = "1.png"
    };

    public CatalogItemServiceTest()
    {
        _catalogItemRepository = new Mock<ICatalogGameItemRepository>();
        _dbContextWrapper = new Mock<IDbContextWrapper<ApplicationDbContext>>();
        _logger = new Mock<ILogger<CatalogGameService>>();
        _mapper = new Mock<IMapper>();

        var dbContextTransaction = new Mock<IDbContextTransaction>();
        _dbContextWrapper.Setup(s => s.BeginTransactionAsync(CancellationToken.None)).ReturnsAsync(dbContextTransaction.Object);

        _catalogService = new CatalogGameService(_dbContextWrapper.Object, _logger.Object, _catalogItemRepository.Object, _mapper.Object);
    }

    [Fact]
    public async Task AddAsync_Success()
    {
        // arrange
        var testResult = 1;

        _catalogItemRepository.Setup(s => s.Add(
            It.IsAny<string>(),
            It.IsAny<string>(),
            It.IsAny<decimal>(),
            It.IsAny<string>(),
            It.IsAny<string>())).ReturnsAsync(testResult);

        // act
        var result = await _catalogService.AddAsync(_testItem.Name, _testItem.Description, _testItem.Price, _testItem.PictureFileName, _testItem.CompanyName);

        // assert
        result.Should().Be(testResult);
    }

    [Fact]
    public async Task AddAsync_Failed()
    {
        // arrange
        int? testResult = null;

        _catalogItemRepository.Setup(s => s.Add(
		   It.IsAny<string>(),
		   It.IsAny<string>(),
		   It.IsAny<decimal>(),
		   It.IsAny<string>(),
		   It.IsAny<string>())).ReturnsAsync(testResult);

		// act
        var result = await _catalogService.AddAsync(_testItem.Name, _testItem.Description, _testItem.Price, _testItem.PictureFileName, _testItem.CompanyName);

		// assert
        result.Should().Be(testResult);
    }

    [Fact]
    public async Task Remove_Success()
    {
        // arrange
        bool removeItemResponceSuccess = true;

        _catalogItemRepository.Setup(s => s.DeleteGame(
            It.IsAny<int>())).ReturnsAsync(removeItemResponceSuccess);

        // act
        var result = await _catalogService.DeleteGame(_testItem.Id);

        // assert
        result.Should().Be(removeItemResponceSuccess);
    }

    [Fact]
    public async Task Remove_Failed()
    {
        // arrange
        bool removeItemResponceFailed = false;

        _catalogItemRepository.Setup(s => s.DeleteGame(
            It.IsAny<int>())).ReturnsAsync(removeItemResponceFailed);

        // act
        var result = await _catalogService.DeleteGame(_testItem.Id);

        // assert
        result.Should().Be(removeItemResponceFailed);
    }

    [Fact]

    public async Task Update_Succes()
    {
        // arrange
        bool updateItemSucces = true;

        _catalogItemRepository.Setup(s => s.UpdateGame(
           It.IsAny<int>(),
           It.IsAny<string>(),
           It.IsAny<string>(),
           It.IsAny<decimal>(),
           It.IsAny<string>(),
           It.IsAny<string>())).ReturnsAsync(updateItemSucces);

		// act
        var result = await _catalogService.UpdateGameAsync(_testItem.Id, _testItem.Name, _testItem.Description, _testItem.Price, _testItem.PictureFileName, _testItem.CompanyName);

        // assert
        result.Should().Be(updateItemSucces);
    }

    [Fact]
    public async Task Update_Failed()
    {
        // arrange
        var updateItemFailed = false;

        _catalogItemRepository.Setup(s => s.UpdateGame(
			It.IsAny<int>(),
			It.IsAny<string>(),
			It.IsAny<string>(),
			It.IsAny<decimal>(),
			It.IsAny<string>(),
			It.IsAny<string>())).ReturnsAsync(updateItemFailed);

		// act
        var result = await _catalogService.UpdateGameAsync(_testItem.Id, _testItem.Name, _testItem.Description, _testItem.Price, _testItem.PictureFileName, _testItem.CompanyName);

		// assert
        result.Should().Be(updateItemFailed);
    }
}
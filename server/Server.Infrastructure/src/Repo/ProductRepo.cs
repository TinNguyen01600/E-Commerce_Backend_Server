using Microsoft.EntityFrameworkCore;
using Server.Core.src.Common;
using Server.Core.src.Entity;
using Server.Core.src.RepoAbstract;
using Server.Core.src.ValueObject;
using Server.Infrastructure.src.Database;

namespace Server.Infrastructure.src.Repository;

public class ProductRepo : BaseRepo<Product>, IProductRepo
{
    private DbSet<OrderProduct> _orderProducts;
    public ProductRepo(AppDbContext context) : base(context)
    {
        _orderProducts = context.OrderProducts;
    }

    public override async Task<IEnumerable<Product>> GetAllAsync(QueryOptions options)
    {
        var allData = _data.Include("Images").Include("Category").Skip(options.PageNo).Take(options.PageSize);
        if (options.sortType == SortType.byTitle && options.sortOrder == SortOrder.asc)
        {
            return allData.OrderBy(item => item.Name).ToArray();
        }
        if (options.sortType == SortType.byTitle && options.sortOrder == SortOrder.desc)
        {
            return allData.OrderByDescending(item => item.Name).ToArray();
        }
        if (options.sortType == SortType.byPrice && options.sortOrder == SortOrder.asc)
        {
            return allData.OrderBy(item => item.Price).ToArray();
        }
        return allData.OrderByDescending(item => item.Price).ToArray();
    }

    public IEnumerable<Product> GetByCategory(Guid categoryId)
    {
        return _data.Include("Images").Include("Category").Where(products => products.Category.Id == categoryId);
    }

    public override async Task<Product> GetOneByIdAsync(Guid id)
    {
        var allData = _data.Include("Images").Include("Category");
        return allData.FirstOrDefault(product => product.Id == id);
    }

    public IEnumerable<Product> GetMostPurchased(int topNumber)
    {
        var mostPurchasedProducts = _orderProducts
                .GroupBy(orderProduct => orderProduct.Product.Id)
                .Select(group => new
                {
                    ProductId = group.Key,
                    TotalQuantity = group.Sum(item => item.Quantity)
                })
                .OrderByDescending(item => item.TotalQuantity)
                .Take(topNumber)
                .Join(_data.Include("Images").Include("Category"),
                    orderItem => orderItem.ProductId,
                    product => product.Id,
                    (orderItem, product) => product)
                .ToArray();

        return mostPurchasedProducts;
    }
}
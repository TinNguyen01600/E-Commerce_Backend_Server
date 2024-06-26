using Server.Core.src.Common;
using Server.Service.src.DTO;

namespace Server.Service.src.ServiceAbstract
{
    public interface IProductService
    {
        Task<IEnumerable<ProductReadDTO>> GetAllProductsAsync(QueryOptions options);
        Task<ProductReadDTO> GetProductById(Guid id);
        Task<ProductReadDTO> CreateProduct(ProductCreateDTO prodImg);
        Task<ProductReadDTO> UpdateProduct(Guid id, ProductUpdateDTO prodImg);
        Task<bool> DeleteProduct(Guid id);
        Task<IEnumerable<ProductReadDTO>> GetAllProductsByCategoryAsync(Guid categoryId);
        Task<IEnumerable<ProductReadDTO>> GetMostPurchasedProductsAsync(int top);
    }
}
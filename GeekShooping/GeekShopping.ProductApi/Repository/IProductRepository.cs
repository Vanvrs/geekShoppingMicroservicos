using GeekShopping.ProductApi.Data.ValuerObjects;
using GeekShopping.ProductApi.Model;

namespace GeekShopping.ProductApi.Repository
{
    public interface IProductRepository
    {
        Task<IEnumerable<ProductVO>> FindAll();
        Task<ProductVO> FindById(long id);
        Task<ProductVO> Create(ProductVO productVo);
        Task<ProductVO> Update(ProductVO productVo);
        Task<bool> Delete(long id);


    }
}

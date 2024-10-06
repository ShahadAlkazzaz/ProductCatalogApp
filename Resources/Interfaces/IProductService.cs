using Resources.Models;

namespace Resources.Interfaces
{
    public interface IProductService<T, TResult> where T : class where TResult : class
    {
        ResultResponse<TResult> Create(T product);
        ResultResponse<IEnumerable<TResult>> GetAll();
        ResultResponse<TResult> Update(string id, T updatedProduct);
        ResultResponse<TResult> Delete(string id);
    }
}

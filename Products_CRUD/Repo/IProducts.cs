using Products_CRUD.Models;

namespace Products_CRUD.Repo
{
    public interface IProducts
    {
        Task<IEnumerable<Products>> Get();
        Task<Products> Find(Guid uid);
        Task<Products> Add(Products model);
        Task<Products> Update(Products model);
        Task<Products> Remove(Products model);
    }
}

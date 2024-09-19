using Products_CRUD.Data;
using Products_CRUD.Models;
using Dapper;
using System.Data;
namespace Products_CRUD.Repo
{
    public class ProductRepo : IProducts
    {
        private readonly DapperDbContext context;

        public ProductRepo(DapperDbContext context)
        {
            this.context = context;
        }
       public async Task<IEnumerable<Products>> Get()
{
    var sql = "Product_CRUD";
    
    using var connection = context.CreateConnection();
    return await connection.QueryAsync<Products>(sql, new { Action = "SelectAll" }, commandType: CommandType.StoredProcedure);
}

public async Task<Products> Find(Guid uid)
{
    var sql = "Product_CRUD";
    
    using var connection = context.CreateConnection();
    return await connection.QueryFirstOrDefaultAsync<Products>(sql, new { ProductID = uid, Action = "Select" }, commandType: CommandType.StoredProcedure);
}

public async Task<Products> Add(Products model)
{
    model.ProductId = Guid.NewGuid();
    
    var sql = "Product_CRUD";
    
    using var connection = context.CreateConnection();
    await connection.ExecuteAsync(sql, new 
    {
        ProductId = model.ProductId,
        ProductName = model.ProductName,
        Price = model.Price,
        ProdcutDescription = model.ProdcutDescription,
        CreatedOn = DateTime.Now,
        Action = "Insert"
    }, commandType: CommandType.StoredProcedure);
    
    return model;
}

public async Task<Products> Update(Products model)
{
    model.UpdateOn = DateTime.Now;
    
    var sql = "Product_CRUD";
    
    using var connection = context.CreateConnection();
    await connection.ExecuteAsync(sql, new 
    {
        ProductId = model.ProductId,
        ProductName = model.ProductName,
        Price = model.Price,
        ProdcutDescription = model.ProdcutDescription,
        UpdateOn = DateTime.Now,
        Action = "Update"
    }, commandType: CommandType.StoredProcedure);
    
    return model;
}

public async Task<Products> Remove(Products model)
{
    var sql = "Product_CRUD";
    
    using var connection = context.CreateConnection();
    await connection.ExecuteAsync(sql, new { ProductID = model.ProductId, Action = "Delete" }, commandType: CommandType.StoredProcedure);
    
    return model;
}
    }
}

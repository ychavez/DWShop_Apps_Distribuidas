using DWShop.Client.Mobile.Models;
using SQLite;

namespace DWShop.Client.Mobile.Context
{
    public class DataContext
    {
        SQLiteAsyncConnection db;

        async Task Init()
        {
            if (db is not null)
                return;

            db = new SQLiteAsyncConnection(Path.Combine(FileSystem.AppDataDirectory, "DWShop.db3"),
                SQLiteOpenFlags.ReadWrite | SQLiteOpenFlags.Create | SQLiteOpenFlags.SharedCache);

            _ = await db.CreateTableAsync<ProductModel>();
        }

        public async Task<List<ProductModel>> GetBasket()
        {
            await Init();
            return await db.Table<ProductModel>().ToListAsync();
        }

        public async Task<ProductModel> GetProduct(int id) 
        {
            await Init();
            return await db.Table<ProductModel>().FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<bool> AddToBasket(ProductModel productModel) 
        {
            await Init();
            var _product = await GetProduct(productModel.Id);

            if (_product is not null)
                return false;

            return await db.InsertAsync(productModel) == 1;
        }

        public async Task<bool> RemoveFromBasket(int id) 
        {
            await Init();

            var _product = await GetProduct(id);


            if (_product is null)
                return false;

            return await db.DeleteAsync(_product) == 1;
        }
    }
}

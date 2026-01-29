using Shoping_Card_DB_Connection.Databases;
using Shoping_Card_DB_Connection.Models;
using System.Runtime.CompilerServices;

namespace Shoping_Card_DB_Connection.DataAccess
{
    public class SqlData : ISqlData
    {
        private readonly ISqlDataAccess _db;
        private const string connectionStringName = "SqlDb";

        public SqlData(ISqlDataAccess db)
        {
            _db = db;
        }

        public async Task<List<UserModel>> GetUserById(int Id)
        {
            return await _db.LoadData<UserModel, dynamic>("SP_GetUserById",
                                new { Id },
                                connectionStringName,
                                true);
        }

        public async Task<List<UserAuthoModel>> GetUserByUsername(string username)
        {
            return await _db.LoadData<UserAuthoModel, dynamic>("dbo.SP_GetUserByUsername",
                                                         new { username },
                                                         connectionStringName,
                                                         true);
        }

        public async Task<List<ProductModel>> GetProducts()
        {
            return await _db.LoadData<ProductModel, dynamic>("select * from ViewAllProducts ORDER BY [Name] ASC",
                                                        new { },
                                                        connectionStringName,
                                                        false);
        }

        public async Task<List<ProductModel>> GetProductById(int Id)
        {
            return await _db.LoadData<ProductModel, dynamic>("SP_GetProductById",
                                                       new { Id },
                                                       connectionStringName,
                                                       true);
        }

        public async Task<List<ProductModel>> GetProductByName(string name)
        {
            return await _db.LoadData<ProductModel, dynamic>("SP_GetProductByName",
                                                       new { name },
                                                       connectionStringName,
                                                       true);
        }

        public async Task<List<ProductModel>> SearchProducts(string? name, int? categoryId, decimal? minPrice, decimal? maxPrice)
        {
            return await _db.LoadData<ProductModel, dynamic>("SP_SearchProducts",
                                                       new
                                                       {
                                                           Name = String.IsNullOrEmpty(name) ? null : name,
                                                           CategoryId = categoryId == 0 ? null : categoryId,
                                                           MinPrice = minPrice == 0 ? null : minPrice,
                                                           MaxPrice = maxPrice == 0 ? null : maxPrice
                                                       },
                                                       connectionStringName,
                                                       true);
        }

        public async Task<List<CategoryModel>> GetAllCategories()
        {
            return await _db.LoadData<CategoryModel, dynamic>("select * from ViewAllCategories ORDER BY [Name] ASC",
                                                        new { },
                                                        connectionStringName,
                                                        false);
        }

        public async Task<List<CategoryModel>> GetCategoryById(int Id)
        {
            return await _db.LoadData<CategoryModel, dynamic>("SP_GetCategoryById",
                                                        new { Id },
                                                        connectionStringName,
                                                        true);
        }

        public async Task<List<CategoryModel>> GetCategoryByName(string name)
        {
            return await _db.LoadData<CategoryModel, dynamic>("SP_GetCategoryByName",
                                                        new { name },
                                                        connectionStringName,
                                                        true);
        }

        public async Task<List<CategoryModel>> SearchCategory(string? name)
        {
            return await _db.LoadData<CategoryModel, dynamic>("SP_SearchCategory",
                                                        new { Name = String.IsNullOrEmpty(name) ? null : name },
                                                        connectionStringName,
                                                        true);
        }

        public async Task<List<PurchaseModel>> GetPurchase(int userId, int productId)
        {
            return await _db.LoadData<PurchaseModel, dynamic>("SP_GetPurchase",
                                                        new { userId, productId },
                                                        connectionStringName,
                                                        true);
        }

        public async Task<List<PurchaseModel>> GetPuchaseById(int id)
        {
            return await _db.LoadData<PurchaseModel, dynamic>("SP_GetPurchaseById",
                                                        new { id },
                                                        connectionStringName,
                                                        true);
        }

        public async Task<List<PurchaseModel>> GetPurchaseByUserId(int userId)
        {
            return await _db.LoadData<PurchaseModel, dynamic>("SP_GetPurchaseByUserId",
                                                        new { userId },
                                                        connectionStringName,
                                                        true);
        }

        public async Task<List<PurchaseModel>> GetUncompletedPurchasesByUserId(int userId)
        {
            return await _db.LoadData<PurchaseModel, dynamic>("SP_GetUncompletedPurchasesByUserId",
                                                        new { userId },
                                                        connectionStringName,
                                                        true);
        }

        public async Task<List<PurchaseModel>> GetSpecificUncompletedPurchaseByUserId(int userId, int productId)
        {
            return await _db.LoadData<PurchaseModel, dynamic>("SP_GetSpecificUncompletedPurchaseByUserId",
                                                        new { userId, productId },
                                                        connectionStringName,
                                                        true);
        }

        public async Task<List<PurchaseModel>> GetCompletedAndSentPurchasesByUserId(int userId)
        {
            return await _db.LoadData<PurchaseModel, dynamic>("SP_GetCompletedAndSentPurchasesByUserId",
                                                        new { userId },
                                                        connectionStringName,
                                                        true);
        }

        public async Task<List<PurchaseModel>> AdminSearchPurchases(int? id, string? name, string? status)
        {
            return await _db.LoadData<PurchaseModel, dynamic>(
                "SP_AdminSearchPurchases",
                new
                {
                    Id = id == 0 ? null : id,
                    productName = string.IsNullOrEmpty(name) ? null : name,
                    status = string.IsNullOrEmpty(status) ? null : status
                },
                connectionStringName,
                true
            );
        }

        public async Task<List<PurchaseModel>> SearchCompletedAndSentPurchasesByUserId(int? id, string? name, string? status, int userId)
        {
            return await _db.LoadData<PurchaseModel, dynamic>("SP_SearchCompletedAndSentPurchasesByUserId",
                                                        new
                                                        {
                                                            Id = id == 0 ? null : id,
                                                            productName = String.IsNullOrEmpty(name) ? null : name,
                                                            status = String.IsNullOrEmpty(status) ? null : status,
                                                            UserId = userId
                                                        },
                                                        connectionStringName,
                                                        true);
        }

        public async Task Createuser(UserModel user)
        {
            await _db.SaveData<dynamic>("dbo.SP_CreateUser",
                                  new { user.Username, user.Password, user.EmailAddress, user.FirstName, user.LastName, user.Country, user.State, user.City, user.ZipCode, user.CardNumber },
                                  connectionStringName,
                                  true);
        }

        public async Task EditUserInfo(int Id, UserModel user)
        {
            await _db.SaveData<dynamic>("SP_EditUserInfo",
                                  new { Id, user.Username, user.EmailAddress, user.FirstName, user.LastName, user.Country, user.State, user.City, user.ZipCode, user.CardNumber },
                                  connectionStringName,
                                  true);
        }

        public async Task CreateCategory(string name)
        {
            await _db.SaveData<dynamic>("SP_CreateNewCategory",
                                  new { name },
                                  connectionStringName,
                                  true);
        }

        public async Task EditCategory(CategoryModel category)
        {
            await _db.SaveData<dynamic>("SP_EditCategoryById",
                                  new { category.Id, category.Name },
                                  connectionStringName,
                                  true);
        }

        public async Task DeleteCategory(int Id)
        {
            await _db.SaveData<dynamic>("SP_DeleteCategory",
                                  new { Id },
                                  connectionStringName,
                                  true);
        }

        public async Task CreateProduct(ProductModel product)
        {
            await _db.SaveData<dynamic>("SP_CreateNewProduct",
                                  new { product.Name, product.Amount, product.Info, product.Price, product.CategoryId },
                                  connectionStringName,
                                  true);
        }

        public async Task EditProduct(ProductModel product)
        {
            await _db.SaveData<dynamic>("SP_EditProduct",
                                  new { product.Id, product.Name, product.Amount, product.Info, product.CategoryId, product.Price },
                                  connectionStringName,
                                  true);
        }

        public async Task DeleteProduct(int Id)
        {
            await _db.SaveData<dynamic>("SP_DeleteProduct",
                                  new { Id },
                                  connectionStringName,
                                  true);
        }

        public async Task CreatePurchase(int userId, int productId)
        {
            await _db.SaveData<dynamic>("SP_CreateNewPurchase",
                                  new { userId, productId },
                                  connectionStringName,
                                  true);
        }

        public async Task UpdatePurchase(int id)
        {
            await _db.SaveData<dynamic>("SP_UpdatePurchase",
                                  new { id },
                                  connectionStringName,
                                  true);
        }

        public async Task UpdatePurchaseQuantity(int id, int quantity)
        {
            await _db.SaveData<dynamic>("SP_UpdatePurchaseQuantity",
                                  new { id, quantity },
                                  connectionStringName,
                                  true);
        }

        public async Task MakePurchaseComplete(int id)
        {
            await _db.SaveData<dynamic>("SP_MakePurchaseComplete",
                                  new { id },
                                  connectionStringName,
                                  true);
        }

        public async Task DeletePurchase(int id)
        {
            await _db.SaveData<dynamic>("SP_DeletePurchase",
                                  new { id },
                                  connectionStringName,
                                  true);
        }

        public async Task MarkPurchaseAsSent(int id)
        {
            await _db.SaveData("SP_MarkPurchaseAsSent", new { Id = id }, connectionStringName, true);
        }
    }
}

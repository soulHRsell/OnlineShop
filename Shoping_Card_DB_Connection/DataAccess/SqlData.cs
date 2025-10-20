using Shoping_Card_DB_Connection.Databases;
using Shoping_Card_DB_Connection.Models;

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

        public List<UserModel> GetUserById(int Id)
        {
            return _db.LoadData<UserModel, dynamic>("SP_GetUserById",
                                new { Id },
                                connectionStringName,
                                true);
        }

        public List<UserAuthoModel> GetUserByUsername(string username)
        {
            return _db.LoadData<UserAuthoModel, dynamic>("dbo.SP_GetUserByUsername",
                                                         new { username },
                                                         connectionStringName,
                                                         true);
        }

        public List<ProductModel> GetProducts()
        {
            return _db.LoadData<ProductModel, dynamic>("select * from ViewAllProducts ORDER BY [Name] ASC",
                                                        new { },
                                                        connectionStringName,
                                                        false);
        }

        public List<ProductModel> GetProductById(int Id)
        {
            return _db.LoadData<ProductModel, dynamic>("SP_GetProductById",
                                                       new { Id },
                                                       connectionStringName,
                                                       true);
        }

        public List<ProductModel> GetProductByName(string name)
        {
            return _db.LoadData<ProductModel, dynamic>("SP_GetProductByName",
                                                       new { name },
                                                       connectionStringName,
                                                       true);
        }

        public List<ProductModel> SearchProducts(string? name, int? categoryId, decimal? minPrice, decimal? maxPrice)
        {
            return _db.LoadData<ProductModel, dynamic>("SP_SearchProducts",
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

        public List<CategoryModel> GetAllCategories()
        {
            return _db.LoadData<CategoryModel, dynamic>("select * from ViewAllCategories ORDER BY [Name] ASC",
                                                        new { },
                                                        connectionStringName,
                                                        false);
        }

        public List<CategoryModel> GetCategoryById(int Id)
        {
            return _db.LoadData<CategoryModel, dynamic>("SP_GetCategoryById",
                                                        new { Id },
                                                        connectionStringName,
                                                        true);
        }

        public List<CategoryModel> GetCategoryByName(string name)
        {
            return _db.LoadData<CategoryModel, dynamic>("SP_GetCategoryByName",
                                                        new { name },
                                                        connectionStringName,
                                                        true);
        }

        public List<CategoryModel> SearchCategory(string? name)
        {
            return _db.LoadData<CategoryModel, dynamic>("SP_SearchCategory",
                                                        new { Name = String.IsNullOrEmpty(name) ? null : name },
                                                        connectionStringName,
                                                        true);
        }

        public List<PurchaseModel> GetPurchase(int userId, int productId)
        {
            return _db.LoadData<PurchaseModel, dynamic>("SP_GetPurchase",
                                                        new { userId, productId },
                                                        connectionStringName,
                                                        true);
        }

        public List<PurchaseModel> GetPuchaseById(int id)
        {
            return _db.LoadData<PurchaseModel, dynamic>("SP_GetPurchaseById",
                                                        new { id },
                                                        connectionStringName,
                                                        true);
        }

        public List<PurchaseModel> GetPurchaseByUserId(int userId)
        {
            return _db.LoadData<PurchaseModel, dynamic>("SP_GetPurchaseByUserId",
                                                        new { userId },
                                                        connectionStringName,
                                                        true);
        }

        public List<PurchaseModel> GetUncompletedPurchasesByUserId(int userId)
        {
            return _db.LoadData<PurchaseModel, dynamic>("SP_GetUncompletedPurchasesByUserId",
                                                        new { userId },
                                                        connectionStringName,
                                                        true);
        }

        public List<PurchaseModel> GetSpecificUncompletedPurchaseByUserId(int userId, int productId)
        {
            return _db.LoadData<PurchaseModel, dynamic>("SP_GetSpecificUncompletedPurchaseByUserId",
                                                        new { userId, productId },
                                                        connectionStringName,
                                                        true);
        }

        public List<PurchaseModel> GetCompletedAndSentPurchasesByUserId(int userId)
        {
            return _db.LoadData<PurchaseModel, dynamic>("SP_GetCompletedAndSentPurchasesByUserId",
                                                        new { userId },
                                                        connectionStringName,
                                                        true);
        }

        public List<PurchaseModel> AdminSearchPurchases(int? id, string? name, string? status)
        {
            return _db.LoadData<PurchaseModel, dynamic>(
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

        public List<PurchaseModel> SearchCompletedAndSentPurchasesByUserId(int? id, string? name, string? status, int userId)
        {
            return _db.LoadData<PurchaseModel, dynamic>("SP_SearchCompletedAndSentPurchasesByUserId",
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

        public void Createuser(UserModel user)
        {
            _db.SaveData<dynamic>("dbo.SP_CreateUser",
                                  new { user.Username, user.Password, user.EmailAddress, user.FirstName, user.LastName, user.Country, user.State, user.City, user.ZipCode, user.CardNumber },
                                  connectionStringName,
                                  true);
        }

        public void EditUserInfo(int Id, UserModel user)
        {
            _db.SaveData<dynamic>("SP_EditUserInfo",
                                  new { Id, user.Username, user.EmailAddress, user.FirstName, user.LastName, user.Country, user.State, user.City, user.ZipCode, user.CardNumber },
                                  connectionStringName,
                                  true);
        }

        public void CreateCategory(string name)
        {
            _db.SaveData<dynamic>("SP_CreateNewCategory",
                                  new { name },
                                  connectionStringName,
                                  true);
        }

        public void EditCategory(CategoryModel category)
        {
            _db.SaveData<dynamic>("SP_EditCategoryById",
                                  new { category.Id, category.Name },
                                  connectionStringName,
                                  true);
        }

        public void DeleteCategory(int Id)
        {
            _db.SaveData<dynamic>("SP_DeleteCategory",
                                  new { Id },
                                  connectionStringName,
                                  true);
        }

        public void CreateProduct(ProductModel product)
        {
            _db.SaveData<dynamic>("SP_CreateNewProduct",
                                  new { product.Name, product.Amount, product.Info, product.Price, product.CategoryId },
                                  connectionStringName,
                                  true);
        }

        public void EditProduct(ProductModel product)
        {
            _db.SaveData<dynamic>("SP_EditProduct",
                                  new { product.Id, product.Name, product.Amount, product.Info, product.CategoryId, product.Price },
                                  connectionStringName,
                                  true);
        }

        public void DeleteProduct(int Id)
        {
            _db.SaveData<dynamic>("SP_DeleteProduct",
                                  new { Id },
                                  connectionStringName,
                                  true);
        }

        public void CreatePurchase(int userId, int productId)
        {
            _db.SaveData<dynamic>("SP_CreateNewPurchase",
                                  new { userId, productId },
                                  connectionStringName,
                                  true);
        }

        public void UpdatePurchase(int id)
        {
            _db.SaveData<dynamic>("SP_UpdatePurchase",
                                  new { id },
                                  connectionStringName,
                                  true);
        }

        public void UpdatePurchaseQuantity(int id, int quantity)
        {
            _db.SaveData<dynamic>("SP_UpdatePurchaseQuantity",
                                  new { id, quantity },
                                  connectionStringName,
                                  true);
        }

        public void MakePurchaseComplete(int id)
        {
            _db.SaveData<dynamic>("SP_MakePurchaseComplete",
                                  new { id },
                                  connectionStringName,
                                  true);
        }

        public void DeletePurchase(int id)
        {
            _db.SaveData<dynamic>("SP_DeletePurchase",
                                  new { id },
                                  connectionStringName,
                                  true);
        }

        public void MarkPurchaseAsSent(int id)
        {
            _db.SaveData("SP_MarkPurchaseAsSent", new { Id = id }, connectionStringName, true);
        }
    }
}

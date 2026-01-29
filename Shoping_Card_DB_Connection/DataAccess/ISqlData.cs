using Shoping_Card_DB_Connection.Models;

namespace Shoping_Card_DB_Connection.DataAccess
{
    public interface ISqlData
    {
        Task<List<PurchaseModel>> AdminSearchPurchases(int? id, string? name, string? status);
        Task CreateCategory(string name);
        Task CreateProduct(ProductModel product);
        Task CreatePurchase(int userId, int productId);
        Task Createuser(UserModel user);
        Task DeleteCategory(int Id);
        Task DeleteProduct(int Id);
        Task DeletePurchase(int id);
        Task EditCategory(CategoryModel category);
        Task EditProduct(ProductModel product);
        Task EditUserInfo(int Id, UserModel user);
        Task<List<CategoryModel>> GetAllCategories();
        Task<List<CategoryModel>> GetCategoryById(int Id);
        Task<List<CategoryModel>> GetCategoryByName(string name);
        Task<List<PurchaseModel>> GetCompletedAndSentPurchasesByUserId(int userId);
        Task<List<ProductModel>> GetProductById(int Id);
        Task<List<ProductModel>> GetProductByName(string name);
        Task<List<ProductModel>> GetProducts();
        Task<List<PurchaseModel>> GetPuchaseById(int id);
        Task<List<PurchaseModel>> GetPurchase(int userId, int productId);
        Task<List<PurchaseModel>> GetPurchaseByUserId(int userId);
        Task<List<PurchaseModel>> GetSpecificUncompletedPurchaseByUserId(int userId, int productId);
        Task<List<PurchaseModel>> GetUncompletedPurchasesByUserId(int userId);
        Task<List<UserModel>> GetUserById(int Id);
        Task<List<UserAuthoModel>> GetUserByUsername(string username);
        Task MakePurchaseComplete(int id);
        Task MarkPurchaseAsSent(int id);
        Task<List<CategoryModel>> SearchCategory(string? name);
        Task<List<PurchaseModel>> SearchCompletedAndSentPurchasesByUserId(int? id, string? name, string? status, int userId);
        Task<List<ProductModel>> SearchProducts(string? name, int? categoryId, decimal? minPrice, decimal? maxPrice);
        Task UpdatePurchase(int id);
        Task UpdatePurchaseQuantity(int id, int quantity);
    }
}
using Shoping_Card_DB_Connection.Models;

namespace Shoping_Card_DB_Connection.DataAccess
{
    public interface ISqlData
    {
        void CreateCategory(string name);
        void CreateProduct(ProductModel product);
        void CreatePurchase(int userId, int productId);
        void Createuser(UserModel user);
        void DeleteCategory(int Id);
        void DeleteProduct(int Id);
        void DeletePurchase(int id);
        void EditCategory(CategoryModel category);
        void EditProduct(ProductModel product);
        void EditUserInfo(int Id, UserModel user);
        List<CategoryModel> GetAllCategories();
        List<CategoryModel> GetCategoryById(int Id);
        List<CategoryModel> GetCategoryByName(string name);
        List<PurchaseModel> GetCompletedAndSentPurchasesByUserId(int userId);
        List<ProductModel> GetProductById(int Id);
        List<ProductModel> GetProductByName(string name);
        List<ProductModel> GetProducts();
        List<PurchaseModel> GetPuchaseById(int id);
        List<PurchaseModel> GetPurchase(int userId, int productId);
        List<PurchaseModel> GetPurchaseByUserId(int userId);
        List<PurchaseModel> GetUncompletedPurchasesByUserId(int userId);
        List<UserModel> GetUserById(int Id);
        List<UserAuthoModel> GetUserByUsername(string username);
        void MakePurchaseComplete(int id);
        List<CategoryModel> SearchCategory(string? name);
        List<PurchaseModel> SearchCompletedAndSentPurchasesByUserId(int? id, string? name, string? status, int userId);
        List<ProductModel> SearchProducts(string? name, int? categoryId, decimal? minPrice, decimal? maxPrice);
        void UpdatePurchase(int id);
        void UpdatePurchaseQuantity(int id, int quantity);
    }
}
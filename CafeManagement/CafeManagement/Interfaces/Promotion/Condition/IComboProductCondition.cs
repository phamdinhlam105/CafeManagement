using CafeManagement.Models;

namespace CafeManagement.Interfaces.Promotion.Condition
{
    public interface IComboProductCondition
    {
        void GetComboProductCondition(List<Product> productlist);
    }
}

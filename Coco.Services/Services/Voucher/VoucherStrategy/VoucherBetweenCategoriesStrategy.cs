using Coco.Core.Commons;
using Coco.Core.Entities;

namespace Coco.Services.Services.Voucher.VoucherStrategy
{
    public class VoucherBetweenCategoriesStrategy : IVoucherStrategy
    {
        List<string> Discounts = new List<string>();
        List<string> CategoriesOrProducts = new List<string>();
        public VirtualCart Execute(VoucherConcrete voucherConcrete, VirtualCart virtualCart)
        {
            Discounts = GetKeys(voucherConcrete.DiscountStrategy);
            CategoriesOrProducts = GetKeys(voucherConcrete.DiscountProductsOrCategories);

            decimal percentage = decimal.Parse(Discounts[0]);
            string firstCategory = CategoriesOrProducts[0];
            string secondCategory = CategoriesOrProducts[1];

            var products = virtualCart.VirtualProductCarts.Select(x => x.Product).ToList();

            var productsCategory = products.Where(x => x.Categories.Any(x => x.Description.Contains(firstCategory) || x.Description.Contains(secondCategory))).ToList();

            if (productsCategory.Any())
            {
                virtualCart.DiscountAmount = 0;
                foreach (var productDiscount in productsCategory)
                {
                    var prod = virtualCart.VirtualProductCarts.First(x => x.Product.Code == productDiscount.Code);

                    prod.DiscountAmount = decimal.Round((decimal)((percentage * prod.Product.Amount.Value) / 100), 2);
                    virtualCart.DiscountAmount += prod.DiscountAmount;
                }
            }
            virtualCart.DiscountAmount = (virtualCart.TotalAmount - virtualCart.DiscountAmount);
            return virtualCart;
        }

        private List<string> GetKeys(string value)
        {
            var keys = new List<string>();
            foreach (var word in value.Split(" "))
            {
                var key = Helper.GetSubString(word, "[", "]");
                if (string.IsNullOrEmpty(key) is false)
                {
                    keys.Add(key);
                }
            }
            return keys;
        }
    }
}

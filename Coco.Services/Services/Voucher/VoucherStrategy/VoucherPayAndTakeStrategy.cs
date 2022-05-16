using Coco.Core.Commons;
using Coco.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coco.Services.Services.Voucher.VoucherStrategy
{
    public class VoucherPayAndTakeStrategy : IVoucherStrategy
    {

        List<string> Discounts = new List<string>();
        List<string> CategoriesOrProducts = new List<string>();
        public VirtualCart Execute(VoucherConcrete voucherConcrete, VirtualCart virtualCart)
        {
            Discounts = GetKeys(voucherConcrete.DiscountStrategy);
            CategoriesOrProducts = GetKeys(voucherConcrete.DiscountProductsOrCategories);

            int pay = Convert.ToInt32(Discounts[0]);
            int take = Convert.ToInt32(Discounts[1]);
            string product = CategoriesOrProducts[0];
            string units = CategoriesOrProducts[1];

            var products = virtualCart.VirtualProductCarts.Where(x => x.Product.Description.Trim().Contains(product.Trim())).Select(x => x.Product).ToList();

            if (products.Any())
            {
                virtualCart.DiscountAmount = 0;
                int limit = Convert.ToInt32(units);

                var prod = virtualCart.VirtualProductCarts.First(x => x.Product.Code == products.First().Code);

                int quantity = prod.Quantity.Value > limit ? limit : prod.Quantity.Value;

                prod.DiscountAmount = decimal.Round((((quantity / take) * pay) * prod.Product.Amount.Value), 2);

                virtualCart.DiscountAmount += prod.DiscountAmount;
                virtualCart.DiscountAmount = (virtualCart.TotalAmount - virtualCart.DiscountAmount);
            }

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

using Coco.Core.Entities;
using Coco.Core.Enums;
using Coco.Services.Services.Voucher.VoucherStrategy;

namespace Coco.Services.Services.Voucher
{
    public class VoucherStrategyContext
    {
        private IVoucherStrategy _voucherStrategy;
        public VirtualCart Execute(VoucherConcrete voucherConcrete, VirtualCart virtualCart)
        {
            switch ((VoucherConcreteStrategy)Enum.Parse(typeof(VoucherConcreteStrategy), voucherConcrete.VoucherStrategy.CodeStrategy))
            {
                case VoucherConcreteStrategy.STRATEGY01:
                    _voucherStrategy = new VoucherBetweenDatesStrategy();
                    break;
                case VoucherConcreteStrategy.STRATEGY02:
                    _voucherStrategy = new VoucherPayAndTakeStrategy();
                    break;
                case VoucherConcreteStrategy.STRATEGY03:
                    _voucherStrategy = new VoucherBetweenCategoriesStrategy();
                    break;
                case VoucherConcreteStrategy.STRATEGY04:
                    //_voucherStrategy = new ;
                    break;
                case VoucherConcreteStrategy.STRATEGY05:
                    //_voucherStrategy = new ;
                    break;
                default:
                    break;
            }

            return _voucherStrategy.Execute(voucherConcrete, virtualCart);
        }
    }
}

using Coco.Core.Entities;
using Coco.Core.Interfaces;
using Coco.Infraestructure.Commons;
using Coco.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coco.Services.Services.Voucher
{
    public class VoucherService : IVoucherService
    {
        private readonly IRepository<Coco.Core.Entities.Voucher> _voucherRepository;
        public VoucherService(IRepository<Coco.Core.Entities.Voucher> voucherRepository)
        {
            _voucherRepository = voucherRepository;
        }

        public async Task<Result<Coco.Core.Entities.Voucher>> GetVoucherByCode(string code)
        {
            var voucher = await _voucherRepository.GetFirstAsync(x => x.Code.Trim() == code.Trim());

            if (voucher == null)
                return Result.Failed<Coco.Core.Entities.Voucher>("Voucher is not found");

            return Result.Success<Coco.Core.Entities.Voucher>(voucher);
        }
    }
}

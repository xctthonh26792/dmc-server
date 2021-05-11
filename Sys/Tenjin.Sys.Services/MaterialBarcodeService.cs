using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Tenjin.Services;
using Tenjin.Sys.Contracts.Interfaces;
using Tenjin.Sys.Models.Entities;
using Tenjin.Sys.Services.Interfaces;

namespace Tenjin.Sys.Services
{
    public class MaterialBarcodeService : BaseService<MaterialBarcode>, IMaterialBarcodeService
    {
        private readonly ISysContext _context;
        public MaterialBarcodeService(ISysContext context) : base(context.MaterialBarcodeRepository)
        {
            _context = context;
        }

        public async Task<string> GetBarcode(string @ref)
        {
            if (!string.IsNullOrEmpty(@ref))
            {
                var entity = await _context.MaterialBarcodeRepository.GetSingleByExpression(x => x.MaterialCode == @ref && string.IsNullOrEmpty(x.ReceiptCode));
                if (entity != null)
                {
                    return entity.Barcode;
                }
            }
            var barcode = $"{DateTime.Today:yyyyMMdd}{Guid.NewGuid():N}";
            var model = new MaterialBarcode
            {
                MaterialCode = @ref ?? string.Empty,
                ReceiptCode = string.Empty,
                Barcode = barcode,
                CreatedDate = DateTime.Now,
                LastModified = DateTime.Now
            };
            await _context.MaterialBarcodeRepository.Add(model);
            return barcode;
        }

        public async Task<IEnumerable<string>> GetBarcode(string @ref, string receipt, int count)
        {
            var entity = _context.MaterialRepository.GetSingleByExpression(x => x.Code == @ref);
            if (entity == null)
            {
                return new List<string>();
            }
            var barcodes = new List<string>();
            var models = new List<MaterialBarcode>();
            for (var i = 0; i < count; i++)
            {
                var barcode = $"{DateTime.Today:yyyyMMdd}{Guid.NewGuid():N}";
                var model = new MaterialBarcode
                {
                    MaterialCode = @ref ?? string.Empty,
                    ReceiptCode = string.Empty,
                    Barcode = barcode,
                    CreatedDate = DateTime.Now,
                    LastModified = DateTime.Now
                };
                barcodes.Add(barcode);
                models.Add(model);
            }
            await _context.MaterialBarcodeRepository.AddMany(models);
            return barcodes;
        }
    }
}

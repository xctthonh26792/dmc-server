using Tenjin.Contracts;
using Tenjin.Contracts.Interfaces;
using Tenjin.Sys.Contracts.Interfaces;
using Tenjin.Sys.Models.Entities;

namespace Tenjin.Sys.Contracts
{
    public class SysContext : BaseContext, ISysContext
    {
        public SysContext(ISysDbBuilder builder) : base(builder)
        {
        }

        public IRepository<User> UserRepository => ResolveRepository<User>();

        public IRepository<Employee> EmployeeRepository => ResolveRepository<Employee>();

        public IRepository<Log> LogRepository => ResolveRepository<Log>();

        public IRepository<CodeGenerate> CodeGenerateRepository => ResolveRepository<CodeGenerate>();

        // systems
        public IRepository<Customer> CustomerRepository => ResolveRepository<Customer>();
        public IRepository<Supplier> SupplierRepository => ResolveRepository<Supplier>();
        public IRepository<Company> CompanyRepository => ResolveRepository<Company>();
        public IRepository<Department> DepartmentRepository => ResolveRepository<Department>();
        public IRepository<JobTitle> JobTitleRepository => ResolveRepository<JobTitle>();
        public IRepository<JobPosition> JobPositionRepository => ResolveRepository<JobPosition>();
        // materials
        public IRepository<MaterialGroup> MaterialGroupRepository => ResolveRepository<MaterialGroup>();
        public IRepository<MaterialSubgroup> MaterialSubgroupRepository => ResolveRepository<MaterialSubgroup>();
        public IRepository<Material> MaterialRepository => ResolveRepository<Material>();
        public IRepository<MaterialBarcode> MaterialBarcodeRepository => ResolveRepository<MaterialBarcode>();
        public IRepository<MaterialReceivingVoucher> MaterialReceivingVoucherRepository => ResolveRepository<MaterialReceivingVoucher>();
        public IRepository<MaterialDeliveryVoucher> MaterialDeliveryVoucherRepository => ResolveRepository<MaterialDeliveryVoucher>();
        public IRepository<Unit> UnitRepository => ResolveRepository<Unit>();
        // products
        public IRepository<ProductGroup> ProductGroupRepository => ResolveRepository<ProductGroup>();
        public IRepository<ProductSubgroup> ProductSubgroupRepository => ResolveRepository<ProductSubgroup>();
        public IRepository<Product> ProductRepository => ResolveRepository<Product>();
        public IRepository<ProductBarcode> ProductBarcodeRepository => ResolveRepository<ProductBarcode>();


        public override IRepository<T> ResolveRepository<T>()
        {
            return new SysRepository<T>(GetDatabase());
        }
    }
}

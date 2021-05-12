using Tenjin.Contracts.Interfaces;
using Tenjin.Sys.Models.Entities;

namespace Tenjin.Sys.Contracts.Interfaces
{
    public interface ISysContext : IContext
    {
        IRepository<User> UserRepository { get; }
        IRepository<Employee> EmployeeRepository { get; }
        IRepository<CodeGenerate> CodeGenerateRepository { get; }
        IRepository<Log> LogRepository { get; }

        // systems
        IRepository<Customer> CustomerRepository { get; }
        IRepository<Supplier> SupplierRepository { get; }
        IRepository<Company> CompanyRepository { get; }
        IRepository<Department> DepartmentRepository { get; }
        IRepository<JobTitle> JobTitleRepository { get; }
        IRepository<JobPosition> JobPositionRepository { get; }

        // materials
        IRepository<MaterialGroup> MaterialGroupRepository { get; }
        IRepository<MaterialSubgroup> MaterialSubgroupRepository { get; }
        IRepository<MaterialGroupType> MaterialGroupTypeRepository { get; }
        IRepository<Material> MaterialRepository { get; }
        IRepository<MaterialBarcode> MaterialBarcodeRepository { get; }
        IRepository<Unit> UnitRepository { get; }

        IRepository<MaterialReceivingVoucher> MaterialReceivingVoucherRepository { get; }
        IRepository<MaterialDeliveryVoucher> MaterialDeliveryVoucherRepository { get; }

        // products
        IRepository<ProductGroup> ProductGroupRepository { get; }
        IRepository<ProductSubgroup> ProductSubgroupRepository { get; }
        IRepository<Product> ProductRepository { get; }
        IRepository<ProductBarcode> ProductBarcodeRepository { get; }
    }
}

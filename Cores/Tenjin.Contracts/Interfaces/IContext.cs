using Tenjin.Models.Entities;

namespace Tenjin.Contracts.Interfaces
{
    public interface IContext
    {
        IRepository<CodeGenerate> CodeGenerateRepository { get; }
        IRepository<T> ResolveRepository<T>() where T : BaseEntity;
    }
}

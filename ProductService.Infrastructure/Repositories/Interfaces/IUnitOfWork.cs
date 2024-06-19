namespace ProductService.Infrastructure.Repositories.Interfaces
{
    public interface IUnitOfWork
    {
        IProductRepository Product { get; }

        Task SaveAsync();
    }
}
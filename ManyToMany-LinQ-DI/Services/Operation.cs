namespace ManyToMany.Services
{
    public interface IOperation { string Id { get; } }
    public interface IOperationTransient : IOperation { }
    public interface IOperationScoped : IOperation { }
    public interface IOperationSingleton : IOperation { }

    public class Operation :
        IOperationTransient, IOperationScoped, IOperationSingleton
    {
        public string Id { get; } = Guid.NewGuid().ToString("N");
    }
}

namespace Restuarant_Management.Interfaces
{
    public interface IOperation
    {
        Guid Id { get; }
    }

    public interface IOperationTransient : IOperation { }
    public interface IOperationScoped : IOperation { }
    public interface IOperationSingleton : IOperation { }

    public class Operation : IOperationTransient, IOperationScoped, IOperationSingleton
    {
        public Guid Id { get; } = Guid.NewGuid();
    }
}
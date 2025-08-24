using System;
using Restuarant_Management.Interfaces;

namespace Restuarant_Management.Services
{
    // Implements all three lifetimes
    public class Operation : IOperationTransient, IOperationScoped, IOperationSingleton
    {
        public Guid Id { get; } = Guid.NewGuid();
    }
}

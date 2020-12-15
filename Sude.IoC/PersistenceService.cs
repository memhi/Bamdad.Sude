using Microsoft.Extensions.DependencyInjection.Extensions;
using Sude.Domain.Interfaces;
using Sude.Persistence.Repository;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class PersistenceService
    {
        public static IServiceCollection AddPersistenceService(this IServiceCollection services)
        {
            services.TryAddEnumerable(new ServiceDescriptor[]
            {
                ServiceDescriptor.Scoped<IUserRepository, UserRepository>(),
                ServiceDescriptor.Scoped<IServingRepository, ServingRepository>(),
                ServiceDescriptor.Scoped<IWorkTypeRepository, WorkTypeRepository>(),
                ServiceDescriptor.Scoped<IWorkRepository, WorkRepository>(),
                ServiceDescriptor.Scoped<IInventoryTypeRepository, InventoryTypeRepository>(),
                ServiceDescriptor.Scoped<IServingInventoryRepository, ServingInventoryRepository>(),
                    ServiceDescriptor.Scoped<IServingInventoryTrackingRepository, ServingInventoryTrackingRepository>(),
                ServiceDescriptor.Scoped<IOrderRepository, OrderRepository>(),
                ServiceDescriptor.Scoped<IOrderDetailRepository, OrderDetailRepository>(),
                     ServiceDescriptor.Scoped<IOrderNumberRepository, OrderNumberRepository>(),
                  ServiceDescriptor.Scoped<ICustomerRepository, CustomerRepository>(),
                    ServiceDescriptor.Scoped<IAddressRepository, AddressRepository>(),
                      ServiceDescriptor.Scoped<IBlogRepository, BlogRepository>(),
                        ServiceDescriptor.Scoped<IBlogCommentRepository, BlogCommentRepository>(),
                          ServiceDescriptor.Scoped<INewsRepository, NewsRepository>(),
                            ServiceDescriptor.Scoped<INewsCommentRepository, NewsCommentRepository>()



            });

            return services;
        }
    }
}

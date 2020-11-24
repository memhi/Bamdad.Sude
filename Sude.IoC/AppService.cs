using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Sude.Application.Interfaces;
using Sude.Application.Services;
using Sude.Persistence.Repository;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class AppService
    {
        public static IServiceCollection AddAppService(this IServiceCollection services)
        {
            services.TryAddEnumerable(new ServiceDescriptor[]
            {
                ServiceDescriptor.Scoped<IUserService, UserService>(),
                ServiceDescriptor.Scoped<IServingService,ServingService>(),
                ServiceDescriptor.Scoped<IWorkTypeService,WorkTypeService>(),
                ServiceDescriptor.Scoped<IWorkService,WorkService>(),
                ServiceDescriptor.Scoped<IInventoryTypeService,InventoryTypeService>(),
                ServiceDescriptor.Scoped<IServingInventoryService,ServingInventoryService>(),
                ServiceDescriptor.Scoped<IOrderService,OrderService>(),
                ServiceDescriptor.Scoped<IOrderDetailService,OrderDetailService>(),
                  ServiceDescriptor.Scoped<ICustomerService,CustomerService>(),
                             ServiceDescriptor.Scoped<IAddressService,AddressService>(),
                                    ServiceDescriptor.Scoped<IBlogService,BlogService>(),
                             ServiceDescriptor.Scoped<INewsService,NewsService>(),
                                  ServiceDescriptor.Scoped<IBlogCommentService,BlogCommentService>(),
                             ServiceDescriptor.Scoped<INewsCommentService,NewsCommentService>()
            });

            return services;
        }
    }
}

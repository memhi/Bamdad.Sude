using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Sude.Application.Interfaces;
using Sude.Application.Services;

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
                ServiceDescriptor.Scoped<IWorkTypeService,WorkTypeService>()
            });

            return services;
        }
    }
}

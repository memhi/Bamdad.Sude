using Sude.Application.Interfaces;
using Sude.Application.Services;
using Sude.Domain.Interfaces;
using Sude.Persistence.Repository;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sude.IoC
{
    public class DependencyContainer
    {
        public static void RegisterServices(IServiceCollection service)
        {
            /*//Application Layer
            service.AddScoped<IUserService, UserService>();

            //Infra Persistence Layer
            service.AddScoped<IUserRepository,UserRepository>();*/

            service.AddAppService().AddPersistenceService();

        }
    }
}

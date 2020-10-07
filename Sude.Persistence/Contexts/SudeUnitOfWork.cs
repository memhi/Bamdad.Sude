using Sude.Domain.Interfaces;
using Sude.Persistence.Repository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sude.Persistence.Contexts
{
    public class SudeUnitOfWork : IDisposable
    {
        private SudeDBContext _ctx;
        private IUserRepository _userRepository;
        //private IRoleRepository _roleRepository;
        public SudeUnitOfWork(DbContextOptions<SudeDBContext> options)
        {
            _ctx = new SudeDBContext(options);
        }

        public IUserRepository UserRepository
        {
            get
            {
                if (_userRepository == null)
                    _userRepository = new UserRepository(_ctx);


                return _userRepository;
            }
        }

       


        public void Dispose()
        {
            _ctx.Dispose();
        }
    }
}

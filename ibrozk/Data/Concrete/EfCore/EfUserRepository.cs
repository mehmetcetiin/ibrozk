using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ibrozk.Data.Abstract;
using ibrozk.Data.Concrete.EfCore;
using ibrozk.Entity;

namespace ibrozk.Data.Concrete
{
    public class EfUserRepository : IUserRepository
    {
        private ImageContext _context;

        public EfUserRepository(ImageContext context)
        {
            _context = context;
        }

        public IQueryable<User> Users => _context.Users;

        public void CreateUser(User user)
        {
            _context.Users.Add(user);
            _context.SaveChanges();
        }
    }
}
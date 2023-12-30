using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ibrozk.Data.Abstract;
using ibrozk.Data.Concrete.EfCore;
using ibrozk.Entity;
using Microsoft.EntityFrameworkCore;

namespace ibrozk.Data.Concrete
{
    public class EfPostRepository : IPostRepository
    {
        private ImageContext _context;

        public EfPostRepository(ImageContext context)
        {
            _context = context;
        }

        public IQueryable<Post> Posts => _context.Posts;

        public void CreatePost(Post post)
        {
            _context.Posts.Add(post);
            _context.SaveChanges();
        }

        public void DeletePost(Post post)
        {
            _context.Posts.Remove(post);
            _context.SaveChanges();
        }



    }
}
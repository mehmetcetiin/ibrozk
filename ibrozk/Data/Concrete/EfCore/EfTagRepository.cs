using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ibrozk.Data.Abstract;
using ibrozk.Data.Concrete.EfCore;
using ibrozk.Entity;

namespace ibrozk.Data.Concrete
{
    public class EfTagRepository : ITagRepository
    {
        private ImageContext _context;

        public EfTagRepository(ImageContext context)
        {
            _context = context;
        }

        public IQueryable<Tag> Tags => _context.Tags;

        public void CreateTag(Tag tag)
        {
            _context.Tags.Add(tag);
            _context.SaveChanges();
        }

        public void DeleteTag(Tag tag)
        {
            _context.Tags.Remove(tag);
            _context.SaveChanges();
        }
    }
}
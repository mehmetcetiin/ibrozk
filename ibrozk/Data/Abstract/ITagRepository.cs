using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ibrozk.Entity;


namespace ibrozk.Data.Abstract
{
    public interface ITagRepository
    {
        IQueryable<Tag> Tags { get; }

        void CreateTag(Tag tag);

        void DeleteTag(Tag tag);

    }
}
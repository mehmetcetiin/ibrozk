using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ibrozk.Entity;


namespace ibrozk.Data.Abstract
{
    public interface IPostRepository
    {
        IQueryable<Post> Posts { get; }

        void CreatePost(Post post);

        void DeletePost(Post post);


    }
}
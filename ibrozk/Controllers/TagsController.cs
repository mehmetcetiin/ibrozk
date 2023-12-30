using System.Linq;
using ibrozk.Data.Abstract;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ibrozk.Controllers
{
    [Authorize]
    public class TagsController : Controller
    {
        private readonly IPostRepository _postRepository;
        private readonly ITagRepository _tagRepository;
        private readonly IWebHostEnvironment _env;

        public TagsController(IPostRepository postRepository, ITagRepository tagRepository, IWebHostEnvironment env)
        {
            _postRepository = postRepository;
            _tagRepository = tagRepository;
            _env = env;
        }

        public IActionResult Index()
        {
            var tags = _tagRepository.Tags.ToList();

            return View(tags);
        }

        [HttpPost]
        public IActionResult Delete(int tagId)
        {
            var tagToDelete = _tagRepository.Tags.FirstOrDefault(t => t.TagId == tagId);

            if (tagToDelete != null)
            {
                // Etiketin bağlı olduğu tüm gönderileri al
                var postsWithDeletedTag = _postRepository.Posts.Where(p => p.Tags.Any(t => t.TagId == tagId)).ToList();

                foreach (var post in postsWithDeletedTag)
                {
                    // Gönderiyi silebilirsiniz.
                    _postRepository.DeletePost(post);

                    // Fotoğraf dosyasını silebilirsiniz.
                    var filePath = Path.Combine(_env.WebRootPath ?? string.Empty, "photos", post.Urlname ?? string.Empty);
                    System.IO.File.Delete(filePath);
                }

                // Etiketi silebilirsiniz.
                _tagRepository.DeleteTag(tagToDelete);
            }

            // Daha sonra Index sayfasına yönlendirin
            return RedirectToAction("Index", "Tags");
        }

    }
}

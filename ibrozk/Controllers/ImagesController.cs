using System.Security.Claims;
using ibrozk.Data.Abstract;
using ibrozk.Data.Concrete.EfCore;
using ibrozk.Entity;
using ibrozk.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace ibrozk.Controllers
{
    public class ImagesController : Controller
    {
        private IPostRepository _postRepository;
        private readonly IWebHostEnvironment _env;


        public ImagesController(IWebHostEnvironment env, IPostRepository postRepository)
        {
            _env = env;
            _postRepository = postRepository;
        }
        public async Task<IActionResult> Index(string tag)
        {
            var claims = User.Claims;
            var posts = _postRepository.Posts;

            if (!string.IsNullOrEmpty(tag))
            {
                posts = posts.Where(x => x.Tags.Any(t => t.Tagname == tag));
            }

            return View(
                new ImagesViewModel { Posts = await posts.ToListAsync() });
        }

        [Authorize]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(ImageCreateViewModel model)
        {
            if (ModelState.IsValid)
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

                // Photo null değilse işlemleri gerçekleştir
                if (model.Photo != null)
                {
                    var uploadDir = Path.Combine(_env.WebRootPath, "photos");

                    // Eğer dizin yoksa oluştur
                    if (!Directory.Exists(uploadDir))
                    {
                        Directory.CreateDirectory(uploadDir);
                    }

                    // Dosya adını benzersiz bir şekilde oluştur
                    var fileName = Path.Combine(uploadDir, model.Photo.FileName);

                    // Dosyayı kaydet
                    var filePath = Path.Combine(uploadDir, fileName);
                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        model.Photo.CopyTo(fileStream);
                    }
                    // Yeni bir Tag listesi oluştur
                    List<Tag> tagList = new List<Tag>();

                    // model.Tag'i kullanarak yeni bir Tag nesnesi oluştur ve listeye ekle
                    Tag newTag = new Tag
                    {
                        Tagname = model.Tag
                    };
                    tagList.Add(newTag);

                    _postRepository.CreatePost(
                        new Post
                        {
                            Urlname = model.Photo.FileName,
                            Column = model.SutunNo,
                            Tags = tagList // Tag listesini kullanarak atama yap
                        }
                    );
                    return RedirectToAction("Index");
                }
                else
                {
                    // Photo null ise gerekli önlemi al
                    ModelState.AddModelError("Photo", "Bir fotoğraf seçilmedi.");
                }
            }

            return View(model);
        }


        [Authorize]
        public async Task<IActionResult> List()
        {


            var posts = _postRepository.Posts;

            return View(await posts.ToListAsync());
        }

        [Authorize]
        public IActionResult Delete(int id)
        {
            var post = _postRepository.Posts.FirstOrDefault(p => p.PostId == id);
            if (post == null)
            {
                return NotFound();
            }

            return View(post);
        }

        [Authorize]
        public IActionResult DeleteConfirmed(int id)
        {
            var post = _postRepository.Posts.FirstOrDefault(p => p.PostId == id);
            if (post == null)
            {
                return NotFound();
            }


            // Fotoğraf dosyasını sil (burada hata işleme eklemek isteyebilirsiniz)
            var filePath = Path.Combine(_env.WebRootPath ?? string.Empty, "photos", post?.Urlname ?? string.Empty);


            System.IO.File.Delete(filePath);

            // Gönderiyi depodan sil
            if (post != null)
            {
                _postRepository.DeletePost(post);
            }

            return RedirectToAction("List");
        }


        [Authorize]
        public IActionResult CreateByList()
        {
            return View();
        }
        [HttpPost]
        public IActionResult CreateByList(List<IFormFile> Photo, ImageCreateViewModel model)
        {
            if (ModelState.IsValid)
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

                // Photo null değilse işlemleri gerçekleştir
                if (Photo != null && Photo.Count > 0)
                {
                    var uploadDir = Path.Combine(_env.WebRootPath, "photos");

                    // Eğer dizin yoksa oluştur
                    if (!Directory.Exists(uploadDir))
                    {
                        Directory.CreateDirectory(uploadDir);
                    }

                    // Her dosya için işlemleri gerçekleştir
                    foreach (var file in Photo)
                    {
                        // Dosya adını benzersiz bir şekilde oluştur
                        var fileName = Path.Combine(uploadDir, file.FileName);

                        // Dosyayı kaydet
                        var filePath = Path.Combine(uploadDir, fileName);
                        using (var fileStream = new FileStream(filePath, FileMode.Create))
                        {
                            file.CopyTo(fileStream);
                        }

                        // Yeni bir Tag listesi oluştur
                        List<Tag> tagList = new List<Tag>();

                        // model.Tag'i kullanarak yeni bir Tag nesnesi oluştur ve listeye ekle
                        Tag newTag = new Tag
                        {
                            Tagname = model.Tag
                        };
                        tagList.Add(newTag);

                        _postRepository.CreatePost(
                            new Post
                            {
                                Urlname = file.FileName,
                                Column = model.SutunNo,
                                Tags = tagList // Tag listesini kullanarak atama yap
                            }
                        );
                    }

                    return RedirectToAction("Index");
                }
                else
                {
                    // Photo null ise gerekli önlemi al
                    ModelState.AddModelError("Photo", "En az bir fotoğraf seçmelisiniz.");
                }
            }

            return View(model);
        }

    }
}
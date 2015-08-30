using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using PhotoSharingApplication1.Models;
using System.Globalization;
using PhotoSharingApplication1.Models;


namespace PhotoSharingApplication1.Controllers
{
    [ValueReporter]
 //   [HandleError(View= "Error")]
    public class PhotoController : Controller
    {
      //  private PhotoSharingContext db = new PhotoSharingContext();
        private IPhotoSharingContext db;

        public PhotoController()
        {
            db = new PhotoSharingContext();
        }

        public PhotoController(IPhotoSharingContext Context)
        {
            db = Context;
        }
        //
        // GET: /Photo/
       // [OutputCache(Duration=600, Location=OutputCacheLocation.Server, VaryByParam="none")]
        public ActionResult Index()
        {
            return View(db.Photos.ToList());
            //return View("Index");
        }


        [ChildActionOnly]
        public ActionResult _PhotoGallery(int number = 0)
        {
            List<Photo> photos;
            if (number == 0)
            {
                photos = db.Photos.ToList();
            }
            else
            {
                photos = (from p in db.Photos
                          orderby p.CreatedDate descending
                          select p).Take(number).ToList();
            }
            return PartialView("_PhotoGallery", photos);
        }

        public ActionResult Display(int id=1)
        {
            Photo photo = db.FindPhotoById(id);
            if(photo == null)
            {
                return HttpNotFound();
            }
            return View("Display", photo);
        }


        public ActionResult DisplayByTitle(string title)
        {
           
            Photo photo = db.FindPhotoById(1);
            if(photo == null)
            {
                return HttpNotFound();
            }
            return View("Display", photo);

        }
        //
        // GET: /Photo/Details/5

        public ActionResult Details(int id = 0)
        {
            Photo photo = db.FindPhotoById(id);
            if (photo == null)
            {
                return HttpNotFound();
            }
            return View(photo);
        }

        //
        // GET: /Photo/Create

        public ActionResult Create()
        {
           // return View();
            Photo newPhoto = new Photo();
            newPhoto.CreatedDate = DateTime.Today;
            return View("Create", newPhoto);
        }

        //
        // POST: /Photo/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Photo photo, HttpPostedFileBase image)
        {
            photo.CreatedDate = DateTime.Today;
            if (ModelState.IsValid)

            {
                if(image != null)
                {
                    photo.ImageMimeType = image.ContentType;
                    photo.PhotoFile = new byte[image.ContentLength];
                    image.InputStream.Read(photo.PhotoFile, 0, image.ContentLength);
                    string stringlonglat = CheckLocaiton("India");
                    if(stringlonglat.StartsWith("Success"))
                    {
                        char[] splitChars = { ':' };
                        string[] coordinates = stringlonglat.Split(splitChars);
                        
                    }

                }
                db.Add<Photo>(photo);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View("Create", photo);
        }

        //
        // GET: /Photo/Edit/5

        public ActionResult Edit(int id = 0)
        {
            Photo photo = db.FindPhotoById(id);
            if (photo == null)
            {
                return HttpNotFound();
            }
            return View(photo);
        }

        //
        // POST: /Photo/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Photo photo)
        {
            if (ModelState.IsValid)
            {
              //  db.Entry(photo).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(photo);
        }

        //
        // GET: /Photo/Delete/5

        public ActionResult Delete(int id = 0)
        {
            Photo photo = db.FindPhotoById(id);
            if (photo == null)
            {
                return HttpNotFound();
            }
            return View(photo);
        }

        //
        // POST: /Photo/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Photo photo = db.FindPhotoById(id);
            db.Delete<Photo>(photo);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        [OutputCache(Duration=600, Location=OutputCacheLocation.Server, VaryByParam="id")]
        public FileContentResult GetImage(int id)
        {
            Photo photo = db.FindPhotoById(id);

            if(photo != null)
            {
                return File(photo.PhotoFile, photo.ImageMimeType);
            }
            else
            {
                return null;
            }
        }

        public ActionResult SlideShow()
        {

          //  throw new NotImplementedException("The Slide show action is not yet ready");
            return View("SlideShow", db.Photos.ToList());
        }


        public ActionResult FavoritesSlideShow()
        {

            List<Photo> favPhotos = new List<Photo>();
            List<int> favoriteIds = Session["Favorites"] as List<int>;
            if(favoriteIds == null)
            {
                favoriteIds = new List<int>();
                Photo currentPhoto;
                foreach(int CurrentId in favoriteIds)
                {
                    currentPhoto = db.FindPhotoById(CurrentId);
                    if(currentPhoto != null)
                    {

                        favPhotos.Add(currentPhoto);
                    }
                }

            }
            return View("SlideShow", favPhotos);
        }

        public ContentResult AddFavourite(int PhotoId)
        {
            List<int> favoriteIds = Session["Favorites"] as List<int>;
            if(favoriteIds == null)
            {
                favoriteIds = new List<int>();
                favoriteIds.Add(PhotoId);
                Session["Favorites"] = favoriteIds;

            }
            return Content("The Picture has been added to your favorites", "text/plain", System.Text.Encoding.Default);
        }

        private string CheckLocaiton(string location)
        {
            PhotoSharingApplication1.net.cloudapp.thanigaivellocationservice.Service1 client = null;
            
            string response = "";
            try
            {
                client = new net.cloudapp.thanigaivellocationservice.Service1();
                response = client.Getlocation("India");

            }catch(Exception e)
            {

                response = "Error: " + e.Message;
            }

            return response;
        }


        //protected override void Dispose(bool disposing)
        //{
        //    db.Dispose();
        //    base.Dispose(disposing);
        //}
    }
}
using System;
using System.Collections.Generic;
using System.Web.Mvc;
using PhotoSharingApplication1.Models;
using PhotoSharingApplication1.Controllers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using PhotoSharingTests.Doubles;

namespace PhotoSharingApplication1.Tests
{
    [TestClass]
    public class PhotoControllerTest
    {
        [TestMethod]
       public void Test_Index_Return_View()
        {
           // PhotoController controller = new PhotoController();
            var context = new FakePhotoSharingContext();
            var controller = new PhotoController(context);
            var result = controller.Index() as ViewResult;
            Assert.AreEqual("Index", result.ViewName);

        }

        [TestMethod]
        public void Test_PhotoGallery_Model_Type()
        {
            var context = new FakePhotoSharingContext();
            context.Photos = new[] { new Photo(), new Photo(), new Photo(), new Photo() }.AsQueryable();
            var controller = new PhotoController(context);
            var result = controller._PhotoGallery() as PartialViewResult;
            Assert.AreEqual(typeof(List<Photo>), result.Model.GetType());

        }

        [TestMethod]
        public void Test_GetImage_Return_Type()
        {
            var context = new FakePhotoSharingContext();
            context.Photos = new[] { new 
                Photo(){ photoId=1, PhotoFile= new byte[1], ImageMimeType= "image/jpeg"}, new Photo(){ photoId=2, PhotoFile= new byte[1], ImageMimeType= "image/jpeg"}, new Photo(){ photoId=3, PhotoFile= new byte[1], ImageMimeType= "image/jpeg"}, new Photo() { photoId=4, PhotoFile= new byte[1], ImageMimeType= "image/jpeg"}}.AsQueryable();
            var controller = new PhotoController(context);
            var result = controller.GetImage(1) as ActionResult;
            Assert.AreEqual(typeof(FileContentResult), result.GetType());

        }


        [TestMethod]
        public void Test_PhotoGallery_No_Parameter()
        {

            var context = new FakePhotoSharingContext();
            context.Photos = new[] { new Photo(), new Photo(), new Photo(), new Photo() }.AsQueryable();
            var controller = new PhotoController(context);
            var result = controller._PhotoGallery() as PartialViewResult;
            var modelPhotos = (IEnumerable<Photo>)result.Model;
            Assert.AreEqual(4, modelPhotos.Count());
        }

        [TestMethod]
        public void Test_PhotoGallery_Int_Parameter()
        {

            var context = new FakePhotoSharingContext();
            context.Photos = new[] { new Photo(), new Photo(), new Photo(), new Photo() }.AsQueryable();
            var controller = new PhotoController(context);
            var result = controller._PhotoGallery(3) as PartialViewResult;
            var modelPhotos = (IEnumerable<Photo>)result.Model;
            Assert.AreEqual(3, modelPhotos.Count());
        }
    }
}

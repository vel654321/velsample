using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.IO;

namespace PhotoSharingApplication1.Models
{
    public class PhotoSharingInitializer : DropCreateDatabaseIfModelChanges<PhotoSharingContext>
    {

        protected override void Seed(PhotoSharingContext context)
        {
            //base.Seed(context);
            var photos = new List<Photo>
            {
                 new Photo{
                     Title = "Test Photo", Description = "Your Description", UserName ="NaokiSato", PhotoFile = getFileBytes("\\Images\\flower.JPG"), ImageMimeType="image/jpeg", CreatedDate =DateTime.Today
                 }
            };

            photos.ForEach(s => context.Photos.Add(s));
            context.SaveChanges();


            var comments = new List<Comment>
            {
                new Comment{
                    PhotoID =1,
                    UserName="NaoikSaito",
                    Subject = "TestComment",
                    Body = "This Comment "+"should appear in "+"photo 1"

                }
            };
            comments.ForEach(s => context.Comments.Add(s));
            context.SaveChanges();
        }
        private byte[] getFileBytes(string path)
        {
            FileStream fileOnDisk = new FileStream(HttpRuntime.AppDomainAppPath + path, FileMode.Open);
            byte[] fileBytes;
            using (BinaryReader br = new BinaryReader(fileOnDisk))
            {
                fileBytes = br.ReadBytes((int)fileOnDisk.Length);
            }
            return fileBytes;
        }
    }
}
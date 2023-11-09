using Amazon.Runtime.Internal.Endpoints.StandardLibrary;
using AwsS3Practice.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.IO;
using System.Net;
using System.Threading.Tasks;

namespace AwsS3Practice.Controllers
{
    //[Route("api/[controller]")]
    [ApiController]
    [Route("documents")]
    public class AwsS3Controller : ControllerBase
    {
        private string projectPath = Directory.GetCurrentDirectory() + "\\wwwroot";
        private readonly IAppConfiguration _appConfiguration;
        private IAws3Services _aws3Services;


        public AwsS3Controller(IAppConfiguration appConfiguration)
        {
            _appConfiguration = appConfiguration;
            _aws3Services = new Aws3Services(_appConfiguration.AwsAccessKey, _appConfiguration.AwsSecretAccessKey, _appConfiguration.AwsSessionToken, _appConfiguration.Region, _appConfiguration.BucketName);

        }


        //[HttpGet("{documentName}")]
        //public ActionResult GetDocumentFromS3(string documentName)
        //{
        //    try
        //    {
        //        if (string.IsNullOrEmpty(documentName))
        //            return Ok("The 'documentName' parameter is required");


        //        var document = _aws3Services.ReadObjectDataAsync(documentName);

        //        return Ok(document);
        //    }
        //    catch (Exception ex)
        //    {
        //        return Ok(ex);
        //    }
        //}




        [HttpPost]
        [Route("arvanmethod")]
        public IActionResult UploadObjectFromFileAsync(string path)
        {
            //string path = "https://assets.reedpopcdn.com/god-of-war-walkthrough-guide-5004-1642178551828.jpg";
            var url = new Uri(path);
            var directoryPath = $"{projectPath}\\InstagramMedias\\";
            if (!Directory.Exists(directoryPath))
            {
                Directory.CreateDirectory(directoryPath);
            }
            var extension = ".jpg";
            var imgName = Guid.NewGuid() + extension;
            string mediaName = imgName;
            using (WebClient wc = new WebClient())
            {
                wc.DownloadFile(url, directoryPath + mediaName);

            }
            string filePath = $"{directoryPath + mediaName}";

            _aws3Services.UploadObjectFromFileAsync(mediaName, filePath);
            return Ok();


        }


        [HttpPost]
        [Route("arvanmethodNew")]
        public IActionResult Upload()
        {
            //string path = "https://assets.reedpopcdn.com/god-of-war-walkthrough-guide-5004-1642178551828.jpg";
            string path = "https://www.aparat.com/v/SJK89";
            //var url = new Uri(path);
            //var directoryPath = $"{projectPath}\\InstagramMedias\\";
            //if (!Directory.Exists(directoryPath))
            //{
            //    Directory.CreateDirectory(directoryPath);
            //}

            WebClient wc = new WebClient();
            Stream fileStream = wc.OpenRead(path);
            byte[] fileBytes = fileStream.ToArrayBytes();

            _aws3Services.Upload(fileBytes);
            return Ok();


        }

        [HttpPost]
        [Route("arvanmethodNewVideo")]
        public IActionResult UploadVideo()
        {
            //string path = "https://assets.reedpopcdn.com/god-of-war-walkthrough-guide-5004-1642178551828.jpg";
            //string path = "https://www.aparat.com/v/SJK89";
            string path = "https://scontent-sjc3-1.cdninstagram.com/v/t50.16885-16/278706268_4945507942233304_8275562822980350770_n.mp4?efg=eyJ2ZW5jb2RlX3RhZyI6InZ0c192b2RfdXJsZ2VuLjcyMC5pZ3R2LmJhc2VsaW5lIiwicWVfZ3JvdXBzIjoiW1wiaWdfd2ViX2RlbGl2ZXJ5X3Z0c19vdGZcIl0ifQ&_nc_ht=scontent-sjc3-1.cdninstagram.com&_nc_cat=104&_nc_ohc=oymHPCCbkTgAX8C7gAO&edm=APU89FABAAAA&vs=713764503146240_472604663&_nc_vs=HBksFQAYJEdGeTRuQkRZZENITTZaRVJBREl6TEt5S3ROaHlidlZCQUFBRhUAAsgBABUAGCRHRkNmb0JCTjNfVU9TUWNFQUN0TzZkZWczTG9mYnZWQkFBQUYVAgLIAQAoABgAGwGIB3VzZV9vaWwBMRUAACa08JL9iMm1PxUCKAJDMywXQFIKn752yLQYEmRhc2hfYmFzZWxpbmVfMV92MREAdewHAA%3D%3D&_nc_rid=5a428a0e6d&ccb=7-5&oh=00_AfAq_XlOt_5mseKE9wY20LXODGWyr0StlKvv69g_DpnVVw&oe=643BDD1A&_nc_sid=86f79a";
            var url=new Uri(path);

            WebClient wc = new WebClient();
            byte[] data = wc.DownloadData(path);

            var result= _aws3Services.UploadVideo(data);
            return Ok();

        }


        //[HttpGet]
        //[Route("test")]
        //public async Task<IActionResult> PutBucketAcl(IFormFile file)
        //{
        //    await _aws3Services.PutBucketAclAsync();
        //    return Ok();

        //}

    }


}

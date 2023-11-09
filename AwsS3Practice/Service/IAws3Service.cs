using Amazon;
using Amazon.S3;
using Amazon.S3.Model;
using Amazon.S3.Transfer;
using Microsoft.AspNetCore.Http;
using System;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace AwsS3Practice.Service
{
    public interface IAws3Services
    {
        Task UploadObjectFromFileAsync(string objectName, string filePath);
        Task<string> Upload(byte[] fileBytes);
        Task<string> UploadVideo(byte[] fileBytes);
        //Task<string> ReadObjectDataAsync(string keyName);

    }

    public class Aws3Services : IAws3Services
    {
        private readonly string _bucketName;
        private readonly IAmazonS3 _awsS3Client;

        public Aws3Services(string awsAccessKeyId, string awsSecretAccessKey, string awsSessionToken, string region, string bucketName)
        {
            _bucketName = bucketName;
            var awsCredentials = new Amazon.Runtime.BasicAWSCredentials(awsAccessKeyId, awsSecretAccessKey);
            var config = new AmazonS3Config { ServiceURL = "https://s3.ir-thr-at1.arvanstorage.ir" };
            _awsS3Client = new AmazonS3Client(awsCredentials, config);

        }

        public async Task<string> Upload(byte[] fileBytes)
        {
            var extension = ".jpg";
            var imgName = Guid.NewGuid() + extension;

            PutObjectRequest request = new PutObjectRequest()
            {
                BucketName = _bucketName,
                Key = imgName,
                InputStream = new MemoryStream(fileBytes),
                CannedACL = S3CannedACL.PublicRead
            };

            await _awsS3Client.PutObjectAsync(request);

            return "done";

        }

        public async Task<string> UploadVideo(byte[] fileBytes)
        {

            var extension = ".mp4";
            var videoName = Guid.NewGuid() + extension;

            //var req = System.Net.WebRequest.Create("https://www.aparat.com/v/SJK89");

                PutObjectRequest request = new PutObjectRequest()
            {
                BucketName = _bucketName,
                Key = videoName,
                InputStream = new MemoryStream(fileBytes),
                CannedACL = S3CannedACL.PublicRead,
                ContentType = "video/mp4",

            };

            request.Metadata.Add("Content-Type", "video/mp4");

            await _awsS3Client.PutObjectAsync(request);

            return "done";

        }

        public async Task UploadObjectFromFileAsync(string objectName, string filePath)
        {
            var putRequest = new PutObjectRequest
            {
                BucketName = _bucketName,
                Key = objectName,
                FilePath = filePath,
                ContentType = "text/plain",
                CannedACL = S3CannedACL.PublicRead

            };

            putRequest.Metadata.Add("x-amz-meta-title", "someTitle");

            PutObjectResponse response = await _awsS3Client.PutObjectAsync(putRequest);

        }

        //public async Task<string> ReadObjectDataAsync(string keyName)
        //{
        //    string responseBody = string.Empty;

        //    try
        //    {
        //        GetObjectRequest request = new GetObjectRequest
        //        {
        //            BucketName = _bucketName,
        //            Key = keyName,
        //        };

        //        using (GetObjectResponse response = await _awsS3Client.GetObjectAsync(request))
        //        using (Stream responseStream = response.ResponseStream)
        //        using (StreamReader reader = new StreamReader(responseStream))
        //        {
        //            // Assume you have "title" as medata added to the object.
        //            string title = response.Metadata["x-amz-meta-title"];
        //            string contentType = response.Headers["Content-Type"];

        //            // Retrieve the contents of the file.
        //            responseBody = reader.ReadToEnd();

        //            // Write the contents of the file to disk.
        //            string filePath = keyName;
        //            return responseBody;



        //        }
        //    }
        //    catch (AmazonS3Exception e)
        //    {
        //        // If the bucket or the object do not exist
        //        return $"Error: '{e.Message}'";
        //    }
        //}


    }
}

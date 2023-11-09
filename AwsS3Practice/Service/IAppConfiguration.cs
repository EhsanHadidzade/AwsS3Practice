namespace AwsS3Practice.Service
{
	public interface IAppConfiguration
	{
		string AwsAccessKey { get; set; }
		string AwsSecretAccessKey { get; set; }
		string AwsSessionToken { get; set; }
		string BucketName { get; set; }
		string Region { get; set; }
	}

    public class AppConfiguration : IAppConfiguration
    {
		public AppConfiguration()
		{
			BucketName = "BucketName In Arvan";
			Region = "702a1812-eb5c-473a-9050-16ea89f78944";
			AwsAccessKey = "8c839b2z-53cc-4344-8216-5c18e98b19fd";
			AwsSecretAccessKey = "86bdba76fb916feg993a2ef10013grq4a96ba68df6cba6cd15bded9e0319as03";
			AwsSessionToken = "test";
		}

		public string BucketName { get; set; }
		public string Region { get; set; }
		public string AwsAccessKey { get; set; }
		public string AwsSecretAccessKey { get; set; }
		public string AwsSessionToken { get; set; }
	}
}

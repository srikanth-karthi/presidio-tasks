using Amazon.S3;
using Amazon.S3.Model;
using System;
using System.IO;
using System.Threading.Tasks;

public class MinIOService
{
    private readonly IAmazonS3 _s3Client;
    private readonly string _bucketName;

    public MinIOService(IConfiguration configuration)
    {
        var awsOptions = new AmazonS3Config
        {
            ServiceURL = configuration["MinIO:ServiceUrl"],
            ForcePathStyle = true
        };

        _s3Client = new AmazonS3Client(configuration["MinIO:AccessKey"], configuration["MinIO:SecretKey"], awsOptions);
        _bucketName = configuration["MinIO:BucketName"];

        
        EnsureBucketExistsAsync().Wait(); 
    }

    public string GetServiceUrl()
    {
        return _s3Client.Config.ServiceURL;
    }

    public string GetBucketName()
    {
        return _bucketName;
    }

    public async Task UploadFileAsync(string key, Stream fileStream)
    {
        var putRequest = new PutObjectRequest
        {
            BucketName = _bucketName,
            Key = key,
            InputStream = fileStream
        };

        await _s3Client.PutObjectAsync(putRequest);
    }

    public async Task<Stream> DownloadFileAsync(string key)
    {
        var getRequest = new GetObjectRequest
        {
            BucketName = _bucketName,
            Key = key
        };

        var response = await _s3Client.GetObjectAsync(getRequest);
        return response.ResponseStream;
    }

    public async Task DeleteFileAsync(string key)
    {
        var deleteRequest = new DeleteObjectRequest
        {
            BucketName = _bucketName,
            Key = key
        };

        await _s3Client.DeleteObjectAsync(deleteRequest);
    }

private async Task EnsureBucketExistsAsync()
{
    try
    {
        var response = await _s3Client.ListBucketsAsync();
        var bucketExists = response.Buckets.Any(b => b.BucketName.Equals(_bucketName, StringComparison.OrdinalIgnoreCase));

        if (!bucketExists)
        {
            await _s3Client.PutBucketAsync(new PutBucketRequest
            {
                BucketName = _bucketName,
                UseClientRegion = true 
            });

            await SetBucketPolicyAsync(_bucketName); // Set the bucket policy to make it public
        }
    }
    catch (AmazonS3Exception ex)
    {
        Console.WriteLine($"Error creating bucket {_bucketName}: {ex.Message}");
        throw; // Handle or throw exception as appropriate for your application
    }
}

private async Task SetBucketPolicyAsync(string bucketName)
{
    var policy = $@"
    {{
        ""Version"": ""2012-10-17"",
        ""Statement"": [
            {{
                ""Effect"": ""Allow"",
                ""Principal"": ""*"",
                ""Action"": ""s3:GetObject"",
                ""Resource"": ""arn:aws:s3:::{bucketName}/*""
            }}
        ]
    }}";

    var request = new PutBucketPolicyRequest
    {
        BucketName = bucketName,
        Policy = policy
    };

    await _s3Client.PutBucketPolicyAsync(request);
}
}
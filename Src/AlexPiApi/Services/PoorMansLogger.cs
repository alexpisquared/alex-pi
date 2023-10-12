using System.IO;
using System.Text;
using System.Threading.Tasks;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Specialized;

namespace AlexPiApi.Services;

public class PoorMansLogger
{
  readonly BlobServiceClient _blobServiceClient;
  readonly BlobContainerClient _containerClient;
  readonly BlobClient _blobClient;
  readonly AppendBlobClient _appendBlobClient;
  const string fileName = "UTF8.log", blobContainerName = "poormanslogs"; // .ToLower(); //!!!

  public PoorMansLogger(string connectionString)
  {
    _blobServiceClient = new BlobServiceClient(connectionString);
    _containerClient = _blobServiceClient.GetBlobContainerClient(blobContainerName);
    _blobClient = _containerClient.GetBlobClient(fileName);
    _appendBlobClient = _containerClient.GetAppendBlobClient(fileName);
  }

  public async Task CreateFileAsync(string content)
  {
    _ = await _containerClient.CreateIfNotExistsAsync();
  }
  public async Task<string> ReadFileAsync()
  {
    var downloadInfo = await _blobClient.DownloadAsync();

    using var reader = new StreamReader(downloadInfo.Value.Content);
    return await reader.ReadToEndAsync();
  }
  public async Task UpdateFileAsync(string newContent)
  {
    using var stream = new MemoryStream(Encoding.UTF8.GetBytes(newContent));
    _ = await _blobClient.UploadAsync(stream, true);
  }
  public async Task DeleteFileAsync()
  {
    _ = await _containerClient.DeleteAsync();
  }
  public async Task AppendToFileAsync(string appendContent)
  {
    if (!await _appendBlobClient.ExistsAsync())
    {
      _ = await _appendBlobClient.CreateAsync();
    }

    using var stream = new MemoryStream(Encoding.UTF8.GetBytes(appendContent));
    _ = await _appendBlobClient.AppendBlockAsync(stream);
  }
}
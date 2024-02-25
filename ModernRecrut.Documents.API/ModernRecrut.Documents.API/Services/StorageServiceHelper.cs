using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Specialized;
using ModernRecrut.Documents.API.Interfaces;
using ModernRecrut.Documents.API.Models;

namespace ModernRecrut.Documents.API.Services
{
    public class StorageServiceHelper : IStorageServiceHelper
    {
        private readonly BlobServiceClient _blobServiceClient;

        public StorageServiceHelper(BlobServiceClient blobServiceClient)
        {
            _blobServiceClient = blobServiceClient;
        }

        public async Task<IEnumerable<string>> ObtenirAll()
        {
            List<string> documents = new List<string>();
            var blobContainer = _blobServiceClient.GetBlobContainerClient("documents");
            var blobs = blobContainer.GetBlobsAsync();

            await foreach (var blob in blobs)
            {
                documents.Add(blob.Name);
            }
            return documents;
        }

        public async Task PostFileAsync(Document document, string fichierNom)
        {
            var blobContainer = _blobServiceClient.GetBlobContainerClient("documents");

            using (Stream stream = document.DocumentDetails.OpenReadStream())
			{
                await blobContainer.UploadBlobAsync(fichierNom, stream);
			}
        }

        public async Task<bool> Supprimer(string nomFichier)
        {
            bool result = true;
            var blobContainer = _blobServiceClient.GetBlobContainerClient("documents");
            BlobClient? blobClient = blobContainer.GetBlobClient(nomFichier);

            if(blobClient != null)
                await blobContainer.DeleteBlobAsync(nomFichier);
            else
                result = false;
            
            return result;
        }
    }
}

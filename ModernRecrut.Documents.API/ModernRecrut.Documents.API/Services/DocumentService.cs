using Microsoft.EntityFrameworkCore;

using ModernRecrut.Documents.API.Entites;
using ModernRecrut.Documents.API.Interfaces;
using ModernRecrut.Documents.API.Models;
using System.IO;

namespace ModernRecrut.Documents.API.Services
{
	public class DocumentService : IDocumentsService
	{
		private readonly IStorageServiceHelper _storageServiceHelper;
		private readonly IConfiguration _config;

        public DocumentService(IStorageServiceHelper storageServiceHelper, IConfiguration config)
        {
            _storageServiceHelper = storageServiceHelper;
            _config = config;
        }

        public async Task<IEnumerable<string>> ObtenirSelonUtilisateurId(string utilisateurId)
		{
			List<string> ListeDocuments = new List<string>();

			Task<IEnumerable<string>> fichiers = _storageServiceHelper.ObtenirAll();

			foreach (var fichier in await fichiers)
			{
				var fichierNom = Path.GetFileName(fichier);
				if (fichierNom.StartsWith($"{utilisateurId}_"))
				{
					ListeDocuments.Add(_config.GetValue<string>("StorageAccount:Url") + fichierNom);
				}
			}

			return ListeDocuments;
		}

		public async Task PostFileAsync(Document document, string utilisateurId)
		{
			int randomNum = new Random().Next(1000, 9999);  // Nombre aléatoire
			string extension = Path.GetExtension(document.DocumentDetails.FileName);  // Extension du fichier
			string fichierNom = $"{utilisateurId}_{document.DocumentType}_{randomNum}{extension}";

            await _storageServiceHelper.PostFileAsync(document, fichierNom);
		}

		public async Task<bool> Supprimer(string fichierNom)
		{
			return await _storageServiceHelper.Supprimer(fichierNom);
		}
	}
}

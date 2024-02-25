using ModernRecrut.Documents.API.Models;

namespace ModernRecrut.Documents.API.Interfaces
{
    public interface IStorageServiceHelper
    {
        Task<IEnumerable<string>> ObtenirAll();
        Task PostFileAsync(Document document, string fichierNom);
        Task<bool> Supprimer(string nomFichier);
    }
}

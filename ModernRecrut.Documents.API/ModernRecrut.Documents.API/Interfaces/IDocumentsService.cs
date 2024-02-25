using ModernRecrut.Documents.API.Entites;
using ModernRecrut.Documents.API.Models;

namespace ModernRecrut.Documents.API.Interfaces
{
    public interface IDocumentsService
    {
        Task<IEnumerable<string>> ObtenirSelonUtilisateurId(string utilisateurId);
        Task PostFileAsync(Document document, string utilisateurId);
        Task<bool> Supprimer(string nomFichier);
    }
}

using PokemonWebApp.Models;

namespace PokemonWebApp.Interfaces
{
    public interface IReviewerRepository
    {
        ICollection<Reviewer> GetReviewers();
        Reviewer GetReviewer(int reviewerid);
        ICollection<Review> GetReviewsByReviewer(int reviewerid);
        bool ReviewerExists(int reviewerid);
        bool CreateReviewer(Reviewer reviewer);
        bool UpdateReviewer(Reviewer reviewer);
        bool DeleteReviewer(Reviewer reviewer);
        bool Save();
       
    }
}

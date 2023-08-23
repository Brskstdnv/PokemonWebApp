using PokemonWebApp.Models;

namespace PokemonWebApp.Interfaces
{
    public interface IReviewRepository
    {
        ICollection<Review> GetReviews();
        Review GetReview(int reviewId);
        ICollection<Review> GetReviewsOfAPokemon(int pokeId);
        bool ReviewExists(int reviewId);

        bool CreateReview(Review review);
        bool UpdateReview(Review review);
        bool DeteteReview(Review review);
        bool Save();
        
        
    }
}

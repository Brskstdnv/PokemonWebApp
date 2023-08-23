using AutoMapper;
using PokemonWebApp.Data;
using PokemonWebApp.Interfaces;
using PokemonWebApp.Models;

namespace PokemonWebApp.Repository
{
    public class ReviewerRepository : IReviewerRepository
    {
        private DataContext _context;
        IMapper _mapper;
        public ReviewerRepository(DataContext context, IMapper mapper)
        {
                _context = context;
                _mapper = mapper;
        }

        public bool CreateReviewer(Reviewer reviewer)
        {
            _context.Add(reviewer);
            return Save();
        }

        public bool DeleteReviewer(Reviewer reviewer)
        {
            _context.Remove(reviewer);
            return Save();
        }

        public Reviewer GetReviewer(int reviewerid)
        {
            return _context.Reviewers.Where(r=>r.Id == reviewerid).FirstOrDefault();
        }

        public ICollection<Reviewer> GetReviewers()
        {
            return _context.Reviewers.OrderBy(r => r.Id).ToList();  
        }

        public ICollection<Review> GetReviewsByReviewer(int reviewerid)
        {
            return _context.Reviews.Where(r=>r.Reviewer.Id == reviewerid).ToList() ;    
        }

        public bool ReviewerExists(int reviewerid)
        {
            return _context.Reviewers.Any(r=> r.Id == reviewerid);  
        }

        public bool Save()
        {
            var saved = _context.SaveChanges(); 
            return saved > 0 ? true : false;
        }

        public bool UpdateReviewer(Reviewer reviewer)
        {
            _context.Update(reviewer);
            return Save();
        }
    }
}

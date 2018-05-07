using Microsoft.EntityFrameworkCore;

namespace Domain
{
    public class AuthorNewsRepository : Repository<AuthorsNews>, IAuthorNewsRepository
    {
        public AuthorNewsRepository(DbContext context) : base(context)
        {
        }
    }
}
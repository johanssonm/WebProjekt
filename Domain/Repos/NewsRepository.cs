using Microsoft.EntityFrameworkCore;

namespace Domain
{
    public class NewsRepository : Repository<News>, INewsRepository
    {
        public NewsRepository(DbContext context)
            : base(context)
        {
        }
    }
}
using Microsoft.EntityFrameworkCore;

namespace Domain
{
    public class NewsCategoriesRepository : Repository<NewsCategories>, INewsCategoriesRepository
    {
        public NewsCategoriesRepository(DbContext context) : base(context)
        {
        }
    }
}
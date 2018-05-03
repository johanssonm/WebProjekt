namespace Domain
{
    public class AuthorsNews
    {
        public int AuthorId { get; set; }
        public int NewsId { get; set; }
        public Author Author { get; set; }
        public News News { get; set; }

        public AuthorsNews(News news, Author author)
        {
            News = news;
            Author = author;
        }
    }
}
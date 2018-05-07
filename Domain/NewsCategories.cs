namespace Domain
{
    //TODO Se över om det går att abstrahera bort Domainlagret mer för att ytterligare förhindra dependencies
    public class NewsCategories
    {
        // public int Id { get; set; }
        public int NewsId { get; set; }
        public int CategoryId { get; set; }

        // public Category Category { get; set; }

        // public News News { get; set; }

    }
}
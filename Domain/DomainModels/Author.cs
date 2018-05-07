using System.Collections.Generic;

namespace Domain
{
    public class Author
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Avatar { get; set; }

        public List<AuthorsNews> Authornews { get; set; }

        public Author()
        {

        }

        public Author(string name)
        {
            Name = name;
            Avatar = $"assets/img/avatar.png";
        }

    }
}
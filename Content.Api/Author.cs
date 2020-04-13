namespace Content.Api
{
    public class Author
    {
        public string Name { get; }

        public string ImageUrl { get; }

        public Author(
            string name,
            string imageUrl)
        {
            Name = name;
            ImageUrl = imageUrl;
        }
    }
}
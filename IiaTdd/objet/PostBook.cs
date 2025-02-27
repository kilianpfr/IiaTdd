namespace IiaTdd.objet
{
    public enum FormatEnum
    {
        Poche,
        Broché,
        GrandFormat 
    }

    public class PostBook
    {
        public string? Isbn { get; set; }
        public string? Title { get; set; }
        public Author? Author { get; set; }
        public string? Editor { get; set; }
        public FormatEnum Format { get; set; }
    }
}
namespace IiaTdd.objet;

public class GetBook
{
    public int Id { get; set; }
    public string? Isbn { get; set; }
    public string? Title { get; set; }
    public Author? Author { get; set; }
    public string? Editor { get; set; }
    public FormatEnum Format { get; set; }
}
namespace IiaTdd.cs.Author;

public class CheckAuthor
{
    public static void CheckAuthorName(objet.Author authorName)
    {
        //si il y as des chiffres dans le nom de l'auteur
        if (authorName.Name != null && authorName.Name.Any(char.IsDigit) || authorName.FirstName != null && authorName.FirstName.Any(char.IsDigit))
        {
            throw new Exception("Nom de l'auteur invalide, il ne doit pas contenir de chiffres");
        }
      
    }
}
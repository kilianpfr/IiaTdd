namespace IiaTdd.cs.Isbn;

public class CheckIsbn
{
    public static int TenOrThirteen(String isbn)
    {
        
        
        isbn = isbn.Replace("-", "");
        isbn = isbn.Replace(" ", "");
        
        if (isbn.Length == 10)
        {
       
            char lastChar = isbn[isbn.Length - 1];
            
            if (Char.IsDigit(lastChar) || lastChar == 'X')
            {
                return 10;
            }

            throw new Exception("ISBN ivalide, le dernier caractère doit être un chiffre ou un X pour un ISBN de 10 chiffres");

        }

        if (isbn.Length == 13)
        {

            if (long.TryParse(isbn, out long n))
            {
                return 13;
            }
            throw new Exception("ISBN ivalide, doit être un chiffre pour un ISBN de 13 chiffres");
        }
        throw new Exception("ISBN ivalide");
    }
}
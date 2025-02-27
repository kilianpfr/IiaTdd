namespace IiaTdd.cs.Isbn;

public class CheckIsbnValide
{
    public static bool CheckIsbnThirteen(string isbn)
    {
        int correctdigitnumber = CheckIsbn.TenOrThirteen(isbn);
        if (correctdigitnumber != 13)
        {
            throw new Exception("ISBN invalide, le nombre de chiffres doit être de 13");
        }
        isbn = isbn.Replace("-", "");
        isbn = isbn.Replace(" ", "");
        int sum = 0;
        for (int i = 0; i < 12; i++)
        {
            int digit = int.Parse(isbn[i].ToString());
            sum += i % 2 == 0 ? digit : digit * 3;
        }

        int check = 10 - sum % 10;
        if (check == 10)
        {
            check = 0;
        }

        return check == int.Parse(isbn[12].ToString());
    }
    
    public static bool CheckIsbnTen(string isbn)
    {
        int correctdigitnumber = CheckIsbn.TenOrThirteen(isbn);
        if (correctdigitnumber != 10)
        {
           throw new Exception("ISBN invalide, le nombre de chiffres doit être de 10");
        }
        int sum = 0;
        for (int i = 0; i < 9; i++)
        {
            int digit = int.Parse(isbn[i].ToString());
            sum += (i + 1) * digit;
        }

        int check = sum % 11;
        if (check == 10)
        {
            return isbn[9] == 'X';
        }

        return check == int.Parse(isbn[9].ToString());
    }
}
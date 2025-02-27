namespace IiaTdd.cs.format;

public class Format
{
    public static void CheckFormatEnum(int format)
    {
        if (format > 2)
        {
            throw new Exception("Format invalide");
        }
    }
}
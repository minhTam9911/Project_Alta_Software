namespace PlantNestBackEnd.Helplers;

public class RandomHelper
{
    public static string RandomString(int length)
    {
        var random = new Random();
        const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
        return new string(Enumerable.Repeat(chars, length)
          .Select(s => s[random.Next(s.Length)]).ToArray());
    }
	public static string RandomInt(int length)
	{
		var random = new Random();
		const string chars = "0123456789";
		return new string(Enumerable.Repeat(chars, length)
		  .Select(s => s[random.Next(s.Length)]).ToArray());
	}
}

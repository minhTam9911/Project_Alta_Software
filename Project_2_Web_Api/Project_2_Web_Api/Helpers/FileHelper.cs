namespace PlantNestBackEnd.Helpers;

public class FileHelper
{
	public static string generateFileName(string fileName)
	{
		var name = Guid.NewGuid().ToString().Replace("-", "");
		var lastIndex = fileName.LastIndexOf('.');
		var extend = fileName.Substring(lastIndex);
		return name+ "." + extend;
	}
}

using CodeChallenge.App.Config;

public class Program
{
    public static void Main(string[] args)
    {
        new App().Configure(args).Run();
    }
}
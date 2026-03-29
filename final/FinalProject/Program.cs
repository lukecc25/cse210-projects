using System;

class Program
{
    static void Main(string[] args)
    {
        MadLibParser parser = new MadLibParser();
        MadLibRepository repository = new MadLibRepository("templates");
        MadLibGame game = new MadLibGame(parser, repository);
        game.RunMenu();
    }
}

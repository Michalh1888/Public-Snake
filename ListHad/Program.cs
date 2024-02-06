// See https://aka.ms/new-console-template for more information
using ListHad;

Console.Title = "HAD 2D"; //zapsání názvu do okna konzole
Had had = new Had(Console.WindowWidth/2, Console.WindowHeight/2, 67);//(60,15,75)// Instance hada
GameDrawing.StartHry();
had.UrceniSmeru(Console.ReadKey());
had.HerniSmycka();

Console.ReadKey();

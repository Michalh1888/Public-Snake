using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace ListHad
{
    internal class GameDrawing
    {
        public static void StartHry()//vykreslení startu hry
        { 
            Console.BackgroundColor = ConsoleColor.Gray; 
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Clear();
            Console.CursorLeft = 10;//Console.WindowWidth / 2;// - 60;
            Console.CursorTop = Console.WindowHeight / 2 - 13;
            Console.WriteLine("                                                                                                                       ");
            Console.WriteLine("                      '||'  '|'|||''||'''||''''|   '||'  '||'  '|'||''''|   '||'  '||'||''|. '||'''',                  ");
            Console.WriteLine("                       '|.  .' ||   ||   ||  .      ||    '|.  .' ||  .      ||    || ||   || ||  .                    ");
            Console.WriteLine("                        ||  |  ||   ||   ||''|      ||     ||  |  ||''|      ||''''|| ||''|'  ||''|                    ");
            Console.WriteLine("                         |||   ||   ||   ||         ||      |||   ||         ||    || ||   |. ||                       ");
            Console.WriteLine("                          |   .||. .||. .||.....|| .|'       |   .||.....|  .||.  .||.||.  '|.||.....|                 ");
            Console.WriteLine("                                                 '''                                                                   ");
            Console.WriteLine("                                                                                                                       ");
            Console.WriteLine("                                       .|'''.|''||'''||'.|'''.'||'  |''|.   '|'||'                                     ");
            Console.WriteLine("                                       ||..  '  ||   || ||..  '|| .'   |'|   | ||                                      ");
            Console.WriteLine("                                        ''|||.  ||   ||  ''|||.||'|.   | '|. | ||                                      ");
            Console.WriteLine("                                      .     '|| ||   ||.     '|||  ||  |   ||| ||                                      ");
            Console.WriteLine("                                      |'....|' .||. .|||'....|.||.  ||.|.   '|.||.                                     ");
            Console.WriteLine("                                                                                                                       ");
            Console.WriteLine("                                                                                                                       ");
            Console.WriteLine("   .|'''.'||    ||'||''''|'||''|. ..|''|'||'  '|..|''||'||'  '|'  '||'  |''||'        '||'  '|'||''''| .|'''.'||'  '|' ");
            Console.WriteLine("   ||..  '|||  ||| ||  .   ||   |.|'    |'|.  ..|'    ||||    |    || .'   ||        ||'|.  .' ||  .   ||..  '||    |  ");
            Console.WriteLine("    ''|||.|'|..'|| ||''|   ||''|'||      |||  |||      |||    |    ||'|.   ||       |  |||  |  ||''|    ''|||.||    |  ");
            Console.WriteLine("  .     '|| '|' || ||      ||   |'|.     ||||| '|.     |||    |    ||  ||  ||      .''''||||   ||     .     '|||    |  ");
            Console.WriteLine("  |'....|.|. | .||.||......||.  '|''|...|'  |   ''|...|' '|..'    .||.  ||.||......|.  .|||   .||.....|'....|' '|..'   ");
            Console.WriteLine("                                                                                                                       ");
        }






        public static void KonecHry(int score)//vykreslení "GAME OVER"
        {
            Console.Clear();
            Console.BackgroundColor = ConsoleColor.Black;
            Console.CursorLeft = Console.WindowWidth / 2 - 43;
            Console.CursorTop = Console.WindowHeight / 2 - 5;
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("                                                                                        ");
            Console.CursorLeft = Console.WindowWidth / 2 - 43;
            Console.WriteLine("  ..|'''.|      |     '||    ||' '||''''|      ..|''||   '||'  '|' '||''''|  '||''|.    ");
            Console.CursorLeft = Console.WindowWidth / 2 - 43;
            Console.WriteLine(" .|'     '     |||     |||  |||   ||  .       .|'    ||   '|.  .'   ||  .     ||   ||   ");
            Console.CursorLeft = Console.WindowWidth / 2 - 43;
            Console.WriteLine(" ||    ....   |  ||    |'|..'||   ||''|       ||      ||   ||  |    ||''|     ||''|'    ");
            Console.CursorLeft = Console.WindowWidth / 2 - 43;
            Console.WriteLine(" '|.    ||   .''''|.   | '|' ||   ||          '|.     ||    |||     ||        ||   |.   ");
            Console.CursorLeft = Console.WindowWidth / 2 - 43;
            Console.WriteLine("  ''|...'|  .|.  .||. .|. | .||. .||.....|     ''|...|'      |     .||.....| .||.  '|'  ");
            Console.CursorLeft = Console.WindowWidth / 2 - 43;
            Console.WriteLine("                                                                                        ");
            Console.WriteLine("");
            Console.SetCursorPosition(Console.WindowWidth / 2-4, Console.CursorTop);
            Console.WriteLine($" Skóre: {score} ");
            if (score > 0)
            {
                Console.BackgroundColor = ConsoleColor.Green;
                Console.ForegroundColor = ConsoleColor.DarkRed;
                for (int i = 0; i < score; i++)
                    Console.Write("██|");
                //zápis do tabulky hráčů?
                Console.Write("\nZapsat do tabulky hráčů? [y/n]: ");
                string vstup = Console.ReadLine().Trim().ToLower();
                if (vstup == "y")
                    HraciDat.TabulkaHracu(score);
                else
                    Console.WriteLine("\nStiskni libovolnou klávesu pro ukončení");
            }
        }
    }
}

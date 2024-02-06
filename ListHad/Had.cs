using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ListHad
{
    internal class Had
    {
        //rozměr konzole X (0-119)max 40(80) sloupců(vykr.2obdelníky vedle sebe=>40 sloupců)
        //rozměr konzole Y (0-30)max 25 řádků
        public bool Zivy { get; private set; } = true;
        public int Smer { get; private set; } = 360;//default => 0 - 1.určení na startu hry
        public int Rychlost { get; private set; }// = 75;
        public int Score { get; private set; } = 0;
        private Random random = new Random();
        private List<Souradnice> had = new List<Souradnice>();
        private int actX;
        private int actY;
        private int lastX;
        private int lastY;
        private int jidloX;
        private int jidloY;
        private bool snezeno;
        private bool kolize;
       
        public Had(int pocX, int pocY, int rychlost)
        {
            Console.CursorVisible = false;
            actX = pocX;
            actY = pocY;
            Rychlost = rychlost;
            had.Add(new Souradnice(actX, actY));
            PoziceJidla();
        }
        public void Vykresli()
        {
            foreach (Souradnice item in had)
            {
                Console.SetCursorPosition(item.X, item.Y);
                if (had.IndexOf(item) == 0)
                    Console.ForegroundColor = ConsoleColor.DarkBlue;
                else
                    Console.ForegroundColor = ConsoleColor.Blue;
                Console.Write("██");
                lastX = item.X;
                lastY = item.Y;
            }
            KontrolaSnezeni(actX, actY);
            if (snezeno)
                PoziceJidla();
            Console.SetCursorPosition(jidloX, jidloY);
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write("██");
        }
        public void Pohyb()
        {
            switch (Smer)
            {
                case 0:
                    had.Insert(0, new Souradnice(actX + 2, actY));
                    actX++;
                    actX++;
                    break;
                case 90:
                    had.Insert(0, new Souradnice(actX, actY - 1));
                    actY--;
                    break;
                case 180:
                    had.Insert(0, new Souradnice(actX - 2, actY));
                    actX--;
                    actX--;
                    break;
                case 270:
                    had.Insert(0, new Souradnice(actX, actY + 1));
                    actY++;
                    break;  
            }
            had.RemoveAt(had.Count - 1);
            KontrolaKolize(actX, actY, true);
            if (kolize)
            {
                Zivy = false;
                GameDrawing.KonecHry(Score);
            }
        }
        private void KontrolaKolize(int x, int y, bool teloHad)
        {
            kolize = false;
            foreach (Souradnice item in had)
            {
                if (item.X == x && item.Y == y)
                {
                    if (!teloHad)
                        kolize = true;
                    else if ((had.IndexOf(item) != 0) && (teloHad))
                        kolize = true;
                }  
            }      
            if ((!kolize)&&(teloHad))
            {
                if (((x < -1) || (x > Console.WindowWidth - 2)) || ((y < 0) || (y > Console.WindowHeight)))//118,30
                    kolize = true;
            }            
        }
        private void PoziceJidla()
        {
            do
            {
                do
                {
                  jidloX = random.Next(0, Console.WindowWidth);
                } while (jidloX % 2 != actX % 2);
                jidloY = random.Next(0, Console.WindowHeight);
                KontrolaKolize(jidloX, jidloY, false);
            } while(kolize);
            snezeno = false;
        }
        public void KontrolaSnezeni(int x, int y)
        {
            if ((x == jidloX) && (y == jidloY))
            {
                had.Add(new Souradnice(lastX, lastY));
                snezeno = true;
                Score++;
                if (Rychlost >= 1)
                    Rychlost--;
            }
        }
        public void UrceniSmeru(ConsoleKeyInfo klavesa)
        {               //reakce na jednotlivé klávesy
            if ((klavesa.Key == ConsoleKey.RightArrow) && ((Smer != 180) || (had.Count == 1)))
                Smer = 0;
            if ((klavesa.Key == ConsoleKey.LeftArrow) && ((Smer != 0) || (had.Count == 1)))
                Smer = 180;
            if ((klavesa.Key == ConsoleKey.DownArrow) && ((Smer != 90) || (had.Count == 1)))
                Smer = 270;
            if ((klavesa.Key == ConsoleKey.UpArrow) && ((Smer != 270) || (had.Count == 1)))
                Smer = 90;
            if (Smer == 360)//default-1.určení na startu hry
                Smer = 0;
        }
        public void HerniSmycka()
        {
            while (Zivy) // Herní smyčka
            {
                Console.BackgroundColor = ConsoleColor.Green; // Nastavení zeleného pozadí
                Console.Clear(); // Vymazání konzole
                Vykresli(); // Vykreslení hada
                Pohyb(); // Posun hada
                Thread.Sleep(Rychlost); // Prodleva 
                if (Console.KeyAvailable) // Pokud je stisknuta nějaká klávesa
                    UrceniSmeru(Console.ReadKey());// Načtení klávesy do metody
            }
        }
    }
}

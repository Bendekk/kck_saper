using System;
using Figgle;
namespace kck_saper
{
    class Pole
    {
        public int wartosc;
        public bool odkryte;
    }
    class Plansza
    {
        protected static int origRow = 8;
        protected static int origCol = 2;
        Pole[,] plansza;
        int koniec;
        public int max_x=10;
        public int max_y=10;
        public int liczba_min=10; 
        protected static void WriteAtInt(int s, int x, int y)
        {
            try
            {
                Console.SetCursorPosition(origCol + x, origRow + y);
                Console.Write(s);
            }
            catch (ArgumentOutOfRangeException e)
            {
                Console.Clear();
                Console.WriteLine(e.Message);
            }
        }
        protected static void WriteAtString(String s, int x, int y)
        {
            try
            {
                Console.SetCursorPosition(origCol + x, origRow + y);
                Console.Write(s);
            }
            catch (ArgumentOutOfRangeException e)
            {
                Console.Clear();
                Console.WriteLine(e.Message);
            }
        }
        public void generuj_plasze(int x, int y) //rozmiary plaszy
        {
            max_x = x;
            max_y = y;
            plansza = new Pole[x, y];
            for (int polex = 0; polex < x; ++polex)
                for (int poley = 0; poley < y; ++poley)
                {
                    plansza[polex, poley] = new Pole();
                    plansza[polex, poley].wartosc = 0;
                    plansza[polex,poley].odkryte = false;
                }
        }
        
        public void ustaw_mine(int x, int y,int max_x,int max_y) //x-mina, max_x rozmiar plaszy
            {
            plansza[x,y].wartosc = 9; //9-mina

            for(int x_dookola=-1; x_dookola<2; ++x_dookola)
                for(int y_dookola = -1; y_dookola < 2; ++y_dookola)
                {
                    if(x + x_dookola > -1 && y + y_dookola > -1 && x +x_dookola < max_x && y +y_dookola < max_y)
                    {
                        if (plansza[x + x_dookola,y + y_dookola].wartosc != 9)
                            plansza[x + x_dookola,y + y_dookola].wartosc += 1;
                    }
                }
            }
        public void losuj_pozycje(int liczba_bomb)
        {
            int pozx, pozy;
            Random rnd = new Random();
            while (liczba_bomb > 0)
            {
                pozx = rnd.Next(max_x);
                pozy = rnd.Next(max_y);
                if (plansza[pozx,pozy].wartosc != 9)
                {
                    ustaw_mine(pozx, pozy, max_x, max_y);
                    --liczba_bomb;
                }
            }
        }
        public void wys_logo()
        {
            Console.WriteLine(FiggleFonts.Standard.Render("My  Sapper!"));
        }
        public bool wys_menu()
        {
            Console.Clear();
            wys_logo();
            Console.Write("1. Wybierz rozmiar Pola (obecnie {0} x {1}) \n", max_x, max_y);
            Console.Write("2. Wybierz liczbe min (obecnie {0}, zalecane liczba {1}) \n", liczba_min, (max_x * max_y) / 10);
            Console.Write("3. Odpal gre \n");
            Console.Write("4. Zamknij program \n");
            Console.Write("Wybierz opcje przez naciśnięcie odpowiedniego przycisku: ");
            switch (Console.ReadLine())
            {
                case "1":
                    wyb_rozmiar_pola();
                    return true;
                case "2":
                    wyb_liczbe_min();
                    return true;
                case "3":
                    generuj_plasze(max_x, max_y);
                    losuj_pozycje(liczba_min);
                    wys_plansze(0,0);
                    return true;
                case "4":
                    return false;
                default:
                    return true;
            }
        }
        public void wyb_rozmiar_pola()
        {
            Console.Clear();
            int x, y;
            wys_logo();
            Console.Write("Obecny rozmiar pola: {0}x{1} \n", max_x,max_y);
            Console.Write("Podaj liczbe rzędów(x):");
            x = Convert.ToInt32(Console.ReadLine());
            Console.Write("Podaj liczbe kolumn(y):");
            y = Convert.ToInt32(Console.ReadLine());
            Console.Clear();
            wys_logo();
            Console.Write("Zmienić rozmiar pola z {0}x{1} na {2}x{3}? (t,n)\n", max_x, max_y, x, y);
            while (1==1)
            {
                switch (Console.ReadLine())
                {
                    case "t":
                        max_x = x;
                        max_y = y;
                        wys_menu();
                        return;
                    case "n":
                        wys_menu();
                        return;
                    default:
                        Console.Clear();
                        wys_logo();
                        Console.Write("Podaj poprawną odpowiedz \n");
                        Console.Write("Zmienić rozmiar pola z {0}x{1} na {2}x{3}? (t,n)\n", max_x, max_y, x, y);
                        break;
                }
            }

        }
        public void wyb_liczbe_min()
        {
            Console.Clear();
            wys_logo();
            Console.Write("Obecna liczba min: {0} \n", liczba_min);
            Console.Write("Wybierz liczbe min (zalecana liczba {0}): ",(max_x*max_y)/10);
            int bomby = Convert.ToInt32(Console.ReadLine());
            Console.Clear();
            wys_logo();
            Console.Write("Zmienić liczbe min z {0} na {1}? (t,n)\n",liczba_min,bomby );
            while(1==1)
            {
                switch(Console.ReadLine())
                {
                    case "t":
                        liczba_min = bomby;
                        wys_menu();
                        return;
                    case "n":
                        wys_menu();
                        return;
                    default:
                        Console.Clear();
                        wys_logo();
                        Console.Write("Podaj poprawną odpowiedz \n");
                        Console.Write("Zmienić liczbe min z {0} na {1}? (t,n) \n", liczba_min, bomby);
                        break;

                }
            }
        }

        public void wys_plansze(int obx,int oby)
        {
            Console.Clear();
            wys_logo();
            int x = obx, y = oby;
            Console.WriteLine("Wielkosc pola {0} x {1}", max_x,max_y);
            for (int i = 0; i < max_x; ++i)
            {
                for (int j = 0; j < max_y; ++j)
                {
                    if (plansza[i,j].odkryte)
                    {
                        if (i == x &&  j == y)
                            Console.ForegroundColor = ConsoleColor.Blue;
                        WriteAtInt(plansza[i, j].wartosc,i,j);
                        Console.ForegroundColor = ConsoleColor.White;
                    }
                    else
                    {
                        if (i == x && j == y)
                           Console.ForegroundColor = ConsoleColor.Blue;
                        WriteAtString("*", i, j);
                        Console.ForegroundColor = ConsoleColor.White;
                    }
                }
                Console.WriteLine(" \n Obecne pole rząd {0} kolumna {1} \n", x+1, y+1);
                Console.Write("z-bomba");
                Console.Write("\n");
            }

            gra(x, y);
        }
        public void gra(int x, int y)
        {
            while (koniec==0)
            {
                if (Console.KeyAvailable)
                {
                    ConsoleKeyInfo key = Console.ReadKey(true);
                    switch(key.Key)
                    {
                        case ConsoleKey.LeftArrow:
                            {
                                if(x>0)
                                    --x;
                                wys_plansze(x, y);
                                break;
                            }
                        case ConsoleKey.RightArrow:
                            {
                                if (x < max_x-1)
                                    ++x;
                                wys_plansze(x, y);
                                break;
                            }
                        case ConsoleKey.UpArrow:
                            {
                                if (y > 0)
                                    --y;
                                wys_plansze(x, y);
                                break;
                            }
                        case ConsoleKey.DownArrow:
                            {
                                if (y < max_y - 1)
                                    ++y;
                                wys_plansze(x, y);
                                break;
                            }
                        case ConsoleKey.Z:
                            {
                                Console.Write("x{0} y{1}",x,y);
                                plansza[x, y].odkryte = true;
                                wys_plansze(x, y);
                                break;
                            }
                        case ConsoleKey.Escape:
                            {
                                return;
                            }
                        default:
                            {
                                break;
                            }

                    }
                }
            }
        }
    }
     

    class Program
    {
        static void Main(string[] args)
        {
            Plansza p = new Plansza();
            while(p.wys_menu());
        }
    }
}

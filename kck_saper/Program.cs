using System;
using Figgle;
namespace kck_saper
{
    class Pole
    {
        public int wartosc;
        public int odkryte; //0-nie odkryte 1-odkryte 2-zaznaczone
    }
    class Plansza
    {
        protected static int origRow = 8;
        protected static int origCol = 2;
        Pole[,] plansza;
        int koniec=0; //0-gra trwa 1-wygrana,2-przegrana
        public int max_x=10;
        public int max_y=10;
        public int liczba_min=20;
        public int liczba_odkrytych_pol = 0;
        protected bool check_int(string a)
        {
            if (int.TryParse(a, out int value))
                return false;
            return true;
        }
        protected bool check_size(int min,int max,int x)
        {
            if (x < min || x > max )
                return true;
            return false;
        }
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
                    plansza[polex,poley].odkryte = 0;
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
            Console.Write("2. Wybierz liczbe min (obecnie {0}, zalecane liczba {1}) \n", liczba_min, (max_x * max_y) / 5);
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
                    koniec = 0;
                    liczba_odkrytych_pol = 0;
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
            string x, y;
            int xi, yi;
            wys_logo();
            Console.Write("Obecny rozmiar pola: {0}x{1} \n", max_x,max_y);
            Console.Write("Podaj liczbe rzędów(x):");
            x = (Console.ReadLine());
            while(check_int(x) || check_size(0, 31, Int32.Parse(x)))
            {
                Console.Write("Podaj poprawną liczbe rzędów(x>0 i x<31):");
                x = (Console.ReadLine());
            }
            xi = Int32.Parse(x);
            Console.Write("Podaj liczbe kolumn(y):");
            y = (Console.ReadLine());
            while (check_int(y) || check_size(0, 31, Int32.Parse(x)))
            {
                Console.Write("Podaj poprawną liczbe kolumn(y>0 i y<31):");
                y = (Console.ReadLine());
            }

            yi = Int32.Parse(y);
                Console.Clear();
            wys_logo();
            Console.Write("Zmienić rozmiar pola z {0}x{1} na {2}x{3}? (t,n)\n", max_x, max_y, xi, yi);
            while (1==1)
            {
                switch (Console.ReadLine())
                {
                    case "t":
                        max_x = xi;
                        max_y = yi;
                        Console.Write("Zmienić liczbe min z {0} na zalacane {1}? (t,n)\n", liczba_min, (max_x * max_y) / 5);
                        while (1 == 1)
                        {
                            switch (Console.ReadLine())
                            {
                                case "t":
                                    liczba_min = max_x * max_y / 5;
                                    wys_menu();
                                    return;
                                case "n":
                                    wys_menu();
                                    return;
                                default:
                                    Console.Clear();
                                    wys_logo();
                                    Console.Write("Podaj poprawną odpowiedz \n");
                                    Console.Write("Zmienić liczbe min z {0} na zalacane {1}? (t,n) \n", liczba_min, (max_x * max_y) / 5);
                                    break;

                            }
                        }
                        return;
                    case "n":
                        wys_menu();
                        return;
                    default:
                        Console.Clear();
                        wys_logo();
                        Console.Write("Podaj poprawną odpowiedz \n");
                        Console.Write("Zmienić rozmiar pola z {0}x{1} na {2}x{3}? (t,n)\n", max_x, max_y, xi, yi);
                        break;
                }
            }

        }
        public void wyb_liczbe_min()
        {
            Console.Clear();
            string bombys;
            int bomby;
            wys_logo();
            Console.Write("Obecna liczba min: {0} \n", liczba_min);
            Console.Write("Wybierz liczbe min (zalecana liczba {0}): ",(max_x*max_y)/5);
            bombys = Console.ReadLine();
            while (check_int(bombys) || Int32.Parse(bombys)>max_x*max_y || Int32.Parse(bombys)<0)
            {
                Console.Write("Podaj poprawną liczbe bomb (zalecana liczba {0},maksymalna: {1}): ", (max_x * max_y) / 5,max_x*max_y-1);
                bombys = (Console.ReadLine());
            }
            bomby = Int32.Parse(bombys);
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
        public void odkryte_zero(int x,int y)
        {
            if (x < 0 || x > max_x - 1 || y < 0 || y > max_y - 1)
                return;
            if (plansza[x, y].odkryte == 1)
                return;
            if (plansza[x, y].wartosc != 9 && plansza[x, y].odkryte == 0)
            {
                plansza[x, y].odkryte = 1;
                ++liczba_odkrytych_pol;
            }
            if (plansza[x,y].wartosc != 0)
                return;
            odkryte_zero(x - 1, y - 1);
            odkryte_zero(x - 1, y);
            odkryte_zero(x - 1, y + 1);
            odkryte_zero(x + 1, y - 1);
            odkryte_zero(x + 1, y);
            odkryte_zero(x + 1, y + 1);
            odkryte_zero(x, y - 1);
            odkryte_zero(x, y);
            odkryte_zero(x, y + 1);
        }
        public void czy_wygrana()
        {
            int a = max_x * max_y - liczba_min; //liczba pól pottrzebna do wygranej
            if (liczba_odkrytych_pol == a)
                koniec = 1;
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
                    if (plansza[i,j].odkryte ==1)
                    {
                        if (i == x &&  j == y)
                            Console.ForegroundColor = ConsoleColor.Blue;
                        if (plansza[i,j].wartosc==9)
                            WriteAtString("B", i, j);
                        else
                            WriteAtInt(plansza[i, j].wartosc,i,j);
                        Console.ForegroundColor = ConsoleColor.White;
                    }
                    else if(plansza[i, j].odkryte == 2)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        if (i == x && j == y)
                            Console.ForegroundColor = ConsoleColor.Blue;
                        WriteAtString("X", i, j);
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
                Console.Write("z-odkryj x-zaznacz bombe Esc-zakoncz");
                Console.Write("\n");
            }
            czy_wygrana();
            if (koniec == 1)
            {
                Console.WriteLine(FiggleFonts.Standard.Render("WYGRANA :D"));
            }
            if (koniec == 2)
            {
                Console.WriteLine(FiggleFonts.Standard.Render("PRZEGRANA :("));
            }
            if (koniec==0)
                gra(x, y);
            else
            {
                Console.WriteLine("Naciśnij dowolny przycisk by wrócić do menu \n", x + 1, y + 1);
                Console.ReadKey();
                wys_menu();
            }
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

                                if (plansza[x, y].wartosc == 9)
                                {
                                    plansza[x, y].odkryte = 1;
                                    koniec = 2;
                                }
                                else if (plansza[x, y].wartosc == 0)
                                    odkryte_zero(x, y);
                                else
                                {
                                    plansza[x, y].odkryte = 1;
                                    ++liczba_odkrytych_pol;
                                }
                                wys_plansze(x, y);
                                break;
                            }
                        case ConsoleKey.X:
                            {
                                if(plansza[x, y].odkryte != 1)
                                plansza[x, y].odkryte = 2;
                                wys_plansze(x, y);
                                break;
                            }
                        case ConsoleKey.Escape:
                            {
                                Console.Clear();
                                wys_logo();
                                Console.Write("Napewno chcesz wylączyć sapera i wrócić do menu(t,n)\n");
                                while (1 == 1)
                                {
                                    switch (Console.ReadLine())
                                    {
                                        case "t":
                                            wys_menu();
                                            return;
                                        case "n":
                                            wys_plansze(x, y);
                                            return;
                                        default:
                                            Console.Clear();
                                            wys_logo();
                                            Console.Write("Podaj poprawną odpowiedz \n");
                                            Console.Write("Napewno chcesz wylączyć sapera i wrócić do menu(t,n)\n");
                                            break;

                                    }
                                }
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

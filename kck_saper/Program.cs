using System;

namespace kck_saper
{
    class Pole
    {
        public int wartosc;
        public bool odkryte;
    }
    class Plansza
    {
        Pole[,] plansza;
        int max_x;
        int max_y;
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
        public void wys_plansze()
        {
            Console.WriteLine("Wielkosc pola {0} x {1}", max_x,max_y);
            for (int i = 0; i < max_x; ++i)
            {
                for (int j = 0; j < max_y; ++j)
                {
                    Console.Write(plansza[i, j].wartosc);
                }
                Console.Write("\n");
            }

                Console.ReadKey();
        }
    }
     

    class Program
    {
        static void Main(string[] args)
        {
            Plansza p = new Plansza();
            p.generuj_plasze(10,10);
            p.losuj_pozycje(10);
            p.wys_plansze();
        }
    }
}

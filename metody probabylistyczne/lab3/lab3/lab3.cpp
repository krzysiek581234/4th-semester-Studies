#include <iostream>
#include <time.h>
using namespace std;

const int N = 100000;


double inverseCDF(double y)
{
    // Wstaw tutaj definicję odwrotnej funkcji dystrybuanty dla
    // konkretnego rozkładu prawdopodobieństwa
    // W tym przykładzie użyto funkcji odwrotnej do rozkładu jednostajnego,
    // która po prostu przekształca y z przedziału [0,1] na x z przedziału [50,150]
    return 100.0 * y + 50.0;
}

int main()
{
    srand(time(0));
    int tab[4] = { 0,0,0,0 };
    for (int i = 0; i < N; i++)
    {
        double r = rand() % 1000;
        r = r / 1000;
        if (r < 0.2 && r >= 0)
        {
            //1
            tab[0] += 1;
        }
        else if (r < 0.6 && r >= 0.2)
        {
            //2
            tab[1] += 1;
        }
        else if (r < 0.9 && r >= 0.6)
        {
            //3
            tab[2] += 1;
        }
        else if (r <= 1 && r >= 0.9)
        {
            //4
            tab[3] += 1;
        }
    }
    int suma = 0;
    for (int i = 0; i < 4; i++)
    {
        cout <<"Liczba: " << i<<" "<< tab[i] << endl;
        suma += tab[i];
    }
    cout <<"Suma: " << suma << endl;
    //zadanie 2
    cout << endl;
    int tab2[10];
    for (int i = 0; i < 10; i++)
    {
        tab2[i] = 0;
    }
    for (int i = 0; i < N; i++)
    {
        double y = rand() % 1000;
        y = y / 1000;
        //cout << y << endl;
        double x = inverseCDF(y);
        int index = (int)(x - 50) / 10;
        //cout << index << endl;
        tab2[index] += 1;
    }
    for (int i = 0; i < 10; i++)
    {
        cout<<"Przedzial o numerze"<< i<< " " << tab2[i] << endl;;
    }
}

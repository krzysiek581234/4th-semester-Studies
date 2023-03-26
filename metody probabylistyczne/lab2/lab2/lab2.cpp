#include <iostream>
#include <cmath>
using namespace std;

long long Xn = 15;
const unsigned int M = pow(2, 31);
const unsigned int a = 69069;
const unsigned int c = 1;
const unsigned int N = 100000;
bool bits[32] = {0,0,0,0,0,0,0,0,
                 0,0,0,0,0,0,0,0,
                 0,0,0,0,0,0,0,0,
                 0,1,0,0,1,0,1,1};

long long losuj(unsigned int Xo)
{
    return (a * Xo + c) % M;
}
const int p = 7;
const int q = 3;
bool Xor(int a, int b)
{
    if (a == b)
    {
        return false;
    }
    else
    {
        true;
    }
}

void bitrand(bool* prevbit, bool* nextbit)
{
    int a;
    int b;

    for (int j = 32; j >= 25; j--)
    {
        a = prevbit[j - p];
        b = prevbit[j - q];
        nextbit[j] = Xor(a, b);
    }

}

int main()
{
    int tab[N];
    int rozklad[10];
    for (int i = 0; i < 10; i++)
    {
        rozklad[i] = 0;
    }

    for (int i = 0; i < N; i++)
    {
        tab[i] = losuj(Xn);
        Xn = tab[i];
        int index = 10 * Xn / M;
        rozklad[index] ++;
    }
    for (int i = 0; i < 10; i++)
    {
        cout << rozklad[i] << endl;
    }
    //pierwsza liczba
    /*
    bool bigtable[N][32];
    int a;
    int b;
    for (int g = 25; g > 0; g--)
    {
        a = bits[g - p];
        b = bits[g - q];
        bits[a] = Xor(a, b);
    }
    for (int i = 0; i < 32; i++)
    {
        bigtable[0][i] = bits[i];
    }
    for (int i = 1; i < N; i++)
    {
         bitrand(bits, bigtable[i]);
         for (int i = 0; i < 32; i++)
         {
             bits[i] = bigtable[0][i];
         }
    }
    */
    

}

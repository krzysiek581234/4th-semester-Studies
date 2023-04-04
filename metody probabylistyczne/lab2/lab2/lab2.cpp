#include <iostream>
#include <cmath>
using namespace std;

long long Xn = 15;
const unsigned int M = pow(2, 31);
const unsigned long int W = 4294967295;
const unsigned int a = 69069;
const unsigned int c = 1;
const unsigned int N = 100000;
short int bits[32] = { 1,0,1,1,0,0,0,1,
                      0,1,0,0,1,1,1,1,
                      1,1,1,0,0,0,0,0,
                      0,1,1,1,1,0,0,0 };


long long losuj(unsigned int Xo)
{
    return (a * Xo + c) % M;
}
const int p = 7;
const int q = 3;
short int Xor(int a, int b)
{
    if (a == b)
    {
        return 0;
    }
    else
    {
        return 1;
    }
}

unsigned int converttonumbe(short int* number)
{
    unsigned int  zmienna = 0;
    for (int i = 31; i >= 0; i--)
    {
        zmienna += number[i] * pow(2, i);
    }
    return zmienna;
}
void printbits(short int* a)
{
    for (int i = 31; i >= 0; i--)
    {

        cout << a[i];
        if (i % 4 == 0) cout << " ";

    }
    cout << endl;
}
short int* bitrand(short int* prevbit, short int* nextbit)
{
    int a;
    int b;
    nextbit[0] = Xor(prevbit[6], prevbit[2]);
    nextbit[1] = Xor(prevbit[5], prevbit[1]);
    nextbit[2] = Xor(prevbit[4], prevbit[0]);
    nextbit[3] = Xor(prevbit[3], nextbit[0]);
    nextbit[4] = Xor(prevbit[2], nextbit[1]);
    nextbit[5] = Xor(prevbit[1], nextbit[2]);
    nextbit[6] = Xor(prevbit[0], nextbit[3]);

    for (int i = 7; i < 32; i++)
    {
        a = nextbit[i - p];
        b = nextbit[i - q];
        nextbit[i] = Xor(a, b);
    }
    //printbits(nextbit);
    return nextbit;
}
void copytable(short int* prev, short int* next)
{
    for (int i = 0; i < 32; i++)
    {
        next[i] = prev[i];
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
        //Xn poprzednia wartość
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
    //zadanie 2
    int a, b;
    short int prev[32];
    short int next[32];
    cout << endl;
    //cout<<converttonumbe(bits)<<endl;
    copytable(bits, prev);
    int rozklad2[10] = {0,0,0,0,0,0,0,0,0,0};
    double div = pow(2, 32);
    for (int i = 1; i < N; i++)
    {
        bitrand(prev, next);
        copytable(next, prev);
        unsigned int temp = converttonumbe(next);
        //printbits(next);
        //cout << temp << endl;

        
        double cos = temp / div;
        cos = cos * 10;
        rozklad2[int(cos)]++;
    }
    int suma = 0;
    for (int i = 0; i < 10; i++)
    {
        cout << rozklad2[i] << endl;
        suma += rozklad2[i];
    }

}

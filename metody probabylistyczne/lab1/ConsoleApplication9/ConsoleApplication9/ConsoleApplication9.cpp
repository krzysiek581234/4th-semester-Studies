#include <iostream>
#include <vector>
using namespace std;
int stala = 0;
void wierzchołek(int n, int m, int* tablica)
{
    m++;
    if (m == n)
    {
        for (int i = 0; i < n; i++)
        {
            cout << tablica[i];
        }
        cout << endl;
        stala++;
        return;
    }
    for (int i = 0; i < n; i++)
    {
        tablica[m] = i;
        wierzchołek(n, m, tablica);

    }
}
//permutacja z powtórzeniami

int main()
{
    int n = 4;
    int tablica[4];
    //cin >> n;
    wierzchołek(n, -1, tablica);
    cout << stala << endl;
}

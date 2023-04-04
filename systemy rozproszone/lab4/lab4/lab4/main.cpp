# include <windows.h>
# include <stdio.h>
# include <conio.h>
#define NUMBEROFTHRED 5
// - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
# pragma argsused
struct dane_dla_watku // tablica zawiera dane , ktore otrzymaja watki
{
	char nazwa[50];
	int parametr;
} dane[NUMBEROFTHRED] = { { "[1]" , 5 } , { "[2]" , 8 } , { "[3]" , 12 }, { "[4]" , 18 }, { "[5]" , 20 } };
// priorytety watkow

void gotoxy(int x, int y)
{
	COORD c;
	c.X = x - 1;
	c.Y = y - 1;
	SetConsoleCursorPosition(GetStdHandle(STD_OUTPUT_HANDLE), c);
}
int priorytety[5] = { THREAD_PRIORITY_BELOW_NORMAL ,
THREAD_PRIORITY_NORMAL , THREAD_PRIORITY_ABOVE_NORMAL, THREAD_PRIORITY_NORMAL, THREAD_PRIORITY_NORMAL
};
HANDLE watki[NUMBEROFTHRED]; // dojscia ( uchwyty ) watkow
// deklaracja funkcji watku
DWORD WINAPI funkcja_watku(void* argumenty);
// - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
int main(int argc, char** argv)
{

	DWORD id; // identyfikator watku
	system("cls");
	printf(" Uruchomienie programu \ n ");
	// tworzenie watkow
	//zmienic liczbê watkow z 3 na 5
	for (int i = 0; i < NUMBEROFTHRED; i++)
	{
		watki[i] = CreateThread(
			NULL, // atrybuty bezpieczenstwa
			0, // inicjalna wielkosc stosu
			funkcja_watku, // funkcja watku
			(void*)&dane[i],// dane dla funkcji watku
			0, // flagi utworzenia
			&id);
		if (watki[i] != INVALID_HANDLE_VALUE)
		{
			printf(" Utworzylem watek %s o id %x \n ", dane[i].nazwa, id);
			// ustawienie priorytetu
			SetThreadPriority(watki[i], priorytety[i]);
		}
	}

	Sleep(20000); // uspienie watku glownego na 20 s
	return 0;
}
// - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
// trzy takie funkcje pracuja wspolbieznie w programie
DWORD WINAPI funkcja_watku(void* argumenty)
{
	unsigned int licznik = 0;
	// rzutowanie struktury na wlasny wskaznik
	struct dane_dla_watku* moje_dane = (struct dane_dla_watku*)argumenty;
	// wyswietlenie informacji o uruchomieniu
	gotoxy(1, moje_dane->parametr);
	printf("% s ", moje_dane -> nazwa);
	Sleep(1000);
	// praca , watki sa terminowane przez zakonczenie programu
// - funkcji main
	int i = 0;
	while (1)
	{
		if (i == 20)
		{
			TerminateThread(watki[1], 0);
		}
		else
		{
			i++;
		}
		gotoxy(licznik++ / 5000 + 5, moje_dane -> parametr);
		printf(".");

	}
	return 0;
}
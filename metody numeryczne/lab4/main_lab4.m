clc
clear all
close all


a = 1;   
b = 60000;
eps = 1e-3;

%zadanie 1 bisekcja
[xvect, xdif, fx, it_cnt] = bisection(@compute_impedance,a,b,eps);
figure('Name','Czas - bisekcja - przyblizenie');

plot(1:it_cnt, xvect)
title("Przybliżenie N w kolejnych iteracjach - metoda bisekcji");
ylabel("Liczba parametrów wejściowych N");
xlabel("Numer iteracji");
saveas(gcf, 'zad1_przyblizenia_bisekcja.png');

figure('Name','Czas - bisekcja - Różnica');
semilogy(1:it_cnt, xdif)
title("Różnice pomiędzy wartościami N - metody bisekcji");
ylabel("Różnica pomiędzy obecną i poprzednią wartością N");
xlabel("Numer iteracji");
saveas(gcf, 'zad1_roznice_bisekcja.png');

[xvect,xdif,fx,it_cnt] = secant(@compute_impedance, 1, 60000, 10^-3)
%zadanie 1 sieczne

figure('Name','Czas - sieczne - przyblizenie');
plot(1:it_cnt, xvect)
title("wartość kolejnego przybliżenia N - metody siecznych");
ylabel("Liczba parametrów wejściowych N");
xlabel("Numer iteracji");
saveas(gcf, 'zad1_przyblizenia_sieczne.png');

figure('Name','Czas - Sieczne - Różnica');
semilogy(1:it_cnt, xdif)
title("różnice pomiędzy wartościami N - metody siecznych");
ylabel("Różnica pomiędzy obecną i poprzednią wartością N");
xlabel("Numer iteracji");
saveas(gcf, 'zad1_roznice_sieczne.png');

%zadanie 2 impedancja
[xvect, xdif, fx, it_cnt] = bisection(@impedancja,0,50,10^-12);
figure('Name','impredancja - bisekcja - przyblizenie');
plot(1:it_cnt, xvect)
title("Przybliżenie impedancji - metoda bisekcji");
ylabel("Wartosc w");
xlabel("Numer iteracji");
saveas(gcf, 'zad1_impedancji_bisekcja.png');

figure('Name','impredancja - bisekcja - różnica');
semilogy(1:it_cnt, xdif)
title("Różnice pomiędzy wartościami w - metody bisekcji");
ylabel("Różnica pomiędzy obecną i poprzednią wartością w");
xlabel("Numer iteracji");
saveas(gcf, 'zad1_roznice_impedancji_bisekcja.png');

[xvect,xdif,fx,it_cnt] = secant(@impedancja, 0, 50, 10^-12)
figure('Name','impredancja - sieczne - przyblizenie');
plot(1:it_cnt, xvect)
title("wartość kolejnego przybliżenia W - metody siecznych");
ylabel("Liczba parametrów wejściowych W");
xlabel("Numer iteracji");
saveas(gcf, 'zad1_przyblizenia_impedancja_sieczne.png');

figure('Name','impredancja - sieczne - różnica');
semilogy(1:it_cnt, xdif)
title("różnice pomiędzy wartościami w - metody siecznych");
ylabel("Różnica pomiędzy obecną i poprzednią wartością N");
xlabel("Numer iteracji");
saveas(gcf, 'zad1_roznice_impedancja_sieczne.png');

%zadanie 3 speed

[xvect, xdif, fx, it_cnt] = bisection(@speed,0,50,10^-12);
figure('Name','Speed - bisekcja - przyblizenie');
plot(1:it_cnt, xvect)
title("Przybliżenie impedancji - metoda bisekcji");
ylabel("Wartosc w");
xlabel("Numer iteracji");
saveas(gcf, 'zad3_Speed_bisekcja.png');

figure('Name','Speed - bisekcja - różnica');
semilogy(1:it_cnt, xdif)
title("Różnice pomiędzy wartościami w - metody bisekcji");
ylabel("Różnica pomiędzy obecną i poprzednią wartością w");
xlabel("Numer iteracji");
saveas(gcf, 'zad3_roznice_Speed_bisekcja.png');

[xvect,xdif,fx,it_cnt] = secant(@speed, 0, 50, 10^-12)
figure('Name','Speed - sieczne - przyblizenie');
plot(1:it_cnt, xvect)
title("wartość kolejnego przybliżenia W - metody siecznych");
ylabel("Liczba parametrów wejściowych W");
xlabel("Numer iteracji");
saveas(gcf, 'zad3_przyblizenia_Speed_sieczne.png');

figure('Name','Speed - sieczne - różnica');
semilogy(1:it_cnt, xdif)
title("różnice pomiędzy wartościami w - metody siecznych");
ylabel("Różnica pomiędzy obecną i poprzednią wartością N");
xlabel("Numer iteracji");
saveas(gcf, 'zad3_roznice_Speed_sieczne.png');

%zadanie tan
options = optimset('Display', 'iter');
fzero(@tan,6, options);
fzero(@tan,4.5, options);

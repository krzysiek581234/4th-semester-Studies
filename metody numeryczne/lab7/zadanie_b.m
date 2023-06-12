clc
clear all


%------------------------------------------
load dane_jezioro   % dane XX, YY, FF sa potrzebne jedynie do wizualizacji problemu. 
surf(XX,YY,FF)
shading interp
axis equal
%------------------------------------------
%% Zdefiniuj funkcję, która wyznacza wartość gęstości prawdopodobieństwa zgodnie z 
% W celu weryfikacji poprawności napisanego kodu wygeneruj wykres gęstości prawdopodobieństwa f(x).
x = 0:0.1:20;
wynik = zeros(length(x), 1);

for index = 1:length(x)
    wynik(index) = gestoscPrawdopodobienstwa(x(index));
end

figure()
plot(x, wynik);
title('Funkcja gestosci prawdopodobienstwa wystąpienia awarii sprzętu');
xlabel('Lata używania sprzętu');
ylabel('Prawdopodobienstwo awarii');
saveas(gcf, 'gestosc.png')
%% Prostokąt
clc
clear all
load P_ref

n = 5 % zakres całkowania
errors = []
zakres = 5:50:10^4;
pole = zeros(length(zakres));
index = 1;


for i=zakres % i - liczba prostokątów
    x = n/i; % długość jednego przedziału
    liczbaPros = i/n;
     
    for j = 1:i
        h = gestoscPrawdopodobienstwa((j + 0.5)*x);
        pole(index) = pole(index) + x*h;
    end
    errors = [errors,abs(P_ref - pole(index))];
    index = index +1;
end

figure();
loglog(errors);
title('Metoda Prostokątów - Błąd zależnie od ilościi punktów');
xlabel('Ilość punktów');
ylabel('Błąd liczonej całki');
saveas(gcf, 'prostokatow.png')
%% metoda trapezów

clc
clear all
load P_ref

n = 5 % zakres całkowania
errors = []
zakres = 5:50:10^4;
pole = zeros(length(zakres));
index = 1;


for i=zakres % i - liczba prostokątów
    x = n/i; % długość jednego przedziału
    liczbaPros = i/n;
     
    for j = 1:i
        h1 = gestoscPrawdopodobienstwa(j *x);
        h2 = gestoscPrawdopodobienstwa((j-1) *x);

        pole(index) = pole(index) + x*(h1+h2)/2;
    end
    errors = [errors,abs(P_ref - pole(index))];
    index = index +1;
end

figure();
loglog(errors);
title('Metoda Trapezów - Błąd zależnie od ilościi punktów');
xlabel('Ilość punktów');
ylabel('Błąd liczonej całki');
saveas(gcf, 'Trapez.png')

%% metoda Simpsona:

clc
clear all
load P_ref

n = 5 % zakres całkowania
errors = []
zakres = 5:50:10^4;
pole = zeros(length(zakres));
index = 1;


for i=zakres % i - liczba prostokątów
    x = n/i; % długość jednego przedziału
    liczbaPros = i/n;
     
    for j = 1:i
        xi1 = x* (j+1);
        xi = x *j ;
        param1 = (xi1 - xi)/6;
        param2 = gestoscPrawdopodobienstwa(xi);
        param3 = 4 * gestoscPrawdopodobienstwa((xi1+xi)/2);
        param4 = gestoscPrawdopodobienstwa(xi1);
        tempPole = param1 *(param2 + param3 + param4); 

        pole(index) = pole(index) + tempPole;
    end
    errors = [errors,abs(P_ref - pole(index))];
    index = index +1;
end

figure();
loglog(errors);
title('Metoda Simpsona - Błąd zależnie od ilościi punktów');
xlabel('Ilość punktów');
ylabel('Błąd liczonej całki');
saveas(gcf, 'Simpsona.png')


%% metoda monte carlo

clc
clear all
load P_ref

n = 5 % zakres całkowania
errors = []
zakres = 5:50:10^4;
pole = zeros(length(zakres));
index = 1;


for i=zakres % i - liczba punktów
    x = rand(1,i)*n; % generowanie punktów x
    y = rand(1,i); % generowanie punktów y
    
    below_curve = sum(y < gestoscPrawdopodobienstwa(x)); % ilość punktów pod krzywą
    
    pole(index) = below_curve / i * n; % pole pod krzywą = ilość punktów pod krzywą / całkowita ilość punktów * zakres całkowania
    
    errors = [errors,abs(P_ref - pole(index))];
    index = index +1;
end

figure();
loglog(errors);
title('Metoda monte carlo - Błąd zależnie od ilościi punktów');
xlabel('Ilość punktów');
ylabel('Błąd liczonej całki');
saveas(gcf, 'monteCarlo.png')

%% Określenia czasów działania
clc
clear all

N = 10^7;
n =5;
x = n/N; % długość jednego przedziału
liczbaPros = N/n;
czasy = [];
pole = 0;
%Prostokatow
tic
for j = 1:N
    h = gestoscPrawdopodobienstwa((j + 0.5)*x);
    pole = pole + x*h;
end
czasy(end+1) = toc;

%Trapezow
pole = 0;
tic
for j = 1:N
    h1 = gestoscPrawdopodobienstwa(j *x);
    h2 = gestoscPrawdopodobienstwa((j-1) *x);

    pole = pole + x*(h1+h2)/2;
end
czasy(end+1) = toc;

% Simsona
pole = 0;
tic
for j = 1:N
    xi1 = x* (j+1);
    xi = x *j ;
    param1 = (xi1 - xi)/6;
    param2 = gestoscPrawdopodobienstwa(xi);
    param3 = 4 * gestoscPrawdopodobienstwa((xi1+xi)/2);
    param4 = gestoscPrawdopodobienstwa(xi1);
    tempPole = param1 *(param2 + param3 + param4); 

    pole = pole + tempPole;
end
czasy(end+1) = toc;

%metoda monte carlo
pole = 0;
tic
for i=1:N % i - liczba punktów
    x = rand()*n; % generowanie punktów x
    y = rand(); % generowanie punktów y
    
    below_curve = sum(y < gestoscPrawdopodobienstwa(x)); % ilość punktów pod krzywą
    
    pole = below_curve / i * n; % pole pod krzywą = ilość punktów pod krzywą / całkowita ilość punktów * zakres całkowania

end
czasy(end+1) = toc;


%wykres czasow
figure()
labels = categorical({'Prostokatow','Trapezow','Simpsona','Monte Carlo'});
bar(labels, czasy);
title("Porownanie czasow wykonania roznych metod");
xlabel("Metoda");
ylabel("Czas wykonania [s]");
saveas(gcf, 'czas.png')

%% Zadanie 2
clc
clear all
n = 5;
N = 10^5;
zakres = 1:N;

ilosc =0;
for i=zakres
    xrand = rand()* 100;
    yrand = rand()* 100;
    zrand = rand() * 50 - 50;
    result = glebokosc(xrand,yrand);
    if zrand > result
        ilosc = ilosc +1;
    end
end
 V = (ilosc / N) * 100 * 100 * 50

%------------------------------------------
% Implementacja Monte Carlo dla f(x,y) w celu obliczenia objetosci wody w zbiorniku wodnym. 
% Calka = ?
% Nalezy skorzystac z nastepujacej funkcji:
% z = glebokosc(x,y); % wyznaczanie glebokosci jeziora w punkcie (x,y),
% gdzie x i y sa losowane
%------------------------------------------




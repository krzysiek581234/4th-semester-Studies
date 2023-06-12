%% Zadanie A
clc
clear all
close all
d = 0.85;
N = 10;
density = 3; % parametr decydujacy o gestosci polaczen miedzy stronami
[edges] = generate_network(N, density);


%% Zadanie B - generacja macierzy I, A, B i wektora b
B = sparse(edges(2,:),edges(1,:),1,N,N); % B -macierz sąsiedztwa
issparse(B)
I = speye(N); % I - macierz jednostkowa
L = sum(B); % L liczba odnoścników wychodziących z i-tej strony
b = (1-d)/N * ones(N, 1); %b mają wartość (1 − d)/N, gdzie d jest współczynnikiem tłumienia.
A = spdiags(1./L',0:0,N,N); % macierz jednostkowa



whos('A', 'B', 'I')


%% zadanie C
M = sparse(I - d * B * A);
r = M\b
r
%% Zadanie D
 clc
 clear all
 close all

N = [500, 1000, 3000, 6000, 12000];
density = 10; % parametr decydujacy o gestosci polaczen miedzy stronami
d = 0.85

 for i = 1:5
     [edges] = generate_network(N(i), density);
     B = sparse(edges(2,:),edges(1,:),1,N(i),N(i)); % B -macierz sąsiedztwa
     I = speye(N(i)); % I - macierz jednostkowa
     L = sum(B); % L liczba odnoścników wychodziących z i-tej strony
     var = (1-d)/N(i);

     b =  ones(N(i), 1) * var;
     A = spdiags(1./L',0:0,N(i),N(i)); % macierz jednostkowa
     M = sparse(I - d * B * A);
     
     tic
     r = M\b;
     czas_Gauss(i) = toc;
 end
 figure('Name','D');
 plot(N, czas_Gauss)
 title("Sposob bezposredni - Gauss")
 xlabel("wielkosc Macierzy");
 ylabel("Czas");
print -dpng 'zadanieD.png'

%% Zadanie E

 clc
 clear all
 close all
 N = [500, 1000, 3000, 6000, 12000];
 density = 10; % parametr decydujacy o gestosci polaczen miedzy stronami
 d = 0.85;
 border = 10^(-14);
 for i = 1:5
     [edges] = generate_network(N(i), density);
     B = sparse(edges(2,:),edges(1,:),1,N(i),N(i)); % B -macierz sąsiedztwa
     I = speye(N(i)); % I - macierz jednostkowa
     L = sum(B); % L liczba odnoścników wychodziących z i-tej strony
     var = (1-d)/N(i);
     b =  ones(N(i), 1) * var;
     A = spdiags(1./L',0:0,N(i),N(i)); % macierz jednostkowa
     M = sparse(I - d * B * A);
     L = tril(M,-1) ;
     U = triu(M,1) ;
     D = diag(diag(M));
     r = ones(N(i),1);
     licznik(i) = 0;
     tic
     while(true)
        licznik(i) = licznik(i) +1;
        r = -D \(L + U)*r + D \ b;
        res = M *r -b;
        tab(i,licznik(i)) = norm(res);
        if(norm(res) <= border)
            break;
        end
     end
     czas_Jacobi(i) = toc;
 end
 figure('Name','czas wyznaczenia rozwiązania jacob');
 plot(N, czas_Jacobi)
 title("Zadanie E - czas analizy - Jacobiego");
 ylabel("Czas [s]");
 xlabel("Rozmiar macierzy");
print -dpng 'zadanieE_czas.png'

figure('Name','liczba iteracji');
plot(N, licznik)
title("Zadanie E - liczba iteracji - Jacobiego");
ylabel("Liczba iteracji");
xlabel("Rozmiar macierzy");
print -dpng 'zadanieE_iteracje.png'

figure('Name', 'norma z residuum');
semilogy(tab(2, 1:licznik(2)));
title('Zadanie E - Jacobiego - norma z residum dla macierzy o wielkosci N = 1000')
ylabel("Norma");
xlabel("Nr iteracji");
print -dpng 'zadanieE_norma.png'


%% Zadanie F

 clc
 vars = who; % Pobierz nazwy wszystkich zmiennych
 vars(ismember(vars, 'czas_Jacobi')) = []; % Usuń nazwę wektora do zachowania
 clear(vars{:}); % Usuń wszystkie pozostałe zmienne
 close all
 N = [500, 1000, 3000, 6000, 12000];
 density = 10; % parametr decydujacy o gestosci polaczen miedzy stronami
 d = 0.85;
 border = 10^(-14);
 for i = 1:5
     [edges] = generate_network(N(i), density);
     B = sparse(edges(2,:),edges(1,:),1,N(i),N(i)); % B -macierz sąsiedztwa
     I = speye(N(i)); % I - macierz jednostkowa
     L = sum(B); % L liczba odnoścników wychodziących z i-tej strony
     var = (1-d)/N(i);
     b =  ones(N(i), 1) * var;
     A = spdiags(1./L',0:0,N(i),N(i)); % macierz jednostkowa
     M = sparse(I - d * B * A);
     L = tril(M,-1) ;
     U = triu(M,1) ;
     D = diag(diag(M));
     r = ones(N(i),1);
     licznik(i) = 0;
     tic
     while(true)
        licznik(i) = licznik(i) +1;
        r = -(D + L)\(U*r)+(D+L)\b;
        res = M *r -b;
        tab(i,licznik(i)) = norm(res);
        if(norm(res) <= border)
            break;
        end
     end
     czas_Gauss(i) = toc;
 end
 figure('Name','czas wyznaczenia rozwiązania Gauss');
 plot(N, czas_Gauss)
 title("Zadanie F - czas analizy - Gaussa–Seidla");
 ylabel("Czas [s]");
 xlabel("Rozmiar macierzy");
print -dpng 'zadanieF_czas.png'

figure('Name','4');
plot(N, licznik)
title("Zadanie F - liczba iteracji - Gaussa–Seidla");
ylabel("Liczba iteracji");
xlabel("Rozmiar macierzy");
print -dpng 'zadanieF_iteracje.png'

figure('Name', '5');
semilogy(tab(2, 1:licznik(2)));
title('Zadanie F - Gaussa–Seidla - norma z residum dla macierzy o wielkosci N = 1000')
ylabel("Norma");
xlabel("Nr iteracji");
print -dpng 'zadanieF_norma.png'

% figure('Name', '6');
% plot(N, czas_Gauss, N, czas_Jacobi);
% legend('Gauss', 'Jacobi');
% title('Zadanie F - Prównanie Gaussa–Seidla i Jacobiego');
% ylabel("Czas [s]");
% xlabel("Rozmiar macierzy");
% print -dpng 'zadanieF_porównanie.png'

%% Zadanie G
clc
clear all
close all
border = 10^(-14); % zadana dokładność metod iteracyjnych
licznik(1) = 0;
licznik(2) = 0;
load("Dane_Filtr_Dielektryczny_lab3_MN.mat")
r = M\b;
L = tril(M,-1) ;
U = triu(M,1) ;
D = diag(diag(M));
% Gauss
tab1 = [];
tab2 = [];
quit = 0;
while(true)
   quit = quit +1;
   r = -(D + L)\(U*r)+(D+L)\b;
   res = M *r -b;
   tab1(end+1) = norm(res);
   %norm(res)
   if(norm(res) <= border || quit >= 500)
       break;
   end
end
%Jacobi
quit = 0;
while(true)
   quit = quit +1;
   r = -D \(L + U)*r + D \ b;
   res2 = M *r -b;
   tab2(end+1) = norm(res2);
   %norm(res)
   if(norm(res2) <= border || quit >= 500)
       break;
   end
end

figure('Name', 'Zadanie G - Porównanie Gaussa-Seidla i Jacobiego', 'NumberTitle', 'off');
semilogy(tab1);
hold on
semilogy(tab2);
legend('Gauss', 'Jacobi');
title('Zadanie G - Porównanie Gaussa-Seidla i Jacobiego');
ylabel('Wartość normalna res');
xlabel('Rozmiar macierzy');
print -dpng 'zadanieG_porównanie.png'







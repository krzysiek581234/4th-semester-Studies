clc
%clear all
close all

% odpowiednie fragmenty kodu mozna wykonac poprzez zaznaczenie i wcisniecie F9 w Matlabie
% komentowanie/odkomentowywanie: ctrl+r / ctrl+t

% Zadanie A
%------------------
N = 10;
density = 3; % parametr decydujacy o gestosci polaczen miedzy stronami
[edges] = generate_network(N, density);

%-----------------

% Zadanie B

d = 0.85;
B = sparse(edges(2,:),edges(1,:),ones(1,size(edges,2)),N,N); % B -macierz sąsiedztwa
I = speye(N); % I - macierz jednostkowa
L = sum(B); % L liczba odnoścników wychodziących z i-tej strony
b = (1-d)/N * ones(N, 1); %b mają wartość (1 − d)/N, gdzie d jest współczynnikiem tłumienia.
A = spdiags(1./L',0:0,N,N); % macierz jednostkowa
M = sparse(I - d * B * A);

%------------------
% generacja macierzy I, A, B i wektora b
% macierze A, B i I musza byc przechowywane w formacie sparse (rzadkim)


%-----------------
diary('sparse_test.txt')
whos('A', 'B', 'I', 'M', 'b')
diary off



% Zadanie D
%------------------
clc
clear all
close all

N = [500, 1000, 3000, 6000, 12000];


for i = 1:5
    tic
    % obliczenia start

    % obliczenia stop
    czas_Gauss(i) = toc;
end
plot(N, czas_Gauss)
%------------------



% Zadanie E
%------------------
clc
clear all
close all

% sprawdz przykladowe dzialanie funkcji tril, triu, diag:
 Z = rand(4,4)
 tril(Z,-1) 
 triu(Z,1) 
 diag(diag(Z))


% for i = 1:5
%     tic
%     % obliczenia start
% 
%     % obliczenia stop
%     czas_Jacobi(i) = toc;
% end
% plot(N, czas_Jacobi)
%------------------


% Zadanie F
%------------------


% Zadanie G
%------------------







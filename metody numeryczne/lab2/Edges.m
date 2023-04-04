close all
clear all

edges = sparse([1, 1, 2, 2, 2, 3, 3, 3, 4, 4, 5, 5, 6, 6, 7;
                4, 6, 3, 4, 5, 5, 6, 7, 5, 6, 4, 6, 4, 7, 6]);

N = 7;
d = 0.85;
%size(edges,2)% ma zwrócić tylko liczbę kolumn macierzy
B = sparse(edges(2,:),edges(1,:),1,N,N); % B -macierz sąsiedztwa
%B = sparse(edges(2,:),edges(1,:),ones(1,size(edges,2)),N,N); % B -macierz sąsiedztwa
I = speye(N); % I - macierz jednostkowa
I
L = sum(B); % L liczba odnoścników wychodziących z i-tej strony
b = (1-d)/N * ones(N, 1); %b mają wartość (1 − d)/N, gdzie d jest współczynnikiem tłumienia.
A = spdiags(1./L',0:0,N,N); % macierz jednostkowa
M = sparse(I - d * B * A);

diary('sparse_test.txt')
whos('A', 'B', 'I', 'M', 'b')
diary off

spy(B)

title("Macierz B (rzadka) - elementy niezerowe")
print -dpng spy_b
%pause(4);

r = M\b;
figure('Name', 'PageRank');
bar(r)
title("Wartości PageRank dla konkretnych stron");
xlabel("Strona");
ylabel("Wartość PageRank")
print -dpng bar.png
function [Z] = impedancja(w)
R = 725;
C =  8 * 10^-5;
L = 2;
mianownik2 = w*C - 1/(w*L);
mianownik = 1/R^2 + mianownik2^2;
Z = (1 / sqrt(mianownik)) - 75;

end
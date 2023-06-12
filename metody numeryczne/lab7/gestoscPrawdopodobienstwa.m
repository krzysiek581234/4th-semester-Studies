function [wynik] = gestoscPrawdopodobienstwa(x)
    ni = 10;
    q = 3;
    mianownik = (q * sqrt(2*pi));
    licznik = -(x-ni).^2;
    licznik2 = licznik / (2*q^2);
    wynik = exp(licznik2) / mianownik;
end

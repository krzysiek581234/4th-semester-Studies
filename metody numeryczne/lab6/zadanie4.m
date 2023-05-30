clc
clear all
close all

warning('off','all')

load trajektoria1.mat
N = 60;
xa = aproksymacjaWielomianowa(n,x,N);
ya = aproksymacjaWielomianowa(n,y, N);
za = aproksymacjaWielomianowa(n,z, N);

plot3(x,y,z, 'o');
title("Odczyt lokaliacji drona z Aproksymacja jego trajektorii");
xlabel("X [m]");
ylabel("Y [m]");
zlabel("Z [m]");

hold on;
plot3(xa,ya,za,'g','lineWidth',4);
grid on
axis equal
saveas(gcf, 'zad4.png');
clc
clear all
close all

warning('off','all')

load trajektoria2.mat
N = 60;
err = []; 
M = size(n, 2);
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
saveas(gcf, 'zad5a.png');


for N=1:71
    xa = aproksymacjaWielomianowa(n,x, N);
    ya = aproksymacjaWielomianowa(n,y, N);
    za = aproksymacjaWielomianowa(n,z, N); 
    errx =sum((x-xa).^2);
    errx = sqrt(errx);
    errx = errx/ M;
    erry =sum((x-xa).^2);
    erry = sqrt(errx);
    erry = errx/ M;
    errz =sum((x-xa).^2);
    errz = sqrt(errx);
    errz = errx/ M;
    err(end+1) = [errx+erry+errz];
end
figure();
semilogy(err);
title("Wykres bledu aproksymacji");
xlabel("N");
ylabel("Error");
grid();
saveas(gcf, 'zad5b.png');

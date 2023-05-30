clc
clear all
close all

warning('off','all')

load trajektoria1

% N = 60;
% xa = aproksymacjaWielomianowa(n, x, N);  % aproksymacja wspolrzednej x

plot3(x,y,z,'o');
grid on;
axis equal; 
xlabel("X");
ylabel("Y");
zlabel("Z");
grid on
axis equal
saveas(gcf, 'zad2.png');
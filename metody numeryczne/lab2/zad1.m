close all

a = 2;
r_max = a/2;
n_max = 200;
n = 0;

axis equal;
axis ([0 a 0 a])
x = [];
y = [];
r = [];
sizee = [];
howmanytimestab = [];
while (n < n_max)
    howmanytimes = 0;
    fitin = false;
    xtemp = 0;
    ytemp = 0;
    rtemp = 0;
    while(fitin == false)
        xtemp = rand(1) * a;
        ytemp = rand(1) * a;
        rtemp = rand(1) * r_max;
        if(xtemp + rtemp < a && ytemp + rtemp < a && xtemp - rtemp > 0 && ytemp - rtemp > 0)
            fitin = true;
            if (n>0)
               for i = 1:n
                   x_dec = (x(i) - xtemp).^2;
                   y_dec = (y(i) - ytemp).^2;
                   r_dec = (r(i) + rtemp).^2;
                 if (x_dec + y_dec <= r_dec)
                   fitin = false;
                   break;
                 end
               end
            end
        end
        howmanytimes = howmanytimes + 1;
    end


    plot_circ(xtemp,ytemp,rtemp);
    hold on;
    x(end+1) = [xtemp];
    y(end+1) = [ytemp];
    r(end+1) = [rtemp];

    howmanytimestab(end+1) = howmanytimes;

    sizee(end+1) = power(rtemp,2) * pi;
    n = n + 1;
    pause(0.01);
end

figure('Name', 'Zajętość planszy');
procent_zajetosci = cumsum(sizee);
plot(1:n, procent_zajetosci);
x = power(a,2);
ylim([0 x]);
xlabel('Numer figury');
ylabel('Zajętość planszy');
title('Zajętość planszy w zależności od kolejno dodawanych figurek');
print -dpng zadanie1a

figure('Name', 'Zajętość planszy');
srednia_los = [];
srednia_los = cumsum(howmanytimestab);
for i = 1:n
    srednia_los(i) = srednia_los(i)/i;
end
plot(1:n, srednia_los);
xlabel('Liczba narysowanych okręgów');
ylabel('Liczba losowań');
title('Średnia ilosc losowań');
print -dpng zadanie1b

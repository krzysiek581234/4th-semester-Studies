clear all
clc
K = [5,15,25,35];

[XX,YY]=meshgrid(linspace(0,1,101),linspace(0,1,101));

for i = K
  [x,y,f,xp, yp] = lazik(i);
   
  figure()
  sgtitle(strcat('K=', num2str(i)))
  %wykres 1
  subplot(2,2,1);
  plot(xp,yp,'-o','linewidth',2)
  title("Drogę ruchu Dzielnego Lazika");
  xlabel("x[km]");
  ylabel("y[km]");
  % wykres 2
  subplot(2,2,2);
  surf(reshape(x,i,i),reshape(y,i,i),reshape(f,i,i));
  title("Wartosc probek");
  xlabel("x");
  ylabel("y)");
  zlabel("f(x,y)");
  % wykres 3
  [p] = polyfit2d(x,y,f);
  [FF] = polyval2d(XX,YY,p);
  subplot(2,2,3)
  surf(YY,XX,FF)
  title("Interpolacja wielomianowa")
  ylabel("y[km]");
  xlabel("x[km]");
  zlabel("f(x,y)");
  shading flat;
  %wykres 4
  [p] = trygfit2d(x,y,f);
  [FF]=trygval2d(XX,YY,p);
  subplot(2,2,4);
  surf(YY,XX,FF);
  shading flat;
  title(strcat("Interpolacja trygonometryczna dla K=", num2str(i)));
  ylabel("y[km]");
  xlabel("x[km]");
  zlabel("f(x,y)");
  saveas(gcf, strcat('K-', num2str(i)), 'png');
end

% Zadanie 2
K = 5:45;
divP = [];
divT = [];
for i = K
 [x,y,f,xp, yp] = lazik(i);
 [p] = polyfit2d(x,y,f);
 [FF] = polyval2d(XX,YY,p);
 [pT] = trygfit2d(x,y,f);
 [TT]=trygval2d(XX,YY,pT);
 if i == 5
    prevPol = FF;
    prevTry = TT;
 else
    divP(end +1) = max(max(abs(prevPol - FF)));
    divT(end +1) = max(max(abs(prevTry - TT)));
    prevPol = FF;
    prevTry = TT;
 end
end
 figure()
 plot(6:45,divP)
 title("Zadanie 2 - Wielomianowe");
 ylabel("divP");
 xlabel("K");
 saveas(gcf, strcat('zad2-Wielomianowe-K-', num2str(i)), 'png');
 figure()
 plot(6:45,divT)
 title("Zadanie 2 - Trygonometryczne");
 ylabel("divT");
 xlabel("K");
 saveas(gcf, strcat('zad2-Trygonometryczne-K-', num2str(i)), 'png');

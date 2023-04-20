function [v] = speed(t)
g = 9.81;
m0 = 150000;
q = 2700;
u = 2000;

v = u * log(m0/ (m0 - q*t)) - g*t;
v = v - 750;
end
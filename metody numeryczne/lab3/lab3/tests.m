clc
N = 4;

e = 6;
f = 9;
a1 = 5+e;
a2 = -1;
a3 = -1;
on = ones(N, 1);

A = spdiags([a3*on a2*on a1*on a2*on a3*on], -2:2, N, N);
A = full(A)

b = zeros(N, 1);
for i=0:N-1
     b(i+1) = sin(i * f);
end

b

x = A\b

clear all
clc

A = [11,-1,-1,0;-1,11,-1,-1;-1,-1,11,-1;0,-1,-1,11]
b =  ones(4, 1)

L = tril(A,-1) 
U = triu(A,1) 
D = diag(diag(A))

DL_ = (D + L) * -1
DL = (D + L)

test =  DL \ b

x = linsolve(DL, b)

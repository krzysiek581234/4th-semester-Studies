function [xvect,xdif, fx, it_cnt] = bisection(fun,a,b,eps)
xvect = [];
xdif = [];
fx = [];
result = -10;
for i = 1:1000
    c = (a + b)/2;
    fc = feval(fun, c);
    fa = feval(fun, a);
    xvect(end+1) = c;
    xdif(end+1) = abs(a - c);
    fx(end+1) = fc;
    if abs(fc) < eps || abs(a - b) < eps %jak blisko jest to zera
        it_cnt = i;
        return;
    end
    if fc * fa < 0
        b = c;
    else
        a = c;
    end
end
it_cnt = i;
end


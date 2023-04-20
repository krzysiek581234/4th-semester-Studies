function [xvect,xdif,fx,it_cnt] = secant(fun,xkm1,xk,eps)

  xvect = [];
  xdif = [];
  fx = [];

for i = 1:1000
    fxk = feval(fun, xk);
    x_kp1 = xk - (fxk*(xk - xkm1)/(fxk - feval(fun, xkm1)));
    xvect(end+1) = x_kp1;
    xdif(end+1) = abs(x_kp1 - xk);
    fx(end+1) = feval(fun, x_kp1);
    if abs(fx(end)) < eps
        it_cnt = i;
        return;
    end
    xkm1 = xk;
    xk = x_kp1;
end


end


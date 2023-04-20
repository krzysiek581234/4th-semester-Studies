function [value] = compute_impedance( omega )

value = (omega^1.43 + omega^1.13)/1000 - 5000;

end


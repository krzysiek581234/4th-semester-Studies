import numpy as np
import matplotlib.pyplot as plt

from data import get_data, inspect_data, split_data

data = get_data()
inspect_data(data)

train_data, test_data = split_data(data)

# Simple Linear Regression
# predict MPG (y, dependent variable) using Weight (x, independent variable) using closed-form solution
# y = theta_0 + theta_1 * x - we want to find theta_0 andtheta_1 parameters that minimize the prediction error

# We can calculate the error using MSE metric:
# MSE = SUM (from i=1 to n) (actual_output - predicted_output) ** 2

# get the columns
y_train = train_data['MPG'].to_numpy()
x_train = train_data['Weight'].to_numpy()

y_test = test_data['MPG'].to_numpy()
x_test = test_data['Weight'].to_numpy()
# TODO: standardization
y_traintemp =  y_train.mean();
x_traintemp =  x_train.mean();
y_trainstd = y_train.std();
x_trainstd = x_train.std();

y_train = (y_train-y_traintemp) / y_trainstd
x_train = (x_train-x_traintemp) / x_trainstd

y_test = (y_test - y_traintemp) / y_trainstd
x_test = (x_test-x_traintemp) / x_trainstd

# TODO: calculate closed-form solution
# wyraz wolny
X = np.c_[np.ones((len(x_train), 1)), x_train]
# θ = (X^T X)^-1 X^T y
# Obliczamy iloczyn macierzy X^T i X
XT_X = X.T.dot(X)
# Obliczamy macierz odwrotną do macierzy XT_X
XT_X_inv = np.linalg.inv(XT_X)
# Obliczamy iloczyn macierzy XT_X_inv i X^T
XT_X_inv_XT = XT_X_inv.dot(X.T)
# Obliczamy ostateczne wartości wag
theta_best = XT_X_inv_XT.dot(y_train)
print(theta_best)
#theta_best = np.linalg.inv(X_train.T.dot(X_train)).dot(X_train.T).dot(y_train)
#theta_best = [0, 0]

# TODO: calculate error
X_test = np.column_stack((np.ones(len(x_test)), x_test))
y_p = X_test.dot(theta_best)
mse = ((y_p - y_test)**2).mean()
print(mse)



# plot the regression line
x = np.linspace(min(x_test), max(x_test), 100)
y = float(theta_best[0]) + float(theta_best[1]) * x
plt.plot(x, y)
plt.scatter(x_test, y_test)
plt.xlabel('Weight')
plt.ylabel('MPG')
plt.show()


# TODO: calculate theta using Batch Gradient Descent
n = 0.01
teta = np.random.rand(2)
for i in range(1000):
    mse2 = 2 * X.T.dot(X.dot(teta) - y_train) / len(x_train)
    teta -= n * mse2

# TODO: calculate error
X_test = np.column_stack((np.ones(len(x_test)), x_test))
y_p = X_test.dot(teta)
mse = ((y_p - y_test)**2).mean()
print(mse)

# plot the regression line
x = np.linspace(min(x_test), max(x_test), 100)
y = float(theta_best[0]) + float(theta_best[1]) * x
plt.plot(x, y)
plt.scatter(x_test, y_test)
plt.xlabel('Weight')
plt.ylabel('MPG')
plt.show()
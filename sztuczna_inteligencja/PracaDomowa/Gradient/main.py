import numpy as np
import matplotlib.pyplot as plt

# X = np.outer(np.linspace(-2,2,10), np.ones(10))
# Y = X.copy().T
# Z = np.array([[2,1,3,7,2], [5,8,1,2,3]])

X = np.arange(0,5,1)
Y = np.array([2,2.4,2.5,2.1,2.2])
Z = np.array([[2,1,3,7,2], [5,8,1,2,3]])

# Rzutowanie Z na 2D
Z_2d = Z.max(axis=0) - Z  # obliczanie głębokości jako różnicy maksymalnej wartości w kolumnie i wartości w kolumnie
Z_2d_norm = (Z_2d - Z_2d.min()) / (Z_2d.max() - Z_2d.min())  # normalizacja wartości do zakresu [0, 1]

fig, ax = plt.subplots()
im = ax.imshow(Z_2d_norm.T, cmap='viridis', extent=[X.min(), X.max(), Y.min(), Y.max()], aspect='auto')
ax.set_title('test')
fig.colorbar(im, ax=ax)
plt.show()
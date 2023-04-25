import random

print("Start")
my_array = []
tablica = [[0 for j in range(5)] for i in range(5)]
for i in range(0, 100000):
    random_floatX = random.random()
    if random_floatX < 0.15:
        x = 1
        y = 3
    elif random_floatX < 0.35 and random_floatX >= 0.15:
        x = 2
        random_floatY = random.random()
        if (random_floatY < 0.75):
            y = 1
        else:
            y = 4
    elif random_floatX < 0.55 and random_floatX >= 0.35:
        x = 3
        y = 1
    else:
        x = 4
        random_floatY = random.random() * 0.45
        if random_floatY < 0.3:
            y = 2
        else:
            y = 4
    tablica[x][y] = tablica[x][y] + 1

for i in range(1,5):
    for j in range(1,5):
        print(tablica[i][j], end=' ')
    print()
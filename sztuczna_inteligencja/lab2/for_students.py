from itertools import compress
import random
import time
import matplotlib.pyplot as plt

from data import *

def initial_population(individual_size, population_size):
    return [[random.choice([True, False]) for _ in range(individual_size)] for _ in range(population_size)]

def fitness(items, knapsack_max_capacity, individual):
    total_weight = sum(compress(items['Weight'], individual))
    if total_weight > knapsack_max_capacity:
        return 0
    return sum(compress(items['Value'], individual))

def population_best(items, knapsack_max_capacity, population):
    best_individual = None
    best_individual_fitness = -1
    for individual in population:
        individual_fitness = fitness(items, knapsack_max_capacity, individual)
        if individual_fitness > best_individual_fitness:
            best_individual = individual
            best_individual_fitness = individual_fitness
    return best_individual, best_individual_fitness


import random


def roulette_selection(population, fitness_scores):

    total_fitness = sum(fitness_scores)
    spin = random.uniform(0, total_fitness)
    position = 0
    selected_individual = None
    for i in range(len(population)):
        position += fitness_scores[i]
        if position >= spin:
            selected_individual = population[i]
            break
    return selected_individual

def generatechild(a, b):
    if len(a) != len(b):
        raise ValueError("Listy wejściowe powinny mieć tę samą długość.")
    child = []
    dlogosc = len(a)
    for cos in range(0,dlogosc,2):
        child.append(a[cos])
        child.append(b[cos+1])
    rand = random.randint(0, 25)
    randwhat  = random.randint(0, 1)
    child[rand] = randwhat
    return child

items, knapsack_max_capacity = get_big()
#print(items)

population_size = 100
generations = 1000
n_selection = 20 #liczba 1 pokolenia
n_elite = 6

start_time = time.time()
best_solution = None
best_fitness = 0
population_history = []
best_history = []
population = initial_population(len(items), population_size)
for _ in range(generations):
    population_history.append(population)
    # TODO: implement genetic algorithm
    generationSet = []
    listoffitness = []
    parents = []
    children = []


    for a in population:
        listoffitness.append(fitness(items, knapsack_max_capacity, a))

    for _ in range(n_selection):
        parents.append(roulette_selection(population, listoffitness))

    #Generuj dzieci
    polowa = len(parents)//2
    for p in range(0,polowa):
        children.append(generatechild(parents[p], parents[p+polowa]))
        children.append(generatechild(parents[p+polowa], parents[p]))

    for _ in range(n_elite):
        elit = population_best(items, knapsack_max_capacity, parents)[0]
        children.append(elit)
        parents.remove(elit)

    population = children

    best_individual, best_individual_fitness = population_best(items, knapsack_max_capacity, population)
    if best_individual_fitness > best_fitness:
        best_solution = best_individual
        best_fitness = best_individual_fitness
    best_history.append(best_fitness)

end_time = time.time()
total_time = end_time - start_time
print('Best solution:', list(compress(items['Name'], best_solution)))
print('Best solution value:', best_fitness)
print('Time: ', total_time)

# plot generations
x = []
y = []
top_best = 10
for i, population in enumerate(population_history):
    plotted_individuals = min(len(population), top_best)
    x.extend([i] * plotted_individuals)
    population_fitnesses = [fitness(items, knapsack_max_capacity, individual) for individual in population]
    population_fitnesses.sort(reverse=True)
    y.extend(population_fitnesses[:plotted_individuals])
plt.scatter(x, y, marker='.')
plt.plot(best_history, 'r')
plt.xlabel('Generation')
plt.ylabel('Fitness')
plt.show()

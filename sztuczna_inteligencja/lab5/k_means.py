import numpy as np
import random


def initialize_centroids_forgy(data, k):
    # Konwertujemy data na listę, jeśli jest typu numpy.ndarray
    if isinstance(data, np.ndarray):
        data = data.tolist()

    # Wybieramy 3 losowe wartości jako centroidy
    centroids = random.sample(data, k)

    return centroids

def initialize_centroids_kmeans_pp(data, k):
    # TODO implement kmeans++ initizalization
    return None


def calculate_distance(point1, point2):
    # Sprawdzamy, czy punkty mają tę samą liczbę współrzędnych
    if len(point1) != len(point2):
        raise ValueError("The dimensions of the points do not match.")
    diff = np.array(point1) - np.array(point2)
    squared_diff = np.power(diff, 2)
    sum_squared_diff = np.sum(squared_diff)
    distance = np.sqrt(sum_squared_diff)
    if(distance== 0.0):
        return 1000
    return distance
def assign_to_cluster(data, centroid):
    # TODO find the closest cluster for each data point
    mindistance = 10000
    result = np.zeros(150)
    theclose = np.zeros((150, 3))

    for i in range(150):
        for x in range(3):
            theclose[i][x] = calculate_distance(data[i], centroid[x])
            index = np.argmax(theclose[i])
            result[i] = index

    return result.astype(int)

def update_centroids(data, assignments):
    # TODO find new centroids based on the assignments

    for i in range(100):
        if(assignments)
    return None

def mean_intra_distance(data, assignments, centroids):
    return np.sqrt(np.sum((data - centroids[assignments, :])**2))

def k_means(data, num_centroids, kmeansplusplus= False):
    # centroids initizalization
    if kmeansplusplus:
        centroids = initialize_centroids_kmeans_pp(data, num_centroids)
    else: 
        centroids = initialize_centroids_forgy(data, num_centroids)

    
    assignments  = assign_to_cluster(data, centroids)
    for i in range(100): # max number of iteration = 100
        print(f"Intra distance after {i} iterations: {mean_intra_distance(data, assignments, np.array(centroids))}")
        centroids = update_centroids(data, assignments)
        new_assignments = assign_to_cluster(data, centroids)
        if np.all(new_assignments == assignments): # stop if nothing changed
            break
        else:
            assignments = new_assignments

    return new_assignments, centroids, mean_intra_distance(data, new_assignments, centroids)         


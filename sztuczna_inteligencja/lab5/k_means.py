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

    centroids = np.zeros((k, data.shape[1]))
    centroids[0] = data[np.random.choice(data.shape[0])]
    for z in range(1,k):
        distance = np.zeros(len(data))
        for i, x in enumerate(data):
            for j in centroids:
                distance[i] += calculate_distance(x, j)
        index = np.argmax(distance)
    centroids[z] = data[index]
    return centroids

    # n = data.shape[0]
    # centroids = np.zeros((k, data.shape[1]))
    # centroids[0] = data[np.random.choice(n)]
    # for i in range(1, k):
    #     distances = np.zeros(n)
    #     for j in range(i):
    #         distances += np.linalg.norm(data - centroids[j], axis=1) ** 2
    #     max_distance_indices = np.argmax(distances)
    #     centroids[i] = data[max_distance_indices]
    # return centroids


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
    result = np.zeros(len(data)).astype(int)
    theclose = np.zeros((len(data), len(centroid)))

    for i in range(len(data)):
        for x in range(len(centroid)):
            theclose[i][x] = calculate_distance(data[i], centroid[x])
        index = np.argmin(theclose[i])
        result[i] = int(index)

    return result.astype(int)

def update_centroids(data, assignments):
    num_clusters = np.max(assignments) + 1  # Liczba klastrów (centroidów)

    centroids = np.zeros((num_clusters, data.shape[1]))  # Inicjalizacja macierzy centroidów

    for cluster in range(num_clusters):
        cluster_points = data[assignments == cluster]  # Punkty przypisane do danego klastra
        if len(cluster_points) > 0:
            centroids[cluster] = np.mean(cluster_points, axis=0)  # Obliczenie nowego centroidu jako średnia punktów klastra

    return centroids


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


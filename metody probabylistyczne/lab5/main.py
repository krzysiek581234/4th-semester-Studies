import math

with open('Dane.txt', 'r') as file:
    # Read all the lines of the file
    lines = file.readlines()
    # Convert each string to a float and store in a list
    float_list = [float(line) for line in lines]

print(float_list)

#moment wykły rzędu 1
def momentwykly(a):
    m = 0
    for i in float_list:
        m += i**(a)
    m /= len(float_list)
    return m
print(f"Moment zwykły 1: {momentwykly(1)}")
print(f"Moment zwykły 2: {momentwykly(2)}")

def momentCentralny(a):
    m =0
    avg = sum(float_list)/len(float_list)
    for i in float_list:
        m += (i - avg)**a
    m /= len(float_list)
    return m

print(f"Moment Centralny 1: {momentCentralny(1)}")
print(f"Moment Centralny 2: {momentCentralny(2)}")

def war():
    m =0
    avg = sum(float_list)/len(float_list)
    for i in float_list:
        m += (i - avg)**2
    m /= len(float_list)
    return math.sqrt(m)


print(f"Odchylenie Standardowe: {war()}")

def odchylenieprzecietne():
    m =0
    avg = sum(float_list)/len(float_list)
    for i in float_list:
        m += abs(i - avg)
    m /= len(float_list)
    return m

print(f"Odchylenie przecietne: {odchylenieprzecietne()}")
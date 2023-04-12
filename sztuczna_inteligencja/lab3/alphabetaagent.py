import random
from exceptions import AgentException

inf = 10000000
class AlphaBetaAgent:
    def __init__(self, my_token='o'):
        self.my_token = my_token

    def decide(self, connect4):
        if connect4.who_moves != self.my_token:
            raise AgentException('not my round')

        best = connect4.possible_drops()[0]
        connect4.drop_token(best)
        stanBest = self.minmax(connect4, False,10000000, -10000000,  0)
        connect4.collect_token(best)

        for i in connect4.possible_drops():
            connect4.drop_token(i)
            stanI = self.minmax(connect4, False,10000000, -10000000,  0)
            connect4.collect_token(i)
            if stanI > stanBest:
                stanBest = stanI
                best = i
        return connect4.possible_drops()[best-1]

    def minmax(self, connect4, czy_max, alfa, beta, depth):
        if depth == 5:
            return 0
        stan = self.evaluate(connect4)
        if stan != 2:
            return stan
        
        if czy_max:
            opt = -10000000
            for i in connect4.possible_drops():
                connect4.drop_token(i)
                move = self.minmax(connect4, ~czy_max, alfa, beta, depth+1)
                opt = max(opt, move)
                connect4.collect_token(i)
                alfa = max(alfa, opt)
                if alfa >= beta:
                    return alfa
        else:
            opt = 10000000
            for i in connect4.possible_drops():
                connect4.drop_token(i)
                move = self.minmax(connect4, ~czy_max,alfa, beta,  depth+1)
                opt = min(opt, move)
                connect4.collect_token(i)
                beta = min(beta, opt)
                if alfa >= beta:
                    return beta
        return opt

    def evaluate(self, connect4):
        if not connect4.possible_drops():
            return 0
        for four in connect4.iter_fours():
            if four == ['o', 'o', 'o', 'o']:
                return 1
            elif four == ['x', 'x', 'x', 'x']:
                return -1
        return 2

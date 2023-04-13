import random
from exceptions import AgentException

inf = 10000000
class AlphaBetaAgent:
    def __init__(self, my_token='o'):
        self.my_token = my_token
        self.opositetoken = ' '
        if (my_token == 'o'):
            self.opositetoken = 'x'
        else:
            self.opositetoken = 'o'

    def decide(self, connect4):
        if connect4.who_moves != self.my_token:
            raise AgentException('not my round')

        pos = connect4.possible_drops()[0]
        connect4.drop_token(pos)
        XBest = self.minmax(connect4, False, 10, -10, 0)
        connect4.collect_token(pos)

        for x in connect4.possible_drops():
            connect4.drop_token(x)
            eva = self.minmax(connect4, False, -10,10, 0)
            connect4.undo_drop_token(x)
            if XBest < eva:
                XBest = eva
                pos = x
        return pos

    def minmax(self,connect4, czyMax,alfa, beta, depth):
        if(depth == 7):
            return 0
        stan = self.evaluate(connect4)
        if stan != 2:
            return stan
        else:
            if czyMax:
                opt = -10
                for y in connect4.possible_drops():
                    connect4.drop_token(y)
                    eva = self.minmax(connect4, not czyMax, -10,10, depth+1)
                    opt = max(eva, opt)
                    connect4.undo_drop_token(y)
                    alfa = max(alfa, opt)
                    if alfa >= beta:
                        return alfa
            else:
                opt = 10
                for y in connect4.possible_drops():
                    connect4.drop_token(y)
                    eva = self.minmax(connect4, not czyMax, -10,10, depth+1)
                    opt = min(eva, opt)
                    connect4.undo_drop_token(y)
                    beta = min(beta, opt)
                    if alfa >= beta:
                        return beta
            return opt


    def evaluate(self, connect4):
        if not connect4.possible_drops():
            return 0
        for four in connect4.iter_fours():
            if four == [self.my_token, self.my_token, self.my_token, self.my_token]:
                return 1
            elif four == [self.opositetoken, self.opositetoken, self.opositetoken, self.opositetoken]:
                return -1
        return 2

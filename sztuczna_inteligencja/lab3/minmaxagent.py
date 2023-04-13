import random
from exceptions import AgentException

class MinMaxA:
    def __init__(self, my_token):
        self.my_token = my_token
        self.opositetoken = ' '
        if(my_token == 'o'):
            self.opositetoken = 'x'
        else:
            self.opositetoken = 'o'


    def decide(self,connect4):
        if connect4.who_moves != self.my_token:
            raise("Wrong order")

        pos = connect4.possible_drops()[0]
        connect4.drop_token(pos)
        XBest = self.minmax(connect4, 0, False)
        connect4.collect_token(pos)

        for x in connect4.possible_drops():
            connect4.drop_token(x)
            eva = self.minmax(connect4, 0, False)
            connect4.undo_drop_token(x)
            if XBest < eva:
                XBest = eva
                pos = x
        return pos

    def minmax(self,connect4, depth, czyMax):
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
                    eva = self.minmax(connect4, depth+1, not czyMax)
                    opt = max(eva, opt)
                    connect4.undo_drop_token(y)
            else:
                opt = 10
                for y in connect4.possible_drops():
                    connect4.drop_token(y)
                    # connect4.draw()
                    eva = self.minmax(connect4, depth+1, not czyMax)
                    opt = min(eva, opt)
                    connect4.undo_drop_token(y)
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
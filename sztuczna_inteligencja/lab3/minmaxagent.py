import random
from exceptions import AgentException

class MinMaxA:
    def __init__(self, my_token):
        self.my_token = my_token
    def decide(self,connect4):
        if connect4.who_moves != self.my_token:
            raise("Wrong order")

        chose = connect4.possible_drops()
        maxEva = -10
        eva = -10
        for ch in chose:
            connect4.drop_token(ch)
            eva = self.minmax(connect4, 0, True)
            connect4.undo_drop_token(ch)
            maxEva = max(eva, maxEva)
        return maxEva

    def minmax(self,connect4, depth, czyMax):
        if(depth == 5):
            return 0
        else:
            depth = depth +1
            if (connect4._check_game_over() == True):
                return 1
            else:
                if czyMax:
                    maxEva = -10
                    for y in connect4.possible_drops():
                        connect4.drop_token(y)
                        connect4.draw()
                        eva = self.minmax(connect4, depth, not czyMax)
                        connect4.undo_drop_token(y)
                        maxEva = max(eva,maxEva)

                    return  maxEva
                else:
                    minEva = 10
                    for y in connect4.possible_drops():
                        connect4.drop_token(y)
                        connect4.draw()
                        eva = self.minmax(connect4, depth, not czyMax)
                        connect4.undo_drop_token(y)
                        minEva = min(eva, minEva)

                    return minEva
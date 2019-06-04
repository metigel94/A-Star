# ------------------------------------------------------------------------------------------------
# This python scripts implements the A*-star (A-star) pathfinding algorithm.
# 
# The script is meant to be coupled with a game (e.g., Unity) where it can handle
# the pathfinding of up to 3 clients and four different tile-costs.
# 
# The tile costs should be defined within the grid in ascending
# order, i.e. '0' = lowest tile cost and '3' = unwalkable.
# 
# This script receives an input-grid and up to three starting coordinates over a socket, 
# processes them and sends the fastest path from each starting coordinates to the
# end node, back over that same socket.
# 
# For the script to work, it is important for the clients to connect to 
# the three sockets that are defined in line 139, line 169 and line 200.
# ------------------------------------------------------------------------------------------------


import time
import zmq
from threading import Thread


# This function converts the incoming string into a grid into the size given as the argument
def split_grid(input, size):
    return [input[start:start+size] for start in range(0, len(input), size)]


# A node class for A* Pathfinding
class Node():

    def __init__(self, parent=None, position=None):
        self.parent = parent
        self.position = position

        self.g = 0
        self.h = 0
        self.f = 0

    def __eq__(self, other):
        return self.position == other.position


# astar() returns a list of tuples as a path from the given start coordinates to the given
# end coordinates in the provided grid
def astar(maze, start, end):

    # Create start and end node
    start_node = Node(None, start)
    start_node.g = start_node.h = start_node.f = 0
    end_node = Node(None, end)
    end_node.g = end_node.h = end_node.f = 0

    # Initialize both open and closed list
    open_list = []
    closed_list = []

    # Add the start node
    open_list.append(start_node)

    # Loop until you find the end
    while len(open_list) > 0:

        # Get the current node
        current_node = open_list[0]
        current_index = 0
        for index, item in enumerate(open_list):
            if item.f < current_node.f:
                current_node = item
                current_index = index

        # Pop current off open list, add to closed list
        open_list.pop(current_index)
        closed_list.append(current_node)

        # Found the goal
        if current_node == end_node:
            path = []
            current = current_node
            while current is not None:
                current.position = (current.position[0], current.position[1]+1)
                path.append(current.position)
                current = current.parent
            return path[::-1] # Return reversed path

        # Generate children
        children = []
        for new_position in [(0, -1), (0, 1), (-1, 0), (1, 0), (-1, -1), (-1, 1), (1, -1), (1, 1)]: # Adjacent squares

            # Get node position
            node_position = (current_node.position[0] + new_position[0], current_node.position[1] + new_position[1])

            # Make sure within range
            if node_position[0] > (len(maze) - 1) or node_position[0] < 0 or node_position[1] > (len(maze[len(maze)-1]) -1) or node_position[1] < 0:
                continue

            # Define different tile costs in ascending order
            if maze[node_position[0]][node_position[1]] == 3:
                continue

            if maze[node_position[0]][node_position[1]] == 1:
                new_node = Node(current_node, node_position)
                new_node.g = 2
            
            if maze[node_position[0]][node_position[1]] == 2:
                new_node = Node(current_node, node_position)
                new_node.g = 4

            if maze[node_position[0]][node_position[1]] == 0:
                new_node = Node(current_node, node_position)

            # Append
            children.append(new_node)

        # Loop through children
        for child in children:

            # Child is on the closed list
            for closed_child in closed_list:
                if child == closed_child:
                    continue

            # Create the f, g, and h values
            child.g += current_node.g + 1
            child.h = ((child.position[0] - end_node.position[0]) ** 2) + ((child.position[1] - end_node.position[1]) ** 2)
            child.f = child.g + child.h

            
            # Child is already in the open list
            for open_node in open_list:
                if child == open_node and child.g > open_node.g:
                    continue

            # Add the child to the open list
            open_list.append(child)



# Server class to provide communication over TCP protocol
class Server():

    # This server class defines 3 indentical functions. Each function is concerned with one of the clients in the game.
    # The only difference between the functions is the socket that is bound to the server.
    def run_first_server(self):

        # Create server socket and bind to port number
        context = zmq.Context()
        socket = context.socket(zmq.REP)
        socket.bind("tcp://*:5555")

        gridSend = False

        # Create an infinite loop so values can be sent and received constantly
        while True:
            # receive data from socket (coordinates of first client)
            message = socket.recv()
            
            # decode and split string of coordinates
            positions = message.decode().split(",")

            # define start and end coordinates of first client
            startPosition = (50 - int(positions[0]), int(positions[1]))
            endPosition = (50 - int(positions[2]), int(positions[3]))

            # The grid has a size of 50x50. Therefore, we use the split_grid function from earlier,
            # to split the grid into a new list, every 50 characters.
            if(gridSend == False):
                grid = positions[4]
                grid = list(grid)
                grid = [int(i) for i in grid]
                grid = split_grid(grid, 50)
                gridSend = True

            # Now we pass the grid and the start and end coordinates of the client to the astar() function
            # and store the return value of the function, which is the final path, in a variable.
            finalPath = astar(grid, startPosition, endPosition)
            
            # The coordinates in the final path are split by ', ' and stored in a variable.
            pathToUnity = ", ".join(map(str, finalPath))
            
            time.sleep(0.01)

            # The final path is encoded using utf-8, so that it can be sent over the socket
            # and received by Unity.
            socket.send(pathToUnity.encode())


    # Functionality and implementation is identical to 'run_first_server'
    def run_second_server(self):
        context = zmq.Context()
        socket1 = context.socket(zmq.REP)
        socket1.bind("tcp://*:5550")

        gridSend1 = False

        while True:
            message = socket1.recv()
            
            positions = message.decode().split(",")

            startPosition = (50 - int(positions[0]), int(positions[1]))
            endPosition = (50 - int(positions[2]), int(positions[3]))

            if(gridSend1 == False):
                grid = positions[4]
                grid = list(grid)
                grid = [int(i) for i in grid]
                grid = split_grid(grid, 50)
                gridSend1 = True
        
            finalPath = astar(grid, startPosition, endPosition)
            
            pathToUnity = ", ".join(map(str, finalPath))
            
            time.sleep(0.01)

            #  In the real world usage, after you finish your work, send your output here
            socket1.send(pathToUnity.encode())


    # Functionality and implementation is identical to 'run_first_server'
    def run_third_server(self):
            context = zmq.Context()
            socket2 = context.socket(zmq.REP)
            socket2.bind("tcp://*:5540")

            gridSend2 = False

            while True:
                message = socket2.recv()
                
                positions = message.decode().split(",")

                startPosition = (50 - int(positions[0]), int(positions[1]))
                endPosition = (50 - int(positions[2]), int(positions[3]))

                if(gridSend2 == False):
                    grid = positions[4]
                    grid = list(grid)
                    grid = [int(i) for i in grid]
                    grid = split_grid(grid, 50)
                    gridSend2 = True
            
                # finalPath = astar(grid, testTouple, testTouple2)
                finalPath = astar(grid, startPosition, endPosition)
                
                print(finalPath)
                
                pathToUnity = ", ".join(map(str, finalPath))
                
                time.sleep(0.01)

                #  In the real world usage, after you finish your work, send your output here
                socket2.send(pathToUnity.encode())


def main():
    # An instance of the server class is created and a thread 
    # for each function in the class is created.
    # Finally, all three threads are started, to allow for multithreaded
    # computing of the path for each client.
    server = Server()
    thread1 = Thread(target = server.run_first_server)
    thread2 = Thread(target = server.run_second_server)
    thread3 = Thread(target = server.run_third_server)
    thread1.start()
    thread2.start()
    thread3.start()

if __name__ == '__main__':
    main()
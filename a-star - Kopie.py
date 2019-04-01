import time
import zmq

class Node():
    """A node class for A* Pathfinding"""

    def __init__(self, parent=None, position=None):
        self.parent = parent
        self.position = position

        self.g = 0
        self.h = 0
        self.f = 0

    def __eq__(self, other):
        return self.position == other.position



def astar(maze, start, end):
    """Returns a list of tuples as a path from the given start to the given end in the given maze"""

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

            # Make sure walkable terrain
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


context = zmq.Context()
socket = context.socket(zmq.REP)
socket.bind("tcp://*:5555")

gridSend = False


def splitGrid(input, size):
	return [input[start:start+size] for start in range(0, len(input), size)]

while True:
    message = socket.recv()
    
    positions = message.decode().split(",")

    startPosition = (50 - int(positions[0]), int(positions[1]))
    endPosition = (50 - int(positions[2]), int(positions[3]))

    if(gridSend == False):
        grid = positions[4]
        grid = list(grid)
        grid = [int(i) for i in grid]
        grid = splitGrid(grid, 50)
        gridSend = True
  
    # finalPath = astar(grid, testTouple, testTouple2)
    finalPath = astar(grid, startPosition, endPosition)
    
    # print(finalPath)
    
    pathToUnity = ", ".join(map(str, finalPath))
       
    time.sleep(0.01)

    #  In the real world usage, after you finish your work, send your output here
    socket.send(pathToUnity.encode())
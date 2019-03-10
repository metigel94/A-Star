import time
import zmq

context = zmq.Context()
socket = context.socket(zmq.REP)
socket.bind("tcp://*:5555")

while True:

    #  Wait for next request from client
    message = socket.recv()

    #  In the real world usage, you just need to replace time.sleep() with
    #  whatever work you want python to do.
    time.sleep(1)

    #  Send reply back to client
    #  In the real world usage, after you finish your work, send your output here
    socket.send(b"fucking work you piece of fing trash!")
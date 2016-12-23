import socket
import os
import sys
import time

host = '192.168.1.3'  # Symbolic name meaning all available interfaces
port = 8888        # Arbitrary non-privileged port

#Debug Data
print("host: " + host)
print("port: ",port)
s = socket.socket(socket.AF_INET, socket.SOCK_STREAM)
s.bind((host, port))
s.listen(1)
conn, addr = s.accept()
print ('Connected by', addr)

while 1:
    data = conn.recv(16)
    if not data: break
    if (data == "test_btn"):
        print ("Command: " + data.decode('UTF-8','strict'))

package org.example;

import java.io.IOException;
import java.io.ObjectInputStream;
import java.io.ObjectOutputStream;
import java.net.Socket;

public class ClientHandler implements Runnable{
    private Socket clientSocket;
    public ClientHandler(Socket socket) {
        this.clientSocket = socket;
    }
    @Override
    public void run() {
        try (ObjectInputStream in = new ObjectInputStream(clientSocket.getInputStream());
             ObjectOutputStream out = new ObjectOutputStream(clientSocket.getOutputStream())) {

            String message;
            do {
                message = in.readUTF();
                System.out.println("Received message from client: " + message);

                out.writeUTF("Server received message: " + message);
                out.flush();
            } while (!message.equals("exit"));

            System.out.println("Closing client socket");
            clientSocket.close();
        } catch (IOException e) {
            e.printStackTrace();
        }
    }
}
package org.example;


import java.io.IOException;
import java.net.InetAddress;
import java.net.ServerSocket;
import java.net.Socket;

public class Main {
    public static void main(String[] args)
    {
        try (ServerSocket server = new ServerSocket(8080)) {
                while (true) {
                    //System.out.println("dzialam Server");
                    Socket clientSocket = server.accept();
                    Thread thread = new Thread(new ClientHandler(clientSocket));
                    thread.start();
                }
        } catch (IOException ex) {
            System.err.println(ex);
        }
    }
}
package org.example;

import java.io.IOException;
import java.net.ServerSocket;
import java.net.Socket;

// Press Shift twice to open the Search Everywhere dialog and type `show whitespaces`,
// then press Enter. You can now see whitespace characters in your code.
public class Main {
    public static void main(String[] args)
    {
        try (ServerSocket server = new ServerSocket(8080)) {
            try {
                while (true)
                {
                    Socket socket = server.accept();
                    Thread thread = new Thread(new ThreadServer(socket));
                    thread.start();
                }
            } catch (IOException ex) {
                System.err.println(ex);
            }
        } catch (IOException ex) {
            System.err.println(ex);
        }

    }
}
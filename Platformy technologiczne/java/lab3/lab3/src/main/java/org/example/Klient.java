package org.example;

import java.io.IOException;
import java.io.ObjectInputStream;
import java.io.ObjectOutputStream;
import java.net.InetAddress;
import java.net.Socket;

public class Klient {

    public static void main(String[] args) {
        try (Socket socket = new Socket("localhost", 8080)) {
            try
                    (
                ObjectInputStream in = new ObjectInputStream(socket.getInputStream());
                ObjectOutputStream out = new ObjectOutputStream(socket.getOutputStream());
                    ){
                String cos = in.readUTF();
                System.out.println(cos);
                out.write(5);
                out.flush();
            }
            catch (IOException e) {
                throw new RuntimeException(e);
            }

            //System.out.println("working");
        } catch (IOException ex) {
            System.err.println(ex);
        }
    }


}

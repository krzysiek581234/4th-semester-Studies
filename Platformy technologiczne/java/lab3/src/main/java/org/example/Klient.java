package org.example;

import java.io.IOException;
import java.io.ObjectInputStream;
import java.io.ObjectOutputStream;
import java.net.Socket;
import java.util.Scanner;

public class Klient {
    public Klient() {
    }
    public static void main(String[] args)
    {
        try (Socket client = new Socket("localhost", 8080);
             ObjectOutputStream os = new ObjectOutputStream(client.getOutputStream());
             ObjectInputStream is = new ObjectInputStream(client.getInputStream());
             Scanner scanner = new Scanner(System.in)) {

            System.out.println(is.readUTF());
            // wysłanie danych do serwera
            System.out.print("Podaj Liczbe: ");
            String message = scanner.nextLine();
            int messINT = Integer.parseInt(message);
            os.writeInt(messINT);
            os.flush();
            System.out.println(is.readUTF());
            for (int i = 0; i < messINT; i++) {
                System.out.print("Podaj wyraz: zostało: " + (messINT - i) + " wyrazów ");
                Message mess = new Message(scanner.nextLine(), i);
                os.writeObject(mess);
            }
            System.out.println(is.readUTF());
            is.close();
        } catch (IOException e) {
            System.err.println(e);
        }

    }

}

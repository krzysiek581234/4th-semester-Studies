package org.example;

import java.util.Iterator;
import java.util.LinkedList;
import java.util.Scanner;

// Press Shift twice to open the Search Everywhere dialog and type `show whitespaces`,
// then press Enter. You can now see whitespace characters in your code.
public class Main {
    // Producer
    public static void main(String[] args)
    {
        int numberofProducers = 2;
        int numberofConsumers = 2;
        LinkedList<Thread> producenci = new LinkedList<>();
        LinkedList<Thread> konsumenci = new LinkedList<>();
        Magazyn mag = new Magazyn();
        for (int i = 0; i < numberofProducers; i++) {
            producenci.add(new Thread(new producent(i,mag)));
            konsumenci.add(new Thread(new client(i,mag)));
        }
        Iterator pro = producenci.iterator();
        Iterator con = konsumenci.iterator();
        System.out.println("Start");
        while(pro.hasNext()) {
            Thread element = (Thread)pro.next();
            element.start();
        }
        while(con.hasNext()) {
            Thread element = (Thread)con.next();
            element.start();
        }

        while(true)
        {
            Scanner scan = new Scanner(System.in);
            String userInput = scan.next();
            if (userInput.equals("e")) {
                return;
            }
        }
    }
}
package org.example;

import java.util.ArrayList;
import java.util.List;

public class Main {
    public static void main(String[] args) {
    List<Thread> watki = new ArrayList<Thread>();

    for (int i =1 ; i<=5;i++)
    {
        watki.add(new Thread(new run(i*10000,i)));
    }
    for (Thread element : watki)
    {
        element.start();
    }



    }
}

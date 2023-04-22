package org.example;

import static java.lang.Math.pow;

public class run implements Runnable{
    public run(int licznik, int id)
    {
        this.licznik = licznik;
        this.id = id;
    }

    private int licznik;
    private int id;
    private double suma =0;
    void licz()
    {
        for (int i =1; i<=licznik;i++)
        {
            suma += pow(-1,i-1)/ (2*i-1);
        }
        suma = suma * 4;
    }

    @Override
    public void run() {
        System.out.println("licze licze");
        licz();
        System.out.println(suma);
    }

}

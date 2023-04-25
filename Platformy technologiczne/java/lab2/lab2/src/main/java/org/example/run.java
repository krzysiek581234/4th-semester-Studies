package org.example;

import java.util.LinkedList;
import java.util.List;

import static java.lang.Math.pow;

public class run implements Runnable{
    private ResultsCollector ResC;
    private Taskmenager taskMen;

    public run(int id, ResultsCollector Rec, Taskmenager taskMen)
    {
        this.id = id;
        this.ResC = Rec;
        this.taskMen = taskMen;
    }

    private int licznik;
    private int templiznik =0;
    private int id;
    private double suma =0;
    void licz()
    {
        for (int i =1; i<=licznik;i++)
        {
            try {
                // uśpienie wątku na 2 sekundy
                Thread.sleep(100);
            } catch (InterruptedException e) {
                // obsługa wyjątku
            }
            if(!taskMen.NotExit)
            {
                break;
            }
            templiznik++;
            suma += pow(-1,i-1)/ (2*i-1);
        }
        suma = suma * 4;

    }

    @Override
    public void run() {
        while(taskMen.NotExit)
        {
            try {
                this.licznik = this.taskMen.getTask();
                if (this.licznik == 0)
                {
                    break;
                }
                suma = 0;
            } catch (InterruptedException e) {
                throw new RuntimeException(e);
            }
            System.out.println("Licznione przez: " + this.id);
            licz();
            if(taskMen.NotExit)
            {
                ResC.addResult(suma, id, licznik);
                System.out.println("Pi: " + suma + " id: " + id + " Licznik: " + licznik);
            }
            else
            {
                ResC.addResult(suma, id, templiznik);
                System.out.println("id " + id + " licznik " + templiznik + " result " + suma);
            }
        }

    }

}

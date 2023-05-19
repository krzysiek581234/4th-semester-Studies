package org.example;

import java.util.Random;

public class client implements Runnable{
    private Magazyn magazyn;
    private int id;
    private boolean notExit = true;
    public client(int id,Magazyn magazyn)
    {
        this.id = id;
        this.magazyn = magazyn;
    }
    @Override
    public void run() {
        while (this.notExit) {
            Random random = new Random();
            int index = random.nextInt(3); // Wylosowanie liczby całkowitej od 0 do 2
            magazyn.removeFromMag(index);
            System.out.println(magazyn);
            try {
                Thread.sleep(500L);
            } catch (InterruptedException e) {
                throw new RuntimeException(e);
            }
        }
    }
}

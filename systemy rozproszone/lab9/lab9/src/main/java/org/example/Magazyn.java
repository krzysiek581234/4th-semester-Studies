package org.example;

public class Magazyn {
    private int[] zasoby;
    public Magazyn()
    {
        this.zasoby = new int[3];
        zasoby[0] = 1;
        zasoby[1] = 1;
        zasoby[2] = 1;
    }
    public void addToMag(int index)
    {
        this.zasoby[index] += 1;
        System.out.println("Dodano do magazynu " + index);
    }
    public void removeFromMag(int index)
    {
        if(this.zasoby[index] > 0)
            this.zasoby[index] -= 1;
        else
            System.out.println("nie można skonsumować");
        System.out.println("Zabrano z magazynu " + index);
    }
    @Override
    public String toString()
    {
        return "Stan magazynu: " + zasoby[0] + " " + zasoby[1] + " " + zasoby[2];
    }
}

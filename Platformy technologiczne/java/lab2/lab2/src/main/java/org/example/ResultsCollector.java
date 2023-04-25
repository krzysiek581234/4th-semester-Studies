package org.example;

import java.util.LinkedList;
import java.util.List;

public class ResultsCollector {
    private List<Double> results = new LinkedList<>();
    private List<Integer> id = new LinkedList<>();

    private List<Integer> licznik = new LinkedList<>();

    public synchronized void addResult(double result,int Id, int licznik) {
        results.add(result);
        id.add(Id);
        this.licznik.add(licznik);
    }
    public void printresult()
    {
        System.out.println();
        for (int i = 0; i < licznik.size(); i++)
        {
            System.out.println("id " + id.get(i) + " licznik " + licznik.get(i) + " result " + results.get(i));
        }
    }

    public synchronized List<Integer> getResults() {
        return new LinkedList(results);
    }
}

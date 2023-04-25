package org.example;

import java.util.ArrayList;
import java.util.LinkedList;
import java.util.List;
import java.util.Scanner;

public class Main {
    public static void main(String[] args) throws InterruptedException
    {
        System.out.println(args[0]);
        int NumberOfThreds = Integer.parseInt(args[0]);
        LinkedList<Integer> list = new LinkedList<Integer>();
        Taskmenager taskMen = new Taskmenager();
        ResultsCollector ResultColl = new ResultsCollector();
        LinkedList<Thread>  watki = new LinkedList<Thread>();

        taskMen.addTask(10);
        taskMen.addTask(20);
        taskMen.addTask(30);
        taskMen.addTask(40);
        int id =0;
        for (int i =1 ; i<=NumberOfThreds;i++)
        {
            id = i;
            watki.add(new Thread(new run(id,ResultColl,taskMen)));
        }
        for (Thread element : watki)
        {
            element.start();
        }
        while (true)
        {
            Scanner scan = new Scanner(System.in);
            String userInput = scan.next();
            if(userInput.equals("exit"))
            {

                taskMen.stop();
                ResultColl.printresult();
                break;
            }
            else
            {
                id++;
                taskMen.addTask(Integer.parseInt(userInput));
            }
        }


    }
}

package org.example;

import java.util.ArrayList;
import java.util.LinkedList;
import java.util.List;
import java.util.Objects;

public class Taskmenager
{

    private LinkedList<Integer> list = new LinkedList<Integer>();
    public boolean NotExit = true;
    public synchronized void addTask(int task)
    {
        list.add(task);
        notifyAll();
    }
    public synchronized void removeTask(Runnable task)
    {
        list.remove(task);
    }
    public synchronized Integer getTask() throws InterruptedException {
        while (list.isEmpty() && NotExit) {
            wait();
        }
        if (!NotExit) return 0;
        else return list.remove(0);
    }
    public synchronized void stop() {
        NotExit = false;
        notifyAll();
    }



}

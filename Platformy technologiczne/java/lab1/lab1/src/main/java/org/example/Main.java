package org.example;

import java.util.*;

// Press Shift twice to open the Search Everywhere dialog and type `show whitespaces`,
// then press Enter. You can now see whitespace characters in your code.
public class Main {
    static void printSet(Set<Mage> set, int deep)
    {
        for(Mage a : set)
        {
            for (int i=0 ;i<deep; i++)
            {
                System.out.print("\t");
            }

            System.out.println(a);
            if(a.getSet() != null)
            {
                printSet(a.getSet(),deep+1);
            }
        }
    }
    static int policzDzieci(Mage b, int deep)
    {
        if(b.getSet() != null)
        {
            for(Mage a : b.getSet())
            {
                deep = policzDzieci(a,deep) + 1;
            }
            return deep;
        }
        else
        {
            return deep;
        }
        //return deep;
    }
    public static Map<Mage, Integer> generate(Set <Mage> set, String mode)
    {
        Map<Mage, Integer> statistics;
        if(mode.equals("brak"))
        {
            statistics = new HashMap<>();
        }
        else
        {
            statistics = new TreeMap<>();
        }
        for (Mage m: set)
        {
            int count = policzDzieci(m, 0);
            statistics.put(m, count);
        }
        return statistics;
    }
    public static void main(String[] args)
    {
        Set<Mage> zbior, zbior2, zbior3;
        if(args[0].equals("brak"))
        {
            zbior = new HashSet<Mage>();
            zbior2 = new HashSet<Mage>();
            zbior3 = new HashSet<Mage>();
        } else if(args[0].equals("natural"))
        {
            zbior = new TreeSet<Mage>();
            zbior2 = new TreeSet<Mage>();
            zbior3 = new TreeSet<Mage>();
        } else
        {
            zbior = new TreeSet<Mage>(new MageCom());
            zbior2 = new TreeSet<Mage>(new MageCom());
            zbior3 = new TreeSet<Mage>(new MageCom());
        }

        Mage mag1 = new Mage("Giga",11,21,null);
        Mage mag2 = new Mage("Maciek",12,21,null);
        Mage mag3 = new Mage("Przydupas",3,10, null);
        Mage mag4 = new Mage("Merlin", 50, 750.0, null);
        Mage mag5 = new Mage("Dumbledore", 60, 1000.0, null);
        Mage mag6 = new Mage("Harry Potter", 20, 200.0, null);
        Mage mag7 = new Mage("Voldemort", 71, 800.0, null);
        Mage mag8 = new Mage("Draco Malfoy", 18, 300.0, null);
        Mage mag9 = new Mage("Severus Snape", 40, 500.0, zbior);
        Mage mag10 = new Mage("Vi Snape", 20, 550.0, zbior2);

        Mage mag11 = new Mage("X", 71, 800.0, null);
        Mage mag12 = new Mage("X", 70, 801.0, null);
        zbior.add(mag11);
        zbior.add(mag12);

        zbior.add(mag1);
        zbior.add(mag2);
        zbior.add(mag3);
        zbior.add(mag4);
        zbior.add(mag5);
        zbior.add(mag6);
        zbior3.add(mag7);
        zbior2.add(mag8);
        zbior2.add(mag9);
        zbior3.add(mag10);

        Mage master = new Mage("Dumbledore", 50, 100.0,  zbior3);

        System.out.println("Zawartość zbioru:");
        printSet(zbior3,0);
        System.out.println(policzDzieci(mag10,0));
        Map<Mage, Integer> test = generate(zbior2, args[0]);
        System.out.println(test);
    }

}
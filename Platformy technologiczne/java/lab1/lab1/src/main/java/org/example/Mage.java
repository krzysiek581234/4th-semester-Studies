package org.example;

import java.util.HashSet;
import java.util.Objects;
import java.util.Set;
import java.util.TreeSet;


public class Mage implements Comparable<Mage> {
    private String name;
    private int level;
    private double power;
    private Set<Mage> apprentices;
    //Oskar proszę czy mam apprentices definiować w konstruktorze
    public Mage(String name, int level, double power,Set<Mage> apprentices)
    {
        this.name = name;
        this.level = level;
        this.power = power;
        this.apprentices = apprentices;
    }
    public void addToSet(Mage a)
    {
        this.apprentices.add(a);
    }
    public Set<Mage> getSet()
    {
        return this.apprentices;
    }
    public String getName() {
        return name;
    }
    public int getLevel() {
        return level;
    }
    public double getPower() {
        return power;
    }
    public Set<Mage> getApprentices() {
        return apprentices;
    }
    public void addMag(Mage a,String type)
    {
        if(this.apprentices == null)
        {
            if(type.equals("brak"))
            {
                this.apprentices = new HashSet<Mage>();

            }
            else if(type.equals("natural"))
            {
                this.apprentices = new TreeSet<Mage>();

            }
            else
            {
                this.apprentices = new TreeSet<Mage>(new MageCom());
            }
        }
        apprentices.add(a);
    }
    public boolean equals(Mage a)
    {
        if(this.name==a.name && this.level==a.level && this.power == a.power && Objects.equals(this.apprentices, a.apprentices))
            return true;
        else
            return false;
    }
    public int hashCode()
    {
        return Objects.hash(this.name, this.level, this.power);
    }
    @Override
    public String toString()
    {
        return "Mage{name='"+this.name+"', level="+this.level+", power="+this.power+"}";
    }

    @Override
    public int compareTo(Mage mage) {
        int result = name.compareTo(mage.name);
        if (result == 0) {
            result = Integer.compare(level, mage.level);
            if (result == 0) {
                result = Double.compare(power, mage.power);
            }
        }
        return result;
    }
}

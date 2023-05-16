package org.example;

import java.util.Optional;

public class EntityControler {
    private Repository rep;

    public EntityControler(Repository rep) {
        this.rep = rep;
    }
    //próba pobrania nieistniejącego obiektu powoduje zwrócenie obiektu String o wartości not found
    //próba pobrania istniejącego obiektu zwraca obiekt String reprezentując znaleziony obiekt encyjny,
    public String find(String name)
    {
        Optional<Entity> result = rep.find(name);
        if(result.isPresent())
        {
            Entity ent = result.get();
            return ent.getName();
        }
        else
        {
            return "notfound";
        }
    }
    // usunięcia nieistniejącego obiektu powoduje zwrócenie obiektu String o wartości done,
    // próba usunięcia nieistniejącego obiektu powoduje zwrócenie obiektu String o wartości not found,

    public String delete(String name)
    {
        try
        {
            rep.delete(name);
            return "done";
        }
        catch(IllegalArgumentException e)
        {
            return "not found";
        }

    }
    //próba zapisania nowego obiektu skutkuje wywołaniem metody z serwisu poprawnym parametrem i zwróceniem obiektu String o wartości done
    public String save(String name, String level) {
        int levl = Integer.parseInt(level);
        Entity ent = new Entity(levl, name);
        try
        {
            rep.save(ent);
            return "done";
        }
        catch (IllegalArgumentException e)
        {
            return "bad request";
        }
    }


}

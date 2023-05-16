package org.example;

import java.util.*;

public class Repository {
    private Collection<Entity> collection;

    public Repository() {
        this.collection = new ArrayList<Entity>();
    }
    public Optional<Entity> find(String name) {
        for (Entity entity : collection) {
            if (entity.getName().equals(name)) {
                return Optional.of(entity);
            }
        }
        return Optional.empty();
    }


    //próba usunięcia nieistniejącego obiektu powoduje IllegalArgumentException
    public void delete(String name)
    {
        boolean result = collection.removeIf(entity -> entity.getName().equals(name));
        if(!result)
        {
            throw new IllegalArgumentException("Object with the specified name does not exist");
        }
    }
    //próba zapisania obiektu, którego klucz główny już znajduje się w repozytorium powoduje IllegalArgumentException.
    public void save(Entity entity) {
        int id = entity.getId();
        boolean entityExists = collection.stream()
                .anyMatch(existingEntity -> existingEntity.getId() == id);
        if (entityExists)
        {
            throw new IllegalArgumentException("bad request");
        }
        collection.add(entity);
    }
}
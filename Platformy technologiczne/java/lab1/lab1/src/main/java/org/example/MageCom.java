package org.example;

import java.util.Comparator;
import java.util.Objects;

public class MageCom implements Comparator<Mage> {
    @Override
    public int compare(Mage o1, Mage o2) {
        int result = Integer.compare(o1.getLevel(), o2.getLevel());

        if (result == 0)
        {
            result = o1.getName().compareTo(o2.getName());
            if (result == 0)
            {
                result = Double.compare(o1.getPower(), o2.getPower());
            }
        }
        return result;
    }
}

package demo;

import java.util.ArrayList;
import java.util.List;

public class Instance {
    public int add(int num1, int num2) {
        return num1 + num2;
    }

    public String concat(String a, String b) {
        return a + b;
    }

    public void hello(String name) {
        System.out.println("Hello," + name);
    }

    public List<Person> merge(List<Person> persons, Person other) {
        List<Person> result = new ArrayList<Person>();
        if (persons != null) {
            result.addAll(persons);
        }
        if (other != null) {
            result.add(other);
        }
        return result;
    }
}

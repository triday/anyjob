package demo;

import java.util.ArrayList;
import java.util.List;
import java.util.concurrent.CompletableFuture;
import java.util.concurrent.Future;

public class Task {
    public Future<Integer> add(int num1, int num2) {
        return CompletableFuture.completedFuture(num1 + num2);
    }

    public Future<String> concat(String a, String b) {
        return CompletableFuture.completedFuture(a + b);
    }

    public Future<Void> hello(String name) {
        System.out.println("Hello," + name);
        return CompletableFuture.completedFuture(null);
    }

    public Future<List<Person>> merge(List<Person> persons, Person other) {
        List<Person> result = new ArrayList<Person>();
        if (persons != null) {
            result.addAll(persons);
        }
        if (other != null) {
            result.add(other);
        }
        return CompletableFuture.completedFuture(result);
    }
}

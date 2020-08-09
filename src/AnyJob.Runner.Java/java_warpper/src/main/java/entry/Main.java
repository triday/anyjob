package entry;

import com.alibaba.fastjson.*;
import com.alibaba.fastjson.util.TypeUtils;

import java.io.*;
import java.lang.reflect.Method;
import java.lang.reflect.Modifier;
import java.lang.reflect.Parameter;
import java.nio.charset.Charset;
import java.util.*;
import java.util.stream.Collectors;

public class Main {
    public static void main(String[] args) {
        if (args.length != 4) {
            System.out.println("Usage: java_warpper {className} {funcName} {inputFile} {outputFile}.");
            System.exit(1);
            return;
        }
        String className = args[0];
        String funcName = args[1];
        String inputFile = args[2];
        String outputFile = args[3];
        try {
            Class cls = Class.forName(className);
            Method method = getMethod(cls, funcName);
            JSONObject inputs = readFromInputFile(inputFile);
            if(inputs==null) inputs = new JSONObject();
            Object[] actionArgs = parseArgument(inputs, method);
            Object result = invokeMethod(method, cls, actionArgs);
            writeToOutputFile(outputFile, result);
            System.exit(0);
        } catch (Exception exception) {
            writeToOutputFile(outputFile, exception);
            System.exit(1);
        }
    }

    private static JSONObject readFromInputFile(String inputFile) {
        try {
            String content = readFileContent(inputFile);
            return JSONObject.parseObject(content);
        } catch (Exception ex) {
            throw new RuntimeException("Read input file error.", ex);
        }
    }

    private static Object[] parseArgument(JSONObject jsonObject, Method method) {
        Parameter[] parameters = method.getParameters();
        Object[] args = new Object[parameters.length];
        for (int i = 0; i < parameters.length; i++) {
            if(!parameters[i].isNamePresent()) {
                throw new IllegalArgumentException("Parameter names are not present, please compile with '-parameters' arguments.");
            }
            if (jsonObject.containsKey(parameters[i].getName())) {
                Object arg = jsonObject.get(parameters[i].getName());
                args[i] = TypeUtils.cast(arg, parameters[i].getParameterizedType(), null);
            } else {
                args[i] = null;
            }
        }
        return args;
    }

    private static void writeToOutputFile(String outputFile, Object obj) {
        try {
            Map<String, Object> results = new HashMap<>();
            if (obj instanceof Throwable) {
                results.put("error", buildErrorInfo((Throwable) obj));
            } else {
                results.put("result", obj);
            }
            String content = JSONObject.toJSONString(results);
            writeContentToFile(outputFile, content);
        } catch (Exception ex) {
            ex.printStackTrace();
        }
    }

    private static Map<String, String> buildErrorInfo(Throwable throwable) {
        Map<String, String> error = new HashMap<>();
        error.put("type", throwable.getClass().getName());
        error.put("message", throwable.getMessage());
        error.put("stack", Arrays.stream(throwable.getStackTrace()).map(s -> s.toString()).collect(Collectors.joining("\n")));
        return error;
    }

    private static Method getMethod(Class cls, String funcName) {
        List<Method> methods = Arrays.stream(cls.getMethods()).filter(p -> p.getName().equals(funcName)).collect(Collectors.toList());
        if (methods.size() == 0) {
            throw new RuntimeException(String.format("Can not find method '%s' in class '%s'.",,funcName, cls.getTypeName()));
        }
        if (methods.size() > 1) {
            throw new RuntimeException(String.format("Duplicate method '%s' in class '%s'.", funcName, cls.getTypeName()));
        }
        return methods.get(0);
    }

    private static Object invokeMethod(Method method, Class cls, Object... args) {
        try {
            int modifier = method.getModifiers();
            if (Modifier.isStatic(modifier)) {
                return method.invoke(null, args);
            } else {
                Object instance = cls.newInstance();
                return method.invoke(instance, args);
            }
        } catch (Exception ex) {
            throw new RuntimeException("Invoke method error.", ex);
        }
    }

    private static String readFileContent(String fileName) throws IOException {
        try (BufferedReader br = new BufferedReader(new InputStreamReader(new FileInputStream(fileName),
                Charset.forName("UTF-8")))) {
            StringBuilder sb = new StringBuilder(1024);
            char[] buffer = new char[16 * 1024];
            while (br.read(buffer) != -1) {
                sb.append(buffer);
            }
            return sb.toString();
        }
    }

    private static void writeContentToFile(String file, String str) throws IOException {
        try (BufferedWriter bw = new BufferedWriter(new OutputStreamWriter(new FileOutputStream(file,
                false),
                Charset.forName("UTF-8")));) {
            bw.write(str);
        }
    }
}

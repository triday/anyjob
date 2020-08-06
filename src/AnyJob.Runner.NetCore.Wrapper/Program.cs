using System;
using System.Linq;
using System.Reflection;
using System.Text.Json;
using System.Threading.Tasks;

namespace AnyJob.Runner.NetCore.Wrapper
{
    class Program
    {
       
        static JsonSerializerOptions JsonSerializerOptions = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        };
        static void Main(string[] args)
        {
            var document = JsonDocument.Parse("{\"num1\":100,\"num12\":200}");
            var type = typeof(Test);
            var methodInfo = type.GetMethod("Add");
            var arguments = ParseArguments(document, methodInfo);
            var res = InvokeMethod(type, methodInfo, arguments);
            //string dll = args[0];
            //string type = args[1];
            //string method = args[2];
            //string inputFile = args[3];
            //string outputFile = args[4];
            Console.WriteLine("Hello World!");

        }

        private static Assembly LoadAssembly(string dll)
        {
            return Assembly.LoadFrom(dll);
        }
        private static object InvokeMethod(Type type, MethodInfo method, object[] arguments)
        {
            var instance = method.IsStatic ? null : Activator.CreateInstance(type);
            var invokeResult = method.Invoke(instance, arguments);
            if (invokeResult == null) return null;
            if (method.ReturnType.IsGenericType)
            {
                if (method.ReturnType.GetGenericTypeDefinition() == typeof(ValueTask<>)||method.ReturnType.GetGenericTypeDefinition()==typeof(Task<>))
                {
                    return (invokeResult as dynamic).Result;
                }
            }
            if (method.ReturnType == typeof(Task)||method.ReturnType==typeof(ValueTask))
            {
                (invokeResult as dynamic).GetAwaiter().GetResult();
                return null;
            }
          
          

            return invokeResult;
            
        }

        private static object GetTaskResult(MethodInfo method, object methodReturn)
        {
            if (method.ReturnType.IsGenericType)
            {
                return (methodReturn as dynamic).Result;
            }
            else
            {
                (methodReturn as Task).Wait();
                return null;
            }
        }

        private static Type GetType(Assembly assembly, string typeName)
        {
            return assembly.GetType(typeName);
        }
        private static object[] ParseArguments(JsonDocument document, MethodInfo methodInfo)
        {
            return methodInfo.GetParameters().Select(p =>
             {
                 if (document.RootElement.TryGetProperty(p.Name, out var jsonElement))
                 {
                     return JsonSerializer.Deserialize(jsonElement.GetRawText(), p.ParameterType, JsonSerializerOptions);
                 }
                 else
                 {
                     return DefaultValue.Get(p.ParameterType);
                 }
             }).ToArray();

        }


    }
}

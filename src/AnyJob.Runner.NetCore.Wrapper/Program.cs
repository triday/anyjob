using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text.Json;
using System.Threading.Tasks;

namespace AnyJob.Runner.NetCore.Wrapper
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length != 5)
            {
                throw new Exception("Usage: NetCore_Wrapper {{assemblyFile}} {{typeName}} {{methodName}} {{inputFile}} {{outputFile}}.");
            }
            string assemblyName = args[0];
            string typeName = args[1];
            string methodName = args[2];
            string inputFile = args[3];
            string outputFile = args[4];
            try
            {
                var assembly = LoadAssembly(assemblyName);
                var type = LoadType(assembly, typeName);
                var method = FindMethod(type, methodName);
                var document = ReadInputFile(inputFile);
                var arguments = ParseArguments(document, method);
                var result = InvokeMethod(type, method, arguments);
                WriteToOutput(result, outputFile);
            }
#pragma warning disable CA1031 // 不捕获常规异常类型
            catch (Exception ex)
#pragma warning restore CA1031 // 不捕获常规异常类型
            {
                WriteToOutput(ex, outputFile);
            }
        }

        private static void WriteToOutput(object resultOrException, string filePath)
        {
            try
            {
                if (resultOrException is Exception exception)
                {
                    JsonUtils.WriteResultToFile(new Dictionary<string, object>
                    {
                        ["error"] = TypedError.FromException(exception)
                    }, filePath); ;
                }
                else
                {
                    JsonUtils.WriteResultToFile(new Dictionary<string, object>
                    {
                        ["result"] = resultOrException
                    }, filePath);
                }
            }
#pragma warning disable CA1031 // 不捕获常规异常类型
            catch (Exception ex)
#pragma warning restore CA1031 // 不捕获常规异常类型
            {
                Console.WriteLine($"Write to output file failure, the output file '{filePath}'.");
                Console.WriteLine(ex);
            }
        }

        private static Assembly LoadAssembly(string assembly)
        {
            try
            {
                return Assembly.LoadFrom(assembly);
            }
            catch (Exception ex)
            {

                throw CodeException.LoadAssemblyError(assembly, ex);
            }

        }
        private static Type LoadType(Assembly assembly, string type)
        {
            try
            {
                return assembly.GetType(type, true);
            }
            catch (Exception ex)
            {
                throw CodeException.LoadTypeError(type, assembly.FullName, ex);
            }

        }
        private static MethodInfo FindMethod(Type type, string methodName)
        {
            var methods = type.GetMethods(BindingFlags.Public | BindingFlags.Instance | BindingFlags.Static)
                        .Where(p => p.Name == methodName).ToList();
            if (methods.Count > 1)
            {
                throw CodeException.DuplicateMethodError(methodName, type);
            }
            if (methods.Count == 0)
            {
                throw CodeException.MethodNotFoundError(methodName, type);
            }
            return methods.First();
        }

        private static JsonDocument ReadInputFile(string filePath)
        {
            try
            {
                return JsonUtils.FromFile(filePath);
            }
            catch (Exception ex)
            {
                throw CodeException.ReadInputFileError(filePath, ex);
            }
        }
        private static object InvokeMethod(Type type, MethodInfo method, object[] arguments)
        {
            try
            {
                var instance = method.IsStatic ? null : Activator.CreateInstance(type);
                var invokeResult = method.Invoke(instance, arguments);
                if (invokeResult == null) return null;
                if (method.ReturnType.IsGenericType)
                {
                    if (method.ReturnType.GetGenericTypeDefinition() == typeof(ValueTask<>) || method.ReturnType.GetGenericTypeDefinition() == typeof(Task<>))
                    {
                        return (invokeResult as dynamic).Result;
                    }
                }
                if (method.ReturnType == typeof(Task) || method.ReturnType == typeof(ValueTask))
                {
                    (invokeResult as dynamic).GetAwaiter().GetResult();
                    return null;
                }
                return invokeResult;
            }
            catch (Exception ex)
            {
                throw CodeException.InvokeMethodError(ex);
            }
        }

        private static object[] ParseArguments(JsonDocument document, MethodInfo methodInfo)
        {
            return methodInfo.GetParameters().Select(p =>
             {
                 if (document.RootElement.TryGetProperty(p.Name, out var jsonElement))
                 {
                     return JsonUtils.Deserialize(jsonElement.GetRawText(), p.ParameterType);
                 }
                 else
                 {
                     return DefaultValue.Get(p.ParameterType);
                 }
             }).ToArray();
        }
        private static object DeserializeParameter(JsonElement jsonElement, ParameterInfo parameterInfo)
        {
            try
            {
                return JsonUtils.Deserialize(jsonElement.GetRawText(), parameterInfo.ParameterType);
            }
            catch (Exception ex)
            {

                throw CodeException.ConvertParameterValueError(parameterInfo, ex);
            }
        }
    }
}

using System;
using System.Reflection;
using System.Linq;

namespace Csharp_Examples
{
    public static class ReflectionsExamples
    {
        public static void RunTests()
        {
            var assembly = Assembly.GetExecutingAssembly();

            var types = assembly.GetTypes().Where(t => t.GetCustomAttributes<ClassExampleAttribute>().Any());
            foreach (var type in types)
            {
                Console.WriteLine(type);
                var methods = type.GetMethods().Where(m => m.GetCustomAttributes<MethodExampleAttribute>().Any());
                foreach (var method in methods)
                {
                    Console.WriteLine(method.Name);
                }
            }
            Sample sample = new Sample { Name = "Test name", Age = 24 };
            SomeClass some = new SomeClass();
            object[] parametersArray = { some };

            assembly.GetTypes().ToList().ForEach(t => {
                Console.WriteLine(t.FullName);
            });

            Console.WriteLine("\nTest Invoke: ");

            Type sampleType = assembly.GetType("Csharp_Examples.Sample");
            sampleType.GetMethod("ProcessThis").Invoke(sample, parametersArray);

            Console.WriteLine("\nTest Another way: ");

            Type s = typeof(Sample);
            object sampleInstance = Activator.CreateInstance(s, null);
            s.GetMethod("ExampleMethod").Invoke(sampleInstance, null);

            MethodInfo methodInfo = s.GetMethod("ProcessThis");
            ParameterInfo[] parameterInfos = methodInfo.GetParameters();
            foreach (ParameterInfo p in parameterInfos) { Console.WriteLine("Param: " + p.Name); }

            methodInfo.Invoke(sampleInstance, parametersArray);
        }
    }
    //Reflections Usage//
    [ClassExample]
    public class Sample
    {
        public string Name { get; set; }
        public int Age;

        [MethodExample]
        public void ExampleMethod()
        {
            Console.WriteLine("Message from inside the ExampleMethod.");
        }

        public void NonAttributeMethod()
        {
            Console.WriteLine("Message from inside the NonAttributeMethod.");
        }

        public static void ProcessThis(SomeClass some)
        {
            some.Name = "Sample Name"; some.PrintInfo();
        }
    }

    [AttributeUsage(AttributeTargets.Class)]
    public class ClassExampleAttribute : Attribute { }

    [AttributeUsage(AttributeTargets.Method)]
    public class MethodExampleAttribute : Attribute { }

    public class SomeClass
    {
        public string Name;

        public void PrintInfo()
        {
            Console.WriteLine(Name + " was called.");
        }
    }

    public static class AssemblyLoader
    {
        public static void Load(string methodName, string assemblyFile)
        {
            Assembly assembly = Assembly.LoadFile("...Assembly1.dll");
            Type type = assembly.GetType("TestAssembly.Main");

            MethodInfo methodInfo = type?.GetMethod(methodName);
            if (methodInfo != null)
            {
                object result = null;
                ParameterInfo[] parameters = methodInfo.GetParameters();
                object classInstance = Activator.CreateInstance(type, null);

                if (parameters.Length == 0)
                    result = methodInfo.Invoke(classInstance, null);
                else
                {
                    object[] parametersArray = new object[] { "Hello" };
                    result = methodInfo.Invoke(classInstance, parametersArray);
                }
            }
        }
    }
}

using System;
namespace Csharp_Examples
{
    public static class  ExtensionsExamples
    {
        public static void RunTests()
        {
            NoMethods noMethods = new NoMethods();
            noMethods.WhatDoIDo("Some tool", "some work.");
            noMethods.DoSomething();
        }
    }

    public class NoMethods
    {
        public string Name;
        public string Purpose;
    }


    //Extensions I want Added//
    public static class Extensions
    {
        public static void WhatDoIDo(this NoMethods obj, string name, string purpose)
        {
            obj.Name = name;
            obj.Purpose = purpose;
        }

        public static void DoSomething(this NoMethods obj)
        {
            Console.WriteLine(obj.Name + " does " + obj.Purpose);
        }
    }
}

using System;
namespace Csharp_Examples
{
    public static class GenericsExamples
    {
        public static void RunTests()
        {
            // Call service, dal, etc..
            ResultInt result = new ResultInt { Success = true, Data = 5 };
            ResultString result2 = new ResultString { Success = false, Data = "Failure" };

            //Console.WriteLine(result.GetResults());
            //Console.WriteLine(result2.GetResults());

            DataWrapper<ResultInt>.InvokeGeneric(result);

            DataWrapper<ResultString> dw = new DataWrapper<ResultString>(result2);
            dw.InvokeGeneric();

            DataFactory.CreateComponent<ResultString>(true, "Mikey");
            result.GetComponent<ResultInt>();
            result2.GetComponent<ResultString>();
        }
    }


    //----------------Using Interfaces
    public interface IResultable
    {
        string GetResults();
    }

    public class ResultInt : IResultable
    {
        public bool Success { get; set; }
        public int Data { get; set; }

        public string GetResults()
        {
            return "Success: " + Success.ToString() +
                   "  Data: " + Data.ToString();
        }
    }

    //----------------Using Inheritance
    public class Resultable
    {
        public virtual string GetResults() {
            return String.Empty;
        }
    }

    public class ResultString : Resultable
    {
        public bool Success { get; set; }
        public string Data { get; set; }

        public ResultString()
        {

        }

        public ResultString(bool val, string data)
        {
            Success = val;
            Data = data;
        }

        public override string GetResults()
        {
            return "Success: " + Success.ToString() +
                    "  Data: " + Data.ToString();
        }
    }


    //----------------Generic Wrapper for Tests
    public class DataWrapper<T>
    {
        T instanceVariable;

        public DataWrapper(T param)
        {
            instanceVariable = param;
        }

        public void InvokeGeneric()
        {
            var value = instanceVariable.GetType().GetMethod("GetResults").Invoke(instanceVariable, null);
            Console.WriteLine(value);
        }

        public static void InvokeGeneric(T randomClass)
        {
            var value = randomClass.GetType().GetMethod("GetResults").Invoke(randomClass, null);
            Console.WriteLine(value);
        }
    }

    public static class DataFactory
    {
        public static void CreateComponent<X>(bool value, string data)
        {
            var sample = typeof(X);
            object instance = Activator.CreateInstance(sample, new object[] { value, data });
            Console.WriteLine(sample.GetMethod("GetResults").Invoke(instance, null));
        }

        //Extension Type
        public static void GetComponent<Y>(this object obj)
        {
            Y sample = (Y)obj;
            Console.WriteLine(sample.GetType().GetMethod("GetResults").Invoke(sample, null));
        }
    }
}

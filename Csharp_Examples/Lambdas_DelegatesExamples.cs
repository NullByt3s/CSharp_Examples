using System;
namespace Csharp_Examples
{
    public static class Lambdas_DelegatesExamples
    {
        public static void RunTests()
        {
            //What is Delegate?
            //Def 1 : type that represents references to methods with
            //          a particular parameter list and return type.
            //Def 2 : an entity passing something to another entity,
            //          and narrowly to various specific forms of relationships.

            Func<int, string> howManyLicks = delegate(int value) 
            {
                return value + " licks to the center of a TootsiePop.";
            };
            Console.WriteLine("How many licks does it take?, " + howManyLicks(3));

            //What is a Lambda?
            // Def: is a function definition that is not bound to an identifier.

            howManyLicks = (myVal) => { return "Whoops, I did " + myVal + " lick too many."; };
            Console.WriteLine(howManyLicks(1));


            //What is a closure?
            // Def: is a first-class function with free variables that are bound in the lexical environment.

            //What is a “first-class function”?
            // Def: a function similary to a first-class datatype, function can be assigned to a variable, 
            //  passed around, and invoked. Delegates and Lambda Functions for example.

            //What are free variables?
            // Def: variable which is referenced in a function that 
            //  is not a parameter of the function or a local variable of the function

            //For example
            string outsideString = " outside.";

            //The Function is closing over the variable outsideString inside the function.
            Func<string, string> whereAreYou = delegate (string weather){
                return weather + outsideString;
            };
            Console.WriteLine("Where are you variable? " + whereAreYou("I am"));



            //Interesting Stuff

            //Binding some free variables to the lexical environment
            var Call = GetCallers();

            Console.WriteLine(Call("Mark"));

            Console.WriteLine(Call("Jayson"));

            Console.WriteLine(Call("Susie"));

            //Binding up a method with some data and passing it around
            // == the definition of a class.

            Console.WriteLine("End of Lambdas/Delegates");
        }


        public static Func<string, string> GetCallers()
        {
            //Local string variable
            string CurrentCallers = "Callers: ";

            //Creating a function
            Func<string, string> log = delegate (string name){

                //Binding a local variable inside the delegate
                CurrentCallers += name;
                return CurrentCallers;
            };

            //Returning a function with bound free variable
            return log;
        }

        //Another way
        public static Func<string, string> GetCallersV2()
        {
            string CurrentCallers = "Callers: ";

            //Same as above only shorter with lambdas
            return caller => CurrentCallers += caller;
        }
    }
}

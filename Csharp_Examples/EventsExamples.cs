using System;
namespace Csharp_Examples
{
    public static class EventsExamples
    {
        public static void RunTests()
        {
            Scheduler sch = new Scheduler();
            Player player = new Player("Michael", sch);

            sch.StartTurn();
            sch.EndTurn();

            sch.StartTurn();
        }
    }

    public class Player
    {
        private string _name;
        private Scheduler _ts;

        public Player(string name, Scheduler ts)
        {
            _name = name;
            _ts = ts;

            //chaining delegates
            _ts.OnTurn += (object sender, ScheduledEventArgs args) =>
            {
                string state = "ending";
                if (args.IsTurn)
                {
                    state = "starting";
                    args.UpdateTurn();
                    Console.WriteLine("Turn " + args.TurnCount + " :");
                }

                Console.WriteLine("Player, " + _name + " is " + state + " his turn."); 
            };

            _ts.OnTurn += (object sender, ScheduledEventArgs args) => { 
                    Console.WriteLine(_name + (args.IsTurn? " attacks." : " defends."));
            };
        }
    }

    //Creating a class that inherits from eventArgs
    //This allows us to have custom properties
    public class ScheduledEventArgs: EventArgs
    {
        public bool IsTurn { get; set; }
        public int TurnCount { get { return count; } }
        private static int count;
        public void UpdateTurn() { count++; }
    }

    public delegate void TurnEventHandler(object sender, ScheduledEventArgs args);
    public class Scheduler
    {
        //public events are just glorified delegates
        public event TurnEventHandler OnTurn;

        public void StartTurn()
        {
            OnTurn(this, new ScheduledEventArgs { IsTurn = true});
        }

        public void EndTurn()
        {
            OnTurn(this, new ScheduledEventArgs { IsTurn = false});
        }
    }
}

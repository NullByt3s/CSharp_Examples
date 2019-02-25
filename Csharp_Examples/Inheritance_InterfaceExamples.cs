using System;
namespace Csharp_Examples
{
    public static class Inheritance_InterfaceExamples
    {
        public static void RunTests()
        {
            Console.WriteLine("Base Ability: ");
            Ability normal = new Ability();
            normal.Activate();

            Console.WriteLine("\n\nDerived Ability: ");
            Ability openChest = new Open("Open Chest.");
            openChest.Activate();


            Console.WriteLine("\n\nInterface Spell: ");
            Fireball fireball = new Fireball(3.5f, (d) => { Console.WriteLine("Burn..."); });

            Console.WriteLine("-----------");
            fireball.Cast();

            Console.WriteLine("\n\nUsing both Interface & Inheritance: ");
            SuperAbility myUlt = new SuperAbility("Lord of Vermillion", (cool) =>
            {
                Console.WriteLine("Lightning crashes down from the heavens, the caster needs to rest for " + cool + "seconds");
            });

            myUlt.SetCooldown(6);
            myUlt.Cast();

            Console.WriteLine("\n\nEnd of Inheritance/Interface Examples");
        }

        //----------------Base Class----------------
        private class Ability
        {
            //Protected so derived classes can access
            protected string _name = "No Name";

            //Getter
            public string MyName() { return _name; }

            //Virtual method to be overwritten later
            public virtual void Activate()
            {
                Console.WriteLine("Ability is now on cooldown.");
            }
        }


        //----------------Using Inheritance----------------
        private class Open : Ability
        {
            public Open(string name)
            {
                //Accessing & Setting parent variable
                _name = name;
            }

            public override void Activate()
            {
                //Overriding original functinality
                Console.WriteLine("Using the ability " + MyName());

                //Using Parent Functionality also
                base.Activate();
            }
        }



        //----------------Using Interfaces----------------
        private interface Castable
        {
            //Mandatory Function prototype
            //All classes using it must have this inside the class.
            void Cast();
        }


        //Looks exactly like inheritance
        private class Fireball : Castable
        {
            public Action<float> MyAction { get; private set; }
            float damage;

            public Fireball(float damage, Action<float> action)
            {
                this.damage = damage;
                MyAction = action;
            }

            //Implementing interface function
            public void Cast()
            {
                MyAction.Invoke(damage);
                Console.WriteLine(GetType().Name + " was cast, doing " + damage + " damage.");
            }
        }


        //----------------Inheritance VS Interface----------------

        //Whats the difference?
        //  Answer: Not much except...

        //Classes Can only Inherit from only 1 class
        //But Classes can have many interfaces.

        private class Ultimate
        {
            protected int cooldown;

            public void SetCooldown(int seconds)
            {
                cooldown = seconds;
            }
        }

        /*

        ERROR: Cant have multiple base classes.
        
        public class SuperAbility : Ability, Ultimate  
        {
            public SuperAbility(string supername)
            {
                _name = supername;
            }
        }
        */


        //Chaing Ultimate to an interface
        private interface UltimateV2
        {
            void SetCooldown(int seconds);
        }


        //Combining everything together
        private class SuperAbility : Ability, UltimateV2, Castable
        {
            int cooldown = int.MaxValue;
            Action<int> ultimateAction;

            public SuperAbility(string supername, Action<int> action)
            {
                _name = supername;
                ultimateAction = action;
            }

            public void SetCooldown(int seconds)
            {
                cooldown = seconds;
            }

            public void Cast()
            {
                Console.WriteLine(_name +  " spell was used, screen fades black.");
                ultimateAction.Invoke(cooldown);
                Activate();
            }
        }




    }
}

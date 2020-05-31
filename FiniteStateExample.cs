using System;
/// 
/// In this file i want to give an example of how a finite state machine could be used in different settings to the ones we've seen in our practical tasks 
/// my hope is that you can see this example and maybe it will help to reinforce the concept of finite state machines and supply an example to better illustrate this concept
/// for this im going to create a character object, the character has multiple guns he can use but the guns can only be used in a certain order once the ammunition of the previous gun is comsumed.
/// eg gun 1 -> gun 2 -> gun 3 -> gun 1 etc.
/// Similar to Aphelios from League of Legends
/// 
///  For instance the player has 
///  - state 1 where he can shoot a handgun (50 ammo) and he has the ability to punch
///  - state 2 where he can shoot a shotgun (20 ammo) and he also has the ability to kick
///  - state 3 where he can use a minigun (500 ammo) and he lacks the ability to punch or kick
/// 
/// 

namespace FiniteStateMachine
{
    class Character
    {
        // this is the variable that allows you to forward an operation to its class variant, it is set to static so all nested classes may access it
        public static State _currentState;

        //the player begins in our base state where he has a handgun and the ability to punch
        public void initiateCharacter()
        {
            _currentState = new state1();
        }
        //forwards the shoot method to its current state
        public void Shoot()
        {
            _currentState.Shoot();
        }
        //performs the relevant melee attack depending on the state
        public void Melee()
        {
            //since the player can do one of either Punch or kick we can call both of them for every player input
            _currentState.Kick();
            _currentState.Punch();
        }
        public abstract class State
        {
            //base class for the states
            //this class contains the abstract methods that are implemented in our states 
            public int _ammunition;
            public abstract void Punch();
            public abstract void Kick();
            public abstract void Shoot();
        }
        // these nested classes represent the states that the character can have
        // in each state we override the methods created in the abstract template class
        // this way we can ensure that the correct actions happen when a method is called for the character object
        class state1 : State
        {
            public state1()
            {
                Console.WriteLine("State 1: Hangun and Punching");
                this._ammunition = 50;
            }
            public override void Punch()
            {
                Console.WriteLine("Punch!");
            }
            public override void Kick()
            { 
            }
            public override void Shoot()
            {
                Console.WriteLine("pew!");
                _ammunition -= 1;
                if (_ammunition == 0)
                {
                    _currentState = new state2();
                }
            }
        }
        class state2 : State
        {
            public state2()
            {
                Console.WriteLine("State 2: Shotgun and Kicking");
                this._ammunition = 20;
            }
            public override void Punch()
            {
            }
            public override void Kick()
            {
                Console.WriteLine("Kick!");
            }
            public override void Shoot()
            {
                Console.WriteLine("Boom!");
                _ammunition -= 1;
                if (_ammunition == 0)
                {
                    _currentState = new state3();
                }
            }
        }
        class state3 : State
        {
            public state3()
            {
                Console.WriteLine("State 3: Minigun only");
                this._ammunition = 500;
            }
            public override void Punch()
            {
            }
            public override void Kick()
            {
            }
            public override void Shoot()
            {
                Console.WriteLine("PEW!");
                _ammunition -= 1;
                if (_ammunition == 0)
                {
                    _currentState = new state1();
                }
            }
        }
    }
    class FiniteStateExample
    {
        static void Main(string[] args)
        {
            Character Character = new Character();
            //Creates that character object
            Character.initiateCharacter();

            //performs the melee attack for the current state
            Character.Melee();
            //expends all ammo for the handgun and moves to the next state
            for (int x = 0; x < 50; x++)
            {
                Character.Shoot();
            }
            //performs the melee attack for the current state
            Character.Melee();
            //expends all ammo for the shotgun and moves to the next state
            for (int x = 0; x < 20; x++)
            {
                Character.Shoot();
            }
            //performs the melee attack for the current state
            //since the minigun has no melee abilities nothing happens
            Character.Melee();
            //expends all ammo for the minigun and moves to the next state / back to state 1
            for (int x = 0; x < 500; x++)
            {
                Character.Shoot();
            }

        }
    }
}

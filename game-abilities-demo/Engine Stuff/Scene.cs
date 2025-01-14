﻿using System;
using System.Collections.Generic;
using System.Text;
using Raylib_cs;

namespace GameAbilitiesDemo
{
    class Scene
    {
        /// <summary>
        /// Array that stores all actors in the scene
        /// </summary>
        private Actor[] _actors;
        /// <summary>
        /// Array that stores UI stuff only
        /// </summary>
        private Actor[] _UIElements;
        public static Actor[] SceneOneActors;
        public static Actor[] SceneOneUIElements;

        public Scene()
        {
            _actors = new Actor[0];
            _UIElements = new Actor[0];
        }

        /// <summary>
        /// Returns the private actors array
        /// </summary>
        public Actor[] Actors
        {
            get { return _actors; }
        }
        
        /// <summary>
        /// Contains a virtual start function that can be used by inherrting classes
        /// </summary>
        public virtual void Start()
        { }

        /// <summary>
        /// Calls update for every actor in the scene.
        /// Also checks for collision for every actor in the scene.
        /// Calls start for the actor if it hasn't already been called
        /// </summary>
        public virtual void Update(float deltaTime)
        {
            for (int i = 0; i < _actors.Length; i++)
            {
                if (!_actors[i].Started)
                    _actors[i].Start();
                _actors[i].Update(deltaTime);

                for (int j = 0; j < _actors.Length; j++)
                {
                    if (i < _actors.Length)
                        if (_actors[i].CheckForCollision(_actors[j]) && j != i)
                        _actors[i].OnCollision(_actors[j]);
                }
            }
        }

        /// <summary>
        /// Updates all of the scene's UI elements and starts them if they have not already been started.
        /// </summary>
        /// <param name="deltaTime"></param>
        public virtual void UpdateUI(float deltaTime)
        {
            for (int i = 0; i < _UIElements.Length; i++)
            {
                if (!_UIElements[i].Started)
                    _UIElements[i].Start();
                _UIElements[i].Update(deltaTime);
            }
        }

        /// <summary>
        /// Calls all of the scene's actor's draw functions.
        /// </summary>
        public virtual void Draw()
        {
            for (int i = 0; i < _actors.Length; i++)
                _actors[i].Draw();
        }

        /// <summary>
        /// Calls all of the scene's UI element's draw functions
        /// </summary>
        public virtual void DrawUI()
        {
            for (int i = 0; i < _UIElements.Length; i++)
                _UIElements[i].Draw();
        }



        /// <summary>
        /// Called at the end of a scene
        /// Removes all actors from the scene
        /// </summary>
        public virtual void End()
        {
            for (int i = 0; i < _actors.Length; i++)
                _actors[i].DestroySelf();
        }

        /// <summary>
        /// Initializes all actors for all scenes and stores them inside static arrays
        /// </summary>
        public static void InitializeActors()
        {
            //Initializing scene one actors
            Player player = new Player(0, 1, 0, 6, 0.5f, 1, Color.SKYBLUE);
            EnemySpawner enemySpawner = new EnemySpawner(player);
            Engine.Camera = new Camera(player);

            //Initializing scene one UI elements 
            UIText leaveGameUI = new UIText(0, 20, 10, Shape.CUBE, "Lose Text", Color.PURPLE, 800, 200, 50, "LEAVE GAME: ESC");
            UIText dashAbilityUI = new UIText(0, 90, 10, Shape.CUBE, "Lose Text", Color.PURPLE, 800, 200, 50, "DASH: CTRL");
            UIText throwGrenadeUI = new UIText(0, 160, 10, Shape.CUBE, "Lose Text", Color.PURPLE, 800, 200, 50, "THROW GRENADE: ONE");
            UIText fortifyAbilityUI = new UIText(0, 230, 10, Shape.CUBE, "Lose Text", Color.PURPLE, 800, 200, 50, "FORTIFY: TWO");

            SceneOneActors = new Actor[] { enemySpawner, player, Engine.Camera };
            SceneOneUIElements = new Actor[] { dashAbilityUI, throwGrenadeUI, fortifyAbilityUI, leaveGameUI };
        }

        /// <summary>
        /// Adds an actor the scenes list of actors
        /// </summary>
        /// <param name="actor"></param>
        public void AddActor(Actor actor)
        {
            //Create a temp array larger than the original
            Actor[] tempArray = new Actor[_actors.Length + 1];

            //Copy all values from the original array into the temp array
            for (int i = 0; i < _actors.Length; i++)
            {
                tempArray[i] = _actors[i];
            }
            //Adds the new actor to the end of the new array
            tempArray[_actors.Length] = actor;

            //Set the old array to be the new array;
            _actors = tempArray;
        }

        /// <summary>
        /// Adds an array of actors the scenes list of actors
        /// </summary>
        /// <param name="actor"></param>
        public void AddActor(Actor[] actors)
        {
            //Create a temp array larger than the original
            Actor[] tempArray = new Actor[_actors.Length + actors.Length];

            int j = 0;
            //Copy all values from the original array into the temp array
            for (int i = 0; i < _actors.Length; i++)
            {
                tempArray[i] = _actors[i];
                j++;
            }
            
            for (int i = 0; i < actors.Length; i++)
            {
                tempArray[j] = actors[i];
                j++;
            }

            //Set the old array to be the new array;
            _actors = tempArray;
        }

        /// <summary>
        /// Adds an actor to the scenes list of UI elements.
        /// </summary>
        /// <param name="actor"></param>
        public void AddUIElement(Actor UI)
        {
            //Create a temp array larger than the original
            Actor[] tempArray = new Actor[_UIElements.Length + 1];

            //Copy all values from the original array into the temp array
            for (int i = 0; i < _UIElements.Length; i++)
            {
                tempArray[i] = _UIElements[i];
            }
            //Adds the new actor to the end of the new array
            tempArray[_UIElements.Length] = UI;

            //Set the old array to be the new array;
            _UIElements = tempArray;
        }

        /// <summary>
        /// Adds an array of UI elements to the_UIEelements array
        /// </summary>
        /// <param name="actors">The array of UI elements</param>
        public void AddUIElement(Actor[] actors)
        {
            //Create a temp array larger than the original
            Actor[] tempArray = new Actor[_UIElements.Length + actors.Length];

            int j = 0;
            //Copy all values from the original array into the temp array
            for (int i = 0; i < _UIElements.Length; i++)
            {
                tempArray[i] = _UIElements[i];
                j++;
            }
            
            for (int i = 0; i < actors.Length; i++)
            {
                tempArray[j] = actors[i];
                j++;
            }

            //Set the old array to be the new array;
            _UIElements = tempArray;
        }

        /// <summary>
        /// Removes an actor from the scene list of actors
        /// </summary>
        /// <param name="actor"></param>
        /// <returns></returns>
        public bool RemoveUIElement(UIText actor)
        {
            //Create a variable to store if the removal was successful
            bool actorRemoved = false;
            //Create a new array that is smaller than the original
            Actor[] tempArray = new Actor[_UIElements.Length - 1];

            //Creates a variable to store the index of the temparray
            int j = 0;
            //Copies all of the values except the actor we don't want into the new array
            for (int i = 0; i < _UIElements.Length; i++)
            {
                //If the actor that th eloop is on is no tthe one to remove...
                if (_UIElements[i] != actor)
                {
                    //...add the actor into the new array and increment the temp array counter
                    tempArray[j] = _UIElements[i];
                    j++;
                }

                //Otherwise if this actor is the one to remove...
                else
                {
                    //...Set acorRemoved to true
                    actorRemoved = true;
                }
            }

            //If the actor removal was successful...
            if (actorRemoved)
                //Set the actors array to be the new array
                _UIElements = tempArray;

            //Return if an actor was removed
            return actorRemoved;
        }


        /// <summary>
        /// Removes an actor from the scene list of actors
        /// </summary>
        /// <param name="actor"></param>
        /// <returns>True or false depending whether or not an actor has been removed</returns>
        public bool RemoveActor(Actor actor)
        {
            //Create a variable to store if the removal was successful
            bool actorRemoved = false;
            //Create a new array that is smaller than the original
            Actor[] tempArray = new Actor[_actors.Length - 1];

            //Creates a variable to store the index of the temparray
            int j = 0;
            //Copies all of the values except the actor we don't want into the new array
            for (int i = 0; i < _actors.Length; i++)
            {
                //If the actor that th eloop is on is no tthe one to remove...
                if (_actors[i] != actor)
                {
                    //...add the actor into the new array and increment the temp array counter
                    tempArray[j] = _actors[i];
                    j++;
                }

                //Otherwise if this actor is the one to remove...
                else
                {
                    //...Set acorRemoved to true
                    actorRemoved = true;
                }
            }

            //If the actor removal was successful...
            if (actorRemoved)
                //Set the actors array to be the new array
                _actors = tempArray;

            //Return if an actor was removed
            return actorRemoved;
        }
    }
}

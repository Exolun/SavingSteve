using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Scripts
{

    public enum GameStates
    {
        Intro,
        Playing,
        Paused,
        Dead
    }
    
    public static class GameManager
    {
        #region Fields
        private static GameStates currentState = GameStates.Playing;
        #endregion

        #region Getters/Setters
        public static string GetCurrentLevel()
        {
            return SceneManager.GetActiveScene().name;
        }
        

        public static GameStates CurrentState()
        {
            return currentState;
        }

        public static void SetState(GameStates state)
        {
            currentState = state;
        }
        #endregion

        #region Global Events
        public static void PlayerDied()
        {
            GameManager.SetState(GameStates.Dead);
            GameObject.FindWithTag("UI").GetComponent<UIController>().ShowDeath();
            GameObject.FindWithTag("Audio").GetComponent<AudioController>().PlaySound("Death");
        }

        public static void PlayerCompletedLevel()
        {
            string current = GetCurrentLevel();
            int levelNumber = int.Parse(current.Split('-')[1]);
            string next = String.Format("Level-{0}", levelNumber + 1);
            SceneManager.LoadScene(next);            
        }

        internal static void PlayerWins()
        {
            GameObject.FindWithTag("UI").GetComponent<UIController>().ShowVictory();            
            GameManager.SetState(GameStates.Paused);
        }
        #endregion
    }
}

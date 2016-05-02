using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Scripts
{
    enum IntroStates
    {
        Initial,
        MeetSteve,
        SteveLikesWalks,
        StevePoorJudgment,
        ShowGlasses,
        SteveIsBlind,
        WalkingThroughADungeon,
        ShowDungeon,
        ExplainSpheres,
        ShowSpheres,
        UseArrowKeys,
        FinalConfirmation,
        StartLevelOne,
    }

    public class IntroManager : MonoBehaviour
    {
        public GameObject steveCam;
        public GameObject glasses;
        public GameObject hazards;
        public GameObject spheres;
        public GameObject arrows;
        public GameObject arrowCam;

        private static IntroStates currentState = IntroStates.Initial;
        private UIController ui;

        private TimeSpan? timeToWait = null;
        private DateTime timeWaitingStarted;

        public void Start()
        {
            GameManager.SetState(GameStates.Intro);
            this.ui = GameObject.FindWithTag("UI").GetComponent<UIController>();
            ui.ShowIntro();            
        }

        public void Update()
        {
            if(timeToWait != null)
            {
                if(DateTime.Now - timeWaitingStarted > timeToWait)
                {
                    timeToWait = null;
                    DoNext();
                }
            }
        }

        public void DoNext()
        {
            currentState++;
            if(currentState == IntroStates.MeetSteve)
            {
                this.steveCam.SetActive(true);
                this.ui.HideDialogs();
                timeWaitingStarted = DateTime.Now;
                timeToWait = TimeSpan.FromSeconds(1);
            }
            else if(currentState == IntroStates.SteveLikesWalks)
            {
                this.ui.ShowIntro();
                GameObject.FindGameObjectWithTag("HeaderText").SetActive(false);
                this.setIntroText("Steve likes to explore during his morning walks");
            }
            else if (currentState == IntroStates.SteveLikesWalks)
            {
                this.ui.ShowIntro();
                GameObject.FindGameObjectWithTag("HeaderText").SetActive(false);
                this.setIntroText("Steve likes to explore during his morning walks.");
            }
            else if (currentState == IntroStates.StevePoorJudgment)
            {
                this.setIntroText("Steve has very poor judgement when choosing the route for his walks.");
            }
            else if (currentState == IntroStates.ShowGlasses)
            {
                this.ui.HideDialogs();
                this.glasses.SetActive(true);
                timeWaitingStarted = DateTime.Now;
                timeToWait = TimeSpan.FromSeconds(1);
            }
            else if (currentState == IntroStates.SteveIsBlind)
            {
                this.ui.ShowIntro();
                setIntroText("Steve also happens to be blind!");                
            }
            else if (currentState == IntroStates.WalkingThroughADungeon)
            {
                setIntroText("Unfortunately Steve's route this morning passes directly through a perilous dungeon!");                
            }
            else if(currentState == IntroStates.ShowDungeon)
            {
                this.ui.HideDialogs();
                timeWaitingStarted = DateTime.Now;
                timeToWait = TimeSpan.FromSeconds(1);
                this.steveCam.SetActive(false);
                this.hazards.SetActive(true);
            }
            else if(currentState == IntroStates.ExplainSpheres) 
            {
                this.ui.ShowIntro();
                setIntroText("Steve's path  through the dungeon is indicated by yellow spheres leading the way.");                
            } 
            else if(currentState == IntroStates.ShowSpheres)
            {
                this.ui.HideDialogs();
                this.spheres.SetActive(true);
                timeWaitingStarted = DateTime.Now;
                timeToWait = TimeSpan.FromSeconds(1);
            }
            else if(currentState == IntroStates.UseArrowKeys)
            {
                this.ui.ShowIntro();
                setIntroText("Use the arrow keys or WASD to shift the dungeon obstacles to prevent Steve's death.");
                this.arrows.SetActive(true);
            }
            else if(currentState == IntroStates.FinalConfirmation)
            {
                setIntroText("Simple, right?  Press Next to start the game!");                
            }
            else if (currentState == IntroStates.StartLevelOne)
            {
                GameManager.SetState(GameStates.Playing);
                SceneManager.LoadScene("Level-1");
            }
        }

        private void setIntroText(string text)
        {
            var introTxt = GameObject.FindGameObjectWithTag("IntroText");
            introTxt.GetComponent<UnityEngine.UI.Text>().text = text;
        }
    }
}

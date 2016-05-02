using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Assets.Scripts;

public class InputController : MonoBehaviour {    
    private UIController ui;

    private GameObject upArrow;
    private GameObject downArrow;
    private GameObject leftArrow;
    private GameObject rightArrow;

    private GameObject objectSelected = null;
    private Dictionary<KeyCode, GameObject> directionalKeyMappings;

    void Start () {
        this.ui = GameObject.Find("UI").GetComponent<UIController>();

        this.upArrow = GameObject.Find("UpArrow");
        this.downArrow = GameObject.Find("DownArrow");
        this.leftArrow = GameObject.Find("LeftArrow");
        this.rightArrow = GameObject.Find("RightArrow");

        this.directionalKeyMappings = new Dictionary<KeyCode, GameObject>()
        {
            { KeyCode.UpArrow, upArrow },
            { KeyCode.W, upArrow },

            { KeyCode.DownArrow, downArrow },
            { KeyCode.S, downArrow },

            { KeyCode.LeftArrow, leftArrow },
            { KeyCode.A, leftArrow },

            { KeyCode.RightArrow, rightArrow },
            { KeyCode.D, rightArrow }
        };
    }

    
	void Update ()
    {
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            if(GameManager.CurrentState() == GameStates.Dead)
            {
                this.ui.DeathExit();
            }
            else if (GameManager.CurrentState() == GameStates.Intro)
            {
                this.ui.SkipIntro();
            }
            else
            {
                this.ui.ShowExit();
            }
        }        

        if(Input.GetKeyUp(KeyCode.Return))
        {
            if (GameManager.CurrentState() == GameStates.Intro)
            {
                this.ui.AdvanceIntro();
            }
            else
            {
                this.ui.TryAgain();
            }
        }


        //if (Input.GetMouseButtonDown(0) && objectSelected == null)
        //{
        //    Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        //    RaycastHit hit;

        //    if (Physics.Raycast(ray, out hit))
        //    {
        //        if (hit.collider != null)
        //        {
        //            if (hit.collider.gameObject.CompareTag("Arrow"))
        //            {
        //                this.objectSelected = hit.collider.gameObject;
        //                this.objectSelected.SendMessage("Pressed");
        //            }
        //        }
        //    }
        //}

        this.handleArrowKeys();
    }

    private void handleArrowKeys()
    {
        foreach (var keyToDirectionalArrowMapping in this.directionalKeyMappings)
        {
            if (Input.GetKeyDown(keyToDirectionalArrowMapping.Key))
            {
                if(keyToDirectionalArrowMapping.Value != null)
                {
                    keyToDirectionalArrowMapping.Value.SendMessage("Pressed");
                }
            }
            else if (Input.GetKeyUp(keyToDirectionalArrowMapping.Key))
            {
                if (keyToDirectionalArrowMapping.Value != null)
                {
                    keyToDirectionalArrowMapping.Value.SendMessage("Released");
                }
            }
        }
    }
}

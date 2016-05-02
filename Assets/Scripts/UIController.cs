using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using Assets.Scripts;
using System.Collections.Generic;
using System.Linq;

public class UIController : MonoBehaviour {

    public GameObject DeathDialog;
    public GameObject VictoryDialog;
    public GameObject ExitDialog;
    public GameObject IntroDialog;


    private System.Random rng = new System.Random();
    private List<string> deathMessages = new List<string>() {
        "Why did you let Steve die.  You're trying to save him.  Right?",
        "His death was in vain, and it's all your fault.",
        "It was a needless, unncessary death.",
        "His children will miss him dearly.  Please take good care of them.",
        "His goldfish will also die if you don't save him.",
        "Was it fun to watch Steve die?  You're the worst.",
        "He has a family, you know.",
        "Maybe just try a little harder?  I know you can do it!"
    };
    private string previousMessage = null;

    public void ShowExit()
    {
        this.ExitDialog.SetActive(true);
    }

    public void ShowIntro()
    {
        this.IntroDialog.SetActive(true);
    }
    
    public void ShowDeath()
    {   
        this.DeathDialog.SetActive(true);
        var textObj = GameObject.FindGameObjectWithTag("LamentationText");

        var msgOptions = deathMessages.Where(msg => msg != previousMessage).ToArray();

        string deathMsg = msgOptions[rng.Next() % msgOptions.Length];
        textObj.GetComponent<UnityEngine.UI.Text>().text = deathMsg;
        this.previousMessage = deathMsg;
    }

    public void ShowVictory()
    {
        this.VictoryDialog.SetActive(true);
    }

    #region UI Actions    
    public void ExitGame()
    {
        Application.Quit();
    }
    
    public void DeathExit()
    {
        if (!this.DeathDialog.activeInHierarchy)
            return;

        Application.Quit();
    }

    public void TryAgain()
    {
        if (!this.DeathDialog.activeInHierarchy)
            return;

        HideDialogs();
        SceneManager.LoadScene(GameManager.GetCurrentLevel());
        GameManager.SetState(GameStates.Playing);
    }

    public void PlayAgain()
    {
        HideDialogs();
        SceneManager.LoadScene("Level-1");
        GameManager.SetState(GameStates.Playing);
    }

    public void AdvanceIntro()
    {
        GameObject.Find("Intro").GetComponent<IntroManager>().DoNext();
    }

    public void SkipIntro()
    {
        SceneManager.LoadScene("Level-1");
        GameManager.SetState(GameStates.Playing);
    }

    public void HideDialogs()
    {
        this.DeathDialog.SetActive(false);
        this.VictoryDialog.SetActive(false);
        this.ExitDialog.SetActive(false);
        this.IntroDialog.SetActive(false);
    }
    #endregion
}

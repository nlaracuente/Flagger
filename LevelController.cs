using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelController : Singleton<LevelController>
{
    [Tooltip("How long the teleporting flag takes before it teleports again")] 
    public float teleportingFlagDelay = 1.5f;
    public float colorChangeFlagDelay = 1.5f;
    public float playerVictoryDelay = 1f;

    FlagCapturedPanel flagCapturedPanel;
    FlagCapturedPanel Panel 
    {
        get
        {
            if (flagCapturedPanel == null)
                flagCapturedPanel = FindObjectOfType<FlagCapturedPanel>();

            return flagCapturedPanel;
        }
    }

    Player player;
    Player Player
    {
        get
        {
            if (player == null)
                player = FindObjectOfType<Player>();
            return player;
        }
    }

    public void StartGame()
    {
        StartCoroutine(StartGameRoutine());
    }

    /// <summary>
    /// Triggers player landing
    /// Triggers flag reveal
    /// Starts the game
    /// </summary>
    /// <returns></returns>
    IEnumerator StartGameRoutine()
    {
        MouseController.Instance.SetHandCursor();

        yield return StartCoroutine(Player.Initialize());
        yield return StartCoroutine(Flag.Instance.Initialize());
        
        Player.DisableControls = false;
    }

    public void TriggerFlagCapturedRoutine()
    {
        StartCoroutine(FlagCapturedRoutine());
    }

    IEnumerator FlagCapturedRoutine()
    {
        Player.DisableControls = true;

       // Panel.OpenAlertPanel(); 
        Flag.Instance.HideFlag();
        yield return new WaitForEndOfFrame();

        player.FlagCaptured();
        yield return new WaitForSeconds(playerVictoryDelay);
        
        Flag.Instance.NextRandomBehavior();

        //Panel.CloseAlertPanel();
    }

    public void GameCompleted()
    {
        MenuController.Instance.GameCompleted();
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}

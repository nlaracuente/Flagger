using UnityEngine;

public class MenuController : Singleton<MenuController>
{
    [SerializeField] GameObject titleScreenGO;
    [SerializeField] GameObject pauseScreenGO;
    [SerializeField] GameObject gameCompletedScreenGO;

    public bool IsGamePaused { get { return titleScreenGO.activeSelf ||  pauseScreenGO.activeSelf || gameCompletedScreenGO.activeSelf; } }

    private void Start()
    {
        titleScreenGO.SetActive(true);
        pauseScreenGO.SetActive(false);
        gameCompletedScreenGO.SetActive(false);
    }

    private void Update()
    {
        if (titleScreenGO.activeSelf || gameCompletedScreenGO.activeSelf)
            return;

        if(Input.GetKeyDown(KeyCode.Escape))
        {
            pauseScreenGO.SetActive(!pauseScreenGO.activeSelf);
            Time.timeScale = pauseScreenGO.activeSelf ? 0f : 1f;

            if (pauseScreenGO.activeSelf)
            {
                MouseController.Instance.SetFingerCursor();
                AudioManager.Instance.PauseAll();
            }                
            else
            {
                AudioManager.Instance.ResumeAll();
            }
                
        }
    }

    public void CollectFlags()
    {
        Time.timeScale = 1f;
        titleScreenGO.SetActive(false);
        gameCompletedScreenGO.SetActive(false);
        LevelController.Instance.StartGame();
    }

    public void GameCompleted()
    {
        Time.timeScale = 0f;
        AudioManager.Instance.StopAllSounds();
        gameCompletedScreenGO.SetActive(true);
        MouseController.Instance.SetFingerCursor();
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}

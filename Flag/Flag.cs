using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.EventSystems;

[RequireComponent(typeof(Collider2D))]
public class Flag : Singleton<Flag>, IPointerEnterHandler, IPointerUpHandler, IPointerDownHandler, IPointerExitHandler
{
    [SerializeField] float flagUpAnimLength = 1f;
    [SerializeField] float flagDownAnimLength = 1f;
    [SerializeField] Animator animController;

    [SerializeField] GameObject flagSpriteGO;
    [SerializeField] GameObject triggerZoneGO;
    [SerializeField] GameObject leftFovGO;
    [SerializeField] GameObject rightFovGO;
    [SerializeField] GameObject triggersGO;
    [SerializeField] SpriteRenderer flagSpriteRenderer;

    [SerializeField] AudioClipInfo initialFlagUpClipInfo;
    [SerializeField] List<AudioClipInfo> flagUpClipInfos;
    [SerializeField] AudioClipInfo flagDownClipInfo;
    [SerializeField] AudioClipInfo flagClickedClipInfo;

    [SerializeField] Color rightColor;
    [SerializeField] List<Color> wrongColors; 
    [SerializeField] List<FlagBehavior.Type> behaviorTypes = new List<FlagBehavior.Type>();

    /// <summary>
    /// Keeps track of all the behaviors the flag can choose from
    /// so that we can keep track of how many have been used and
    /// restart the game easily
    /// </summary>
    List<FlagBehavior> allBehaviors;

    /// <summary>
    /// Total available behaviors to play with
    /// </summary>
    List<FlagBehavior> behaviors;

    FlagBehavior activeBehavior;
    Player player;

    bool flagCaptured = false;
    bool playInitialSound = true;
    bool flagIsUp = false;
    bool flagUpRoutineRunning = false;
    bool flagDownRoutineRunning = false;

    bool flagLowered = false;
    bool flagFlipped = false;
    bool isFirstBehavior = true;

    public bool IsWrongColor { get { return flagSpriteRenderer.color != rightColor; } }
    public int TotalBehaviors 
    {
        get {
            if (allBehaviors != null && allBehaviors.Any())
                return allBehaviors.Count();
            return 0;
        } 
    }

    public int TotalCompleted { get; private set; }

    void Start()
    {
        rightColor = flagSpriteRenderer.color;

        player = FindObjectOfType<Player>();
        GetComponent<Collider2D>().isTrigger = true;
        allBehaviors = new List<FlagBehavior>();

        foreach (var type in behaviorTypes)
        {
            // Allowing duplicates for now
            allBehaviors.Add(FlagBehaviorFactory.Create(type, this));
        }
    }

    private void Update()
    {
        animController.SetBool("FlagLowered", flagLowered);
        animController.SetBool("FlagFlipped", flagFlipped);
    }

    /// <summary>
    /// Allow time for the flag to show itself as the player lands
    /// </summary>
    /// <returns></returns>
    public IEnumerator Initialize()
    {
        ResetBools();
        TotalCompleted = 0;
        behaviors = new List<FlagBehavior>(allBehaviors);
        yield return StartCoroutine(NewBehaviorRoutine());
    }

    void ResetBools()
    {
        flagCaptured = false;
        playInitialSound = true;
        flagIsUp = false;
        flagUpRoutineRunning = false;
        flagDownRoutineRunning = false;
        flagLowered = false;
        flagFlipped = false;
    }

    public void NextRandomBehavior()
    {
        if (behaviors.Count < 1)
        {
            isFirstBehavior = true;
            LevelController.Instance.GameCompleted();
        }
        else
            StartCoroutine(NewBehaviorRoutine());
    }
    IEnumerator NewBehaviorRoutine()
    {
        player.DisableControls = true;

        // Ensure flag is in the ground and visible
        HideFlag();
        yield return new WaitForEndOfFrame();

        // Disable the FOVs
        leftFovGO.SetActive(false);
        rightFovGO.SetActive(false);

        var index = RandomNumberGenerator.Instance.Next(behaviors.Count);
        if (isFirstBehavior)
        {
            index = 0;
            isFirstBehavior = false;
        }

        // Become the next flag type
        activeBehavior = behaviors[index];
        behaviors.RemoveAt(index);
        activeBehavior.OnStart();

        flagCaptured = false;
        player.DisableControls = false;

        HintSystemPanel.Instance.SetHint(activeBehavior.Hint);
    }

    /// <summary>
    /// Disable all triggers
    /// Mark it as collected
    /// Trigger routine
    /// </summary>
    public void FlagCaptured()
    {
        TotalCompleted++;
        flagCaptured = true;
        DisableTriggers();
        HintSystemPanel.Instance.SetHint("");
        LevelController.Instance.TriggerFlagCapturedRoutine();
    }

    public void SetRandomFlagColor()
    {
        flagSpriteRenderer.color = wrongColors[RandomNumberGenerator.Instance.Next(wrongColors.Count)];
    }

    public void SetRightFlagColor()
    {
        flagSpriteRenderer.color = rightColor;
    }

    public void TriggerFlagTeleport()
    {
        StartCoroutine(TeleportFlagRoutine());
    }

    IEnumerator TeleportFlagRoutine()
    {
        FlagDown();
        yield return new WaitForSeconds(flagDownAnimLength);

        activeBehavior.MoveToRandomPoint();
        yield return new WaitForEndOfFrame();

        FlagUp();
    }

    public void EnableRandomFOV()
    {
        if( RandomNumberGenerator.Instance.Next(2) == 0)
            leftFovGO.SetActive(true);
        else
            rightFovGO.SetActive(true);
    }

    void DisableTriggers()
    {
        triggersGO.SetActive(false);
    }

    void EnableTriggers()
    {
        triggersGO.SetActive(true);
    }

    public void EnableLeftFOV()
    {
        leftFovGO.SetActive(true);
    }

    public void EnableRightFOV()
    {
        rightFovGO.SetActive(true);
    }

    /// <summary>
    /// Player has entered the
    ///     - "trigger" zone
    ///     - field of vision
    ///     - capture zone
    /// </summary>
    /// <param name="trigger"></param>
    /// <param name="collision"></param>
    public void OnFlagTriggerEntered(FlagTrigger trigger, Collider2D collision)
    {
        if (!flagIsUp || flagCaptured)
            return;

        if (trigger.CompareTag("CaptureZone") && collision.CompareTag("Player"))
            activeBehavior.OnPlayerTouchesFlag();
        else
            OnFlagTriggerStay(trigger, collision);
    }

    public void OnFlagTriggerStay(FlagTrigger trigger, Collider2D collision)
    {
        // ignore while hidden or captured
        if (!flagIsUp || flagCaptured)
            return;

        switch (trigger.tag)
        {
            case "TriggerZone":
                if (collision.CompareTag("Player"))
                    activeBehavior.OnPlayerInTriggerZone();
                break;

            case "FieldOfVision":
                if (collision.CompareTag("Player"))
                    activeBehavior.OnPlayerInFieldOfVision();
                break;

            case "CaptureZone":
                if (collision.CompareTag("PlayerFoV"))
                    activeBehavior.OnPlayerLookingAtFlag();
                break;
        }
    }

    /// <summary>
    /// Player no longer in:
    ///     - Field of view
    /// Player's FoV no longer in:
    ///     - Field of view
    ///     - Capture zone
    /// </summary>
    /// <param name="trigger"></param>
    /// <param name="collision"></param>
    public void OnFlagTriggerExit(FlagTrigger trigger, Collider2D collision)
    {
        switch (trigger.tag)
        {
            case "FieldOfVision":
                if (collision.CompareTag("Player"))
                    activeBehavior.OnPlayerNotInFieldOfVision();
                break;

            case "CaptureZone":
                if (collision.CompareTag("PlayerFoV"))
                    activeBehavior.OnPlayerNotLookingAtFlag();
                break;
        }
    }

    public void HideFlag()
    {
        flagIsUp = false;
        DisableTriggers();
        animController.SetTrigger("HideFlag");
    }

    public void FlagDown(bool disableTriggers = true)
    {
        if (!flagDownRoutineRunning)
            StartCoroutine(FlagDownRoutine(disableTriggers));
    }

    public IEnumerator FlagDownRoutine(bool disableTriggers = true)
    {
        flagDownRoutineRunning = true;
        flagIsUp = false;

        if (disableTriggers)
            DisableTriggers();

        AudioManager.Instance.Play2DSound(flagDownClipInfo);
        animController.SetTrigger("FlagDown");

        yield return new WaitForSeconds(flagDownAnimLength);
        flagDownRoutineRunning = false;
    }

    public void FlagUp()
    {
        if(!flagUpRoutineRunning)
            StartCoroutine(FlagUpRoutine());
    }

    public IEnumerator FlagUpRoutine()
    {
        flagUpRoutineRunning = true;

        if (!playInitialSound)
            AudioManager.Instance.PlayRandom2DClip(flagUpClipInfos);
        else
        {
            playInitialSound = false;
            AudioManager.Instance.Play2DSound(initialFlagUpClipInfo);
        }

        animController.SetTrigger("FlagUp");
        yield return new WaitForSeconds(flagUpAnimLength);

        flagIsUp = true;
        flagUpRoutineRunning = false;
        EnableTriggers();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (flagIsUp && !flagCaptured)
            activeBehavior.OnMouseOverFlag();
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (flagIsUp && !flagCaptured)
            activeBehavior.OnMouseButtonUpFlag();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (flagIsUp && !flagCaptured)
            activeBehavior.OnMouseButtonDownFlag();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        activeBehavior.OnMouseExistsFlag();
    }

    public void PlayClickSound()
    {
        if (flagIsUp && !flagCaptured)
            AudioManager.Instance.Play2DSound(flagClickedClipInfo);
    }

    public void FlagLowered()
    {
        flagLowered = true;
    }

    public void FlagFlipped()
    {
        flagFlipped = true;
    }

    public void FlagIdled()
    {
        flagLowered = false;
        flagFlipped = false;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(Collider2D))]
public class Player : Singleton<Player>
{
    [SerializeField] GameObject avatarGO;
    [SerializeField] GameObject flagCapturedGO;

    [SerializeField] Animator animController;
    [SerializeField] float speed = 5f;
    [SerializeField] float victoryAnimationTime = 2f;
    [SerializeField] AudioClipInfo walkingClipInfo;
    [SerializeField] AudioClipInfo victoryClipInfo;

    AudioSource playerWalkAudioSource;

    Vector3 spawnPoint;
    Rigidbody2D rigidBody;

    Vector2 playerInput;

    public bool DisableControls { get; set; } = true;
    public bool VictoryPose { get; private set; } = false;

    private void Start()
    {
        if (animController == null)
            animController = GetComponent<Animator>();

        spawnPoint = transform.position;
        rigidBody = GetComponent<Rigidbody2D>();
        playerInput = Vector2.zero;
    }

    public IEnumerator Initialize()
    {
        transform.position = spawnPoint;
        avatarGO.transform.position = spawnPoint;

        // Sets player off screen to complete the falling animation
        transform.position = new Vector3(transform.position.x, 7f, transform.position.z);
        yield return new WaitForEndOfFrame();

        // Plays intro animation
        animController.SetTrigger("Intro");

        // Puts the player object at 0f so that when the animation finishes player
        // the player is on screen
        transform.position = new Vector3(transform.position.x, 0f, transform.position.z);

        // This should be the first "thud" which can trigger the flag popping up
        yield return new WaitForSeconds(.20f);
    }

    private void Update()
    {
        playerInput = Vector2.zero;
        flagCapturedGO.SetActive(VictoryPose);
        animController.SetBool("Victory", VictoryPose);

        if (VictoryPose || MenuController.Instance.IsGamePaused)
            return;

        if (DisableControls)
        {
            animController.SetBool("PlayerWalk", false);
            if (playerWalkAudioSource != null)
                playerWalkAudioSource.Stop();

            return;
        }

        playerInput = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        animController.SetBool("PlayerWalk", playerInput != Vector2.zero);
    }

    void FixedUpdate()
    {
        var dir = playerInput.normalized;
        var velocity = Camera.main.orthographicSize * dir * speed;

        // Play/Stop audio
        if (velocity == Vector2.zero && playerWalkAudioSource != null)
        {
            playerWalkAudioSource.Stop();            
        }            
        else if (velocity != Vector2.zero && playerWalkAudioSource == null)
        {
            playerWalkAudioSource = AudioManager.Instance.Play2DSound(walkingClipInfo);
            playerWalkAudioSource.loop = true;
        }

        // Move/Stop
        if(velocity == Vector2.zero)
            rigidBody.velocity = velocity;
        else
            rigidBody.MovePosition(rigidBody.position + velocity * Time.deltaTime);
    }

    public void FlagCaptured()
    {
        StartCoroutine(FlagCapturedRoutine());
    }

    IEnumerator FlagCapturedRoutine()
    {
        VictoryPose = true;
        AudioManager.Instance.Play2DSound(victoryClipInfo);        
        yield return new WaitForSeconds(victoryAnimationTime);
        VictoryPose = false;        
    }
}

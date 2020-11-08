using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(Collider2D))]
public class Player_OldVersion : MonoBehaviour
{
    [SerializeField] float speed = 8f;
    [SerializeField, Tooltip("In seconds")] int moveTime = 2;

    System.Random random;
    Rigidbody2D rigidBody;
    IEnumerator curMoveRoutine;
    ArrowController arrowController;
    
    void Start()
    {
        random = new System.Random();
        rigidBody = GetComponent<Rigidbody2D>();
        arrowController = FindObjectOfType<ArrowController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (curMoveRoutine != null)
            return;

        var directions = arrowController.ActiveDirections;
        if(!directions.Any())
        {
            rigidBody.velocity = Vector2.zero;
            return;
        }

        var index = random.Next(directions.Count);
        var direction = directions[index];

        curMoveRoutine = MoveRoutine(direction);
        StartCoroutine(curMoveRoutine);
    }

    public void MoveInDirection(ArrowDirection direction)
    {
        Vector2 velocity = GetVelocityForDirection(direction);
    }

    IEnumerator MoveRoutine(ArrowDirection direction)
    {
        Vector2 velocity = GetVelocityForDirection(direction);

        var tTime = Time.time + moveTime;
        while (curMoveRoutine != null && tTime > Time.time)
        {
            rigidBody.MovePosition(rigidBody.position + velocity * Time.deltaTime);
            yield return new WaitForEndOfFrame();
        }

        curMoveRoutine = null;
    }

    private Vector2 GetVelocityForDirection(ArrowDirection direction)
    {
        var dir = Vector2.zero;
        switch (direction)
        {
            case ArrowDirection.Up:
                dir = Vector2.up;
                break;
            case ArrowDirection.Down:
                dir = Vector2.down;
                break;
            case ArrowDirection.Left:
                dir = Vector2.left;
                break;
            case ArrowDirection.Right:
                dir = Vector2.right;
                break;
        }

        var velocity = dir * speed;
        return velocity;
    }

    public void CancelMovement()
    {
        if (curMoveRoutine == null)
            return;

        curMoveRoutine = null;
        rigidBody.velocity = Vector2.zero;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(Collider2D))]
public class PlayerClone_OldVersion : MonoBehaviour
{
    public Vector2 Velocity { get; set; }
    Rigidbody2D rigidBody;

    private void Update()
    {
        rigidBody.MovePosition(rigidBody.position + Velocity * Time.deltaTime);
    }
}

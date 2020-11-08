﻿using UnityEngine;

[RequireComponent(typeof(Renderer))]
public class ScrollingBackground : MonoBehaviour
{
    [SerializeField, Range(0.001f, 0.1f), Tooltip("How fast it scrolls")]
    float speed = 0.1f;

    Renderer m_renderer;   

    /// <summary>
    /// Set references
    /// Sets the offset to previously saved one
    /// </summary>
    private void Awake()
    {
        m_renderer = GetComponent<Renderer>();
        m_renderer.material.mainTextureOffset = new Vector2(Time.time * speed, 0.0f);
    }

    /// <summary>
    /// Trigger scrolling
    /// </summary>
    private void LateUpdate()
    {
        // MatOffset = (MatOffset + speed * Time.deltaTime) % 1;
        m_renderer.material.mainTextureOffset = new Vector2(Time.time * speed, 0.0f);
    }
}
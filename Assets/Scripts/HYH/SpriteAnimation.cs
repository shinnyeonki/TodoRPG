using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteAnimation : MonoBehaviour
{
    SpriteRenderer m_SpriteRenderer;
    Animator m_Animator;

    public void Init()
    {
        m_Animator = GetComponent<Animator>();
        m_SpriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void Play()
    {
        m_Animator.Play("Hit");
    }
}

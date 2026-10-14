using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimations : MonoBehaviour
{
    public static PlayerAnimations Instance { get; private set; }

    [SerializeField] private List<Sprite> sprites;
    [SerializeField] private SpriteRenderer spriteRenderer;

    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    void Start()
    {
        spriteRenderer.sprite = sprites[(int) PlayerState.Idle];
    }

    public void Impact()
    {
        StartCoroutine(ImpactAnimation());
    }

    public void Death()
    {
        spriteRenderer.sprite = sprites[(int) PlayerState.Death];
    }

    private IEnumerator ImpactAnimation()
    {
        spriteRenderer.sprite = sprites[(int) PlayerState.Impact];
        yield return new WaitForSeconds(0.5f);
        spriteRenderer.sprite = sprites[(int) PlayerState.Idle];
    }
}

public enum PlayerState
    {
        Idle = 0,
        Impact = 1,
        Death = 2
    }

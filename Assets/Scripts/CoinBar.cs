using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinBar : MonoBehaviour
{
    static CoinBar instance = null;
    public static CoinBar Instance { get { return instance; } }

    Animator animator = null;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance!=this)
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        BounceAnimation();
        collision.gameObject.SetActive(false);
    }

    public void BounceAnimation()
    {
        animator.Play("CoinbarBounceAnimation", -1, 0f);
    }
}

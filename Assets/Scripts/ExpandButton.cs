using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExpandButton : MonoBehaviour
{

    Animator animator = null;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void ExpandAnimation()
    {
        animator.SetTrigger("Expand");
    }

   
}

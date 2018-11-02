using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerSelect : MonoBehaviour {

    public Animator animator;
    public GameObject open, close;

    public void Open()
    {
        if (open.activeInHierarchy)
        {
            animator.SetBool("Open", true);
            close.SetActive(true);
            open.SetActive(false);
        }
        else
        {
            animator.SetBool("Open", false);
            close.SetActive(false);
            open.SetActive(true);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerSelect : MonoBehaviour {

    public Animator animator;
    public GameObject open, close;

    public void Open()
    {
        animator.SetBool("Open", true);
        close.SetActive(true);
        open.SetActive(false);
    }

    public void Close()
    {
        animator.SetBool("Open", false);
        close.SetActive(false);
        open.SetActive(true);
    }
}

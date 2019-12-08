﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Life : MonoBehaviour
{
    [SerializeField] private int maxLife;
    [SerializeField] GameObject[] spriteHearts;
    [SerializeField] GameObject menu;
    public static int life;
    void Start() {
        life = maxLife;
        StartCoroutine(LowerUpdate());
        menu.GetComponent<Animator>().SetBool("perde", false);
    }
    public void ResetLife()
    {
        Debug.Log("StartResetLife()");
        life = 3;
        for (int i = 0; i < spriteHearts.Length; i++)
        {
            Animator anim2 = spriteHearts[i].GetComponent<Animator>();
            anim2.SetBool("Broken", false);
        }
    }
    private void SetHearts(int curLife){
        for(int i = 0; i < spriteHearts.Length; i++){
            if(i > (curLife - 1)){
                Animator anim = spriteHearts[i].GetComponent<Animator>();
                anim.SetBool("Broken", true);
            }
        }
    }    
    public void DecrementLife(){
        life--;
        if(life > maxLife)
            life = maxLife;
        else if(life <= 0)
            Invoke("ResetScene", 0.2f);
        gameObject.GetComponent<AudioSource>().Play();
    }

    IEnumerator LowerUpdate(){
        for(;;){
            yield return new WaitForSeconds(0.01f);
            SetHearts(life);
        }
    }

    void ResetScene(){
        menu.GetComponent<Animator>().SetBool("perde", true);
        menu.transform.GetChild(0).gameObject.SetActive(true);
        GameObject.FindWithTag("Player").SetActive(false);
        print(life);
    }
}

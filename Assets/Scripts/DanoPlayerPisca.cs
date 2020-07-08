﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DanoPlayerPisca : MonoBehaviour
{
    private SpriteRenderer rd;
    void Start(){
        rd = GetComponent<SpriteRenderer>();
    }

    void Pisca(){
        rd.color = new Color(0.9f, 0, 0, 0.5f);
    }

    void Despisca(){
        rd.color = new Color(1, 1, 1);
    }
    public void Comeca(){
        InvokeRepeating("Despisca", 0.1f, 0.2f);
        InvokeRepeating("Pisca", 0.2f, 0.2f);
        if(GetComponent<controladorJogador>())
            GetComponent<controladorJogador>().imortal = true;
        else if(GetComponent<AvoidancePlayerBehaviour>())
            GetComponent<AvoidancePlayerBehaviour>().imortal = true;
    }

    public void QuebraRepeticao(){
        CancelInvoke("Pisca");
        CancelInvoke("Despisca");
        if(GetComponent<controladorJogador>())
            GetComponent<controladorJogador>().imortal = false;
        else if(GetComponent<AvoidancePlayerBehaviour>())
            GetComponent<AvoidancePlayerBehaviour>().imortal = false;
    }
    
}

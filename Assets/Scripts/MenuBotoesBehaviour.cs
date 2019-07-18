using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuBotoesBehaviour : MonoBehaviour
{
    private Vector2 tamInicial;

    void Start(){
        tamInicial = transform.position;
    }

    public void Aumenta(){
        transform.localScale *= 1.2f;
    }

    public void Normal(){
        transform.localScale /= 1.2f;
    }
}

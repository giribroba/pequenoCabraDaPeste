using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IconesBehaviour : MonoBehaviour
{
    private Image img;
    void Start(){
        img = GetComponent<Image>();
    }
    public void Comeca(){
        InvokeRepeating("Despisca", 0.5f, 1f);
        InvokeRepeating("Pisca", 1f, 1f);
    }
    void Pisca(){
        img.enabled = true;
    }
    void Despisca(){
        img.enabled = false;
    }
}

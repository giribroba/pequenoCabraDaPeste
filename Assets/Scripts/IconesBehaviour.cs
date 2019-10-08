using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IconesBehaviour : MonoBehaviour
{
    [SerializeField] private Image seta;
    private Image img;
    void Start(){
        img = GetComponent<Image>();
    }
    public void Comeca(){

        InvokeRepeating("Despisca", 0.5f, 1f);
        InvokeRepeating("Pisca", 1f, 1f);
    }
    public void Pisca(){
        seta.enabled = true;
        img.enabled = true;
    }
    void Despisca(){
        seta.enabled = false;
        img.enabled = false;
    }
    public void QuebraRepeticao()
    {
        CancelInvoke("Pisca");
        CancelInvoke("Despisca");
        seta.enabled = false;
    }
}

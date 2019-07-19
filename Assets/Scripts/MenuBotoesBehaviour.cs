using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuBotoesBehaviour : MonoBehaviour
{
    [SerializeField] private Sprite botaoNovo;
    private Sprite botaoNormal;
    private Image img;

    void Start(){
        img = GetComponent<Image>();
        botaoNormal = img.sprite;
    }

    public void Aumenta(){
        transform.localScale *= 1.2f;
        img.sprite = botaoNovo;
    }

    public void Normal(){
        transform.localScale /= 1.2f;
        img.sprite = botaoNormal;
    }
}

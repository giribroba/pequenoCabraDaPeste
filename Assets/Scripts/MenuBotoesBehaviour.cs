using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuBotoesBehaviour : MonoBehaviour
{
    [SerializeField] private AudioSource som;
    [SerializeField] private Sprite botaoNovo;
    [SerializeField] private bool trocaAsset;
    private Sprite botaoNormal;
    private Image img;
    void Start(){
        img = GetComponent<Image>();
        botaoNormal = img.sprite;
    }

    public void Aumenta(){
        transform.localScale *= 1.2f;
        som.Play();
        if(trocaAsset)
            img.sprite = botaoNovo;
        //TODO: impedir que o som seja sobrescrito
    }

    public void Normal(){
        transform.localScale /= 1.2f;
        if (trocaAsset)
            img.sprite = botaoNormal;
    }

    public void Despausa()
    {
        Time.timeScale = 1;
        GameObject.FindWithTag("Pause").GetComponent<Animator>().SetBool("perde", false);
    }

}

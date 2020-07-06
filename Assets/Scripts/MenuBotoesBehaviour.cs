﻿using UnityEngine;
using UnityEngine.UI;

public class MenuBotoesBehaviour : MonoBehaviour
{
    [SerializeField] private AudioSource som;
    [SerializeField] private Sprite botaoNovo;
    [SerializeField] private bool trocaAsset;
    public static bool inPause;
    public static bool controleVisivel =
#if UNITY_ANDROID
true;
#else
false;
#endif
    [SerializeField] GameObject bControle;
    private Sprite botaoNormal;
    private Image img;
    void Start()
    {
        img = GetComponent<Image>();
        botaoNormal = img.sprite;
#if UNITY_STANDALONE
        bControle.SetActive(false);
#endif
    }
#if UNITY_ANDROID
    private void Update()
    {
        bControle.GetComponent<Image>().color = (controleVisivel ? Color.green : Color.red);
    }

    public void Controle()
    {
        controleVisivel = !controleVisivel;
    }
#endif

    public void Aumenta()
    {
        transform.localScale *= 1.2f;
        som.Play();
        if (trocaAsset)
            img.sprite = botaoNovo;
        //TODO: impedir que o som seja sobrescrito
    }

    public void Normal()
    {
        transform.localScale /= 1.2f;
        if (trocaAsset)
            img.sprite = botaoNormal;
    }

    public void Despausa()
    {
        Time.timeScale = 1; inPause = false;
        GameObject.FindWithTag("Pause").GetComponent<Animator>().SetBool("perde", false);
    }

    public void Pausa()
    {
        Time.timeScale = 0; inPause = true;
        GameObject.FindWithTag("Pause").GetComponent<Animator>().SetBool("perde", true);
    }

}

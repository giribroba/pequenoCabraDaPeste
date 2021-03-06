﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BotaoController : MonoBehaviour
{
    [SerializeField] Animator gameOver;

    Life vida;
    GameObject player;
    controladorJogador playerS;
    void Start()
    {
        if (SceneManager.GetActiveScene().name == "Exploração")
        {
            vida = GameObject.Find("Vidas").GetComponent<Life>();
            player = GameObject.FindWithTag("Player");
            playerS = GameObject.Find("Player").GetComponent<controladorJogador>();
        }
    }

    public void ControleBotoes(string cenaDestino)
    {
        if (cenaDestino == "Exploração")
        {
            Debug.Log("Jogo Iniciado");
        }
        SceneManager.LoadScene(cenaDestino);
    }
    public void Reinicia()
    {
        //if (playerS.ReturnLevel() == 1)
        //{
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        //}
        //else
        //{
        // player.SetActive(true);
        // gameOver.SetBool("perde", false);
        // vida.ResetLife(); 
        StartCoroutine("Sobe");
        //}
        // if (playerS.ReturnLevel() == 2)
        // {

        // }else if(playerS.ReturnLevel() == 3)
        // {

        // }

    }
    public void SairJogo()
    {
        Application.Quit();
    }

    private IEnumerator Sobe()
    {
        yield return new WaitForSeconds(0.5f);
        this.transform.GetChild(0).gameObject.SetActive(false);
    }

    public void ReiniciaRunner()
    {

        StartCoroutine(ReiniciaRunnerTrasition());

    }

    IEnumerator ReiniciaRunnerTrasition()
    {

        GameObject.Find("RestartTrasition").GetComponent<Animation>().Play();
        yield return new WaitForSeconds(.4f);
        RunnerController.instace.currentState = RunnerController.State.beforeRunner;

    }

}

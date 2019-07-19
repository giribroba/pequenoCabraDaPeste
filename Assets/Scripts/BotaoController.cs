using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BotaoController : MonoBehaviour
{
    
    public void ControleBotoes(string cenaDestino){
        SceneManager.LoadScene(cenaDestino);
    }
    public void Reinicia(){
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void SairJogo(){
        Application.Quit();
    }
}

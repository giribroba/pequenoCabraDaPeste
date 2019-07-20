using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PulaVideoCreditos : MonoBehaviour
{
    [SerializeField] AudioSource som;
    void Start(){
        Invoke("TocarPalmas", 2f);
        Invoke("PulaVideo", 10.5f);
    }
        
    void PulaVideo(){
        SceneManager.LoadScene("Menu");
    }
    void TocarPalmas(){
        som.Play();
    }
}

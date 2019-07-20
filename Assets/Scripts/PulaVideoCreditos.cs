using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PulaVideoCreditos : MonoBehaviour
{
    void Start(){
        Invoke("PulaVideo", 10.5f);
    }
        
    void PulaVideo(){
        SceneManager.LoadScene("Menu");
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuAlfa : MonoBehaviour
{

    public static MenuAlfa instance;
    public bool isArcade;

    private void Awake()
    {

        instance = this;

    }

    public void CarregarCena(string nomeCena)
    {

        DontDestroyOnLoad(this);
        SceneManager.LoadScene(nomeCena);

    }

    public void IsArcade(string arcadeOrHistory)
    {

        if (arcadeOrHistory == "arcade") isArcade = true;
        else isArcade = false;

    }

}

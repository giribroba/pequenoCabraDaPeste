using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Creditos : MonoBehaviour
{
    [SerializeField] float tempo;
    void Start()
    {
        StartCoroutine("Fim");
    }

    private IEnumerator Fim()
    {
        yield return new WaitForSeconds(tempo);
        SceneManager.LoadScene("Menu");
    }
}

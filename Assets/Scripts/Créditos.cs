using System.Collections;
using System.Collections.Generic;
using UnityEngine.Video;
using UnityEngine;

public class Créditos : MonoBehaviour
{
    [SerializeField] float tempo;
    void Start()
    {
        StartCoroutine("Creditos");
    }

    private IEnumerator Creditos()
    {
        yield return new WaitForSeconds(tempo);
        GetComponent<VideoPlayer>().Play();
    }
}

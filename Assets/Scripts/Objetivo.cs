using System.Collections.Generic;
using UnityEngine;

public class Objetivo : MonoBehaviour
{
    private static List<GameObject> objetivos;
    private GameObject objetivoUnico, player, marcador;
    [SerializeField] private float xMax;

    private void Start()
    {
        player = GameObject.FindWithTag("Player");
        marcador = GameObject.FindWithTag("Marcador");
    }

    public static void SetObjetivo(string objetivo)
    {
        objetivos = new List<GameObject>(GameObject.FindGameObjectsWithTag(objetivo));
    }

    private void ObjetivoUnico()
    {
        objetivoUnico = objetivos[0];
        foreach (var i in objetivos)
        {
            if (Vector2.Distance(player.transform.position, i.transform.position) < Vector2.Distance(player.transform.position, objetivoUnico.transform.position))
            {
                objetivoUnico = i;
            }
        }
    }

    private void Apontar()
    {
        if (NaTela(objetivoUnico.transform))
        {
            marcador.GetComponent<SpriteRenderer>().flipX = false;
            marcador.transform.right = new Vector2(objetivoUnico.transform.position.x - player.transform.position.x, objetivoUnico.transform.position.y - player.transform.position.y);
        }
        else
        {
            marcador.transform.localRotation = Quaternion.identity;
            marcador.GetComponent<SpriteRenderer>().flipX = objetivoUnico.transform.position.x < player.transform.position.x;
        }
    }

    private bool NaTela(Transform alvo)
    {
        return ((Mathf.Abs(alvo.position.x) < xMax) && alvo.position.y > -20);
    }

    void Update()
    {
        ObjetivoUnico();
        Apontar();
    }
}
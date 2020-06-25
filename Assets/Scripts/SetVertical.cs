using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SetVertical : MonoBehaviour, IPointerUpHandler, IPointerDownHandler
{
    private enum tipo { U, D };
    [SerializeField] tipo botao;
    [SerializeField] GameObject poco;


    public void OnPointerDown(PointerEventData eventData)
    {
        if (botao == tipo.U)
        {
            poco.GetComponent<pocoBehaviour>().vertical = 1;
        }
        if (botao == tipo.D)
        {
            poco.GetComponent<pocoBehaviour>().vertical = -1;
        }
    }
    public void OnPointerUp(PointerEventData eventData)
    {
        poco.GetComponent<pocoBehaviour>().vertical = 0;
    }
}

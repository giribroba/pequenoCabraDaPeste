using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotacionarSempre : MonoBehaviour
{
    private GameObject planeta;
    void Update(){
        planeta = GameObject.FindWithTag("Redoneta");
        transform.up = -new Vector2(planeta.transform.position.x - transform.position.x, planeta.transform.position.y - transform.position.y);
    }
}

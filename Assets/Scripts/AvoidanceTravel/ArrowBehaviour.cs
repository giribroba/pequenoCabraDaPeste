using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowBehaviour : MonoBehaviour
{
    private GameObject planet, pl;
    private float x, y, mod = -1.2f;
    public float rot;
    // Start is called before the first frame update
    void Start()
    {
        planet = GameObject.FindWithTag("Planeta");
        pl = GameObject.FindWithTag("Player");
        transform.right = new Vector3(planet.transform.position.x - pl.transform.position.x, planet.transform.position.y - pl.transform.position.y);
        rot = transform.rotation.eulerAngles.z;
        if(rot >= 140 && rot <= 215)
            x = -32.16159f;
        else if(rot <= 40 || (rot <= 360 && rot >= 315))
            x = 32.16159f;
        // x = (rot >= 140) ? -32.07379f : (rot <= 40) ? 32.07379f : 0;
        transform.localPosition += new Vector3(x, 0);
    }

    // Update is called once per frame
    void Update()
    {        
        transform.right = new Vector3(planet.transform.position.x - pl.transform.position.x, planet.transform.position.y - pl.transform.position.y);
        rot = transform.rotation.eulerAngles.z;
        if(rot <= 360 && rot >= 350)
            mod = -360;
        x = (rot <= 140 && rot >= 40) ? ((rot - 90) * -0.14f) + pl.transform.position.x /*: () ?*/ : transform.position.x;
        y = (rot >= 140 && rot <= 215) ? ((rot - 178.75f) * -0.09f) + pl.transform.position.y : (rot <= 40 || (rot <= 360 && rot >= 315)) ? ((rot + mod) * 0.09f) + pl.transform.position.y : transform.position.y;
        transform.position = new Vector3(x, y);
        //print(transform.rotation.eulerAngles.z);
    }
}

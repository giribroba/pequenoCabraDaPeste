using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AvoidanceController : MonoBehaviour
{
    public static AvoidanceController instance;
    public bool arcade;

    private void Awake()
    {

        instance = this;
        arcade = MenuAlfa.instance != null ? MenuAlfa.instance.isArcade : arcade;
        if (MenuAlfa.instance != null) Destroy(MenuAlfa.instance.gameObject);


    }
}

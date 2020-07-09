using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExploreController : MonoBehaviour
{

    public static ExploreController instance;
    public bool arcade;

    private void Awake()
    {

        instance = this;
        arcade = MenuAlfa.instance != null ? MenuAlfa.instance.isArcade : arcade;
        if (MenuAlfa.instance != null) Destroy(MenuAlfa.instance.gameObject);

    }

}

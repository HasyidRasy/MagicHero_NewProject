using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NextSceneLoader : MonoBehaviour
{
    public AsynLoader loader;
    void Start()
    {
        loader.LoadLevelBtn("Tutorial");
    }
}

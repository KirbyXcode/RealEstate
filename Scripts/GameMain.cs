using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DevelopEngine;

public class GameMain : MonoBehaviour 
{
    private readonly string configPath = "/config.txt";

    IEnumerator Start()
    {
        Configuration.LoadConfig(configPath);
        while (!Configuration.IsDone)
            yield return null;
    }
}

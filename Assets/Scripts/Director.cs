using UnityEngine;
using System.Collections;
using System.Collections.Generic;
//Done
public class Director : System.Object
{
    public ISceneController CurrentSceneCtrl { get; set; }

    private static Director instance;

    public static Director GetDirector()
    {
        if (instance == null)
            instance = new Director();
        return instance;
    }
}

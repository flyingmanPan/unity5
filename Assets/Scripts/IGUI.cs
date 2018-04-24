using UnityEngine;
using System.Collections;
using System.Collections.Generic;
//Almost Done
public class IGUI : MonoBehaviour
{
    private IUserAction action;
    bool first = true;
    // Use this for initialization
    void Start()
    {
        Debug.Log("GUI start");
        action = Director.GetDirector().CurrentSceneCtrl as IUserAction;
    }

    // Update is called once per frame
    private void OnGUI()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            //Debug.Log("Fired Pressed");
            //Debug.Log(Input.mousePosition);

            Vector3 mp = Input.mousePosition; //get Screen Position
            action.Hit(mp);
        }
        GUI.Label(new Rect(1000, 0, 400, 400), action.GetScore().ToString());
        if (first && GUI.Button(new Rect(50, 50, 100, 50),
            "Old style"))
        {
            first = false;
            action.SetMode(ActionMode.SELF);
            action.SetGameStatus(GameStatus.RoundBegin);
        }
        if (first && GUI.Button(new Rect(50, 150, 100, 50),
            "RigidBody"))
        {
            first = false;
            action.SetMode(ActionMode.RIGIDBODY);
            action.SetGameStatus(GameStatus.RoundBegin);
        }
        if (!first && action.GetGameStatus() == GameStatus.RoundEnd 
            && GUI.Button(new Rect(0, 0, 100, 50), "Next"))
        {
            action.SetGameStatus(GameStatus.RoundBegin);
        }
    }
}

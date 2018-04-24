using UnityEngine;
using System.Collections;
using System;
//Done
public enum GameStatus
{
    Running,
    Pause,
    Start,
    RoundBegin,
    RoundEnd
}
public enum ActionMode
{
    SELF,
    RIGIDBODY,
    DEFAULT
}
public interface IUserAction
{
    GameStatus GetGameStatus();
    void SetGameStatus(GameStatus gs);
    int GetScore();
    void Hit(Vector3 pos);
    ActionMode GetMode();
    void SetMode(ActionMode m);
}
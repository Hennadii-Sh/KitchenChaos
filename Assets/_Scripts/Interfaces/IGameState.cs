using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IGameState
{
    //public GameState GameState { get; }
    public void GameStateHandling();
}

//public enum GameState
//{
//    WaitingToStart,
//    CountdownToStart,
//    GamePlaying,
//    GameOver
//}
//     GamePaused,
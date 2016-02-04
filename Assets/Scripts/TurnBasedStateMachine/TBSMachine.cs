using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TBSMachine : MonoBehaviour {

	public enum TurnState
    {
        Init,
        Player,
        Enemy,
        Win,
        Lose
    }

    public TurnState turn;

    void Start()
    {
        turn = TurnState.Init;
    }

    void Update()
    {
        switch (turn)
        {
            case (TurnState.Init):
                InitiateTurn();
                break;
            case (TurnState.Player):
                PlayerTurn();
                break;
            case (TurnState.Enemy):
                EnemyTurn();
                break;
            case (TurnState.Win):
                break;
            case (TurnState.Lose):
                break;
        }
    }

    void InitiateTurn()
    {
        turn = TurnState.Player;
    }

    void PlayerTurn()
    {

    }

    void EndPlayerTurn()
    {
        if (turn == TurnState.Player)
        {
            turn = TurnState.Enemy;
        }
    }

    void EnemyTurn()
    {
        turn = TurnState.Player;
    }

    void Win()
    {

    }

    void Lose()
    {

    }
}

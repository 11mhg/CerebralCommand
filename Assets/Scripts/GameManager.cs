using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameManager : MonoBehaviour {
	private static GameManager instance = null;

    public List<Class> playerUnits = new List<Class>();

	//CONVENTION:
	//Index 0 is player, everything else are the units that are a part of the player's company
	//public List<BaseClass> Units = new List<BaseClass>();

	//Singleton pattern to keep game from recreating gamemanager each time.
	//Will hold all of the values necessary for player.
	//TO DO: DATA STRUCTURE REQUIRED

	void Awake () 
	{
		if (instance == null){
			instance = this;
		}else if (instance != null){
			Destroy (gameObject);
		}
        StartGame();
		DontDestroyOnLoad(gameObject);
	}

    void StartGame()
    {
        playerUnits.Add(new Tacticien());
        playerUnits.Add(new Warrior());
        playerUnits.Add(new Rogue());
        playerUnits.Add(new Mage());
        playerUnits.Add(new Healer());
    }

    void Save(List<Class> _playerUnits)
    {
        playerUnits = _playerUnits;
    }

    List<Class> Load()
    {
        return playerUnits;
    }


}

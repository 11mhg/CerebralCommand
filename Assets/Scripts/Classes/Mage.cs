using UnityEngine;
using System.Collections;

public class Mage : Class {

	public Mage()
	{
        level = 1;
        setStats();
	}

    public Mage(int _level)
    {
        level = _level;
        setStats();
    }
	
	public void setStats(){
        Random.seed = Mathf.RoundToInt(Time.time);
        float chance = Random.Range(0, 100);
        if (chance < 75 && chance > 25)
        {
            spec += 3;
            HP += 2;
            luck += 1;
            acc += 2;
            dodge += 3;
            physArmor += 1;
            magicalArmor += 3;
            movement = 4;
        }
        else if (chance > 74)
        {
            spec += 4;
            HP += 3;
            luck += 2;
            acc += 3;
            dodge += 4;
            physArmor += 2;
            magicalArmor += 4;
            movement = 4;
        }
        else
        {
            spec += 2;
            HP += 1;
            luck += 0;
            acc += 1;
            dodge += 2;
            physArmor += 0;
            magicalArmor += 2;
            movement = 4;
        }
        weapon = new BaseMageWeapon();
    }
	
	public void LevelUp(){
		level+=1;
		setStats ();
	}
}

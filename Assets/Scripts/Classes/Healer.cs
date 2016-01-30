using UnityEngine;
using System.Collections;

public class Healer : Class {

	public Healer()
	{
        level = 1;
        setStats();
	}

    public Healer(int _level)
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
            HP += 1;
            luck += 3;
            acc += 0;
            dodge += 1;
            physArmor += 1;
            magicalArmor += 3;
            movement =5;
        }
        else if (chance > 74)
        {
            spec += 4;
            HP += 2;
            luck += 4;
            acc += 0;
            dodge += 2;
            physArmor += 2;
            magicalArmor += 4;
            movement = 5;
        }
        else
        {
            spec += 2;
            HP += 0;
            luck += 2;
            acc += 0;
            dodge += 0;
            physArmor += 0;
            magicalArmor += 2;
            movement = 5;
        }
        weapon = new BaseHealerWeapon();
    }
	
	public void LevelUp(){
		level+=1;
		setStats ();
	}
}

using UnityEngine;
using System.Collections;

public class Warrior : Class
{
	public Warrior()
	{
        level = 1;
		setStats();
	}

    public Warrior(int _level)
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
            HP += 3;
            luck += 2;
            acc += 2;
            dodge += 2;
            physArmor += 3;
            magicalArmor += 2 ;
            movement = 6;
        }else if (chance > 74)
        {
            spec += 4;
            HP += 4;
            luck += 3;
            acc += 3;
            dodge += 3;
            physArmor += 4;
            magicalArmor += 3;
            movement = 6;
        }
        else
        {
            spec += 2;
            HP += 2;
            luck += 1;
            acc += 1;
            dodge += 1;
            physArmor += 2;
            magicalArmor += 2;
            movement = 6;
        }
        weapon = new BaseWarriorWeapon();
	}
	
	public void LevelUp(){
		level+=1;
		setStats ();
	}

}


using UnityEngine;
using System.Collections;

public class Rogue : Class {

	public Rogue()
	{
        level = 1;
        setStats();
	}

    public Rogue(int _level)
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
            luck += 3;
            acc += 3;
            dodge += 3;
            physArmor += 2;
            magicalArmor += 1;
            movement = 3;
        }
        else if (chance > 74)
        {
            spec += 4;
            HP += 3;
            luck += 4;
            acc +=4;
            dodge += 4;
            physArmor += 3;
            magicalArmor += 2;
            movement = 3;
        }
        else
        {
            spec += 2;
            HP += 1;
            luck += 2;
            acc += 2;
            dodge += 2;
            physArmor += 1;
            magicalArmor += 0;
            movement = 3;
        }
        weapon = new BaseRogueWeapon();
    }
	
	public void LevelUp(){
		level+=1;
		setStats ();
	}
}

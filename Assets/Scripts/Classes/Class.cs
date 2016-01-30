using UnityEngine;
using System.Collections;

public abstract class Class:MonoBehaviour{
	//Class Name
	public string className;
	//Class Description
	public string classDescription;
	//Stats
	public int level;
	public int HP;
    public int spec;
    public int luck;
    public int acc;
    public int dodge;
    public int physArmor;
    public int magicalArmor;
    public int movement;
    public int terrainBonus;
    public int range;
    public BaseWeaponClass weapon;

    public bool PercentChanceHit(Class other)
    {
        int chance = Mathf.RoundToInt(weapon.weaponAcc + (2f * acc) + (0.5f * luck) - 2f * other.dodge - other.luck - other.terrainBonus);
        Random.seed = Mathf.RoundToInt(Time.time);
        int roll = Mathf.RoundToInt(Random.Range(0, 100));
        return (roll <= chance);
    }

    public bool critHit(Class other)
    {
        Random.seed = Mathf.RoundToInt(Time.time);
        if (Random.Range(0,100) <= (weapon.weaponCrit+luck - other.luck))
        {
            return true;
        }
        return false;
    }

    public int calculateDamage(Class other,bool isPhysical, bool isCrit)
    {
        //crit stuff
        int critmod = 1;
        if (isCrit) { critmod = 3; }
        //damage calculation stuff
        if (isPhysical)
        {
            return (this.spec + this.weapon.weaponDamage - other.physArmor)*critmod;
        }
        else
        {
            return (this.spec + this.weapon.weaponDamage - other.magicalArmor)*critmod;
        }
    }

}

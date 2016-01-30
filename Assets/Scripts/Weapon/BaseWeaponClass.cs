using System;


public abstract class BaseWeaponClass
{

    public String name
    { get; set; }
    public int weaponDamage
    { get; set; }
    public int weaponCrit
    { get; set; }
    public int weaponAcc
    { get; set; }

    public BaseWeaponClass() { }
}


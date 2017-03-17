using UnityEngine;
using System.Collections;

public class PropertiesWeapon
{
    public float Cadency;
    public int NumberBullets;
    public float SpeedBullet;
    public float DamageBullet;
    public float TimeRecharge;
    public string Name;
    public string NameClass;
    public bool IsUsing;
    public bool Rechargeable;
    public PropertiesWeapon(float _cadency, int _numberBullets,float _speedBullet,float _damageBullet,float _timeRecharge, string _name, string _nameClass, bool _isUsing, bool _rechargeable)
    {
        Cadency = _cadency;
        NumberBullets = _numberBullets;
        SpeedBullet = _speedBullet;
        DamageBullet = _damageBullet;
        TimeRecharge = _timeRecharge;
        Name = _name;
        NameClass = _nameClass;
        IsUsing = _isUsing;
        Rechargeable = _rechargeable;
    }
}

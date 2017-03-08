using UnityEngine;
using System.Collections;

public class PropertiesWeapon
{
    public float Cadency;
    public int NumberBullets;
    public float SpeedBullet;
    public string Name;
    public string NameClass;
    public bool IsUsing;

    public PropertiesWeapon(float _cadency, int _numberBullets,float _speedBullet, string _name, string _nameClass, bool _isUsing)
    {
        Cadency = _cadency;
        NumberBullets = _numberBullets;
        SpeedBullet = _speedBullet;
        Name = _name;
        NameClass = _nameClass;
        IsUsing = _isUsing;
    }
}

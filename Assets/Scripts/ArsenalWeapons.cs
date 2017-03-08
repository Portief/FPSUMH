using UnityEngine;
using System.Collections.Generic;

public class ArsenalWeapons {

    public Dictionary<int, PropertiesWeapon> Weapons;
    public ArsenalWeapons()
    {
      Weapons= new Dictionary<int, PropertiesWeapon> { { 0, new PropertiesWeapon(0, 0, 300f, "CañonGauss", "IngenieroElectronico",true) } };
    }
}

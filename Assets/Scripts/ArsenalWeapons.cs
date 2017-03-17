using UnityEngine;
using System.Collections.Generic;

public class ArsenalWeapons {

    public Dictionary<int, PropertiesWeapon> Weapons;
    public ArsenalWeapons()
    {
      Weapons= new Dictionary<int, PropertiesWeapon> { { 0, new PropertiesWeapon(0.1f, 10, 400f,10.0f,4.0f, "CañonGauss", "IngenieroElectronico",true,true) }, { 1, new PropertiesWeapon(0.08f, 3, 400f, 50.0f, 4.0f, "CañonGaussIncreased", "IngenieroElectronico", false,false) } };
    }
}

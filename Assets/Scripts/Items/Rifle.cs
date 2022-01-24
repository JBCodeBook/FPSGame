using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rifle : Weapon
{
    FireArmScript fireArmScript;

    private void Awake()
    {
        fireArmScript = GetComponent<FireArmScript>();
    }
    public override void Use()
    {
        fireArmScript.Use();
    }
}

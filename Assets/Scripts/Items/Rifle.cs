using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rifle : Weapon
{
    FireArmScript fireArmScript;

    private void Awake()
    {
        fireArmScript = FindObjectOfType<FireArmScript>();
    }
    public override void Use()
    {
        fireArmScript.Use();
    }
}

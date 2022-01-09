using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "FPS/New Gun")]
public class GunInfo : ItemInfo
{
	public float damage;
	public float range;
    public int pellets;
    public float bloom;   
    public AudioClip gunshotSound;
    public float pitchRandomization;
    public float shotVolume;
    public LayerMask[] layermasks;

    //public int ammo;
    //public int burst; // 0 semi | 1 auto | 2+ burst fire
    //public int clipsize;
    //public float firerate;
    //public GameObject prefab;
    //public GameObject display;
    //public bool recovery;
    //public float recoil;
    //public float kickback;
    //public float aimSpeed;
    //public float reload;
    //[Range(0, 1)] public float mainFOV;
    //[Range(0, 1)] public float weaponFOV;

    private int stash; //current ammo
    private int clip; //current clip
}
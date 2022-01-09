using System.Collections;
using UnityEngine;

public class FireArmScript : Weapon
{

    CameraManager cameraManager;
    public GameObject impactEffect;
    public GameObject[] bulletholePrefab;

    [Header("Gun Stats")]
    public GunInfo currentGunData;
    float range;
    float damage;
    float bloom;

    // --- Audio ---
    public AudioClip GunShotClip;
    public AudioSource source;
    public Vector2 audioPitch = new Vector2(.9f, 1.1f);

    // --- Muzzle ---
    public GameObject muzzlePrefab;
    public GameObject muzzlePosition;

    // --- Config ---
    public bool autoFire;
    public float shotDelay = .5f;
    public bool rotate = true;
    public float rotationSpeed = .25f;

    // --- Timing ---
    [SerializeField] private float timeLastFired;

    // --- Projectile ---
    [Tooltip("The projectile gameobject to instantiate each time the weapon is fired.")]
    public GameObject projectilePrefab;
    //[Tooltip("Sometimes a mesh will want to be disabled on fire. For example: when a rocket is fired, we instantiate a new rocket, and disable" +
    //   " the visible rocket attached to the rocket launcher")]
    //public GameObject projectileToDisableOnFire;

    // --- Impact ---
    public enum LayersInGame
    {
        DEFAULT = 0,
        GROUND = 6,
        WALL = 7,
        HUMAN = 9,
    }

    private void Start()
    {
        if(source != null) source.clip = GunShotClip;
        timeLastFired = 0;
    }

    private void Awake()
    {
        cameraManager = GetComponentInParent<CameraManager>();
        damage = ((GunInfo)itemInfo).damage;
        range = ((GunInfo)itemInfo).range;
        bloom = ((GunInfo)itemInfo).bloom;
    }

    public override void Use()
    {
        FireWeapon();

        if (autoFire && ((timeLastFired + shotDelay) <= Time.time))
        {
            FireWeapon();
        }
    }

    /// <summary>
    /// Creates an instance of the muzzle flash.
    /// Also creates an instance of the audioSource so that multiple shots are not overlapped on the same audio source.
    /// Insert projectile code in this function.
    /// </summary>
    public void FireWeapon()
    {
        if (projectilePrefab != null)
        {
            GameObject newProjectile = Instantiate(projectilePrefab, muzzlePosition.transform.position, muzzlePosition.transform.rotation, transform);
        }

        Debug.Log(cameraManager.transform.position);
        RaycastHit hit;
        if (Physics.Raycast(cameraManager.transform.position, cameraManager.transform.forward, out hit, range))
        {


            Transform t_spawn = cameraManager.transform;

            /*for (int i = 0; i < Mathf.Max(1, currentGunData.pellets); i++)
            {
                //bloom
                Vector3 t_bloom = t_spawn.position + t_spawn.forward * 1000f;
                t_bloom += Random.Range(-bloom, bloom) * t_spawn.up;
                t_bloom += Random.Range(-bloom, bloom) * t_spawn.right;
                t_bloom -= t_spawn.position;
                t_bloom.Normalize();

                //raycast
                RaycastHit t_hit = new RaycastHit();

                if (Physics.Raycast(t_spawn.position, t_bloom, out t_hit, 1000f))
                {
                    GameObject t_newHole;
                    GameObject impactGO;
                    switch (t_hit.collider.gameObject.layer)
                    {
                        case (int)LayersInGame.WALL:
                            impactGO = Instantiate(impactEffect, t_hit.point, Quaternion.LookRotation(t_hit.normal));
                            impactGO.transform.parent = t_hit.transform;
                            Destroy(impactGO, 2f);

                            t_newHole = Instantiate(bulletholePrefab[2], t_hit.point + t_hit.normal * 0.001f, Quaternion.identity) as GameObject;
                            t_newHole.transform.LookAt(t_hit.point + t_hit.normal);
                            Destroy(t_newHole, 5f);
                            break;
                        case (int)LayersInGame.HUMAN:
                            t_newHole = Instantiate(bulletholePrefab[0], t_hit.point + t_hit.normal * 0.001f, Quaternion.identity) as GameObject;
                            t_newHole.transform.LookAt(t_hit.point + t_hit.normal);
                            t_newHole.transform.parent = t_hit.transform;

                            Destroy(t_newHole, .5f);
                            break;
                        case (int)LayersInGame.DEFAULT:
                            impactGO = Instantiate(impactEffect, t_hit.point, Quaternion.LookRotation(t_hit.normal));
                            Destroy(impactGO, 2f);
                            break;

                    }

                    
                    t_hit.collider.gameObject.GetComponent<IDamageable>()?.TakeDamage(damage);

                    EnemyAITree enemryAI = hit.transform.GetComponent<EnemyAITree>();

                    if (enemryAI != null)
                    {
                        enemryAI.TakeDamage(damage);
                    }



                }
            }*/



        }

        // --- Spawn muzzle flash ---
        var flash = Instantiate(muzzlePrefab, this.gameObject.transform.Find("MuzzlePosition").transform);
        flash.gameObject.GetComponent<ParticleSystem>().Play();

        // --- Handle Audio ---
        if (source != null)
        {
            // --- Sometimes the source is not attached to the weapon for easy instantiation on quick firing weapons like machineguns, 
            // so that each shot gets its own audio source, but sometimes it's fine to use just 1 source. We don't want to instantiate 
            // the parent gameobject or the program will get stuck in a loop, so we check to see if the source is a child object ---
            if(source.transform.IsChildOf(transform))
            {
                source.Play();
            }
            else
            {
                // --- Instantiate prefab for audio, delete after a few seconds ---
                AudioSource newAS = Instantiate(source);
                if ((newAS = Instantiate(source)) != null && newAS.outputAudioMixerGroup != null && newAS.outputAudioMixerGroup.audioMixer != null)
                {
                    // --- Change pitch to give variation to repeated shots ---
                    newAS.outputAudioMixerGroup.audioMixer.SetFloat("Pitch", Random.Range(audioPitch.x, audioPitch.y));
                    newAS.pitch = Random.Range(audioPitch.x, audioPitch.y);

                    // --- Play the gunshot sound ---
                    newAS.PlayOneShot(((GunInfo)itemInfo).gunshotSound);

                    // --- Remove after a few seconds. Test script only. When using in project I recommend using an object pool ---
                    Destroy(newAS.gameObject, 4);
                }
            }
        }
        
        // --- Insert custom code here to shoot projectile or hitscan from weapon ---

    }
}

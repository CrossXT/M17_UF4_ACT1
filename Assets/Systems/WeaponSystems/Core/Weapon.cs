using UnityEngine;

public class Weapon : MonoBehaviour
{
    public enum WeaponType
    {
        ShotByShot,
        ContinuousShot,
    }

    [SerializeField] public WeaponType weapontype;

    [Header("Debug")]
    public bool debugShot;
    public bool debugStartShooting;
    public bool debugStopShooting;

    //public BarrelBase[] allBarrels;

    private void OnValidate()
    {
        if (debugShot)
        {
            debugShot = false;
            Shot();
        }

        if (debugStartShooting)
        {
            debugStartShooting = false;
            StartShooting();
        }

        if (debugStopShooting)
        {
            debugStopShooting = false;
            StopShooting();
        }
    }

    public void Awake()
    {
        //allBarrels = GetComponentInChildren<BarrelBase>();
    }

    public void Shot()
    {
        //foreach (BarrelBase barrel in allBarrels)
        //{
        //    barrel.ShootOnce();
        //}
    }

    public void StartShooting()
    {
        //foreach (BarrelBase barrel in allBarrels)
        //{
        //    barrel.StartShooting();
        //}
    }

    public void StopShooting()
    {
        //foreach (BarrelBase barrel in allBarrels)
        //{
        //    barrel.StartShooting();
        //}
    }

    public void NotifySelected()
    {
        //Nada de momento
    }

    public void NotifyDeselected()
    {
        if( weapontype == WeaponType.ContinuousShot)
        {
            StopShooting();
        }
    }
}

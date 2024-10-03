using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using TMPro;

public class GunLogic : MonoBehaviour
{
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private GameObject bulletSpawnPoint;
    [SerializeField] private float shootCoolDown;
    private bool able2Shoot = true;
    [SerializeField] private TMP_Text AmmoDisplay;
    [SerializeField] private int AmmoNumber;
    private int CurrentAmmo;
    private AudioSource audio;
    [SerializeField] private AudioClip shootsound;
    [SerializeField] private AudioClip emptygunclip;
    [SerializeField] private AudioClip reload;
    [SerializeField] private bool IsEquiped;
    public bool EquipedData 
    {
        get { return IsEquiped; }
    }
    private Rigidbody GunRB;
    void Start()
    {
        CurrentAmmo = AmmoNumber;
        audio = gameObject.GetComponent<AudioSource>();
        GunRB = this.gameObject.GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (IsEquiped == true)
        {
            AmmoDisplay.text = $"Ammo: {CurrentAmmo}";
            GunRB.useGravity = false;
            if (Input.GetButtonDown("Fire1") && able2Shoot == true && CurrentAmmo > 0)
            {
                audio.PlayOneShot(shootsound);
                Instantiate(bulletPrefab, bulletSpawnPoint.transform.position, bulletSpawnPoint.transform.rotation);
                StartCoroutine(ShootCoolDown());
                CurrentAmmo--;
            }
            else if (Input.GetButtonDown("Fire1") && able2Shoot == true && CurrentAmmo <= 0)
            {
                audio.PlayOneShot(emptygunclip);
            }
        }
        else if (IsEquiped == false) 
        {
            GunRB.useGravity = true;
        }
    }

    public void Equiped() 
    { 
        IsEquiped = true;        
    }

    public void UnEquiped()
    {
        IsEquiped = false;
    }

    public void Reload() 
    {
        audio.PlayOneShot(reload);
        CurrentAmmo = AmmoNumber;
    }

    IEnumerator ShootCoolDown() 
    {
        able2Shoot = false;
        yield return new WaitForSeconds(shootCoolDown);
        able2Shoot = true;
    }
}

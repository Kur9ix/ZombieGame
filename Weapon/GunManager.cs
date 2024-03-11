using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunManager : MonoBehaviour
{
    public bool gunEquippd;
    private float damage;

    public GameObject weaponHolder;

    [SerializeField]
    private GameObject InventoryManager;
    [SerializeField]
    private SoundWave soundWave;
    [SerializeField]
    private UiManager uiManager;

    public Inventory inventory;
    public AudioClip audioClip;
    public ParticleSystem Muzzelflasch;
    public ParticleSystem bullet;
    float timeToShoot;

    public new AudioSource audio;
    public new ParticleSystem particleSystem;

    void Start()
    {
        inventory = InventoryManager.GetComponent<Inventory>();
    }

    void Update()
    {
        if (gunEquippd)
        {
            weaponHolder.GetComponent<SpriteRenderer>().sprite = inventory.equippedSlot.itemObject;
            audio.clip = inventory.equippedSlot.audioClip;

            Muzzelflasch = inventory.equippedSlot.Muzzelflasch;
            bullet = inventory.equippedSlot.bullet;

            if (Input.GetKey(KeyCode.Mouse0) && !uiManager.UiActive)
            {
                if (Time.time > timeToShoot && inventory.equippedSlot.currentMag > 0)
                {
                    StartCoroutine(FireDelay());
                }
            }
            if (Input.GetKey(KeyCode.R))
            {
                reload();
            }
        }

    }

    IEnumerator FireDelay()
    {
        shoot();
        timeToShoot = Time.time + inventory.equippedSlot.timetoFire;
        yield return new WaitForSeconds(inventory.equippedSlot.timetoFire);
    }

    void shoot()
    {
        print("Shoot");
        Vector3 muzzelPos = new Vector3(transform.position.x + inventory.equippedSlot.MuzzelPos.x, transform.position.y + inventory.equippedSlot.MuzzelPos.y, transform.position.z);
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        
        mousePosition.z = 0f;
        Vector2 muzzlePos = new Vector2(muzzelPos.x, muzzelPos.y);

        Vector2 direction = mousePosition - (Vector3)muzzlePos;

        RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, inventory.equippedSlot.distance);
        if(audio != null && Muzzelflasch != null && bullet != null){
            audio.PlayOneShot(audio.clip);
            Muzzelflasch.Play();
            bullet.Play();
        }
        if (hit && hit.transform.tag == "enemy")
        {
            Debug.Log("Hit object: " + hit.collider.gameObject.name);
            hit.transform.GetComponent<EnemyHealth>().updateHealth(inventory.equippedSlot.damage);
        }
        inventory.equippedSlot.currentMag -= 1;
    }


    Item AssoultRifelAmmo;
    Item PistolAmmo;


    void reload()
    {
        inventory.equippedSlot.currentMag = inventory.equippedSlot.magSize;
        if (inventory.equippedSlot.itemName == "")
        {
            inventory.findItem(AssoultRifelAmmo);
        }
        else if (inventory.equippedSlot.itemName == "")
        {
            inventory.findItem(PistolAmmo);
        }
        //inventory.finditem(InventoryManager.GetComponent<Inventory>().equippedSlot.); Ammom Item Prefab
    }
}

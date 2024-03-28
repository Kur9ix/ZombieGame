using UnityEngine;

public enum ItemType
{
    Weapon,
    mealeWeapon,
    Useable,
    Ammo,
    Tool
}

public class Item : MonoBehaviour
{
    public ItemType itemType;
    public string itemName;
    public float itemWeight;

    public bool stackable;

    public bool automatic;
    public float timetoFire;
    public float damage;
    public float spread;
    public int distance;
    public int magSize;

    public int currentMag;
    public float heal;
    public float duration;

    public Sprite itemObject;
    public Vector2 MuzzelPostion;
    public AudioClip audioClip;
    public ParticleSystem bullet; 
    public ParticleSystem Muzzelflasch;

    public void Start(){
        gameObject.GetComponent<SpriteRenderer>().sprite = itemObject; 
    }
}


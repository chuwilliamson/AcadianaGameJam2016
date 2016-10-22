using Items;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public Inventory m_Inventory;

    public GameObject armPrefab;
    public GameObject legPrefab;


    // Use this for initialization
    void Awake()
    {
        //set components
        m_AudioSource = GetComponent<AudioSource>();

    }
    void Start()
    {
        var armLeftobj = BuildLimb(armPrefab, new Arm(), true);
        var armRightobj = BuildLimb(armPrefab, new Arm());
        var legRightobj = BuildLimb(legPrefab, new Leg(), true);
        var legLeftobj = BuildLimb(legPrefab, new Leg());
        StartCoroutine("CheckDead");
    }

    IEnumerator CheckDead()
    {

        while (m_Inventory.limbCount >0)
            yield return null;
        while (m_HitCounter > 0)
            yield return null;
        DoDead();

    }

    void DoDead()
    {
        GetComponent<SpriteRenderer>().sprite = DeadSprite;
    }


    private GameObject BuildLimb(Object template, Limb limb, bool flipX = false, bool flipY = false)
    {
        var obj = Instantiate(template, transform) as GameObject;
        if (!obj)
            return null;
        //check to see if some rando added an EnemyLimbBehaviour to the prefab
        //or if some
        var objComp = obj.GetComponent<EnemyLimbBehaviour>();
        if(!objComp)
            objComp = obj.AddComponent<EnemyLimbBehaviour>();

        objComp.Init(limb);

        obj.GetComponent<SpriteRenderer>().flipX = flipX;
        obj.GetComponent<SpriteRenderer>().flipY = flipY;

        m_Inventory.AddLimb(limb);

        return obj;
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {

        //to test hitting different body parts
        if (m_Inventory.PopArm(transform.position))
        {
            m_AudioSource.clip = AudioClips[0];
            m_AudioSource.Play();

        }
        else if (m_Inventory.PopLeg(transform.position))
        {
            m_AudioSource.clip = AudioClips[1];
            m_AudioSource.Play();
        }
        else
        {
            m_AudioSource.clip = AudioClips[2];

            m_HitCounter -= 1;

            m_AudioSource.Play();
        }
    }
    private AudioSource m_AudioSource;
    public AudioClip[] AudioClips;
    [SerializeField]
    private int m_HitCounter = 2;
    public Inventory inventory
    {
        get
        {
            return m_Inventory;
        }
    }

    public Sprite DeadSprite;
}

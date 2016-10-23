using Items;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;

using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public Inventory m_Inventory;

    public GameObject armPrefab;
    public GameObject legPrefab;

    public static UnityEvent onEnemyDead;
    // Use this for initialization
    void Awake()
    {
        //set components
        m_AudioSource = GetComponent<AudioSource>();
        if (onEnemyDead == null)
            onEnemyDead = new UnityEvent();

    }
    public List<Sprite> m_armPool;
    public List<Sprite> m_legPool;
    public List<Sprite> m_torsoPool;
    void Start()
    {

        var armLeftobj = BuildLimb(armPrefab, new Arm(), true);

        armLeftobj.GetComponent<SpriteRenderer>().sprite = m_armPool[Random.Range(0, m_armPool.Count - 1)];
        var armRightobj = BuildLimb(armPrefab, new Arm());
        armRightobj.GetComponent<SpriteRenderer>().sprite = m_armPool[Random.Range(0, m_armPool.Count - 1)];
        var legRightobj = BuildLimb(legPrefab, new Leg(), true);

        var legLeftobj = BuildLimb(legPrefab, new Leg());
        StartCoroutine("CheckDead");
    }

    IEnumerator CheckDead()
    {
        while (m_Inventory.limbCount > 0)
            yield return null;
        while (m_HitCounter > 0)
            yield return null;
        DoDead();
        yield return (new WaitForSeconds(3));
        Destroy(gameObject);

    }
    bool dead = false;

    void DoDead()
    {
        dead = true;
        GetComponent<SpriteRenderer>().sprite = DeadSprite;
        GetComponent<ZambieBoid>().enabled = false;
        GetComponent<Rigidbody2D>().velocity.Set(0, 0);
        GetComponent<BoxCollider2D>().enabled = false;
        onEnemyDead.Invoke();
        
    }

    private GameObject BuildLimb(Object template, Limb limb, bool flipX = false, bool flipY = false)
    {
        var obj = Instantiate(template, transform) as GameObject;
        obj.transform.localPosition = Vector3.zero;
        if (!obj)
            return null;

        limb.parent = obj;

        //check to see if some rando added an EnemyLimbBehaviour to the prefab
        //or if some
        var objComp = obj.GetComponent<EnemyLimbBehaviour>();
        if (!objComp)
            objComp = obj.AddComponent<EnemyLimbBehaviour>();

        objComp.Init(limb);

        obj.GetComponent<SpriteRenderer>().flipX = flipX;
        obj.GetComponent<SpriteRenderer>().flipY = flipY;

        m_Inventory.AddLimb(limb);

        return obj;
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.gameObject.GetComponent<ZombiePlayerController>())
        {
            //to test hitting different body parts
            var poppedArm = m_Inventory.PopArm();
            if (poppedArm != null)
            {
                if (!LimbObjectFactory.Create(poppedArm, transform.position))
                    return;

                m_AudioSource.clip = AudioClips[0];
                m_AudioSource.Play();

                return;
            }

            var poppedLeg = m_Inventory.PopLeg();
            if (poppedLeg != null)
            {
                if (!LimbObjectFactory.Create(poppedLeg, transform.position))
                    return;

                m_AudioSource.clip = AudioClips[1];
                m_AudioSource.Play();

                return;
            }

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

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
        while (m_Inventory.limbCount > 0)
            yield return null;
        while (m_HitCounter > 0)
            yield return null;
        DoDead();

    }

    private void FixedUpdate()
    {
        var player = FindObjectOfType<ZombiePlayerController>();
        if (!player)
            return;

        var rigidbody2D = GetComponent<Rigidbody2D>();
        if (!rigidbody2D)
            return;

        //if (player.transform.position.x > transform.position.x)
        //    rigidbody2D.velocity = new Vector2(1f, 0f);
        //else if(player.transform.position.y > transform.position.y)
        //    rigidbody2D.velocity = new Vector2(0f, 1f);

        //else if (player.transform.position.x < transform.position.x)
        //    rigidbody2D.velocity = new Vector2(-1f, 0f);
        //else if (player.transform.position.y < transform.position.y)
        //    rigidbody2D.velocity = new Vector2(0f, -1f);

        var animator = GetComponent<Animator>();
        if (!animator)
            return;

        var forward = rigidbody2D.velocity.y < 0;
        var backward = rigidbody2D.velocity.y > 0;

        var left = rigidbody2D.velocity.x < 0;
        var right = rigidbody2D.velocity.x > 0;

        //animator.SetBool("Forward", forward);
        //animator.SetBool("Backward", backward);

        //animator.SetBool("Left", left);
        //animator.SetBool("Right", right);

        //animator.SetFloat("Horizontal", rigidbody2D.velocity.x);
        //animator.SetFloat("Vertical", rigidbody2D.velocity.y);

        //foreach (var limb in m_Inventory.limbs)
        //{
        //    var limbAnimator = limb.parent.GetComponent<Animator>();
        //    if (!limbAnimator)
        //        continue;

        //    limbAnimator.SetBool("Forward", forward);
        //    limbAnimator.SetBool("Backward", backward);

        //    limbAnimator.SetBool("Left", left);
        //    limbAnimator.SetBool("Right", right);

        //    limbAnimator.SetFloat("Horizontal", rigidbody2D.velocity.x);
        //    limbAnimator.SetFloat("Vertical", rigidbody2D.velocity.y);
        //}
    }

    void DoDead()
    {
        GetComponent<SpriteRenderer>().sprite = DeadSprite;
        GetComponent<ZambieBoid>().enabled = false;
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

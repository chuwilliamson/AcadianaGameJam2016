using UnityEngine;
using System.Collections;
using Items;
using UnityEngine.Events;
public class ObjectSpawner : MonoBehaviour
{
    public GameStateManager gm;
    void Awake()
    {
        if (onCombatUpdate == null)
            onCombatUpdate = new UnityEvent();
        gm = FindObjectOfType<GameStateManager>();
        if (!gm)
        {
            Debug.LogWarning("no gm found making default 25 enemies");
            numEnemies = 25;
            
        }
        else
        {
            numEnemies = gm.combatRound.enemyCount;
        }

        
        for (int i = 0; i < numEnemies; i++)
            Spawn(prefab);
    }
    void Start()
    {        
        EnemyController.onEnemyDead.AddListener(OnEnemyDead);
    }
    public int numEnemies;
    public UnityEvent onCombatUpdate;
    void OnEnemyDead()
    {
        numEnemies--;
        onCombatUpdate.Invoke();
    }
    void Update()
    {
        if (numEnemies <= 0)
        {
            if(gm)
                gm.ToCombat();          
                
        } 
    }
    void Spawn(GameObject go)
    {
        float randx = Random.Range(-25f, 25f);
        float randy = Random.Range(-25f, 25f);
        Vector3 randPos = new Vector3(randx, randy, 0);
        GameObject thing = Instantiate(go, randPos, Quaternion.identity, transform) as GameObject;
        thing.name = "Zambie"; 
    }

 
    // Update is called once per frame
    public GameObject prefab; 
}


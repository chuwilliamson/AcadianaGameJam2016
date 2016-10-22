using Items;

using UnityEngine;

public class EnemyLimbBehaviour : MonoBehaviour
{
    [SerializeField]
    private Limb m_Limb;

    public Limb limb
    {
        get { return m_Limb; }
    }

    public bool Init(Limb newLimb)
    {
        m_Limb = newLimb;

        // Attach to the event
        m_Limb.inInventoryEvent.AddListener(
            inInventory =>
            {
                if (!inInventory)
                    Destroy(gameObject);
            });

        return true;
    }
}

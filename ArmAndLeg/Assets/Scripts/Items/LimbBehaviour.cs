using System.Collections;

using UnityEngine;

namespace Items
{
    public class LimbBehaviour : MonoBehaviour
    {
        [SerializeField]
        private Limb m_Limb;

        public bool Init(Limb newLimb)
        {
            m_Limb = newLimb;

            // Attach to the event
            m_Limb.inInventoryEvent.AddListener(
                inInventory =>
                {
                    // If no longer in inventory then start to disappear
                    if (!inInventory)
                        StartCoroutine(Disappear());
                });

            return true;
        }

        private IEnumerator Disappear()
        {
            yield return new WaitForSeconds(m_Limb.aliveTime);

            if (!m_Limb.inInventory)
                Destroy(gameObject);
        }
    }
}

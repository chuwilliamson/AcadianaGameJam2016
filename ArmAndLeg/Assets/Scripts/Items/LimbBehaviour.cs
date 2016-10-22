using System.Collections;

using UnityEngine;

namespace Items
{
    public class LimbBehaviour : MonoBehaviour
    {
        [SerializeField]
        private Limb m_Limb;

        [SerializeField]
        private float m_AliveTime;

        private bool m_CanPickUp;

        public Limb limb
        {
            get { return m_Limb; }
        }

        public float aliveTime
        {
            get { return m_AliveTime; }
            set { m_AliveTime = value; }
        }

        public bool canPickUp
        {
            get { return m_CanPickUp; }
        }

        public bool Init(Limb newLimb, float newAliveTime)
        {
            m_Limb = newLimb;
            m_AliveTime = newAliveTime;

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
            var rigidbody = GetComponent<Rigidbody2D>();

            var time = 0f;

            while (time < m_AliveTime)
            {
                while (
                    rigidbody &&
                    (Mathf.Abs(rigidbody.velocity.x) > 0.1f || Mathf.Abs(rigidbody.velocity.y) > 0.1f))
                {
                    m_CanPickUp = false;
                    yield return false;
                }

                time += Time.deltaTime;

                var spriteRenderer = GetComponent<SpriteRenderer>();
                if (spriteRenderer)
                {
                    spriteRenderer.color =
                        new Color(
                            spriteRenderer.color.r,
                            spriteRenderer.color.g,
                            spriteRenderer.color.b,
                            spriteRenderer.color.a - 1f / m_AliveTime * Time.deltaTime);
                }

                m_CanPickUp = true;
                yield return false;
            }

            if (!m_Limb.inInventory)
                Destroy(gameObject);
        }
    }
}

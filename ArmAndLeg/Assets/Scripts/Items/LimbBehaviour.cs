using System.Collections;
using System.Runtime.Remoting.Messaging;

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
            m_Limb.parent = gameObject;

            m_AliveTime = newAliveTime;

            StartCoroutine(Disappear());

            return true;
        }

        private IEnumerator Disappear()
        {
            m_CanPickUp = false;

            yield return new WaitForSeconds(0.5f);

            var rigidbody = GetComponent<Rigidbody2D>();
            yield return new WaitUntil(
                () => Mathf.Abs(rigidbody.velocity.x) < 0.1f || Mathf.Abs(rigidbody.velocity.y) < 0.1f);

            m_CanPickUp = true;

            var time = 0f;
            while (time < m_AliveTime)
            {
                time += Time.deltaTime;

                var spriteRenderer = GetComponent<SpriteRenderer>();
                if (spriteRenderer)
                {
                    spriteRenderer.color =
                        new Color(
                            spriteRenderer.color.r,
                            spriteRenderer.color.g,
                            spriteRenderer.color.b,
                            1f - time / m_AliveTime);
                }
                transform.localScale =
                    new Vector3(
                        1f + Mathf.Sin(time * 6f) / 3f,
                        1f + Mathf.Sin(time * 6f) / 3f,
                        1f);

                yield return false;
            }

            if (!m_Limb.inInventory)
                Destroy(gameObject);
        }
    }
}

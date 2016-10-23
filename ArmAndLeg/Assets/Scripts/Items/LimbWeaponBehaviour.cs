using UnityEngine;
using UnityEngine.Events;

namespace Items
{
    public class LimbWeaponBehaviour : MonoBehaviour
    {
        private GameObject m_Owner;
        private Limb m_Limb;

        private Rigidbody2D m_Rigidbody2D;
        private Collider2D m_Collider2D;

        private UnityEvent m_OnDestroyEvent = new UnityEvent();

        public GameObject owner
        {
            get { return m_Owner; }
            set { m_Owner = value; }
        }

        public Limb limb
        {
            get { return m_Limb; }
        }

        public UnityEvent onDestroyEvent
        {
            get { return m_OnDestroyEvent; }
        }

        public bool Init(GameObject owner, Limb newLimb)
        {
            m_Owner = owner;

            m_Collider2D = GetComponent<Collider2D>();

            m_Rigidbody2D = GetComponent<Rigidbody2D>();
            if (m_Rigidbody2D)
            {
                m_Rigidbody2D.AddForce(-m_Owner.transform.up * 500f);
                m_Rigidbody2D.AddTorque(500f);
            }

            m_Limb = newLimb;
            m_Limb.parent = gameObject;

            return true;
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject == m_Owner)
                return;

            var otherEnemy = other.gameObject.GetComponent<EnemyController>();
            if (!otherEnemy)
                return;

            if (!otherEnemy.TakeDamage())
                return;

            m_Limb.durability--;

            if (m_Limb.durability <= 0)
            {
                var newLimbObject = LimbObjectFactory.CreateObject(
                    m_Limb,
                    transform.position,
                    LimbObjectFactory.PhysicsType.None);

                var newLimbObjectRigidbody = newLimbObject.GetComponent<Rigidbody2D>();
                if (newLimbObjectRigidbody)
                {
                    newLimbObjectRigidbody.velocity = m_Rigidbody2D.velocity;
                    newLimbObjectRigidbody.angularVelocity = m_Rigidbody2D.angularVelocity;
                }

                Destroy(gameObject);
            }
        }

        private void OnDestroy()
        {
            m_OnDestroyEvent.Invoke();
        }
    }
}

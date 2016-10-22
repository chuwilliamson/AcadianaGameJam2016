using System.Collections;

using UnityEngine;

namespace Items
{
    public abstract class Limb : MonoBehaviour
    {
        private bool m_InInventory;

        [SerializeField]
        private float m_RangedDamage = 1f;
        [SerializeField]
        private float m_MeleeDamage = 1f;

        [SerializeField]
        private float m_AliveTime = 3f;

        public bool inInventory
        {
            get { return m_InInventory; }
            set
            {
                m_InInventory = value;
                if (!value)
                    StartCoroutine(Update());
            }
        }

        public abstract bool Swing();
        public abstract bool Throw();

        private IEnumerator Update()
        {
            yield return new WaitForSeconds(m_AliveTime);

            Destroy(gameObject);
        }
    }
}

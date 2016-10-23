using System.Collections;

using UnityEngine;
using UnityEngine.Events;

namespace Items
{
    public class MyBoolEvent : UnityEvent<bool> { }

    public abstract class Limb
    {
        private GameObject m_Parent;

        private bool m_InInventory;

        [SerializeField]
        private float m_RangedDamage = 1f;
        [SerializeField]
        private float m_MeleeDamage = 1f;

        public MyBoolEvent inInventoryEvent = new MyBoolEvent();

        public GameObject parent
        {
            get { return m_Parent; }
            set { m_Parent = value; }
        }

        public bool inInventory
        {
            get { return m_InInventory; }
            set
            {
                m_InInventory = value;

                inInventoryEvent.Invoke(value);
            }
        }

        public abstract bool Swing();
        public abstract bool Throw();
    }
}

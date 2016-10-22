using System.Collections;

using UnityEngine;
using UnityEngine.Events;

namespace Items
{
    public class MyBoolEvent : UnityEvent<bool> { }

    public abstract class Limb
    {
        private bool m_InInventory;

        [SerializeField]
        private float m_RangedDamage = 1f;
        [SerializeField]
        private float m_MeleeDamage = 1f;

        [SerializeField]
        private float m_AliveTime = 3f;

        public MyBoolEvent inInventoryEvent = new MyBoolEvent();

        public bool inInventory
        {
            get { return m_InInventory; }
            set
            {
                m_InInventory = value;

                inInventoryEvent.Invoke(value);
            }
        }

        public float aliveTime
        {
            get { return m_AliveTime; }
        }

        public abstract bool Swing();
        public abstract bool Throw();
    }
}

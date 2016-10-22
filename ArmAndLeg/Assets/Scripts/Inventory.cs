﻿using System;
using System.Collections.Generic;
using System.Linq;

using Items;

using UnityEngine;
using UnityEngine.Events;

using Object = UnityEngine.Object;

[Serializable]
public class MyIntEvent : UnityEvent<int> { }

[Serializable]
public class Inventory
{
    /// <summary> The list of generic limbs that the inventory manages </summary>
    [SerializeField]
    private List<Limb> m_Limbs = new List<Limb>();

    /// <summary> The internal count of arms </summary>
    private int m_ArmCount;
    /// <summary> The internal count of legs </summary>
    private int m_LegCount;

    /// <summary>
    /// This is how you can interface with changes made to the arm count in the inventory in real time.
    /// Practical uses would be for UI display which needs to know the current amount at all times
    /// </summary>
    public MyIntEvent armCountEvent = new MyIntEvent();
    /// <summary>
    /// This is how you can interface with changes made to the leg count in the inventory in real time.
    /// Practical uses would be for UI display which needs to know the current amount at all times
    /// </summary>
    public MyIntEvent legCountEvent = new MyIntEvent();

    /// <summary> The total amount of limbs the inventory contains </summary>
    public int limbCount
    {
        get { return m_Limbs.Count; }
    }

    /// <summary> The total amount of arms the inventory contains </summary>
    public int armCount
    {
        get { return m_ArmCount; }
        private set
        {
            if (value < 0)
                return;

            m_ArmCount = value;
            armCountEvent.Invoke(value);
        }
    }
    /// <summary> The total amount of legs the inventory contains </summary>
    public int legCount
    {
        get { return m_LegCount; }
        private set
        {
            if (value < 0)
                return;

            m_LegCount = value;
            legCountEvent.Invoke(value);
        }
    }

    public IEnumerable<Arm> arms
    {
        get { return m_Limbs.OfType<Arm>(); }
    }
    public IEnumerable<Leg> legs
    {
        get { return m_Limbs.OfType<Leg>(); }
    }

    public bool AddLimb(Limb newLimb)
    {
        m_Limbs.Add(newLimb);

        if (newLimb is Arm)
            armCount++;
        else if (newLimb is Leg)
            legCount++;

        newLimb.inInventory = true;

        return true;
    }

    public bool PopLimb(Transform parentTransform, int popNumber = 1)
    {
        if (popNumber > m_Limbs.Count)
            return false;

        for (var i = 0; i < popNumber; ++i)
            if (!RemoveLimb(m_Limbs.Last(), parentTransform))
                return false;

        return true;
    }

    public bool PopArm(Transform parentTransform, int popNumber = 1)
    {
        if (popNumber > armCount)
            return false;

        for (var i = 0; i < popNumber; ++i)
            if (!RemoveLimb(arms.Last(), parentTransform))
                return false;

        return true;
    }

    public bool PopLeg(Transform parentTransform, int popNumber = 1)
    {
        if (popNumber > legCount)
            return false;

        for (var i = 0; i < popNumber; ++i)
            if (!RemoveLimb(legs.Last(), parentTransform))
                return false;

        return true;
    }

    public bool RemoveLimb(Limb limb, Transform parentTransform)
    {
        if (m_Limbs.Count == 0)
            return false;

        m_Limbs.Remove(limb);

        if (limb is Arm)
            armCount--;
        else if (limb is Leg)
            legCount--;

        if (!CreateLimbObject(limb, parentTransform))
            return false;

        limb.inInventory = false;

        return true;
    }

    private static bool CreateLimbObject(Limb limb, Transform parentTransform)
    {
        var newLimbObject =
                Object.Instantiate(
                    new GameObject(),
                    parentTransform.position,
                    Quaternion.identity) as GameObject;

        if (newLimbObject == null)
            return false;

        var newLimbBehaviour = newLimbObject.AddComponent<LimbBehaviour>();

        newLimbBehaviour.Init(limb);

        return true;
    }
}
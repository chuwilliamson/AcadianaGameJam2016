﻿using UnityEngine;

namespace Factories
{
    [CreateAssetMenu(fileName = "NewLimbFactorySettings", menuName = "Settings/Limb Factory Settings")]
    public class LimbObjectFactorySettings : ScriptableObject
    {
        [Space]
        public GameObject limbPrefab;

        [Space]
        public Sprite defaultArmSprite;
        public Sprite defaultLegSprite;

        [Space]
        public float aliveTime;

        [Space]
        public float translationRandomMin;
        public float translationRandomMax;

        [Space]
        public float rotationRandomMin;
        public float rotationRandomMax;
    }
}
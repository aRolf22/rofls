using UnityEngine;

[CreateAssetMenu(fileName = "AmmoHitEffect_", menuName = "Scriptable Objects/Weapons/Ammo Hit Effect")]
public class AmmoHitEffectSO : ScriptableObject
{
    [System.Serializable]
    public class HitEffectPrefab
    {
        [Tooltip("The prefab containing the particle system for this hit effect")]
        public GameObject prefab;
        
        [Tooltip("The color gradient for the hit effect")]
        public Gradient colorGradient;
        
        [Tooltip("The length of time the particle system is emitting particles")]
        public float duration;
        
        [Tooltip("The start particle size")]
        public float startParticleSize;
        
        [Tooltip("The start particle speed")]
        public float startParticleSpeed;
        
        [Tooltip("The particle lifetime")]
        public float startLifetime;
        
        [Tooltip("The maximum number of particles")]
        public int maxParticleNumber;
        
        [Tooltip("The number of particles emitted per second")]
        public int emissionRate;
        
        [Tooltip("How many particles should be emitted in the burst")]
        public int burstParticleNumber;
        
        [Tooltip("The gravity on the particles")]
        public float effectGravity;
        
        [Tooltip("The sprite for the particle effect")]
        public Sprite sprite;
        
        [Tooltip("The min velocity for particles over lifetime")]
        public Vector3 velocityOverLifetimeMin;
        
        [Tooltip("The max velocity for particles over lifetime")]
        public Vector3 velocityOverLifetimeMax;
    }

    [Header("HIT EFFECT DETAILS")]
    public HitEffectPrefab[] hitEffectPrefabs;

    #region Validation
#if UNITY_EDITOR
    private void OnValidate()
    {
        if (hitEffectPrefabs == null || hitEffectPrefabs.Length == 0)
        {
            Debug.LogWarning($"No hit effect prefabs assigned in {name}");
            return;
        }

        for (int i = 0; i < hitEffectPrefabs.Length; i++)
        {
            var effect = hitEffectPrefabs[i];
            HelperUtilities.ValidateCheckNullValue(this, $"{nameof(hitEffectPrefabs)}[{i}].prefab", effect.prefab);
            HelperUtilities.ValidateCheckPositiveValue(this, $"{nameof(hitEffectPrefabs)}[{i}].duration", effect.duration, false);
            HelperUtilities.ValidateCheckPositiveValue(this, $"{nameof(hitEffectPrefabs)}[{i}].startParticleSize", effect.startParticleSize, false);
            HelperUtilities.ValidateCheckPositiveValue(this, $"{nameof(hitEffectPrefabs)}[{i}].startParticleSpeed", effect.startParticleSpeed, false);
            HelperUtilities.ValidateCheckPositiveValue(this, $"{nameof(hitEffectPrefabs)}[{i}].startLifetime", effect.startLifetime, false);
            HelperUtilities.ValidateCheckPositiveValue(this, $"{nameof(hitEffectPrefabs)}[{i}].maxParticleNumber", effect.maxParticleNumber, false);
            HelperUtilities.ValidateCheckPositiveValue(this, $"{nameof(hitEffectPrefabs)}[{i}].emissionRate", effect.emissionRate, true);
            HelperUtilities.ValidateCheckPositiveValue(this, $"{nameof(hitEffectPrefabs)}[{i}].burstParticleNumber", effect.burstParticleNumber, true);
        }
    }
#endif
    #endregion
}
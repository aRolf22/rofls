using UnityEngine;

[CreateAssetMenu(fileName = "AmmoHitEffect_", menuName = "Scriptable Objects/Weapons/Ammo Hit Effect")]
public class AmmoHitEffectSO : ScriptableObject
{
    [System.Serializable]
    public class HitEffectPrefab
    {
        [Tooltip("The prefab containing the particle system for this hit effect")]
        public GameObject prefab;
        
        [Header("Color Gradient")]
        public bool applyColorGradient = true;
        [Tooltip("The color gradient for the hit effect")]
        public Gradient colorGradient;
        
        [Header("Duration")]
        public bool applyDuration = true;
        [Tooltip("The length of time the particle system is emitting particles")]
        public float duration;
        
        [Header("Particle Size")]
        public bool applyStartParticleSize = true;
        [Tooltip("The start particle size")]
        public float startParticleSize;
        
        [Header("Particle Speed")]
        public bool applyStartParticleSpeed = true;
        [Tooltip("The start particle speed")]
        public float startParticleSpeed;
        
        [Header("Particle Lifetime")]
        public bool applyStartLifetime = true;
        [Tooltip("The particle lifetime")]
        public float startLifetime;
        
        [Header("Max Particles")]
        public bool applyMaxParticleNumber = true;
        [Tooltip("The maximum number of particles")]
        public int maxParticleNumber;
        
        [Header("Emission Rate")]
        public bool applyEmissionRate = true;
        [Tooltip("The number of particles emitted per second")]
        public int emissionRate;
        
        [Header("Burst Particles")]
        public bool applyBurstParticleNumber = true;
        [Tooltip("How many particles should be emitted in the burst")]
        public int burstParticleNumber;
        
        [Header("Gravity")]
        public bool applyEffectGravity = true;
        [Tooltip("The gravity on the particles")]
        public float effectGravity;
        
        [Header("Sprite")]
        public bool applySprite = true;
        [Tooltip("The sprite for the particle effect")]
        public Sprite sprite;
        
        [Header("Velocity Over Lifetime")]
        public bool applyVelocityOverLifetime = true;
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
            
            if (effect.applyDuration)
                HelperUtilities.ValidateCheckPositiveValue(this, $"{nameof(hitEffectPrefabs)}[{i}].duration", effect.duration, false);
            
            if (effect.applyStartParticleSize)
                HelperUtilities.ValidateCheckPositiveValue(this, $"{nameof(hitEffectPrefabs)}[{i}].startParticleSize", effect.startParticleSize, false);
            
            if (effect.applyStartParticleSpeed)
                HelperUtilities.ValidateCheckPositiveValue(this, $"{nameof(hitEffectPrefabs)}[{i}].startParticleSpeed", effect.startParticleSpeed, false);
            
            if (effect.applyStartLifetime)
                HelperUtilities.ValidateCheckPositiveValue(this, $"{nameof(hitEffectPrefabs)}[{i}].startLifetime", effect.startLifetime, false);
            
            if (effect.applyMaxParticleNumber)
                HelperUtilities.ValidateCheckPositiveValue(this, $"{nameof(hitEffectPrefabs)}[{i}].maxParticleNumber", effect.maxParticleNumber, false);
            
            if (effect.applyEmissionRate)
                HelperUtilities.ValidateCheckPositiveValue(this, $"{nameof(hitEffectPrefabs)}[{i}].emissionRate", effect.emissionRate, true);
            
            if (effect.applyBurstParticleNumber)
                HelperUtilities.ValidateCheckPositiveValue(this, $"{nameof(hitEffectPrefabs)}[{i}].burstParticleNumber", effect.burstParticleNumber, true);
        }
    }
#endif
    #endregion
}
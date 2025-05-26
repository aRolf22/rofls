using UnityEngine;

[CreateAssetMenu(fileName = "WeaponShootEffect_", menuName = "Scriptable Objects/Weapons/Weapon Shoot Effect")]
public class WeaponShootEffectSO : ScriptableObject
{
    [System.Serializable]
    public class ShootEffectPrefab
    {
        [Tooltip("The prefab containing the particle system for this shoot effect")]
        public GameObject prefab;

        [Header("Follow Position")]
        [Tooltip("If true, the effect will follow the shoot position after spawning")]
        public bool followShootPosition = false;

        [Header("Color Settings")]
        public bool applyColorGradient = true;
        [Tooltip("The color gradient for this shoot effect. This gradient shows the color of particles during their lifetime - from left to right")]
        public Gradient colorGradient;
        public bool applyStartColor = true;
        [Tooltip("The start color for particles")]
        public Color startColor;
        

        [Header("Duration")]
        public bool applyDuration = true;
        [Tooltip("The length of time the particle system is emitting particles")]
        public float duration;

        [Header("Particle Size")]
        public bool applyStartParticleSize = true;
        [Tooltip("The start particle size for this particle effect")]
        public float startParticleSize;

        [Header("Particle Speed")]
        public bool applyStartParticleSpeed = true;
        [Tooltip("The start particle speed for this particle effect")]
        public float startParticleSpeed;

        [Header("Particle Lifetime")]
        public bool applyStartLifetime = true;
        [Tooltip("The particle lifetime for this particle effect")]
        public float startLifetime;

        [Header("Max Particles")]
        public bool applyMaxParticleNumber = true;
        [Tooltip("The maximum number of particles to be emitted for this effect")]
        public int maxParticleNumber;

        [Header("Emission Rate")]
        public bool applyEmissionRate = true;
        [Tooltip("The number of particles emitted per second. If zero it will just be the burst number")]
        public int emissionRate;

        [Header("Burst Particles")]
        public bool applyBurstParticleNumber = true;
        [Tooltip("How many particles should be emitted in this particle effect burst")]
        public int burstParticleNumber;

        [Header("Gravity")]
        public bool applyEffectGravity = true;
        [Tooltip("The gravity on the particles - a small negative number will make them float up")]
        public float effectGravity;

        [Header("Sprite")]
        public bool applySprite = true;
        [Tooltip("The sprite for this particle effect. If none is specified then the default particle sprite will be used")]
        public Sprite sprite;

        [Header("Velocity Over Lifetime")]
        public bool applyVelocityOverLifetime = true;
        [Tooltip("The min velocity for the particle over its lifetime. A random value between min and max will be generated.")]
        public Vector3 velocityOverLifetimeMin;
        [Tooltip("The max velocity for the particle over its lifetime. A random value between min and max will be generated.")]
        public Vector3 velocityOverLifetimeMax;
    }

    [Header("WEAPON SHOOT EFFECT DETAILS")]
    public ShootEffectPrefab[] shootEffectPrefabs;

    #region Validation
#if UNITY_EDITOR
    private void OnValidate()
    {
        if (shootEffectPrefabs == null || shootEffectPrefabs.Length == 0)
        {
            Debug.LogWarning($"No shoot effect prefabs assigned in {name}");
            return;
        }

        for (int i = 0; i < shootEffectPrefabs.Length; i++)
        {
            var effect = shootEffectPrefabs[i];
            HelperUtilities.ValidateCheckNullValue(this, $"{nameof(shootEffectPrefabs)}[{i}].prefab", effect.prefab);
            
            if (effect.applyDuration)
                HelperUtilities.ValidateCheckPositiveValue(this, $"{nameof(shootEffectPrefabs)}[{i}].duration", effect.duration, false);
            
            if (effect.applyStartParticleSize)
                HelperUtilities.ValidateCheckPositiveValue(this, $"{nameof(shootEffectPrefabs)}[{i}].startParticleSize", effect.startParticleSize, false);
            
            if (effect.applyStartParticleSpeed)
                HelperUtilities.ValidateCheckPositiveValue(this, $"{nameof(shootEffectPrefabs)}[{i}].startParticleSpeed", effect.startParticleSpeed, false);
            
            if (effect.applyStartLifetime)
                HelperUtilities.ValidateCheckPositiveValue(this, $"{nameof(shootEffectPrefabs)}[{i}].startLifetime", effect.startLifetime, false);
            
            if (effect.applyMaxParticleNumber)
                HelperUtilities.ValidateCheckPositiveValue(this, $"{nameof(shootEffectPrefabs)}[{i}].maxParticleNumber", effect.maxParticleNumber, false);
            
            if (effect.applyEmissionRate)
                HelperUtilities.ValidateCheckPositiveValue(this, $"{nameof(shootEffectPrefabs)}[{i}].emissionRate", effect.emissionRate, true);
            
            if (effect.applyBurstParticleNumber)
                HelperUtilities.ValidateCheckPositiveValue(this, $"{nameof(shootEffectPrefabs)}[{i}].burstParticleNumber", effect.burstParticleNumber, true);
        }
    }
#endif
    #endregion
}
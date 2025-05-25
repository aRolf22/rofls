using UnityEngine;

[CreateAssetMenu(fileName = "WeaponShootEffect_", menuName = "Scriptable Objects/Weapons/Weapon Shoot Effect")]
public class WeaponShootEffectSO : ScriptableObject
{
    [System.Serializable]
    public class ShootEffectPrefab
    {
        #region Tooltip
        [Tooltip("The prefab containing the particle system for this shoot effect")]
        #endregion Tooltip
        public GameObject prefab;

        #region Tooltip
        [Tooltip("The color gradient for this shoot effect. This gradient shows the color of particles during their lifetime - from left to right")]
        #endregion Tooltip
        public Gradient colorGradient;

        #region Tooltip
        [Tooltip("The length of time the particle system is emitting particles")]
        #endregion Tooltip
        public float duration;

        #region Tooltip
        [Tooltip("The start particle size for this particle effect")]
        #endregion Tooltip
        public float startParticleSize;

        #region Tooltip
        [Tooltip("The start particle speed for this particle effect")]
        #endregion Tooltip
        public float startParticleSpeed;

        #region Tooltip
        [Tooltip("The particle lifetime for this particle effect")]
        #endregion Tooltip
        public float startLifetime;

        #region Tooltip
        [Tooltip("The maximum number of particles to be emitted for this effect")]
        #endregion Tooltip
        public int maxParticleNumber;

        #region Tooltip
        [Tooltip("The number of particles emitted per second. If zero it will just be the burst number")]
        #endregion Tooltip
        public int emissionRate;

        #region Tooltip
        [Tooltip("How many particles should be emitted in this particle effect burst")]
        #endregion Tooltip
        public int burstParticleNumber;

        #region Tooltip
        [Tooltip("The gravity on the particles - a small negative number will make them float up")]
        #endregion
        public float effectGravity;

        #region Tooltip
        [Tooltip("The sprite for this particle effect. If none is specified then the default particle sprite will be used")]
        #endregion Tooltip
        public Sprite sprite;

        #region Tooltip
        [Tooltip("The min velocity for the particle over its lifetime. A random value between min and max will be generated.")]
        #endregion Tooltip
        public Vector3 velocityOverLifetimeMin;

        #region Tooltip
        [Tooltip("The max velocity for the particle over its lifetime. A random value between min and max will be generated.")]
        #endregion Tooltip
        public Vector3 velocityOverLifetimeMax;
    }

    #region Header WEAPON SHOOT EFFECT DETAILS
    [Space(10)]
    [Header("WEAPON SHOOT EFFECT DETAILS")]
    #endregion Header WEAPON SHOOT EFFECT DETAILS

    #region Tooltip
    [Tooltip("List of shoot effect prefabs with their individual parameters")]
    #endregion Tooltip
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
            HelperUtilities.ValidateCheckPositiveValue(this, $"{nameof(shootEffectPrefabs)}[{i}].duration", effect.duration, false);
            HelperUtilities.ValidateCheckPositiveValue(this, $"{nameof(shootEffectPrefabs)}[{i}].startParticleSize", effect.startParticleSize, false);
            HelperUtilities.ValidateCheckPositiveValue(this, $"{nameof(shootEffectPrefabs)}[{i}].startParticleSpeed", effect.startParticleSpeed, false);
            HelperUtilities.ValidateCheckPositiveValue(this, $"{nameof(shootEffectPrefabs)}[{i}].startLifetime", effect.startLifetime, false);
            HelperUtilities.ValidateCheckPositiveValue(this, $"{nameof(shootEffectPrefabs)}[{i}].maxParticleNumber", effect.maxParticleNumber, false);
            HelperUtilities.ValidateCheckPositiveValue(this, $"{nameof(shootEffectPrefabs)}[{i}].emissionRate", effect.emissionRate, true);
            HelperUtilities.ValidateCheckPositiveValue(this, $"{nameof(shootEffectPrefabs)}[{i}].burstParticleNumber", effect.burstParticleNumber, true);
        }
    }

#endif

    #endregion Validation
}
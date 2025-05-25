using UnityEngine;

[DisallowMultipleComponent]
public class WeaponShootEffect : MonoBehaviour
{
    private new ParticleSystem particleSystem;

    private void Awake()
    {
        // Get the ParticleSystem component
        particleSystem = GetComponent<ParticleSystem>();
    }

    /// <summary>
    /// Set the Shoot Effect with specific settings
    /// </summary>
    public void SetShootEffect(WeaponShootEffectSO.ShootEffectPrefab effectSettings, float aimAngle)
    {
        if (particleSystem == null)
        {
            Debug.LogWarning("No ParticleSystem component found");
            return;
        }

        // Set all effect parameters
        SetShootEffectColorGradient(effectSettings.colorGradient);
        SetShootEffectParticleStartingValues(
            effectSettings.duration,
            effectSettings.startParticleSize,
            effectSettings.startParticleSpeed,
            effectSettings.startLifetime,
            effectSettings.effectGravity,
            effectSettings.maxParticleNumber);
        SetShootEffectParticleEmission(effectSettings.emissionRate, effectSettings.burstParticleNumber);
        SetShootEffectParticleSprite(effectSettings.sprite);
        SetShootEffectVelocityOverLifeTime(effectSettings.velocityOverLifetimeMin, effectSettings.velocityOverLifetimeMax);
        SetEmmitterRotation(aimAngle);
    }

    /// <summary>
    /// Set the shoot effect particle system color gradient
    /// </summary>
    private void SetShootEffectColorGradient(Gradient gradient)
    {
        ParticleSystem.ColorOverLifetimeModule colorOverLifetimeModule = particleSystem.colorOverLifetime;
        colorOverLifetimeModule.color = gradient;
    }

    /// <summary>
    /// Set shoot effect particle system starting values
    /// </summary>
    private void SetShootEffectParticleStartingValues(float duration, float startParticleSize,
        float startParticleSpeed, float startLifetime, float effectGravity, int maxParticles)
    {
        ParticleSystem.MainModule mainModule = particleSystem.main;

        mainModule.duration = duration;
        mainModule.startSize = startParticleSize;
        mainModule.startSpeed = startParticleSpeed;
        mainModule.startLifetime = startLifetime;
        mainModule.gravityModifier = effectGravity;
        mainModule.maxParticles = maxParticles;
    }

    /// <summary>
    /// Set shoot effect particle system particle burst particle number
    /// </summary>
    private void SetShootEffectParticleEmission(int emissionRate, float burstParticleNumber)
    {
        ParticleSystem.EmissionModule emissionModule = particleSystem.emission;

        ParticleSystem.Burst burst = new ParticleSystem.Burst(0f, burstParticleNumber);
        emissionModule.SetBurst(0, burst);
        emissionModule.rateOverTime = emissionRate;
    }

    /// <summary>
    /// Set shoot effect particle system sprite
    /// </summary>
    private void SetShootEffectParticleSprite(Sprite sprite)
    {
        if (sprite == null) return;

        ParticleSystem.TextureSheetAnimationModule textureSheetAnimationModule = particleSystem.textureSheetAnimation;
        textureSheetAnimationModule.SetSprite(0, sprite);
    }

    /// <summary>
    /// Set the rotation of the emmitter to match the aim angle
    /// </summary>
    private void SetEmmitterRotation(float aimAngle)
    {
        transform.eulerAngles = new Vector3(0f, 0f, aimAngle);
    }

    /// <summary>
    /// Set the shoot effect velocity over lifetime
    /// </summary>
    private void SetShootEffectVelocityOverLifeTime(Vector3 minVelocity, Vector3 maxVelocity)
    {
        ParticleSystem.VelocityOverLifetimeModule velocityOverLifetimeModule = particleSystem.velocityOverLifetime;

        // Define min max X velocity
        ParticleSystem.MinMaxCurve minMaxCurveX = new ParticleSystem.MinMaxCurve();
        minMaxCurveX.mode = ParticleSystemCurveMode.TwoConstants;
        minMaxCurveX.constantMin = minVelocity.x;
        minMaxCurveX.constantMax = maxVelocity.x;
        velocityOverLifetimeModule.x = minMaxCurveX;

        // Define min max Y velocity
        ParticleSystem.MinMaxCurve minMaxCurveY = new ParticleSystem.MinMaxCurve();
        minMaxCurveY.mode = ParticleSystemCurveMode.TwoConstants;
        minMaxCurveY.constantMin = minVelocity.y;
        minMaxCurveY.constantMax = maxVelocity.y;
        velocityOverLifetimeModule.y = minMaxCurveY;

        // Define min max Z velocity
        ParticleSystem.MinMaxCurve minMaxCurveZ = new ParticleSystem.MinMaxCurve();
        minMaxCurveZ.mode = ParticleSystemCurveMode.TwoConstants;
        minMaxCurveZ.constantMin = minVelocity.z;
        minMaxCurveZ.constantMax = maxVelocity.z;
        velocityOverLifetimeModule.z = minMaxCurveZ;
    }
}
using UnityEngine;

[DisallowMultipleComponent]
public class AmmoHitEffect : MonoBehaviour
{
    private new ParticleSystem particleSystem;

    private void Awake()
    {
        particleSystem = GetComponent<ParticleSystem>();
    }

    public void SetHitEffect(AmmoHitEffectSO.HitEffectPrefab effectSettings)
    {
        if (particleSystem == null)
        {
            Debug.LogWarning("No ParticleSystem component found");
            return;
        }

        if (effectSettings.applyColorGradient)
            SetHitEffectColorGradient(effectSettings.colorGradient);
            
        if (effectSettings.applyStartColor)
            SetHitEffectStartColor(effectSettings.startColor);


        if (effectSettings.applyDuration || effectSettings.applyStartParticleSize || effectSettings.applyStartParticleSpeed ||
            effectSettings.applyStartLifetime || effectSettings.applyEffectGravity || effectSettings.applyMaxParticleNumber)
        {
            SetHitEffectParticleStartingValues(
                effectSettings.applyDuration ? effectSettings.duration : particleSystem.main.duration,
                effectSettings.applyStartParticleSize ? effectSettings.startParticleSize : particleSystem.main.startSize.constant,
                effectSettings.applyStartParticleSpeed ? effectSettings.startParticleSpeed : particleSystem.main.startSpeed.constant,
                effectSettings.applyStartLifetime ? effectSettings.startLifetime : particleSystem.main.startLifetime.constant,
                effectSettings.applyEffectGravity ? effectSettings.effectGravity : particleSystem.main.gravityModifier.constant,
                effectSettings.applyMaxParticleNumber ? effectSettings.maxParticleNumber : particleSystem.main.maxParticles
            );
        }

        if (effectSettings.applyEmissionRate || effectSettings.applyBurstParticleNumber)
        {
            SetHitEffectParticleEmission(
                effectSettings.applyEmissionRate ? effectSettings.emissionRate : particleSystem.emission.rateOverTime.constant,
                effectSettings.applyBurstParticleNumber ? effectSettings.burstParticleNumber : particleSystem.emission.GetBurst(0).count.constant
            );
        }

        if (effectSettings.applySprite)
            SetHitEffectParticleSprite(effectSettings.sprite);

        if (effectSettings.applyVelocityOverLifetime)
            SetHitEffectVelocityOverLifeTime(effectSettings.velocityOverLifetimeMin, effectSettings.velocityOverLifetimeMax);
    }

    private void SetHitEffectColorGradient(Gradient gradient)
    {
        ParticleSystem.ColorOverLifetimeModule colorOverLifetimeModule = particleSystem.colorOverLifetime;
        colorOverLifetimeModule.color = gradient;
    }
    
    /// <summary>
    /// Устанавливает стартовый цвет частиц
    /// </summary>
    private void SetHitEffectStartColor(Color color)
    {
        ParticleSystem.MainModule mainModule = particleSystem.main;
        mainModule.startColor = color;
    }

    private void SetHitEffectParticleStartingValues(float duration, float startParticleSize, float startParticleSpeed,
        float startLifetime, float effectGravity, int maxParticles)
    {
        ParticleSystem.MainModule mainModule = particleSystem.main;
        mainModule.duration = duration;
        mainModule.startSize = startParticleSize;
        mainModule.startSpeed = startParticleSpeed;
        mainModule.startLifetime = startLifetime;
        mainModule.gravityModifier = effectGravity;
        mainModule.maxParticles = maxParticles;
    }

    private void SetHitEffectParticleEmission(float emissionRate, float burstParticleNumber)
    {
        ParticleSystem.EmissionModule emissionModule = particleSystem.emission;
        ParticleSystem.Burst burst = new ParticleSystem.Burst(0f, burstParticleNumber);
        emissionModule.SetBurst(0, burst);
        emissionModule.rateOverTime = emissionRate;
    }

    private void SetHitEffectParticleSprite(Sprite sprite)
    {
        if (sprite == null) return;
        ParticleSystem.TextureSheetAnimationModule textureSheetAnimationModule = particleSystem.textureSheetAnimation;
        textureSheetAnimationModule.SetSprite(0, sprite);
    }

    private void SetHitEffectVelocityOverLifeTime(Vector3 minVelocity, Vector3 maxVelocity)
    {
        ParticleSystem.VelocityOverLifetimeModule velocityOverLifetimeModule = particleSystem.velocityOverLifetime;
        
        ParticleSystem.MinMaxCurve minMaxCurveX = new ParticleSystem.MinMaxCurve();
        minMaxCurveX.mode = ParticleSystemCurveMode.TwoConstants;
        minMaxCurveX.constantMin = minVelocity.x;
        minMaxCurveX.constantMax = maxVelocity.x;
        velocityOverLifetimeModule.x = minMaxCurveX;
        
        ParticleSystem.MinMaxCurve minMaxCurveY = new ParticleSystem.MinMaxCurve();
        minMaxCurveY.mode = ParticleSystemCurveMode.TwoConstants;
        minMaxCurveY.constantMin = minVelocity.y;
        minMaxCurveY.constantMax = maxVelocity.y;
        velocityOverLifetimeModule.y = minMaxCurveY;
        
        ParticleSystem.MinMaxCurve minMaxCurveZ = new ParticleSystem.MinMaxCurve();
        minMaxCurveZ.mode = ParticleSystemCurveMode.TwoConstants;
        minMaxCurveZ.constantMin = minVelocity.z;
        minMaxCurveZ.constantMax = maxVelocity.z;
        velocityOverLifetimeModule.z = minMaxCurveZ;
    }
}
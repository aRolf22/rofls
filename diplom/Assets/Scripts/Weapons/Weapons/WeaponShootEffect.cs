using UnityEngine;

[DisallowMultipleComponent]
public class WeaponShootEffect : MonoBehaviour
{
    private new ParticleSystem particleSystem;
    private Transform followTarget;
    private bool requiresFollowUpdate;

    private void Awake()
    {
        particleSystem = GetComponent<ParticleSystem>();
        enabled = false;

    }
    
    private void Update()
    {
        if (followTarget != null)
        {
            transform.position = followTarget.position;
        }
        else if (requiresFollowUpdate)
        {
            requiresFollowUpdate = false;
            enabled = false;
        }
    }

    public void SetShootEffect(WeaponShootEffectSO.ShootEffectPrefab effectSettings, float aimAngle, Transform target = null)
    {
        if (particleSystem == null)
        {
            Debug.LogWarning("No ParticleSystem component found");
            return;
        }

        requiresFollowUpdate = effectSettings.followShootPosition;
        followTarget = target;
        
        enabled = requiresFollowUpdate;


        if (effectSettings.applyColorGradient)
            SetShootEffectColorGradient(effectSettings.colorGradient);

        if (effectSettings.applyStartColor)
            SetShootEffectStartColor(effectSettings.startColor);

        if (effectSettings.applyDuration || effectSettings.applyStartParticleSize || effectSettings.applyStartParticleSpeed ||
            effectSettings.applyStartLifetime || effectSettings.applyEffectGravity || effectSettings.applyMaxParticleNumber)
        {
            SetShootEffectParticleStartingValues(
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
            SetShootEffectParticleEmission(
                effectSettings.applyEmissionRate ? effectSettings.emissionRate : particleSystem.emission.rateOverTime.constant,
                effectSettings.applyBurstParticleNumber ? effectSettings.burstParticleNumber : particleSystem.emission.GetBurst(0).count.constant
            );
        }

        if (effectSettings.applySprite)
            SetShootEffectParticleSprite(effectSettings.sprite);

        if (effectSettings.applyVelocityOverLifetime)
            SetShootEffectVelocityOverLifeTime(effectSettings.velocityOverLifetimeMin, effectSettings.velocityOverLifetimeMax);

        SetEmmitterRotation(aimAngle);
    }

    private void SetShootEffectColorGradient(Gradient gradient)
    {
        ParticleSystem.ColorOverLifetimeModule colorOverLifetimeModule = particleSystem.colorOverLifetime;
        colorOverLifetimeModule.color = gradient;
    }

    private void SetShootEffectStartColor(Color color)
    {
        ParticleSystem.MainModule mainModule = particleSystem.main;
        mainModule.startColor = color;
    }

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

    private void SetShootEffectParticleEmission(float emissionRate, float burstParticleNumber)
    {
        ParticleSystem.EmissionModule emissionModule = particleSystem.emission;
        ParticleSystem.Burst burst = new ParticleSystem.Burst(0f, burstParticleNumber);
        emissionModule.SetBurst(0, burst);
        emissionModule.rateOverTime = emissionRate;
    }

    private void SetShootEffectParticleSprite(Sprite sprite)
    {
        if (sprite == null) return;
        ParticleSystem.TextureSheetAnimationModule textureSheetAnimationModule = particleSystem.textureSheetAnimation;
        textureSheetAnimationModule.SetSprite(0, sprite);
    }

    private void SetShootEffectVelocityOverLifeTime(Vector3 minVelocity, Vector3 maxVelocity)
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

    private void SetEmmitterRotation(float aimAngle)
    {
        transform.eulerAngles = new Vector3(0f, 0f, aimAngle);
    }
}
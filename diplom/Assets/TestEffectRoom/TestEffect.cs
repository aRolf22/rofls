using UnityEngine;

public class TestEffect : MonoBehaviour
{
    public ParticleSystem[] particleSystems; // Массив для систем частиц

    void Update()
    {
        // Проверяем, нажата ли левая клавиша мыши
        if (Input.GetMouseButtonDown(0)) // 0 - это левая кнопка мыши
        {
            PlayParticleEffects();
        }
    }

    void PlayParticleEffects()
    {
        // Проходим по всем системам частиц и запускаем их
        foreach (ParticleSystem ps in particleSystems)
        {
            if (ps != null)
            {
                ps.Play(); // Запускаем систему частиц
            }
        }
    }
}
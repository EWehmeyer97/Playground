using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OceanSimulation : Singleton<OceanSimulation>
{
    public Material ocean;
    public OceanWave[] waves;

    private SineWave[] sineWaves;

    void Start()
    {
        sineWaves = new SineWave[8];
        for(int i = 0; i < waves.Length; i++)
        {
            sineWaves[i].direction = waves[i].direction.normalized;
            sineWaves[i].frequency = 2f / waves[i].waveLength;
            sineWaves[i].amplitude = waves[i].amplitude;
            sineWaves[i].phase = waves[i].speed * sineWaves[i].frequency;

            ocean.SetVector("_OceanDirection_" + i, sineWaves[i].direction);
            ocean.SetFloat("_OceanFrequency_" + i, sineWaves[i].frequency);
            ocean.SetFloat("_OceanAmplitude_" + i, sineWaves[i].amplitude);
            ocean.SetFloat("_OceanSpeed_" + i, sineWaves[i].phase);
        }
        float freq = 2f / waves[0].waveLength;
        float amp = waves[0].amplitude;
        for (int i = waves.Length; i < sineWaves.Length; i++)
        {
            freq *= 1.18f;
            amp *= 0.82f;
            sineWaves[i].direction = Random.insideUnitCircle.normalized;
            sineWaves[i].frequency = freq;
            sineWaves[i].amplitude = amp;
            sineWaves[i].phase = waves[0].speed * freq;

            ocean.SetVector("_OceanDirection_" + i, sineWaves[i].direction);
            ocean.SetFloat("_OceanFrequency_" + i, sineWaves[i].frequency);
            ocean.SetFloat("_OceanAmplitude_" + i, sineWaves[i].amplitude);
            ocean.SetFloat("_OceanSpeed_" + i, sineWaves[i].phase);
        }
    }

    private void OnApplicationQuit()
    {
        for (int i = 0; i < sineWaves.Length; i++)
            ocean.SetFloat("_OceanSpeed_" + i, 0);
    }

    public float GetWaterHeight(Vector3 pos)
    {
        float height = 0;

        for(int i = 0; i < 4; i++)
            height += sineWaves[i].amplitude * Mathf.Sin(sineWaves[i].frequency * (sineWaves[i].direction.x * pos.x + sineWaves[i].direction.y * pos.z) + sineWaves[i].phase * Time.time);
        
        return height;
    }
}

[System.Serializable]
public class OceanWave
{
    public Vector2 direction;
    public float waveLength;
    public float amplitude;
    public float speed;
}

public struct SineWave
{
    public Vector2 direction;
    public float frequency;
    public float amplitude;
    public float phase;
}

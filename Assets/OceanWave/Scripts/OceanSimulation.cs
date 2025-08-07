using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OceanSimulation : Singleton<OceanSimulation>
{
    public Material[] oceanMaterials;
    public OceanWave[] waves;

    [SerializeField] private int waveCount = 8;
    [SerializeField] private int simulateCount = 4;

    private SineWave[] sineWaves;
    private ComputeBuffer waveBuffer;

    void Start()
    {
        waveBuffer = new ComputeBuffer(waveCount, 20); //Byte size of Wave Struct

        sineWaves = new SineWave[waveCount];

        //Change Ocean Waves into mathematical waves
        for (int i = 0; i < waves.Length; i++)
        {
            sineWaves[i].direction = waves[i].direction.normalized;
            sineWaves[i].frequency = 2f / waves[i].waveLength;
            sineWaves[i].amplitude = waves[i].amplitude / (1f - .135335f);
            sineWaves[i].phase = waves[i].speed * sineWaves[i].frequency;
        }

        //Generate additional mathematical Waves as noise
        float freq = sineWaves[0].frequency;
        float amp = sineWaves[0].amplitude;
        for (int i = waves.Length; i < waveCount; i++)
        {
            freq *= 1.18f;
            amp *= 0.82f;
            sineWaves[i].direction = Random.insideUnitCircle.normalized;
            sineWaves[i].frequency = freq;
            sineWaves[i].amplitude = amp;
            sineWaves[i].phase = sineWaves[i-1].phase * 1.037f;
        }

        waveBuffer.SetData(sineWaves);

        foreach (var oceanMat in oceanMaterials)
            SetupOceanMaterial(oceanMat);
    }

    public void SetupOceanMaterial(Material oceanMat)
    {
        oceanMat.SetFloat("_WaveCount", waveCount);
        oceanMat.SetBuffer("_Waves", waveBuffer);
    }

    private void OnApplicationQuit()
    {
        foreach (var oceanMat in oceanMaterials)
            for (int i = 0; i < sineWaves.Length; i++)
                oceanMat.SetFloat("_OceanSpeed_" + i, 0);
    }

    public float GetWaterHeight(Vector3 pos)
    {
        float height = 0;

        for (int i = 0; i < simulateCount; i++)
        {
            float sine = Mathf.Sin(sineWaves[i].frequency * (sineWaves[i].direction.x * pos.x + sineWaves[i].direction.y * pos.z) + sineWaves[i].phase * Time.time);
            height += sineWaves[i].amplitude * (Mathf.Exp(sine - 1) - .135335f);
        }
        
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

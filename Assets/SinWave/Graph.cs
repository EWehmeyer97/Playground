using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Graph : MonoBehaviour
{
    [SerializeField] private Transform pointPrefab;
    [SerializeField] [Range(1, 5)] private int iterations = 3;
    [SerializeField] [Range(0f, 1f)] private float speed = 0.2f;

    [Space]

    [SerializeField] [Range(1, 6)] private int range = 1;
    [SerializeField] [Range(0.01f, 3f)] private float mew = 1f;

    private void Update()
    {
        transform.DestroyChildren();

        int resolution = Pow(2, iterations) * range + 1;
        Vector3 step = Vector3.one * (.2f / iterations);
        int total = resolution - 1;
        for(int i = 0; i <= total; i++)
        {
            Transform point = Instantiate(pointPrefab, transform);
            float position = (2f * i / total - 1f) * range;
            point.localPosition = Vector3.right * position + Vector3.up * Mathf.Sin(mew * Mathf.PI * (position + Time.time * speed));
            point.localScale = step;
        }
    }

    private int Pow(int x, int y)
    {
        int result = 1;
        for (int i = 0; i < y; i++)
            result *= x;
        return result;
    }
}

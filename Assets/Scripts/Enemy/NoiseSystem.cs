
using System;
using UnityEngine;

public static class NoiseSystem
{
    public static event Action<Vector3, float> OnNoiseHeard;

    public static void EmitNoise(Vector3 position, float radius)
    {
        OnNoiseHeard?.Invoke(position, radius);
    }
}

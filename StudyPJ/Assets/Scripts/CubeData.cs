using System;
using UnityEngine;

[Serializable]
public class CubeData
{
    public enum CubeType
    {
        Cube,
        Sphere,
        Capsule,
        Cylinder
    }
    public Vector3 Position { get; set; }
    public Color Color { get; set; }
    public Vector3 Scale { get; set; }
    public Vector3 Rotation { get; set; }

    public CubeType Type { get; set; } = CubeType.Cube;

    public CubeData()
    {
        Position = Vector3.zero;
        Scale = Vector3.one;
        Rotation = Vector3.zero;
        Color = Color.white;
    }
    public CubeData(Vector3 position, Vector3 scale, Quaternion rotation)
    {
        Position = position;
        Scale = scale;
        Rotation = new Vector3(rotation.eulerAngles.x, rotation.eulerAngles.y, rotation.eulerAngles.z);
    }

    public CubeData(Vector3 position, Vector3 scale, Quaternion rotation, Color color)
    {
        Position = position;
        Scale = scale;
        Rotation = new Vector3(rotation.eulerAngles.x, rotation.eulerAngles.y, rotation.eulerAngles.z);
        Color = color;
    }
}
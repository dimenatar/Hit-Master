using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Bone
{
    private string _name;
    private Vector3 _position;
    private Quaternion _rotation;

    public Bone(string name, Vector3 position, Quaternion rotation)
    {
        _name = name;
        _position = position;
        _rotation = rotation;
    }

    public string Name { get => _name; }
    public Vector3 Position { get => _position; }
    public Quaternion Rotation { get => _rotation; }
}

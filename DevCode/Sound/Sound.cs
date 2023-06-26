using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Sound
{
    [SerializeField]
    string name;
    [SerializeField]
    AudioClip clip;

    public string Name { get => name; }
    public AudioClip Clip { get => clip; }
  

}

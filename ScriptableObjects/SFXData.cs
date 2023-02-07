using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="Data", menuName = "FXData/SFXData")]
public class SFXData : ScriptableObject
{
    [SerializeField]
    Sound[] knightSFX;
    public Sound[] KnightSFX { get => knightSFX; }

    [SerializeField]
    Sound[] zombieSFX;
    public Sound[] ZombieSFX { get => zombieSFX; }
    

    public Sound[] GetCategorySFXs(string category)
    {
        switch (category)
        {
            case "Knight":
                return knightSFX;
            case "Zombie":
                return zombieSFX;
            default:
                return null;

        }



    }



}



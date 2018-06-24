using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crystal : Collectable
{
    public enum CrystalType
    {
        Green,
        Blue,
        Red
    }

    public CrystalType ct;

    protected override void OnRabbitHit(HeroRabbit rabbit)
    {
        LevelController.current.CollectedCrystal(ct);
        Destroy(gameObject);
    }
}
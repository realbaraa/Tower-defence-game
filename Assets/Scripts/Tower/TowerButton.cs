﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerButton : MonoBehaviour {

    [SerializeField] private GameObject towerObject;
    [SerializeField] private Sprite towerSprite;

    public Sprite TowerSprite {
        get { return towerSprite; }
    }
    
    public GameObject TowerObject
    {
        get { return towerObject; }
    }


}

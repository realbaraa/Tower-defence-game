using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum proType {rock ,arrow,fireball };

public class ProjectTile : MonoBehaviour {

    [SerializeField]
    private int attackStrength;
    [SerializeField] private proType projectTyleType;

    public int AttackStrength {
        get { return attackStrength; }
    }

    public proType ProjectTileType {
        get { return projectTyleType; }
    }
	
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

    [SerializeField] private Transform exitPoint;
    [SerializeField] private float navigationUpdate;
    [SerializeField] private float healthPoint;

    private int target = 0;
    private Transform enemy;
    private Animator anim;
    private float navigationTime = 0;
    private bool isDead = false;
	// Use this for initialization
	void Start () {
        anim = GetComponent<Animator>();
        enemy = GetComponent<Transform>();
        GameManager.Instance.registerEnemy(this);
	}
	
	// Update is called once per frame
	void Update () {
        if (GameManager.Instance.wayPoints != null&&!isDead)
        {
            navigationTime += Time.deltaTime;
            if (navigationTime > navigationUpdate)
            {
                if (target < GameManager.Instance.wayPoints.Length)
                {
                    enemy.position = Vector2.MoveTowards(enemy.position, GameManager.Instance.wayPoints[target].position, navigationTime);
                }
                else {
                    enemy.position = Vector2.MoveTowards(enemy.position, exitPoint.position, navigationTime);
                }
                navigationTime = 0;
            }
        }
		
	}

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Checkpoint")
        {
            target++;
        }
        else if (other.tag == "Finish")
        {
            GameManager.Instance.unregisterEnemy(this);
        }
        else if (other.tag == "Projecttile")
        {
            ProjectTile newP = other.gameObject.GetComponent<ProjectTile>();
            enemyHit(newP.AttackStrength);
            Destroy(other.gameObject);
        }
    }

    public void enemyHit(int hitPoints)
    {
        if (healthPoint - hitPoints > 0)
        {
            healthPoint -= hitPoints;
        }
        else
        {
            anim.Play("Dying");
            Die();
        }
    }

    public void Die()
    {
        isDead = true;
    }

    //void OnDrawGizmos()
    //{
    //    for (int i = 1; i < GameManager.Instance.wayPoints.Length; i++)
    //        Gizmos.DrawLine(GameManager.Instance.wayPoints[i - 1].transform.position, GameManager.Instance.wayPoints[i].transform.position);
    //}
}

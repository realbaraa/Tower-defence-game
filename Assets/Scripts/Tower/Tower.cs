using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Tower : MonoBehaviour {

    [SerializeField]
    private float timeBetweenAttacks;

    [SerializeField]
    private float attackRadius;
    [SerializeField]
    private ProjectTile projectTile;
    private Enemy targetEnemy = null; 

    private float attackCounter=0;
    private bool isAttacking = false;
	void Start () {
		
	}
	
	void Update () {
        attackCounter -= Time.deltaTime;
        if (targetEnemy == null)
        {
            Enemy nearsetEnemy = getNearestEnemyInRange();
            if (nearsetEnemy != null)
            {
                targetEnemy = nearsetEnemy;
                //targetEnemy = GameManager.Instance.EnemyList.OrderBy(n => (n.transform.position - this.transform.position).magnitude).ToList()[0];
            }
        }
        else {
            if (attackCounter <= 0)
            {
                isAttacking = true;
                attackCounter = timeBetweenAttacks;
            }
            else {
                isAttacking = false;
            }
            if (Vector2.Distance(transform.position, targetEnemy.transform.position) > attackRadius)
                targetEnemy = null;
        }
	}

    void FixedUpdate()
    {
        if (isAttacking)
            attack();
    }

    private void attack()
    {
        isAttacking = false;
        ProjectTile newProjectTile = Instantiate(projectTile) as ProjectTile;
        newProjectTile.transform.localPosition = transform.localPosition;

        if (targetEnemy == null)
            Destroy(newProjectTile);
        else StartCoroutine(moveProjectTile(newProjectTile));
    }

    private IEnumerator moveProjectTile(ProjectTile newProjectTile)
    {
        Enemy enemy = targetEnemy;
        while (getTargetDistance(enemy) > 0.2f && newProjectTile != null && enemy != null)
        {
            var dir = enemy.transform.localPosition - transform.localPosition;
            var angleDir = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            newProjectTile.transform.rotation = Quaternion.AngleAxis(angleDir,Vector3.forward);
            newProjectTile.transform.localPosition = Vector2.MoveTowards(newProjectTile.transform.localPosition, 
                enemy.transform.localPosition,5f*Time.deltaTime);
            yield return null;
        }
        if (newProjectTile != null || enemy == null)
            Destroy(newProjectTile);
    }

    private float getTargetDistance(Enemy thisEnemy)
    {
        thisEnemy = getNearestEnemyInRange();
        if (thisEnemy == null)
            return 0f;

        return Mathf.Abs(Vector2.Distance(transform.localPosition, thisEnemy.transform.localPosition)); 
    }

    //private List<Enemy> getEnemiesInRange()
    //{
    //    List<Enemy> enemiesInRange = new List<Enemy>();
    //    foreach (Enemy enemy in GameManager.Instance.EnemyList)
    //    {
    //        if (Vector2.Distance(transform.localPosition, enemy.transform.localPosition) <= attackRadius)
    //        {
    //            enemiesInRange.Add(enemy);
    //        }
    //    }
    //    return enemiesInRange;
    //}


    private Enemy getNearestEnemyInRange()
    {
        //Enemy nearestEnemy=null;
        //float smallesetDis=float.PositiveInfinity;
        //foreach (Enemy enemy in getEnemiesInRange())
        //{
        //    if (Vector2.Distance(transform.localPosition, enemy.transform.localPosition) < smallesetDis)
        //    {
        //        nearestEnemy = enemy;
        //        smallesetDis = Vector2.Distance(transform.localPosition, enemy.transform.localPosition);
        //    }
        //}
        return GameManager.Instance.EnemyList.FindAll(e => Vector3.Distance(e.transform.position, transform.position) < attackRadius).OrderBy(e => Vector3.Distance(e.transform.position, transform.position)).FirstOrDefault();
    }
}

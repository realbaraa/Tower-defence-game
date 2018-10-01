using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TowerManager : Singleton<TowerManager>  {

    private TowerButton twoerBtnPressed;
    private SpriteRenderer spriteRender;

	void Start () {
        spriteRender = GetComponent<SpriteRenderer>();
	}
	
	void Update () {

        if (Input.GetMouseButtonDown(0))
        {
            Vector2 worldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(worldPoint, Vector2.zero);
            if (hit.collider.tag == "Buildsite") {
                hit.collider.tag = "BuildSiteFull";
                placeTower(hit);
            }
        }

        if (spriteRender.enabled)
        {
            followMouse();
        }


	}

    public void placeTower(RaycastHit2D hit)
    {
        if (!EventSystem.current.IsPointerOverGameObject()&&twoerBtnPressed!=null)
        {
           GameObject newTower= Instantiate(twoerBtnPressed.TowerObject);
            newTower.transform.position = hit.transform.position;
            disableSprite();
        }
    }

    public void followMouse()
    {
        transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.position = new Vector2(transform.position.x, transform.position.y);
    }

    public void SelectedTower(TowerButton twoerSelected)
    {
        twoerBtnPressed = twoerSelected;
        enableSprite(twoerBtnPressed.TowerSprite);
    }

    public void enableSprite(Sprite sprite)
    {
        spriteRender.enabled = true;
        spriteRender.sprite = sprite;
    }

    public void disableSprite()
    {
        spriteRender.enabled = false;
    }

   

  

    

}

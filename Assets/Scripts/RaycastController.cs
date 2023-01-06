using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaycastController : MonoBehaviour
{
    Camera cam;
    public GameController gameController;
    public DeckController deckController;

    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;
        gameController = GameObject.Find("GameController").GetComponent<GameController>();
        deckController = GameObject.Find("Deck").GetComponent<DeckController>();
    }

    void Update()
    {
        // Draw Ray
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = 100f;
        mousePos = cam.ScreenToWorldPoint(mousePos);
        Debug.DrawRay(transform.position, mousePos - transform.position, Color.blue);
        

        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 2000))
            {
                if (!hit.collider.gameObject.GetComponent<CardController>().isClicked && deckController.isClickable)
                {
                    //hit.collider.gameObject.GetComponent<CardController>().isClicked = true;
                    hit.collider.gameObject.GetComponent<CardController>().cardClicked();
                    gameController.clickCount = gameController.clickCount + 1;
                    Debug.Log(hit.collider.gameObject.name);
                    Debug.Log(gameController.clickCount);
                    if (gameController.clickCount == 1)
                    {
                        gameController.card1_name = hit.collider.gameObject.name;
                        Debug.Log(hit.collider.gameObject.name);
                    }
                    else if (gameController.clickCount == 2)
                    {
                        gameController.card2_name = hit.collider.gameObject.name;
                    }
                    Debug.Log(hit.transform.name);
                }
            }
        }
    }
}

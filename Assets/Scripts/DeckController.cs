using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeckController : MonoBehaviour
{
    public bool isClickable;

    public DeckController deckController;

    // Start is called before the first frame update
    void Start()
    {
        deckController = GameObject.Find("Deck").GetComponent<DeckController>();
        isClickable = false;

        deckRandomizer();
    }

    void deckRandomizer()
    {
        Transform[] cards = new Transform[deckController.transform.childCount];

        for (int i = 0; i < deckController.transform.childCount; i++)
            cards[i] = deckController.transform.GetChild(i);

        var cardArray = cards;
        ShuffleArray(cardArray);

        float x = -11;
        float z = 7.5f;
        int positionCount = 0;

        foreach (var card in cardArray)
        {
            card.transform.position = new Vector3(x, 10, z);
            x += 11;
            positionCount++;

            if (positionCount == 3)
            {
                x = -11;
                z += -12.5f;
                positionCount = 0;
            }

        }

    }

    void ShuffleArray<T>(T[] array)
    {
        int n = array.Length;
        for (int i = 0; i < n; i++)
        {
            // Pick a new index higher than current for each item in the array
            int r = i + Random.Range(0, n - i);

            // Swap item into new spot
            T t = array[r];
            array[r] = array[i];
            array[i] = t;
        }
    }

}

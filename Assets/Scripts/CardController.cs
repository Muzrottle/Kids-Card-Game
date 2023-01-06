using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardController : MonoBehaviour
{
    public bool isClicked;
    public AudioSource cardFlip;
    //public bool flipAudioEnabled = false;

    // Start is called before the first frame update
    void Start()
    {
        isClicked = true;
    }

    public void cardClicked()
    {
        isClicked = true;
        cardFlip.Play();
    }

    // Update is called once per frame
    void Update()
    {
        float speed = 10F;

        if (isClicked)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0, 0, 180), Time.deltaTime * speed);
        }
        else
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0, 0, 0), Time.deltaTime * speed);
        }
    }
}

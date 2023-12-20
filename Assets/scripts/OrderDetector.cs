using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class DressupScene : MonoBehaviour
{
    // Assuming you have a list of GameObjects representing the pieces of the suit
    public List<GameObject> suitPieces;

    public AudioClip mySound;
    AudioSource audio;

    // Variables to store the correct order and user's order
    private List<GameObject> correctOrder = new List<GameObject>();
    private List<GameObject> userOrder = new List<GameObject>();


    // Start is called before the first frame update
    void Start()
    {
        // Populate the correct order initially
        correctOrder.AddRange(suitPieces);
        audio = GetComponent<AudioSource>();
        audio.loop = false;
    }

    // Update is called once per frame
    void Update()
    {
        CheckOrder();
    }

    private void CheckOrder()
    {

        for (int i = 0; i < userOrder.Count; i++)
        {
            if (userOrder[i] != correctOrder[i])
            {
                audio.PlayOneShot(mySound);
                return;
            }
            else
            {
                foreach (var piece in userOrder)
                {
                    piece.SetActive(false);
                }
            }
        }
    }
}

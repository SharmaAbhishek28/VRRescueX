using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grow : MonoBehaviour
{
    public float timer = 0f;
    public float growTime = 5f;
    public float maxSize = 2f;
    public bool ismaxSize = false;
    public float delayTime = 25f;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(StartGrowingAfterDelay());
    }

    private IEnumerator StartGrowingAfterDelay()
    {
        yield return new WaitForSeconds(delayTime); // Delay for 25 seconds

        if (!ismaxSize)
        {
            StartCoroutine(Groww());
        }
    }

    private IEnumerator Groww()
    {
        Vector3 startScale = transform.localScale;
        Vector3 maxScale = new Vector3(maxSize, maxSize, maxSize);

        do
        {
            transform.localScale = Vector3.Lerp(startScale, maxScale, timer / growTime);
            timer += Time.deltaTime;
            yield return null;
        }
        while (timer < growTime);

        ismaxSize = true;
    }

    // Update is called once per frame
    void Update()
    {
        // Additional update logic if needed
    }
}
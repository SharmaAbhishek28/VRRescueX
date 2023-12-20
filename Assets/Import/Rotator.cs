using UnityEngine;

public class Rotate : MonoBehaviour
{
    private bool isRotating = false;
    public float speed=50f;

    // Update is called once per frame
    void Update()
    {
        // Rotate the object continuously if isRotating is true
        if (isRotating)
        {
            RotateObject();
        }
    }

    // Function to start rotating the object
    public void StartRotation()
    {
        isRotating = true;
    }

    // Function to stop rotating the object
    public void StopRotation()
    {
        isRotating = false;
    }

    // Function to rotate the object at the Y-axis
    private void RotateObject()
    {
        transform.Rotate(0f,0f, Time.deltaTime * speed); // Adjust the rotation speed as needed
    }
}
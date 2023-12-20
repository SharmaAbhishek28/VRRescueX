using System.Collections.Generic;
using UnityEngine;

public class Dressupcontroller : MonoBehaviour
{
    // Assuming you have a list of GameObjects representing the pieces of the suit
    public List<GameObject> suitPieces;

    // Check the order when needed, e.g., when the user clicks a "Check Order" button
    public void CheckSuitOrder()
    {
        // Capture the user's order dynamically (assuming userOrder is a List<GameObject>)
        List<GameObject> userOrder = GetUserOrder();

        if (CheckOrder(userOrder))
        {
            // Order is correct, disable/destroy the objects
            foreach (var piece in userOrder)
            {
                piece.SetActive(false); // Use Destroy(piece) if you want to destroy the objects
            }
        }
        else
        {
            // Order is wrong, show alert
            ShowAlert("Incorrect order! Please try again.");
        }
    }

    // Method to check if the user's order matches the correct order
    private bool CheckOrder(List<GameObject> userOrder)
    {
        if (userOrder.Count != suitPieces.Count)
        {
            return false; // Order length doesn't match
        }

        for (int i = 0; i < userOrder.Count; i++)
        {
            if (userOrder[i] != suitPieces[i])
            {
                return false; // Piece at position i is incorrect
            }
        }

        return true; // User's order matches the correct order
    }

    // Method to capture the user's order dynamically (you need to implement this based on your UI)
    private List<GameObject> GetUserOrder()
    {
        // Implement code to capture the user's selected order dynamically
        // For example, you might have a UI where the user clicks on the suit pieces
        // and adds them to a list representing the order
        List<GameObject> userOrder = new List<GameObject>();
        // Add your logic to populate userOrder based on user interaction
        return userOrder;
    }

    // Method to show alert (replace with your actual UI alert implementation)
    private void ShowAlert(string message)
    {
        Debug.Log(message); // For simplicity, just log the message to the console
    }
}
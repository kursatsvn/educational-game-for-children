using UnityEngine;

public enum FoodType
{
    Correct,
    Incorrect
}

public class Food : MonoBehaviour
{
    public FoodType foodType; // Besinin t�r� (do�ru ya da yanl��)
}

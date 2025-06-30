using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class UIController : MonoBehaviour
{
    public Button upButton, downButton, leftButton, rightButton; // Y�n tu�lar�na ba�lanacak butonlar
    private PlayerController playerController;

    void Start()
    {
        // PlayerController bile�enini buluyoruz
        playerController = FindObjectOfType<PlayerController>();

        // EventTrigger ekliyoruz
        AddButtonEventTriggers();
    }

    // Butonlara EventTrigger ekliyoruz
    void AddButtonEventTriggers()
    {
        AddEventTrigger(upButton, MoveUp, StopMovement);
        AddEventTrigger(downButton, MoveDown, StopMovement);
        AddEventTrigger(leftButton, MoveLeft, StopMovement);
        AddEventTrigger(rightButton, MoveRight, StopMovement);
    }

    // EventTrigger eklemek i�in yard�mc� fonksiyon
    void AddEventTrigger(Button button, System.Action onPointerDown, System.Action onPointerUp)
    {
        EventTrigger eventTrigger = button.gameObject.AddComponent<EventTrigger>();

        EventTrigger.Entry pointerDownEntry = new EventTrigger.Entry();
        pointerDownEntry.eventID = EventTriggerType.PointerDown;
        pointerDownEntry.callback.AddListener((eventData) => onPointerDown.Invoke());
        eventTrigger.triggers.Add(pointerDownEntry);

        EventTrigger.Entry pointerUpEntry = new EventTrigger.Entry();
        pointerUpEntry.eventID = EventTriggerType.PointerUp;
        pointerUpEntry.callback.AddListener((eventData) => onPointerUp.Invoke());
        eventTrigger.triggers.Add(pointerUpEntry);
    }

    // Bas�ld���nda hareket ba�latma
    void MoveUp()
    {
        playerController.SetMovementDirection(Vector2.up);
    }

    void MoveDown()
    {
        playerController.SetMovementDirection(Vector2.down);
    }

    void MoveLeft()
    {
        playerController.SetMovementDirection(Vector2.left);
    }

    void MoveRight()
    {
        playerController.SetMovementDirection(Vector2.right);
    }

    // Bas�lmay� b�rak�nca hareketi durdurma
    void StopMovement()
    {
        playerController.SetMovementDirection(Vector2.zero);
    }
}

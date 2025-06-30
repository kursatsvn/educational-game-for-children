using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class UIController : MonoBehaviour
{
    public Button upButton, downButton, leftButton, rightButton; // Yön tuþlarýna baðlanacak butonlar
    private PlayerController playerController;

    void Start()
    {
        // PlayerController bileþenini buluyoruz
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

    // EventTrigger eklemek için yardýmcý fonksiyon
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

    // Basýldýðýnda hareket baþlatma
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

    // Basýlmayý býrakýnca hareketi durdurma
    void StopMovement()
    {
        playerController.SetMovementDirection(Vector2.zero);
    }
}

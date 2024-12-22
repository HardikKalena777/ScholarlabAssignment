using UnityEngine;

public class Draggable : MonoBehaviour
{
    Vector3 inputPositionOffset;
    public bool isDragging;
    public bool chemicalAdded;
    public bool resultAchieved;
    Vector3 velocity;
    Vector3 lastPosition;
    public float shakeThreshold;
    public float colorChangeSpeed;
    public Color targetColor;
    public Material conicalFlaskMaterial;

    private void Update()
    {
        if (isDragging)
        {
            // Calculate velocity
            velocity = (transform.position - lastPosition) / Time.deltaTime;

            // Check for shaking and change color
            if (velocity.magnitude > shakeThreshold && chemicalAdded)
            {
                Debug.Log("Shaking");
                Color currentColor = conicalFlaskMaterial.GetColor("_Tint");
                Color newColor = Color.Lerp(currentColor, targetColor, Time.deltaTime * colorChangeSpeed);
                conicalFlaskMaterial.SetColor("_Tint", newColor);
                resultAchieved = true;
            }

            lastPosition = transform.position;
        }

        // Stop dragging on touch release or mouse button release
        if (Input.GetMouseButtonUp(0) || (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended))
        {
            isDragging = false;
        }
    }

    private Vector3 GetInputWorldPosition()
    {
        Vector3 inputScreenPosition;

        if (Input.touchCount > 0) // Touch input
        {
            inputScreenPosition = Input.GetTouch(0).position;
        }
        else // Mouse input
        {
            inputScreenPosition = Input.mousePosition;
        }

        inputScreenPosition.z = Camera.main.WorldToScreenPoint(transform.position).z; // Maintain object's Z position
        return Camera.main.ScreenToWorldPoint(inputScreenPosition);
    }

    private void OnMouseDown()
    {
        if (Input.touchCount == 0) // Only handle mouse when there are no touches
        {
            inputPositionOffset = gameObject.transform.position - GetInputWorldPosition();
        }
    }

    private void OnMouseDrag()
    {
        if (Input.touchCount == 0) // Only handle mouse when there are no touches
        {
            transform.position = GetInputWorldPosition() + inputPositionOffset;
            isDragging = true;
        }
    }

    private void OnMouseUp()
    {
        isDragging = false;
    }

    private void OnTouchBegin()
    {
        if (Input.touchCount > 0)
        {
            inputPositionOffset = gameObject.transform.position - GetInputWorldPosition();
            isDragging = true;
        }
    }

    private void OnTouchMove()
    {
        if (Input.touchCount > 0)
        {
            transform.position = GetInputWorldPosition() + inputPositionOffset;
            isDragging = true;
        }
    }

    private void OnTouchEnd()
    {
        isDragging = false;
    }
}

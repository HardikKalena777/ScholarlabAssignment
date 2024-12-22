using UnityEngine;

public class Draggable : MonoBehaviour
{
    private Vector3 inputPositionOffset;
    public bool isDragging;
    public bool chemicalAdded;
    public bool resultAchieved;
    private Vector3 velocity;
    private Vector3 lastPosition;
    public float shakeThreshold;
    public float colorChangeSpeed;
    public Color targetColor;
    public Material conicalFlaskMaterial;

    private static Draggable currentDraggedObject; // Tracks the currently dragged object

    private void Update()
    {
        // Handle touch or mouse input
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            Vector3 touchPosition = GetWorldPosition(touch.position);

            switch (touch.phase)
            {
                case TouchPhase.Began:
                    if (IsTouchOverGameObject(touchPosition))
                    {
                        OnTouchBegin(touchPosition);
                    }
                    break;

                case TouchPhase.Moved:
                    if (currentDraggedObject == this)
                    {
                        OnTouchMove(touchPosition);
                    }
                    break;

                case TouchPhase.Ended:
                case TouchPhase.Canceled:
                    if (currentDraggedObject == this)
                    {
                        OnTouchEnd();
                    }
                    break;
            }
        }
        else if (Input.GetMouseButton(0)) // Handle mouse input for testing in the editor
        {
            Vector3 mousePosition = GetWorldPosition(Input.mousePosition);

            if (Input.GetMouseButtonDown(0))
            {
                if (IsTouchOverGameObject(mousePosition))
                {
                    OnTouchBegin(mousePosition);
                }
            }
            else if (Input.GetMouseButton(0) && currentDraggedObject == this)
            {
                OnTouchMove(mousePosition);
            }
            else if (Input.GetMouseButtonUp(0) && currentDraggedObject == this)
            {
                OnTouchEnd();
            }
        }

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
    }

    private void OnTouchBegin(Vector3 inputPosition)
    {
        if (currentDraggedObject == null) // Only allow one object to be dragged
        {
            inputPositionOffset = transform.position - inputPosition;
            isDragging = true;
            currentDraggedObject = this; // Set this object as the currently dragged object
        }
    }

    private void OnTouchMove(Vector3 inputPosition)
    {
        if (isDragging)
        {
            transform.position = inputPosition + inputPositionOffset;
        }
    }

    private void OnTouchEnd()
    {
        if (isDragging)
        {
            isDragging = false;
            currentDraggedObject = null; // Clear the current dragged object
        }
    }

    private Vector3 GetWorldPosition(Vector3 screenPosition)
    {
        screenPosition.z = Camera.main.WorldToScreenPoint(transform.position).z; // Maintain Z position
        return Camera.main.ScreenToWorldPoint(screenPosition);
    }

    private bool IsTouchOverGameObject(Vector3 worldPosition)
    {
        Ray ray = Camera.main.ScreenPointToRay(Camera.main.WorldToScreenPoint(worldPosition));
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            return hit.collider != null && hit.collider.gameObject == gameObject;
        }
        return false;
    }
}

using System.Collections;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using TMPro; // For working with TextMeshPro

public class HumanPanel : MonoBehaviour
{
    public Vector2 targetPosition;
    public Vector2 startPosition;
    public float moveDuration = 0.5f;

    public RectTransform rectTransform;
    private Tween moveTween;

    public Human assignedHuman; // Reference to the assigned human object

    // Colors for assigned and not assigned states
    public Color colorHumanAssignedText;
    public Color colorHumanAssignedPanel;
    public Color colorHumanNotAssignedText;
    public Color colorHumanNotAssignedPanel;

    public TextMeshProUGUI humanNameText; // Reference to the human name text
    private Image panelBackground; // Background of the panel, now accessed via GetComponent

    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        startPosition = rectTransform.anchoredPosition;

        // Get the Image component of the background panel
        panelBackground = GetComponent<Image>();

        // Initialize panel colors at the start
        UpdatePanel();
    }

    // Moves the panel to the target position with animation
    public void MoveToTarget(System.Action onComplete)
    {
        if (moveTween != null && moveTween.IsActive())
        {
            moveTween.Kill();
        }

        moveTween = rectTransform.DOAnchorPos(targetPosition, moveDuration)
                                 .SetEase(Ease.InOutQuad)
                                 .OnComplete(() => onComplete?.Invoke());
    }

    // Moves the panel back to the start position
    public void MoveToStart()
    {
        if (moveTween != null && moveTween.IsActive())
        {
            moveTween.Kill();
        }

        moveTween = rectTransform.DOAnchorPos(startPosition, moveDuration)
                                 .SetEase(Ease.InOutQuad);
    }

    public void UpdatePanel()
    {
        if (assignedHuman != null)
        {
            humanNameText.text = assignedHuman.humanName; 
        }
        else
        {
            humanNameText.text = "No Human Assigned";
        }

        UpdatePanelColors();
    }

    // Updates the panel colors based on whether a human is assigned or not
    public void UpdatePanelColors()
    {
        if (assignedHuman != null)
        {
            humanNameText.color = colorHumanAssignedText; // Change text color for assigned human
            panelBackground.color = colorHumanAssignedPanel; // Change panel background color for assigned human
        }
        else
        {
            humanNameText.color = colorHumanNotAssignedText; // Change text color for not assigned human
            panelBackground.color = colorHumanNotAssignedPanel; // Change panel background color for not assigned human
        }
    }

    // Assigns a human and updates the panel accordingly
    public void AssignHuman(Human human)
    {
        assignedHuman = human;
        UpdatePanel();
    }

    // Unassigns the current human and updates the panel
    public void UnassignHuman()
    {
        assignedHuman = null;
        UpdatePanel();
    }
}

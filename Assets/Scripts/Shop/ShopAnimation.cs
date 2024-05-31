using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class ShopAnimation : MonoBehaviour
{
    public RectTransform panel; 
    public Button[] buttons; 
    public float transitionDuration = 0.5f;
    public float buttonStayDuration = 5f;
    private Vector3 offPanelPositionLeft; 
    private Vector3 offPanelPositionRight; 

    // Start is called before the first frame update
    void Start()
    {
        float panelWidth = panel.rect.width;
        offPanelPositionRight = new Vector3(panelWidth, 0, 0); // Off-panel right position

        // Initialize all buttons to off-panel right position
        foreach (Button button in buttons)
        {
            button.transform.localPosition += offPanelPositionRight;
        }

        StartCoroutine(SlideButtons());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private IEnumerator SlideButtons()
    {
        int index = 0;
        while (true)
        {
            yield return StartCoroutine(SlideIn(buttons[index]));
            yield return new WaitForSeconds(buttonStayDuration); 
            yield return StartCoroutine(SlideOut(buttons[index]));

            // Move to the next button, looping back to the first
            index = (index + 1) % buttons.Length;
        }
    }

    private IEnumerator SlideIn(Button button)
    {
        button.interactable = false; // Disable button during animation
        button.transform.localPosition = new Vector3(5000, button.transform.localPosition.y, button.transform.localPosition.z);
        
        yield return button.transform.DOLocalMoveX(0, transitionDuration).SetEase(Ease.OutCirc).WaitForCompletion();
        button.interactable = true; // Enable button after sliding in
    }

    private IEnumerator SlideOut(Button button)
    {
        button.interactable = false; // Disable button
        yield return button.transform.DOLocalMoveX(-5000, transitionDuration).SetEase(Ease.InCirc).WaitForCompletion();
    }

}

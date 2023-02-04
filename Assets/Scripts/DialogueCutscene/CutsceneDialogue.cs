using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.InputSystem;

public class CutsceneDialogue : MonoBehaviour
{
    public TextMeshProUGUI textComponent;
    public string[] boxes;
    public float textSpeed;
    private int index;

    public Cutscenes controls;

    private void Awake()
    {
        controls = new Cutscenes();
        controls.Cutscene.ClickThroughText.performed += _ => ClickThroughText();
    }

    private void OnEnable()
    {
        controls.Cutscene.ClickThroughText.Enable();
    }

    private void OnDisable()
    {
        controls.Cutscene.ClickThroughText.Disable();
    }

    void ClickThroughText()
    {
        if (textComponent.text == boxes[index])
            {
                // start typing next line
                NextLine();
            }
        else
        {
            // instantly fill out line
            StopAllCoroutines();
            textComponent.text = boxes[index];
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        textComponent.text = string.Empty;
        StartDialogue();
    }

    void StartDialogue()
    {
        index = 0;
        StartCoroutine(TypeLine());
    }

    IEnumerator TypeLine()
    {
        // type out each character 1 by 1
        foreach (char c in boxes[index].ToCharArray())
        {
            textComponent.text += c;
            yield return new WaitForSeconds(textSpeed);
        }
    }

    void NextLine()
    {
        if (index < boxes.Length - 1)
        {
            index++;
            textComponent.text = string.Empty;
            StartCoroutine(TypeLine());
        }
        else
        {
            gameObject.SetActive(false);
        }
    }
}

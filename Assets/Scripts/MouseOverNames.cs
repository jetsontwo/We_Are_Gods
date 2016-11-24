using UnityEngine;
using UnityEngine.UI;

public class MouseOverNames : MonoBehaviour
{
    private static Transform mouseOver;
    private static Text nameText;

    void OnMouseEnter()
    {
        if (mouseOver == null)
        {
            GameObject tempCanvas = (GameObject)Instantiate(Resources.Load("Mouse Over Canvas"));
            mouseOver = tempCanvas.transform.FindChild("Mouse Over");
            nameText = mouseOver.GetComponentInChildren<Text>();
        }
        else
        {
            mouseOver.gameObject.SetActive(true);
        }

        mouseOver.position = Input.mousePosition;
        nameText.text = gameObject.name;
    }

    void OnMouseOver()
    {
        mouseOver.position = Input.mousePosition;
    }

    void OnMouseExit()
    {
        mouseOver.gameObject.SetActive(false);
    }
}

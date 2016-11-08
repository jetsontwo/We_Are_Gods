using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Text_Manager : MonoBehaviour {

    public string[] dialogue_array;
    public Text dialogue_box;
    public float text_speed, time_between_text;
    public GameObject text_background;
    private RawImage background_image;

	
	// Update is called once per frame
	void Start () {
        background_image = text_background.GetComponent<RawImage>();
        StartCoroutine(Show_Text(dialogue_array));
	}

    IEnumerator Show_Text(string[] text)
    {
        background_image.enabled = true;
        for(int i = 0; i < text.Length; i++)
        {
            string str = "";
            for (int j = 0; j < text[i].Length; j++)
            {
                str += text[i][j];
                dialogue_box.text = str;
                yield return new WaitForSeconds(text_speed);
            }
            yield return new WaitForSeconds(time_between_text);
        }
        dialogue_box.text = "";
        background_image.enabled = false;

    }
}
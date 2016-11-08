using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Text_Manager : MonoBehaviour {

    public string[] dialogue_array;
    public Text dialogue_box;
    public float text_speed, time_between_text, beginning_wait;
    public GameObject text_background;
    public AudioSource blip;
    private RawImage background_image;

	
	// Update is called once per frame
	void Start () {
        background_image = text_background.GetComponent<RawImage>();
        background_image.enabled = false;
        StartCoroutine(Show_Text(dialogue_array));
	}

    IEnumerator Show_Text(string[] text)
    {
        yield return new WaitForSeconds(beginning_wait);
        background_image.enabled = true;
        for(int i = 0; i < text.Length; i++)
        {
            string str = "";
            for (int j = 0; j < text[i].Length; j++)
            {
                str += text[i][j];
                dialogue_box.text = str;
                blip.Play();
                blip.pitch = Random.Range(0.5f, 0.6f);
                yield return new WaitForSeconds(text_speed);
            }
            yield return new WaitForSeconds(time_between_text);
        }
        dialogue_box.text = "";
        background_image.enabled = false;

    }
}
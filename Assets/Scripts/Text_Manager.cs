using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;
using System.Collections;

public class Text_Manager : MonoBehaviour {

    public DialogueSegment[] dialogue_array;
    public Text dialogue_box;
    public float text_speed, time_between_text, beginning_wait;
    public Font miltiadesFont, hrodebertFont;
    public GameObject text_background, exit_level, cur_touched_object, transferred_component;
    public AudioSource blip;
    private RawImage background_image;
    public string command;
    private bool getToEndOfText = false;

    
    void Awake()
    {
        background_image = text_background.GetComponent<RawImage>();
        background_image.enabled = false;

        if (SceneManager.GetActiveScene().buildIndex == 1)
            StartCoroutine(First_Level(dialogue_array));
        else if (SceneManager.GetActiveScene().buildIndex == 2)
            StartCoroutine(Second_Level(dialogue_array));
    }

    IEnumerator First_Level(DialogueSegment[] text)
    {
        yield return new WaitForSeconds(beginning_wait);
        background_image.enabled = true;

        for(int i = 0; i < text.Length; i++)
        {
            if(text[i].character == DialogueSegment.CharacterList.MILTIADES)
            {
                dialogue_box.font = miltiadesFont;
            }
            else if (text[i].character == DialogueSegment.CharacterList.HRODEBERT)
            {
                dialogue_box.font = hrodebertFont;
            }

            string str = "";
            for (int j = 0; j < text[i].dialogueText.Length; j++)
            {
                if(getToEndOfText)
                {
                    dialogue_box.text = text[i].dialogueText;
                    getToEndOfText = false;
                    break;
                }
                str += text[i].dialogueText[j];
                dialogue_box.text = str;

                blip.pitch = UnityEngine.Random.Range(1.1f, 1.15f);
                blip.Play();

                yield return new WaitForSeconds(text_speed);
            }
            yield return new WaitForSeconds(time_between_text);
        }

        dialogue_box.text = "";
        background_image.enabled = false;

        Instantiate(exit_level, new Vector3(0, 0, 0), Quaternion.identity);
    }


    bool level1_test_function()
    {
        return cur_touched_object.name.Equals("Tree") || cur_touched_object.name.Equals("TurtleCoon");
    }
    bool level1_test_function2()
    {
        return cur_touched_object.name.Equals("Tree") && transferred_component.name.Equals("Run_Right");
    }

    IEnumerator Second_Level(DialogueSegment[] text)
    {
        yield return new WaitForSeconds(beginning_wait);
        background_image.enabled = true;

        string str = "";
        for (int j = 0; j < text[0].dialogueText.Length; j++)
        {
            if (getToEndOfText)
            {
                dialogue_box.text = text[0].dialogueText;
                getToEndOfText = false;
                break;
            }
            str += text[0].dialogueText[j];
            dialogue_box.text = str;
            blip.Play();
            blip.pitch = UnityEngine.Random.Range(0.6f, 0.65f);
            yield return new WaitForSeconds(text_speed);
        }
        yield return new WaitForSeconds(time_between_text);
        dialogue_box.text = "";
        yield return new WaitForSeconds(5);
        str = "";
        for (int j = 0; j < text[1].dialogueText.Length; j++)
        {
            if (getToEndOfText)
            {
                dialogue_box.text = text[1].dialogueText;
                getToEndOfText = false;
                break;
            }
            str += text[1].dialogueText[j];
            dialogue_box.text = str;
            blip.Play();
            blip.pitch = UnityEngine.Random.Range(0.6f, 0.65f);
            yield return new WaitForSeconds(text_speed);
        }
        dialogue_box.text = "";
        yield return new WaitUntil(level1_test_function);

        if (cur_touched_object.name.Equals("TurtleCoon"))
        {
            str = "";
            for (int j = 0; j < text[4].dialogueText.Length; j++)
            {
                if (getToEndOfText)
                {
                    dialogue_box.text = text[1].dialogueText;
                    getToEndOfText = false;
                    break;
                }
                str += text[1].dialogueText[j];
                dialogue_box.text = str;
                blip.Play();
                blip.pitch = UnityEngine.Random.Range(0.6f, 0.65f);
                yield return new WaitForSeconds(text_speed);
            }
            str = "";
            for (int j = 0; j < text[5].dialogueText.Length; j++)
            {
                if (getToEndOfText)
                {
                    dialogue_box.text = text[1].dialogueText;
                    getToEndOfText = false;
                    break;
                }
                str += text[1].dialogueText[j];
                dialogue_box.text = str;
                blip.Play();
                blip.pitch = UnityEngine.Random.Range(0.6f, 0.65f);
                yield return new WaitForSeconds(text_speed);
            }
        }
        else if (cur_touched_object.name.Equals("Tree"))
        {
            str = "";
            for (int j = 0; j < text[1].dialogueText.Length; j++)
            {
                if (getToEndOfText)
                {
                    dialogue_box.text = text[1].dialogueText;
                    getToEndOfText = false;
                    break;
                }
                str += text[1].dialogueText[j];
                dialogue_box.text = str;
                blip.Play();
                blip.pitch = UnityEngine.Random.Range(0.6f, 0.65f);
                yield return new WaitForSeconds(text_speed);
            }
        }

        dialogue_box.text = "";
        yield return new WaitUntil(level1_test_function2);

        str = "";
        for (int j = 0; j < text[1].dialogueText.Length; j++)
        {
            if (getToEndOfText)
            {
                dialogue_box.text = text[1].dialogueText;
                getToEndOfText = false;
                break;
            }
            str += text[1].dialogueText[j];
            dialogue_box.text = str;
            blip.Play();
            blip.pitch = UnityEngine.Random.Range(0.6f, 0.65f);
            yield return new WaitForSeconds(text_speed);
        }
        yield return new WaitForSeconds(time_between_text);

        dialogue_box.text = "";
        background_image.enabled = false;

        //play 4 and 5 together for turtle
    }


    void Update()
    {
        //getToEndOfText = false;
        if(Input.GetKeyDown(KeyCode.Return))
        {
            getToEndOfText = true;
        }
    }
}

[Serializable]
public class DialogueSegment
{
    public enum CharacterList { HRODEBERT, MILTIADES }
    public CharacterList character;

    //public bool requireAction;    //Maybe add later
    public string dialogueText;
}
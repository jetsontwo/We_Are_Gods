using UnityEngine;

public class SongManager : MonoBehaviour
{
    static SongManager songObject;
	
	void Awake ()
    {
        if (songObject == null)
        {
            songObject = this;
            DontDestroyOnLoad(this);
        }
        else
        {
            Destroy(this);
        }
	}
}

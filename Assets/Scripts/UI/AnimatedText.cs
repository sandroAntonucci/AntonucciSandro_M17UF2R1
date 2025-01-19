using System.Collections;
using TMPro;
using UnityEngine;

public class TextAnimation : MonoBehaviour
{
    public TextMeshProUGUI myText;
    public string message;
    public float delay = 0.02f;

    public IEnumerator TypeText(string message)
    {
        myText.text = "";
        foreach (char letter in message.ToCharArray())
        {
            myText.text += letter;
            yield return new WaitForSeconds(delay);
        }
    }
}

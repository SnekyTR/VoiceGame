using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Cinematic_Text : MonoBehaviour
{
    public TextMeshProUGUI textMeshProText;
    private string contentText;
    private float letterPause = 0.3f;
    public void StartText()
    {
        print("Printea");
        contentText = "Este es el reino de Laryan, la tierra de los cuatro ducados. Al norte está el ducado de Krenid, " +
            "hogar de la orden de los hijos del gigante, protectores del reino contra las tierras oscuras. ";
        string writeThis = contentText;
        StartCoroutine(TypeSentence(writeThis));
    }
    IEnumerator TypeSentence(string sentence)
    {
        foreach (char letter in sentence.ToCharArray())
        {
            textMeshProText.text += letter;
            yield return new WaitForSeconds(letterPause);
        }
    }
}

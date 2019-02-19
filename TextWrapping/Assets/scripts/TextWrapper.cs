using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
public class TextWrapper : MonoBehaviour
{
    public TextMesh textMesh;

    public int maxLineChars = 20; // max characters allowed per line
    private int lastMaxLineChars = 20;
    string wrappedText = "";
    public bool disableWhilePlaying = true;



    void Update()
    {
        if (textMesh.text != wrappedText || maxLineChars != lastMaxLineChars)
        {
            int charCount = 0;
            wrappedText = "";
            string line = "";

            char[] separators = new char[] { ' ', '\n', '\t' }; //using escape sequence to create new line and apply space
            string[] words = textMesh.text.Split(separators);

            for (int i = 0; i < words.Length; i++)
            {
                string word = words[i].Trim();

                if (i == 0)
                {
                    line = word;
                    charCount = word.Length;
                }
                else
                {
                    if ((charCount + (charCount > 0 ? 1 : 0) + word.Length) <= maxLineChars)
                    {
                        if (charCount > 0)
                        {
                            line += ' ';
                            charCount += 1;
                        }

                        line += word;
                        charCount += word.Length;
                    }
                    else
                    {
                        if (wrappedText.Length > 0)
                            wrappedText += '\n';

                        wrappedText += line;

                        line = word;
                        charCount = word.Length;
                    }
                }
            }

            if (charCount > 0)
            {
                if (wrappedText.Length > 0)
                    wrappedText += '\n';

                wrappedText += line;
            }

            textMesh.text = wrappedText;
            lastMaxLineChars = maxLineChars;
        }
    }
}
using TMPro;
using System.Collections;
using UnityEngine;

public class DialougeSystemScript : MonoBehaviour
{

    [System.Serializable]
    public class DialougeSegment
    {
        public string NameText;

        [TextArea]
        public string DialougeToType;
        public bool Skippable;

        [Range(1f, 25f)]
        public float TextSpeed;
    }

    [SerializeField] private DialougeSegment[] segments;
    [Space]
    [SerializeField] private TMP_Text NameText;
    [SerializeField] private TMP_Text DialougeText;

    private int DialougeIndex;
    private bool PlayingDialouge;
    private bool Skip;

    void Start()
    {
        StartCoroutine(PlayDialouge(segments[DialougeIndex]));
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            if (DialougeIndex == segments.Length)
            {
                enabled = false;
                return;
            }
            if (!PlayingDialouge)
            {
                StartCoroutine(PlayDialouge(segments[DialougeIndex]));
            }
            else
            {
                if (segments[DialougeIndex].Skippable)
                {
                    Skip = true;
                }
            }
        }
    }

    private IEnumerator PlayDialouge(DialougeSegment segment)
    {
        PlayingDialouge = true;

        DialougeText.SetText(string.Empty);
        NameText.SetText(segment.NameText);

        float delay = 1f / segment.TextSpeed;

        for (int i = 0; i < segment.DialougeToType.Length; i++)
        {
            if (Skip)
            {
                DialougeText.SetText(segment.DialougeToType);
                Skip = false;
                break;
            }

            string AddChunk = string.Empty;
            AddChunk += segment.DialougeToType[i];

            if(segment.DialougeToType[i] == ' ' && i < segment.DialougeToType.Length - 1)
            {
                AddChunk = segment.DialougeToType.Substring(i, 2);
                i++;
            }

            DialougeText.text += AddChunk;
            yield return new WaitForSeconds(delay);
        }

        PlayingDialouge = false;
        DialougeIndex++;
    }

}

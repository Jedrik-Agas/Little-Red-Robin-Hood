using TMPro;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
public class DialougeSystemScript : MonoBehaviour
{

    [System.Serializable]
    public class DialougeSegment
    {
        public string TextName;

        [TextArea]
        public string DialougeToType;
        public bool Skippable;

        [Range(1f, 25f)]
        public float TextSpeed;

        [Range(0f, 1f)]
        public int PortraitType;
    }

    [SerializeField] private DialougeSegment[] segments;
    [Space]
    [SerializeField] private TMP_Text NameText;
    [SerializeField] private TMP_Text DialougeText;
    [SerializeField] private Image TextBox;
    [SerializeField] private RawImage Character1;
    [SerializeField] private RawImage Character2;

    public Texture[] PlayerTexture = new Texture[2];
    public Texture[] CompanionTexture = new Texture[2];

    private int DialougeIndex;
    private bool PlayingDialouge;
    private bool Skip;

    void Start()
    {
        enabled = true;
        NameText.enabled = true;
        DialougeText.enabled = true;
        TextBox.enabled = true;
        Character1.enabled = true;
        Character2.enabled = true;
        StartCoroutine(PlayDialouge(segments[DialougeIndex]));
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            if (DialougeIndex == segments.Length)
            {
                NameText.enabled = false;
                DialougeText.enabled = false;
                TextBox.enabled = false;
                Character1.enabled = false;
                Character2.enabled = false;
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
        NameText.SetText(segment.TextName);

        switch (segment.TextName)
        {
            case "Riding Hood":
                print("Red");
                Character1.texture = PlayerTexture[segment.PortraitType];
                break;
            case "Robin Hood":
                print("Green");
                Character2.texture = CompanionTexture[segment.PortraitType];
                break;
            case "Match Girl":
                print("Dying");
                Character1.texture = PlayerTexture[segment.PortraitType];

                break;
            default:

                break;
        }

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

            if (segment.DialougeToType[i] == ' ' && i < segment.DialougeToType.Length - 1)
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

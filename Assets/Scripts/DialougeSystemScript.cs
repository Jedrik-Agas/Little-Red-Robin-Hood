using TMPro;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
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

        [Range(0,1)]
        public int PortraitType;

    }

    [SerializeField] private DialougeSegment[] segments;
    [Space]
    [SerializeField] private TMP_Text NameText;
    [SerializeField] private TMP_Text DialougeText;
    [SerializeField] private Image TextBox;
    [SerializeField] private RawImage Character1;
    [SerializeField] private RawImage Character2;
    [SerializeField] private RawImage Background;
    [SerializeField] private Button NextButton;

    public Texture[] PlayerTexture = new Texture[0];
    public Texture[] CompanionTexture = new Texture[0];
    public Texture[] BackgroundTexture = new Texture[0];

    public string NextLevel;

    private int DialougeIndex;
    private bool PlayingDialouge;
    private bool Skip;

    void Start()
    {
        enabled = true;
        NameText.enabled = true;
        DialougeText.enabled = true;
        TextBox.enabled = true;
        NextButton.enabled = true;
        Character1.enabled = true;
        Character2.enabled = true;
        StartCoroutine(PlayDialouge(segments[DialougeIndex]));
    }

    void Update()
    {

    }

    public void NextLine()
    {
        if (DialougeIndex == segments.Length)
        {
            Character1.enabled = false;
            Character2.enabled = false;
            enabled = false;
            SceneManager.LoadScene(NextLevel);
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

    private IEnumerator PlayDialouge(DialougeSegment segment)
    {
        PlayingDialouge = true;

        DialougeText.SetText(string.Empty);
        NameText.SetText(segment.TextName);

        switch (segment.TextName)
        {
            case "Riding Hood":
                Character1.texture = PlayerTexture[segment.PortraitType];
                Character1.color = new Color(255f, 255f, 255f, 255f);
                Character2.color = new Color(255f, 255f, 255f, 0.5f);
                break;
            case "Robin Hood":
                Character2.texture = CompanionTexture[segment.PortraitType];
                Character1.color = new Color(255f, 255f, 255f, 0.5f);
                Character2.color = new Color(255f, 255f, 255f, 255f);
                break;
            case "Narrarator":
                Background.texture = BackgroundTexture[segment.PortraitType];
                Character1.color = new Color(255f, 255f, 255f, 0f);
                Character2.color = new Color(255f, 255f, 255f, 0);
                break;
            default:
                Character1.color = new Color(255f, 255f, 255f, 0f);
                Character2.color = new Color(255f, 255f, 255f, 0);
                Background.texture = BackgroundTexture[segment.PortraitType];
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

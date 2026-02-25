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

        public bool HasBackground;

        [Range(0, 5)]
        public int NovelState;

        [Range(1f, 25f)]
        public float TextSpeed;

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
    [SerializeField] private CanvasGroup CanvasGroup;

    private int DialougeIndex;
    private bool PlayingDialouge;
    private bool Skip;
    public bool EndDialouge = false;

    void Start()
    {
        CanvasGroup.alpha = 1.0f;
        CanvasGroup.interactable = true;
        CanvasGroup.blocksRaycasts = true;
        StartCoroutine(PlayDialouge(segments[DialougeIndex]));
    }

    public void NextLine()
    {
        if (DialougeIndex == segments.Length)
        {
            CanvasGroup.alpha = 0f;
            CanvasGroup.interactable = false;
            CanvasGroup.blocksRaycasts = false;
            EndDialouge = true;
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

        if (segment.HasBackground)
        {
            Background.color = new Color(255f, 255f, 255f, 255f);
        }
        else
        {
            Background.color = new Color(255f, 255f, 255f, 0f);
        }

        switch (segment.NovelState)
            {
                case 1:
                    Character1.texture = PlayerTexture[segment.PortraitType];
                    Character1.color = new Color(255f, 255f, 255f, 255f);
                    Character2.color = new Color(255f, 255f, 255f, 0);
                    break;
                case 2:
                    Character2.texture = CompanionTexture[segment.PortraitType];
                    Character1.color = new Color(255f, 255f, 255f, 0f);
                    Character2.color = new Color(255f, 255f, 255f, 255f);
                    break;
                case 3:
                    Character1.texture = PlayerTexture[segment.PortraitType];
                    Character1.color = new Color(255f, 255f, 255f, 255f);
                    Character2.color = new Color(255f, 255f, 255f, 0.5f);
                    break;
                case 4:
                    Character2.texture = CompanionTexture[segment.PortraitType];
                    Character1.color = new Color(255f, 255f, 255f, 0.5f);
                    Character2.color = new Color(255f, 255f, 255f, 255f);
                    break;
                case 5:
                    Background.texture = BackgroundTexture[segment.PortraitType];
                    Character1.color = new Color(255f, 255f, 255f, 0.5f);
                    Character2.color = new Color(255f, 255f, 255f, 0.5f);
                    break;
                default:
                    Background.texture = BackgroundTexture[segment.PortraitType];
                    Character1.color = new Color(255f, 255f, 255f, 0f);
                    Character2.color = new Color(255f, 255f, 255f, 0f);
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

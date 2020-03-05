using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager instance;
    #region Singleton
    private void Awake()
    {
        if (instance != null)
        {
            Destroy(this.gameObject);
        }
        else
        {
            DontDestroyOnLoad(this.gameObject);
            instance = this;
        }
    }
    #endregion Singleton

    public Text text;
    public SpriteRenderer rendererSprite; //sprite - audioclip, spriterenderer - audiosource
    public SpriteRenderer rendererDialogueWindow;

    public List<string> listSentences;
    public List<Sprite> listSprites;
    public List<Sprite> listDialogueWindows;

    private int count; // 대화 진행 상황 카운트
    public Animator animeSprite;
    public Animator animeDialogueWindow;


    void Start()
    {
        count = 0;
        text.text = "";
        listSentences = new List<string>();
        listSprites = new List<Sprite>();
        listDialogueWindows = new List<Sprite>();
    }

    public void ShowDialogue(Dialogue dialogue)
    {
        for(int i = 0; i < dialogue.sentences.Length; i++)
        {
            listSentences.Add(dialogue.sentences[i]);
            listSprites.Add(dialogue.sprites[i]);
            listDialogueWindows.Add(dialogue.dialogueWindows[i]);
        }
        animeSprite.SetBool("Appear", true);
        animeDialogueWindow.SetBool("Appear", true);
        StartCoroutine(StartDialogueCoroutine());

    }

    public void ExitDialogue()
    {
        text.text = "";
        count = 0;
        listSentences.Clear();
        listSprites.Clear();
        listDialogueWindows.Clear();
        animeSprite.SetBool("Appear", false);
        animeDialogueWindow.SetBool("Appear", false);
    }

    IEnumerator StartDialogueCoroutine()
    {
        if(count > 0)
        {
            if (listDialogueWindows[count] != listDialogueWindows[count - 1])
            {
                animeSprite.SetBool("Change", true);
                animeDialogueWindow.SetBool("Appear", false);
                yield return new WaitForSeconds(0.1f);
                rendererDialogueWindow.GetComponent<SpriteRenderer>().sprite = listDialogueWindows[count];
                rendererSprite.GetComponent<SpriteRenderer>().sprite = listSprites[count];
                animeDialogueWindow.SetBool("Appear", true);
                animeSprite.SetBool("Change", false);

            } else
            {
                if (listSprites[count] != listSprites[count - 1])
                {
                    animeSprite.SetBool("Change", true);
                    yield return new WaitForSeconds(0.1f);
                    rendererSprite.GetComponent<SpriteRenderer>().sprite = listSprites[count];
                    animeSprite.SetBool("Change", false);
                }
                else
                {
                    yield return new WaitForSeconds(0.05f);
                }
            }

            
        }
        
        for(int i = 0; i < listSentences[count].Length; i++)
        {
            text.text += listSentences[count][i]; //1글자씩 출력
            yield return new WaitForSeconds(0.01f);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Z))
        {
            count++;
            text.text = "";
            if(count == listSentences.Count - 1)
            {
                StopAllCoroutines();
                ExitDialogue();
            }
            else
            {
                StopAllCoroutines();
                StartCoroutine(StartDialogueCoroutine());
            }
        }
    }
}

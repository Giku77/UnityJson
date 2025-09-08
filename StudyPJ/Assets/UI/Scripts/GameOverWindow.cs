using System.Collections;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class GameOverWindow : GenericWindow
{

    public Button nextButton;
    public TextMeshProUGUI LeftStats;
    public TextMeshProUGUI RightStats;
    public TextMeshProUGUI Score;

    private Coroutine coroutine;

    private int score;


    protected void Awake()
    {
        if (nextButton != null)
            nextButton.onClick.AddListener(OnClickNext);
    }

    public override void Open()
    {
        gameObject.SetActive(true);
        firstSelected = nextButton.gameObject;
        LeftStats.text = string.Empty;
        RightStats.text = string.Empty;
        coroutine = StartCoroutine(updateStats());
        base.Open();
    }

    public IEnumerator updateStats()
    {
        var sringBuilder = new StringBuilder();
        for (int i = 0; i < 3; i++)
        {
            if (i > 0) LeftStats.text = string.Concat(LeftStats.text, "\n");
            int r = Random.Range(0, 1000);
            score += r;
            LeftStats.text = string.Concat(LeftStats.text, r.ToString("D4"));
            yield return new WaitForSeconds(0.5f);
        }
        for (int i = 0; i < 3; i++)
        {
            if (i > 0) RightStats.text = string.Concat(RightStats.text, "\n");
            int r = Random.Range(0, 1000);
            score += r;
            RightStats.text += r.ToString("D4");
            yield return new WaitForSeconds(0.5f);
        }
        yield return new WaitForSeconds(0.5f);
        Score.text = score.ToString("D8");
    }

    public void OnClickNext()
    {
        gameObject.SetActive(false);
        StopCoroutine(coroutine);
        LeftStats.text = "000\n000\n000";
        RightStats.text = "000\n000\n000";
        Score.text = "0000000";
    }


}

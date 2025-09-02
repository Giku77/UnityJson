using UnityEngine;
using UnityEngine.UI;
using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using System.IO;

public class TargetData
{
    public Vector3 position;
    public Vector3 rotation; // eulerAngles
    public Vector3 scale;
    public TargetData() { }
    public TargetData(Vector3 pos, Vector3 rot, Vector3 sca)
    {
        position = pos;
        rotation = rot;
        scale = sca;
    }
}

public class RecPlay : MonoBehaviour
{
    private bool isRecording = false;
    private bool isPlaying = false;
    private bool isPaused = false;

    private readonly List<string> recordedData = new List<string>();
    private List<TargetData> playbackFrames;
    private Coroutine playRoutine;

    public Button playButton;
    public Button recButton;
    public GameObject target;

    private float moveSpeed = 5f;
    private float scaleSpeed = 1f;
    private float rotSpeed = 90f;

    private float frameTime = 1f / 500f; // 500FPS

    string FilePath => Path.Combine(Application.persistentDataPath, "recording.json");

    public void OnRecButton()
    {
        isRecording = !isRecording;

        if (isRecording)
        {
            recordedData.Clear();
        }
        else
        {
            try
            {
                File.WriteAllText(FilePath, string.Join("\n", recordedData) + "\n");
                Debug.Log($"Saved: {FilePath}");
            }
            catch (System.Exception e)
            {
                Debug.LogError($"Save failed: {e}");
            }
        }

        var img = recButton.GetComponent<Image>();
        if (img) img.color = isRecording ? Color.red : Color.white;
    }

    public void OnPlayButton()
    {
        bool next = !isPlaying;

        if (next) 
        {
            if (playRoutine != null) return;

            if (!File.Exists(FilePath))
            {
                Debug.LogWarning($"No file: {FilePath}");
                return;
            }

            try
            {
                var lines = File.ReadAllLines(FilePath);
                playbackFrames = new List<TargetData>(lines.Length);
                foreach (var line in lines)
                {
                    if (string.IsNullOrWhiteSpace(line)) continue;
                    var data = JsonConvert.DeserializeObject<TargetData>(line, new Vector3Converter());
                    if (data != null) playbackFrames.Add(data);
                }
            }
            catch (System.Exception e)
            {
                Debug.LogError($"Load failed: {e}");
                return;
            }

            if (playbackFrames.Count == 0)
            {
                Debug.LogWarning("Nothing to play.");
                return;
            }

            isPlaying = true;
            var img = playButton.GetComponent<Image>();
            if (img) img.color = Color.green;


            playRoutine = StartCoroutine(PlayRoutine()); 
        }
        else 
        {
            StopPlayback();
        }
    }

    public void OnPauseButton()
    {
        isPaused = !isPaused;
    }

    private void Update()
    {
        if (isPaused) return;

        if (target != null)
        {
            float h = Input.GetAxisRaw("Horizontal"); 
            float v = Input.GetAxisRaw("Vertical");
            float z = 0;
            if (Input.GetKey(KeyCode.Z)) z++;
            if (Input.GetKey(KeyCode.X)) z--;
            target.transform.position += new Vector3(h, v, z) * moveSpeed * Time.deltaTime;

            if (Input.GetKey(KeyCode.W))
                target.transform.localScale += Vector3.one * scaleSpeed * Time.deltaTime;
            if (Input.GetKey(KeyCode.S))
                target.transform.localScale -= Vector3.one * scaleSpeed * Time.deltaTime;

            if (Input.GetKey(KeyCode.A))
                target.transform.Rotate(Vector3.right, -rotSpeed * Time.deltaTime);
            if (Input.GetKey(KeyCode.D))
                target.transform.Rotate(Vector3.right, rotSpeed * Time.deltaTime);
            if (Input.GetKey(KeyCode.Q))
                frameTime = Mathf.Max(0.001f, frameTime - 0.001f);
            if (Input.GetKey(KeyCode.E))
                frameTime += 0.001f;
        }

        if (isRecording && target != null)
        {
            var data = new TargetData(
                target.transform.position,
                target.transform.eulerAngles,
                target.transform.localScale
            );
            var json = JsonConvert.SerializeObject(data, new Vector3Converter());
            recordedData.Add(json);
        }
    }

    private IEnumerator PlayRoutine()
    {
        for (int i = 0; i < playbackFrames.Count;)
        {
            if (!isPlaying) yield break;
            if (isPaused) { yield return null; continue; }

            var data = playbackFrames[i];
            if (target != null)
            {
                target.transform.position = data.position;
                target.transform.rotation = Quaternion.Euler(data.rotation);
                target.transform.localScale = data.scale;
            }

            i++;
            yield return new WaitForSeconds(frameTime);
        }

        StopPlayback();
    }


    private void StopPlayback()
    {
        isPlaying = false;
        var img = playButton.GetComponent<Image>();
        if (img) img.color = Color.white;

        if (playRoutine != null)
        {
            StopCoroutine(playRoutine);
            playRoutine = null;
        }
    }
}

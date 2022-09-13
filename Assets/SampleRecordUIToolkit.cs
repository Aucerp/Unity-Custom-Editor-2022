// A simple script that saves frames from the Game view when in Play mode.
// This example is the same as the OnGUI() example, but uses retained-mode UIToolkit UI.
//
// You can put the frames together later to create a video.
// The frames are saved in the project, at the same level of the project hierarchy as the Assets folder.

using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEditor.UIElements;

public class SimpleRecorderUIToolkit : EditorWindow
{
    [SerializeField]
    string fileName = "FileName";

    bool recording = false;
    float lastFrameTime = 0.0f;
    int capturedFrame = 0;

    private Button recordButton;
    private Label statusLabel;

    [MenuItem("Example/Simple Recorder (UI Toolkit)")]
    public static void ShowExample()
    {
        SimpleRecorderUIToolkit wnd = GetWindow<SimpleRecorderUIToolkit>();
        wnd.titleContent = new GUIContent("Simple Recorder (UIToolkit)");
    }

    private void CreateGUI()
    {
        // Each editor window contains a root VisualElement object.
        VisualElement root = rootVisualElement;

        // Create elements and add them to the visual tree.
        root.Add(new PropertyField() { bindingPath = nameof(fileName) });
        recordButton = new Button(ToggleRecording)
        {
            text = "Record"
        };
        statusLabel = new Label();

        root.Add(recordButton);
        root.Add(statusLabel);

        // Bind the created fields to this window's serializable data
        root.Bind(new SerializedObject(this));
    }

    private void ToggleRecording()
    {
        if (recording)  //recording
        {
            SetStatus("Idle...");
            recordButton.text = "Record";
            recording = false;
        }
        else     // idle
        {
            capturedFrame = 0;
            recordButton.text = "Stop";
            recording = true;
        }
    }

    private void SetStatus(string status)
    {
        statusLabel.text = $"Status: \t\t\t{status}";
    }

    void Update()
    {
        if (recording)
        {
            if (EditorApplication.isPlaying && !EditorApplication.isPaused)
            {
                RecordImages();
                Repaint();
            }
            else
                SetStatus("Waiting for Editor to Play");
        }
    }

    void RecordImages()
    {
        if (lastFrameTime < Time.time + (1 / 24f)) // 24fps
        {
            SetStatus("Captured frame " + capturedFrame);
            ScreenCapture.CaptureScreenshot(fileName + " " + capturedFrame + ".png");
            capturedFrame++;
            lastFrameTime = Time.time;
        }
    }
}
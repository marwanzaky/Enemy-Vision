using UnityEngine;
using UnityEditor;
using System.IO;
using UnityEditor.UIElements;
using UnityEngine.UIElements;
using System;

public class Screenshot : EditorWindow
{
    public enum Format { PNG, JPG }

    VisualElement root;
    TextField fileNameTextField;
    Toggle useKeyCodeToggle;

    EnumField keyCodeEnumField;
    EnumField formatEnumField;

    #region Private Properties

    int ShotIndex
    {
        get
        {
            const int DEFAULT_VALUE = 1;
            return PlayerPrefs.GetInt("screenshot-index", DEFAULT_VALUE);
        }
        set => PlayerPrefs.SetInt("screenshot-index", value);
    }

    string Path
    {
        get
        {
            const string defaultValue = "";
            return PlayerPrefs.GetString("screenshot-path", defaultValue);
        }

        set => PlayerPrefs.SetString("screenshot-path", value);
    }

    #endregion

    [MenuItem("Tools/Screenshot _%#T")]
    public static void ShowWindow()
    {
        var window = GetWindow<Screenshot>();
        window.titleContent = new GUIContent("Screenshot Tool");
        window.minSize = new Vector2(250, 50);
    }

    void Update()
    {
        if (useKeyCodeToggle.value)
        {
            if (Input.GetKeyDown((KeyCode)keyCodeEnumField.value))
                Shot();

            Repaint();
        }
    }
    void OnEnable()
    {
        root = rootVisualElement;
        root.styleSheets.Add(Resources.Load<StyleSheet>("UIElements/ScreenshotTool_Style"));

        var visualTree = Resources.Load<VisualTreeAsset>("UIElements/ScreenshotTool_Main");
        visualTree.CloneTree(root);

        keyCodeEnumField = new EnumField(KeyCode.F12);
        formatEnumField = new EnumField("Format", Format.PNG);

        // Setup buttons
        var toolButtons = root.Query<Button>();
        toolButtons.ForEach(SetupButton);

        // Reference to the Use Key Code
        fileNameTextField = root.Q<TextField>(className: "file-name");

        // Reference to the File Name
        useKeyCodeToggle = root.Q<Toggle>(className: "use-keycode-toggle");

        // Shortcut KeyCode Renderer
        useKeyCodeToggle.RegisterValueChangedCallback(res =>
        {
            VisualElementRenderer("shortcut-keycode", el =>
            {
                if (res.newValue)
                    el.Insert(0, keyCodeEnumField);
                else el.RemoveAt(0);
            });
        });

        // Format KeyCode Renderer
        VisualElementRenderer("format", el => { el.Insert(0, formatEnumField); });
    }

    void VisualElementRenderer(string className, Action<VisualElement> funcCall)
    {
        var visualElements = root.Query<VisualElement>(className: className);
        visualElements.ForEach(funcCall);
    }

    void SetupButton(Button button)
    {
        var buttonIcon = button.Q(className: "screenshot-tool-button-icon");
        var buttonName = button.Q<Label>(className: "screenshot-tool-button-name");
        var iconPath = "Icons/" + button.parent.name + " Icon";
        var iconAsset = Resources.Load<Sprite>(iconPath);

        buttonIcon.style.backgroundImage = iconAsset.texture;
        buttonName.text = button.parent.name;
        button.clickable.clicked += () => HandlerButtonClicked(button.parent.name);
        button.tooltip = button.parent.name;
    }

    void HandlerButtonClicked(string name)
    {
        switch (name)
        {
            case "Screenshot": Shot(); break;
            case "Location": SelectPath(); break;
        }
    }

    void Shot()
    {
        if (string.IsNullOrEmpty(Path))
            SelectPath();

        if (string.IsNullOrEmpty(Path))
            return;

        var format = (Screenshot.Format)formatEnumField.value;
        var formatStr = '.' + format.ToString().ToLower();
        var fullName = $"{fileNameTextField.text + ShotIndex + formatStr}";
        var path = System.IO.Path.Combine(Path, fullName);

        ScreenCapture.CaptureScreenshot(path);
        ShotIndex++;

        Debug.Log($"{fullName} '{Path}'");
    }

    void SelectPath()
    {
        Path = EditorUtility.OpenFolderPanel("Select a folder", "", "Screenshot");
    }
}
/*
 * @brief  这个脚本用于在 Unity 编辑器中创建一个自定义窗口，
 *         允许用户输入项目名称，并根据该名称在 Assets 目录下创建默认的文件夹结构。
 *         主要用于快速设置一个新的 Unity 项目的目录结构。
 *
 * @input  projectName: 字符串类型，用户在编辑器窗口中输入的项目名称，用于作为根文件夹的名称。
 *         当用户点击 "Generate!" 按钮时触发创建文件夹操作
 * 
 * @output  在 Assets 目录下创建以项目名称命名的根文件夹，并在根文件夹下创建一系列子文件夹，包括 Animations、Audio、Editor、Materials、Meshes、Prefabs、Scripts、Scenes、Shaders、Textures 和 UI，UI 文件夹下包含 Assets、Fonts 和 Icon 子文件夹。
 */

using UnityEditor; // 引入Unity编辑器相关的命名空间喵
using UnityEngine; // 引入Unity引擎相关的命名空间喵
using System.Collections.Generic; // 引入泛型集合相关的命名空间喵
using System.IO; // 引入文件IO相关的命名空间喵

public class CreateFolders : EditorWindow // 声明一个名为CreateFolders的类，继承自EditorWindow，用于创建自定义编辑器窗口喵
{
    private static string projectName = "PROJECT_NAME"; // 声明一个静态字符串变量projectName，用于存储项目名称，默认值为"PROJECT_NAME"喵
    
    [MenuItem("Assets/Create Default Folders")] // 添加一个菜单项到Assets菜单，点击后会调用SetUpFolders方法喵
    [MenuItem("Tools/Create Default Folders")] // 添加一个菜单项到Tools菜单，点击后会调用SetUpFolders方法喵
    private static void SetUpFolders() // 声明一个静态方法SetUpFolders，用于创建编辑器窗口喵
    {
        CreateFolders window = ScriptableObject.CreateInstance<CreateFolders>(); // 创建一个CreateFolders实例喵
        window.position = new Rect(Screen.width / 2, Screen.height / 2, 400, 150); // 设置窗口的位置和大小喵
        window.ShowPopup(); // 显示弹窗形式的编辑器窗口喵
    }

    private static void CreateAllFolders() // 声明一个静态方法CreateAllFolders，用于创建所有默认文件夹喵
    {
        List<string> folders = new List<string> // 创建一个字符串列表folders，存储需要创建的顶级文件夹名称喵
        {
            "Animations",
            "Audio",
            "Editor",
            "Materials",
            "Meshes",
            "Prefabs",
            "Scripts",
            "Scenes",
            "Shaders",
            "Textures",
            "UI"
        };

        foreach (string folder in folders) // 遍历folders列表，为每个文件夹创建目录喵
        {
            if (!Directory.Exists("Assets/" + folder)) // 检查文件夹是否存在喵
            {
                Directory.CreateDirectory("Assets/" + projectName + "/" + folder); // 如果不存在，则创建该文件夹喵，放在以项目名称为名的文件夹下
            }
        }

        List<string> uiFolders = new List<string> // 创建一个字符串列表uiFolders，存储需要创建的UI子文件夹名称喵
        {
            "Assets",
            "Fonts",
            "Icon"
        };

        foreach (string subfolder in uiFolders) // 遍历uiFolders列表，为每个子文件夹创建目录喵
        {
            if (!Directory.Exists("Assets/" + projectName + "/UI/" + subfolder)) // 检查子文件夹是否存在喵
            {
                Directory.CreateDirectory("Assets/" + projectName + "/UI/" + subfolder); // 如果不存在，则创建该子文件夹喵
            }
        }
        AssetDatabase.Refresh(); // 刷新资源数据库，使Unity编辑器能够识别新创建的文件夹喵
    }

    void OnGUI() // 重写OnGUI方法，用于绘制编辑器窗口的GUI喵
    {
        EditorGUILayout.LabelField("Insert the Project name used as the root folder"); // 绘制一个标签，提示用户输入项目名称喵
        projectName = EditorGUILayout.TextField("Project Name: ", projectName); // 绘制一个文本框，用于用户输入项目名称喵
        this.Repaint(); // 强制重新绘制GUI喵
        GUILayout.Space(70); // 添加一些空白空间喵
        if (GUILayout.Button("Generate!")) { // 绘制一个按钮，点击后会调用CreateAllFolders方法，并关闭窗口喵
            CreateAllFolders(); // 调用CreateAllFolders方法，创建所有文件夹喵
            this.Close(); // 关闭编辑器窗口喵
        }
    }
}
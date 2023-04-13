using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugController : MonoBehaviour
{
    bool showConsole;
    bool showHelp;

    string input;

    public static DebugCommand HELP;
    public static DebugCommand SET_INVENTORY;
    public static DebugCommand<string> PRINT_TO_CONSOLE;

    public int[] newInventory = new int[5];

    public List<object> commandList;

    void Awake() //! replace the GUI with unity UI.
    {
        HELP = new DebugCommand("help", "displays all commands", "help", () =>
        {
            showHelp = true;
        });

        SET_INVENTORY = new DebugCommand("set_inventory", "Sets the player's inventory.", "[0] [0] [0] [0] [0]", () =>
        {
            //OverideSaveData.ModifyInventory(newInventory);
        });

        PRINT_TO_CONSOLE = new DebugCommand<string>("print_to_console", "Sets the player's inventory.", "print_to_console <string>", (x) =>
        {
            DoAPrint.PrintAMessage(x);
        });

        commandList = new List<object>
        {
            HELP,
            SET_INVENTORY,
            PRINT_TO_CONSOLE
        };
    }

    public void OnReturn()
    {
        if (showConsole)
        {
            HandleInput();
            input = "";
        }
    }

    public void OnToggleDebug()
    {
        showConsole = !showConsole;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Equals))
        {
            OnToggleDebug();
        }

        if (Input.GetKeyDown(KeyCode.Return))
        {
            OnReturn();
        }
    }

    Vector2 scroll;

    private void OnGUI()
    {
        if (!showConsole) return;

        float y = 0;

        if (showHelp)
        {
            GUI.Box(new Rect(0, y, Screen.width, 100), "");

            Rect viewport = new Rect(0, 0, Screen.width - 30, 20 * commandList.Count);

            scroll = GUI.BeginScrollView(new Rect(0, y + 5f, Screen.width, 90), scroll, viewport);

            for (int i = 0; i < commandList.Count; i++)
            {
                DebugCommandBase command = commandList[i] as DebugCommandBase;

                string label = $"{command.commandFormat} - {command.commandDescription}";

                Rect labelRect = new Rect(5, 20 * i, viewport.width - 100, 20);

                GUI.Label(labelRect, label);
            }

            GUI.EndScrollView();

            y += 100;

        }

        GUI.Box(new Rect(0, y, Screen.width, 30), "");
        GUI.backgroundColor = new Color(0, 0, 0, 0);
        input = GUI.TextField(new Rect(10f, y + 5f, Screen.width - 20f, 20f), input);

    }

    private void HandleInput()
    {
        string[] properties = input.Split(' ');

        for (int i = 0; i < commandList.Count; i++)
        {
            DebugCommandBase commandBase = commandList[i] as DebugCommandBase;

            if (input.Contains(commandBase.commandId))
            {
                if (commandList[i] as DebugCommand != null)
                {
                    (commandList[i] as DebugCommand).Invoke();
                }
                else if (commandList[i] as DebugCommand<string> != null)
                {
                    if (commandList[i] as DebugCommand<string> == PRINT_TO_CONSOLE)
                    {
                        string longString = ""; // please this is complicated.
                        for (int _i = 1; _i < properties.Length; _i++)
                        {
                            longString += properties[_i];
                        }
                        (commandList[i] as DebugCommand<string>).Invoke(longString);
                    }
                    else
                    {
                        (commandList[i] as DebugCommand<string>).Invoke(properties[1]);
                    }
                }
            }
        }
    }
}

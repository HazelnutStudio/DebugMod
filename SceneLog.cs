using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

namespace DebugMod
{
    // dazcar credit
    // thanks for helping :3
    public static class SceneLog
    {
        private static List<GameObject> _activeGameObjects;


        public static void LogSceneHierarchy()
        {
            List<string> text = new List<string>();

            CLog.Info("Logging every active object in scene hierarchy");

            _activeGameObjects = GameObject.FindObjectsOfType<GameObject>().ToList();

            for (int i = 0; i < _activeGameObjects.Count; i++)
            {
                if (_activeGameObjects[i].transform.parent == null)
                {
                    GameObject obj = _activeGameObjects[i];
                    bool success = _activeGameObjects.Remove(obj);
                    _activeGameObjects.Insert(0, obj);
                    MoveItem(0);
                }
            }


            CLog.Info("List \"sorted\" now logging...");

            for (int i = 0; i < _activeGameObjects.Count; i++)
            {
                string msg = "";
                for (int j = 0; j < GetNumberOfParents(_activeGameObjects[i]); j++)
                {
                    msg += "-";
                }

                msg += $" {_activeGameObjects[i].name}";
                if (_activeGameObjects[i].transform.parent != null)
                {
                    msg += $" (child of {_activeGameObjects[i].transform.parent.name})";
                }
                else
                {
                    msg += " (root)";
                }

                msg += $" - has {_activeGameObjects[i].transform.GetChildCount()} children";

                CLog.Info(msg);
                text.Add(msg);
            }

            CLog.Info("Writing hierarchy to SceneHierarchyDump.txt");
            WriteToFile(text.ToArray());

        }

        private static void MoveItem(int i)
        {
            GameObject obj = _activeGameObjects[i];

            if (obj.transform.childCount > 0)
            {
                for (int j = 0; j < obj.transform.childCount; j++)
                {
                    GameObject child = obj.transform.GetChild(j).gameObject;


                    if (!child.activeSelf)
                    {
                        CLog.Warning($"Child {j} (\"{child.name}\") of {obj.name} is not active.");
                    }
                    else
                    {
                        int childIndex = _activeGameObjects.FindIndex(x => x.gameObject == child);
                        CLog.Info($"childIndex = {childIndex}");
                        if (childIndex == -1)
                            CLog.Warning($"Child {j} (\"{child.name}\") of {obj.name} doesn't exist?");
                        else
                            _activeGameObjects.RemoveAt(childIndex);

                        _activeGameObjects.Insert(i + 1, child);
                        MoveItem(i + 1);
                    }
                }
            }
        }

        public static int GetNumberOfParents(GameObject obj)
        {
            GameObject objectToTest = obj;
            int tests = 0;

            while (objectToTest != null)
            {
                if (objectToTest.transform.parent != null)
                {
                    objectToTest = objectToTest.transform.parent.gameObject;
                    tests++;
                }
                else
                {
                    objectToTest = null;
                }
            }

            return tests;
        }

        private static void WriteToFile(string[] text, string filename = "SceneHierarchyDump.txt")
        {
            string outputPath = Environment.CurrentDirectory;
            StreamWriter outputFile = new StreamWriter(Path.Combine(outputPath, filename)) { AutoFlush = true };

            for (int i = 0; i < text.Length; i++)
            {
                outputFile.WriteLine(text[i]);
            }
        }
    }
}
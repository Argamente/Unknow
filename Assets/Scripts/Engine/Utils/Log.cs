using UnityEngine;
using System;
using Argamente.Managers;

namespace Argamente.Utils
{
    public class Log
    {
        private const string INFO = "Info";
        private const string WARN = "Warn";
        private const string ERROR = "Error";
        private const string FATAL = "Fatal";
        private static string _persistentDataPath = "";

        public static bool isDebugBuild = false;
        public static bool showInApp = false;

        public static void Info (object owner, params object[] values)
        {
            log (owner, INFO, values);   
        }

        public static void Warn (object owner, params object[] values)
        {
            log (owner, WARN, values);
        }

        public static void Error (object owner, params object[] values)
        {
            log (owner, ERROR, values);
        }

        public static void Fatal (object owner, params object[] values)
        {
            log (owner, FATAL, values);
        }


        private static void log (object owner, string name, object[] values)
        {
            string logStr = GetDebugString (owner, name, values);
            if (UnityEngine.Application.platform == RuntimePlatform.WindowsEditor ||
                UnityEngine.Application.platform == RuntimePlatform.OSXEditor)
            {
                if (name == WARN)
                {
                    Debug.LogWarning (logStr);
                }
                else if (name == ERROR)
                {
                    Debug.LogError (logStr);
                }
                else if (name == FATAL)
                {
                    Debug.LogError (logStr);
                }
                else
                {
                    Debug.Log (logStr);
                }
            }


            if (showInApp)
            {
                LogObject.GetInstance ().logStr += logStr + "\n";
            }

            if (_persistentDataPath == "")
            {
                try
                {
                    _persistentDataPath = ApplicationUtils.GetProcessDirectory ();
                }
                catch (Exception e)
                {
                    Debug.LogError (e);
                }
            }

            if (_persistentDataPath != "")
            {
                try
                {
                    string fileName = _persistentDataPath + "/" + name + ".log";

                    FileUtils.AppendTextToFile (logStr + "\n", fileName);
                }
                catch (Exception e)
                {
                    Debug.LogError (e);
                }
            }
        }


        public static string GetDebugString (object owner, string name, params object[] values)
        {
            string logStr = "[Log] " + DateTime.Now.ToString ("T") + " ";
            if (owner == null)
            {
                logStr = "[";
            }
            else
            {
                try
                {
                    logStr += "[owner:\"" + owner + "\" ";
                }
                catch (Exception e)
                {
                    logStr += "[owner:\" \" ";
                    Debug.LogError (e);
                }
            }


            logStr += "name:\"" + name + "\"] - ";

            for (int i = 0; i < values.Length; ++i)
            {
                try
                {
                    logStr += values [i] == null ? "null" : values [i].ToString () + "  ";
                }
                catch (Exception e)
                {
                    logStr += "";
                    Debug.LogError (e);
                }
            }

            return logStr;
        }





    }



    public class LogObject : MonoBehaviour
    {
        private static bool _initialized = false;
        private static LogObject _instance;

        public static LogObject GetInstance ()
        {
            if (!_initialized)
            {
                _instance = SingletonManager.AddComponent<LogObject> ();
                _initialized = true;
            }
            return _instance;
        }

        public string logStr = "";
        private Vector2 scrollPosition;
        private bool show = false;

        private void OnGUI ()
        {
            if (GUI.Button (new Rect (0.0f, 0.0f, 100.0f, 60.0f), "Show Log"))
            {
                this.show = !this.show;
            }
            if (GUI.Button (new Rect (105.0f, 0.0f, 100.0f, 60.0f), "Clear Log"))
            {
                this.logStr = "";
            }

            GUILayout.Space (60f);
            if (this.show)
            {
                scrollPosition = GUILayout.BeginScrollView (scrollPosition, GUILayout.Width (500), GUILayout.Height (300));
                GUILayout.Label (this.logStr);
                GUILayout.EndScrollView ();
            }

        }


    }




}


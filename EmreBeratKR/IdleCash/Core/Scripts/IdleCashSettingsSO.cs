using System;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;
using EmreBeratKR.IdleCash.Exceptions;

namespace EmreBeratKR.IdleCash.Creator
{
    public class IdleCashSettingsSO : ScriptableObject
    {
        public const string MenuItemSettings = MenuItemRoot + "Settings";


        private const string DefaultSettingsFileName = "IdleCash Settings.asset";
        private const string MenuItemRoot = "Tools/EmreBeratKR/IdleCash/";
        private const string BlankType = "";
        
        
        private static readonly string[] DefaultRealTypes = new string[] {"k", "m", "b", "t", "q"};
        private static readonly string[] DefaultLetters = new string[]
        {
            "a", "b", "c", "d", "e", "f", "g", "h", "i", "j", "k", "l", "m",
            "n", "o", "p", "q", "r", "s", "t", "u", "v", "w", "x", "y", "z"
        };
        private const IdleCashTypeCreationMode DefaultCreationMode = IdleCashTypeCreationMode.Blank | IdleCashTypeCreationMode.Reals | IdleCashTypeCreationMode.DoubleLetters;

        
        [SerializeField] private string[] realTypes = DefaultRealTypes;
        [SerializeField] private string[] letters = DefaultLetters;
        [SerializeField] private IdleCashTypeCreationMode creationMode = DefaultCreationMode;

        
        public static string FirstType => Types[0];
        public static string LastType => Types[Types.Count - 1];

        
        public static List<string> Types
        {
            get
            {
                if (Instance.m_Types == null)
                {
                    Instance.CreateTypes();
                }

                return Instance.m_Types;
            }
        }
        

        private static IdleCashSettingsSO Instance
        {
            get
            {
                if (!ms_Instance)
                {
                    var instances = Resources.LoadAll<IdleCashSettingsSO>("");

                    if (instances.Length == 0)
                    {
                        if (ms_LogError)
                        {
                            Debug.LogError(new IdleCashSettingsNotFoundException());
                        }
                        
                        return ms_Instance;
                    }

                    ms_Instance = instances[0];
                }

                return ms_Instance;
            }
        }


        private static IdleCashSettingsSO ms_Instance;
        private static bool ms_LogError = true;
        [HideInInspector, SerializeField]
        private List<string> m_Types;


#if UNITY_EDITOR

        [MenuItem(MenuItemSettings)]
        private static void OpenSettings()
        {
            ms_LogError = false;
            
            var instance = Instance;

            if (!instance)
            {
                TryFindResourcesFolderPath(out var path);
                instance = CreateNewInstance(path);
            }
            
            Selection.activeObject = instance;
            EditorGUIUtility.PingObject(instance);

            ms_LogError = true;
        }

        private static IdleCashSettingsSO CreateNewInstance(string path)
        {
            var newInstance = CreateInstance<IdleCashSettingsSO>();
            newInstance.CreateTypes();
            AssetDatabase.CreateAsset(newInstance, path + "/" + DefaultSettingsFileName);
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
            return newInstance;
        }
        
        private static bool TryFindResourcesFolderPath(out string path)
        {
            if (!TryFindFolderPath("Assets/", "IdleCash", out path)) return false;

            var resourcesPath = path + "/Resources";

            if (!Directory.Exists(resourcesPath))
            {
                AssetDatabase.CreateFolder(path, "Resources");
                AssetDatabase.SaveAssets();
                AssetDatabase.Refresh();
            }

            path = resourcesPath;
            
            return true;
        }
        
        private static bool TryFindFolderPath(string startingPath, string folderName, out string path)
        {
            foreach (var directory in Directory.EnumerateDirectories(startingPath))
            {
                if (directory.EndsWith(folderName))
                {
                    path = directory;
                    return true;
                }

                if (TryFindFolderPath(directory, folderName, out path)) return true;
            }

            path = startingPath;
            return false;
        }
        
        private void OnValidate()
        {
            CreateTypes();
        }
        
        public static void ResetSettingsToDefault()
        {
            Undo.RecordObject(Instance, "Reset IdleCash Settings");
            
            Instance.realTypes = DefaultRealTypes;
            Instance.letters = DefaultLetters;
            Instance.creationMode = DefaultCreationMode;
            
            Instance.CreateTypes();
        }
        
#endif


        public static bool IsValidType(string type)
        {
            return Types.Contains(type);
        }

        public static int GetTypeIndex(string type)
        {
            return Types.IndexOf(type);
        }

        public static string GetNextType(string type)
        {
            var typeIndex = GetTypeIndex(type);
            var nextIndex = typeIndex + 1;
            return nextIndex >= Types.Count ? null : Types[nextIndex];
        }
        
        public static string GetPreviousType(string type)
        {
            var typeIndex = GetTypeIndex(type);
            var previousIndex = typeIndex - 1;
            return previousIndex < 0 ? null : Types[previousIndex];
        }


        private void CreateTypes()
        {
            m_Types = new List<string>();
            
            var creationMode = Instance.creationMode;

            if (CheckCreationMode(creationMode, IdleCashTypeCreationMode.Blank))
            {
                CreateBlankType();
            }

            if (CheckCreationMode(creationMode, IdleCashTypeCreationMode.Reals))
            {
                CreateRealTypes();
            }

            if (CheckCreationMode(creationMode, IdleCashTypeCreationMode.SingleLetters))
            {
                CreateSingleLetterTypes();
            }
            
            if (CheckCreationMode(creationMode, IdleCashTypeCreationMode.DoubleLetters))
            {
                CreateDoubleLetterTypes();
            }
        }

        private void CreateBlankType()
        {
            m_Types.Add(BlankType);
        }

        private void CreateRealTypes()
        {
            foreach (var realType in Instance.realTypes)
            {
                m_Types.Add(realType);
            }
        }

        private void CreateSingleLetterTypes()
        {
            foreach (var letter in Instance.letters)
            {
                m_Types.Add(letter);
            }
        }
        
        private void CreateDoubleLetterTypes()
        {
            var letters = Instance.letters;
            
            foreach (var firstLetter in letters)
            {
                foreach (var secondLetter in letters)
                {
                    var newDoubleLetterType = firstLetter + secondLetter;
                    m_Types.Add(newDoubleLetterType);
                }
            }
        }
        
        
        
        [Flags]
        private enum IdleCashTypeCreationMode
        {
            Blank = 1 << 0,
            Reals = 1 << 1,
            SingleLetters = 1 << 2,
            DoubleLetters = 1 << 3
        }
        
        private static bool CheckCreationMode(IdleCashTypeCreationMode creationMode, IdleCashTypeCreationMode otherCreationMode)
        {
            return (creationMode & otherCreationMode) == otherCreationMode;
        }
    }
}

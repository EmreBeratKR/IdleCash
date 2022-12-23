using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace EmreBeratKR.IdleCash
{
    [CreateAssetMenu(menuName = MenuName)]
    public class IdleCashTypeCreator : ScriptableObject
    {
        private const string MenuName = nameof(IdleCashTypeCreator);
        private const string CreatorAssetPath = "Creators";
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


        private static IdleCashTypeCreator ms_Instance;

        private static IdleCashTypeCreator Instance
        {
            get
            {
                if (ms_Instance == null)
                {
                    var instances = Resources.LoadAll<IdleCashTypeCreator>(CreatorAssetPath);

                    if (instances.Length == 0)
                    {
                        throw new NullReferenceException($"{nameof(IdleCashTypeCreator)} cannot be found in Resources folders! Please Create one from {MenuName}.");
                    }

                    ms_Instance = instances[0];
                }

                return ms_Instance;
            }
        }
        
        
        private List<string> m_Types;

        private static List<string> Types
        {
            get
            {
                if (Instance.m_Types == null)
                {
                    CreateTypes();
                }

                return Instance.m_Types;
            }
        }

        public static string FirstType => Types[0];

        public static string LastType => Types.Last();


        private void OnValidate()
        {
            CreateTypes();
        }


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

        public static void ResetSettingsToDefault()
        {
            Instance.realTypes = DefaultRealTypes;
            Instance.letters = DefaultLetters;
            Instance.creationMode = DefaultCreationMode;
            
            CreateTypes();
        }
        
        
        private static void CreateTypes()
        {
            Instance.m_Types = new List<string>();
            
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

        private static void CreateBlankType()
        {
            Instance.m_Types.Add(BlankType);
        }

        private static void CreateRealTypes()
        {
            foreach (var realType in Instance.realTypes)
            {
                Instance.m_Types.Add(realType);
            }
        }

        private static void CreateSingleLetterTypes()
        {
            foreach (var letter in Instance.letters)
            {
                Instance.m_Types.Add(letter);
            }
        }
        
        private static void CreateDoubleLetterTypes()
        {
            var letters = Instance.letters;
            
            foreach (var firstLetter in letters)
            {
                foreach (var secondLetter in letters)
                {
                    var newDoubleLetterType = firstLetter + secondLetter;
                    Instance.m_Types.Add(newDoubleLetterType);
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class NameFake : MonoBehaviour
{
        [Serializable]
        private class NamesList
        {
            public List<string> names;
        }

        static NamesList namesList;
        static NamesList CurrentNamesList
        {
            get
            {
                if (namesList == null)
                {
                    TextAsset textAsset = Resources.Load("TextFiles/names") as TextAsset;
                    namesList = JsonUtility.FromJson<NamesList>(textAsset.text);
                }
                return namesList;
            }
        }

        public static string GetRandomName()
        {
            return CurrentNamesList.names[UnityEngine.Random.Range(0, CurrentNamesList.names.Count)];
        }
}

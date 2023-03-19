using Cards;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;
using Cards.ScriptableObjects;

namespace Cards
{
    [System.Serializable]
    [CustomEditor(typeof(CardEditor))]
    [CanEditMultipleObjects]
    public class CardEditor : EditorWindow
    {
        [SerializeField] public CardPackConfiguration[] _packs;
        [MenuItem("Tools/Create Deck")]
        public static void ShowMyEditor()
        {
            EditorWindow wnd = GetWindow<CardEditor>();
            wnd.titleContent = new GUIContent("Create Deck");
        }
        public void OnInspectorGUI()
        {

        }
        public void CreateGUI()
        {
            rootVisualElement.Add(new Label("Добавьте карты в колоду"));

            List<CardPropertiesData> _allCards;
            IEnumerable<CardPropertiesData> cards = new List<CardPropertiesData>();
            _packs = FindObjectsOfType<CardPackConfiguration>();
            foreach (var pack in _packs)
            {
                cards = pack.UnionProperties(cards);
            }
            _allCards = new List<CardPropertiesData>(cards);

            var splitView = new TwoPaneSplitView(0, 250, TwoPaneSplitViewOrientation.Horizontal);
            rootVisualElement.Add(splitView);

            var leftPane = new ListView();
            splitView.Add(leftPane);
            var rightPane = new VisualElement();
            splitView.Add(rightPane);

            leftPane.makeItem = () => new Label();
            leftPane.bindItem = (item, index) => { (item as Label).text = _allCards[index].Id.ToString(); };
            leftPane.itemsSource = _allCards;
        }
    }
}

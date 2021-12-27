using TMPro;
using UnityEngine;

public class TextPanel : MonoBehaviour
{
        [SerializeField]
        private TextMeshProUGUI text;
        
        public string Text {  set => text.text = value; }
}
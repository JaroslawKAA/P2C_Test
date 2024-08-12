using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;

namespace UI
{
    public class TextRecord : MonoBehaviour
    {
        [Title("Config")]
        [SerializeField] string headerName;
    
        [Title("Depend")]
        [SerializeField] [Required] TMP_Text outputText;

        public void SetOutput(string text) => outputText.text = $"{headerName}: {text}";
    }
}

using UnityEngine;
using UnityEngine.SceneManagement;

namespace MrRunner
{
    public class EntryController : MonoBehaviour
    {
        [SerializeField]
        private EntryView view;

        void Start()
        {
            view.PlayButton.onClick.AddListener(()=> {
                SceneManager.LoadScene("Game");
            });
            view.OptionsButton.onClick.AddListener(() => {
                view.Dialogue.gameObject.SetActive(true);
            });
        }
    }
}

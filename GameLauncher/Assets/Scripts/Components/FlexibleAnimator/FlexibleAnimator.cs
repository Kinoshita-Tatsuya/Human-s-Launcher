using UnityEngine;

namespace GameLauncher.Components.FlexibleAnimator
{
    [RequireComponent(typeof(Animator))]
    public class FlexibleAnimator : MonoBehaviour
    {
        [SerializeField] protected Vector3 scaleBase = new Vector3(100.0f, 100.0f, 100.0f);

        public Vector3 ScaleBase
        {
            get { return scaleBase; }
            set { scaleBase = value; }
        }

        public Animator Animator { get; set; }

        public void Start()
        {
            Animator = GetComponent<Animator>();
        }

        public void Update()
        {
            var rectTransform = transform as RectTransform;

            rectTransform.localScale = new Vector3(
                scaleBase.x / 100.0f, 
                scaleBase.y / 100.0f, 
                scaleBase.z / 100.0f);
        }
    }
}

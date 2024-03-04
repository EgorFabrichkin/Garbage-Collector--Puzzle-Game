using UnityEngine;

namespace View
{
    public class TrajectoryRenderer : MonoBehaviour
    {
        [SerializeField] private GameObject dotsParent;
        [SerializeField] private GameObject dotsPrefab;
        [SerializeField] private int dotsNumber;
        [SerializeField] private float dotSpacing;
        [Header("Scaling")]
        [SerializeField] [Range(0.01f, 0.3f)] private float dotMinScale;
        [SerializeField] [Range(0.1f, 0.5f)] private float dotMaxScale;
        
        private Vector2 positionDots;
        private Transform[] dotsList;

        private void Start()
        {
            Hide();
            PrepareDots();
        }
        
        private void PrepareDots()
        {
            dotsList = new Transform[dotsNumber];
            dotsPrefab.transform.localScale = Vector3.one * dotMaxScale;

            var scale = dotMaxScale;
            var scaleFactor = scale / dotsNumber;
            
            for (var i = 0; i < dotsNumber; i++)
            {
                dotsList[i] = Instantiate(dotsPrefab, null).transform; // use ObjectPool?
                dotsList[i].parent = dotsParent.transform;
                dotsList[i].localScale = Vector3.one * scale;
                
                if (scale > dotMinScale)
                {
                    scale -= scaleFactor;
                }
            }
        }
        
        public void UpdateDots(Vector3 playerPosition, Vector2 forceDirection)
        {
            var timeStamp = dotSpacing;
            for (var i = 0; i < dotsNumber; i++)
            {
                positionDots.x = (playerPosition.x + forceDirection.x * timeStamp);
                positionDots.y = (playerPosition.y + forceDirection.y * timeStamp) - 
                                 (Physics2D.gravity.magnitude * timeStamp * timeStamp)/2f;
                dotsList[i].position = positionDots;
                timeStamp += dotSpacing;
            }
        }
        
        public void Draw() => dotsParent.SetActive(true);
        
        public void Hide() => dotsParent.SetActive(false);
    }
}
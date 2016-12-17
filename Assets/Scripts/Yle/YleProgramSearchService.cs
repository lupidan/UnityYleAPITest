using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using MiniJSON;

namespace YleService
{
    using JsonDictionary = Dictionary<string, object>;

    public class YleProgramSearchService : MonoBehaviour
    {
        public string BaseUrl = "https://external.api.yle.fi";
        public string AppId = "YOURAPPID";
        public string AppKey = "YOURAPPKEY";

        public List<YleProgram> Programs { get; private set; }
        public bool EndReached           { get; private set; }

        private string _currentQuery;
        private long _currentLimit;
        private long _currentOffset;

        void Start()
        {
            InitializeProgramSearch("how", 10);
            LoadProgramBatch();
        }

        void Update()
        {
            if (Input.GetKeyDown(KeyCode.A))
                LoadProgramBatch();
        }

        public void InitializeProgramSearch(string query, int limit)
        {
            _currentOffset = 0;
            _currentQuery = query;
            _currentLimit = limit;

            EndReached = false;
            Programs = new List<YleProgram>();
        }

        public void LoadProgramBatch()
        {
            StartCoroutine(PerformRequestForNextBatch());
        }

        private IEnumerator PerformRequestForNextBatch()
        {
            object[] args = {BaseUrl, _currentQuery, _currentLimit, _currentOffset, AppKey, AppId};
            string url = string.Format("{0}/v1/programs/items.json?q={1}&limit={2}&offset={3}&app_key={4}&app_id={5}", args);
            Debug.Log(url);

            UnityWebRequest request = UnityWebRequest.Get(url);
            yield return request.Send();

            if(request.isError)
            {
                Debug.Log(request.error);
            }
            else
            {
                JsonDictionary jsonDictionary = Json.Deserialize(request.downloadHandler.text) as JsonDictionary;
                YleSearchResults results = new YleSearchResults(jsonDictionary);
                _currentOffset += results.Programs.Count;
                Programs.AddRange(results.Programs);
                EndReached = Programs.Count >= results.Meta.Count;

                for (int i = 0; i < Programs.Count; i++)
                {
                    Debug.Log(Programs[i].Title);
                }
            }
        }

    }
}

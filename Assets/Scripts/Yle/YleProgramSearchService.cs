using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.Events;
using MiniJSON;

namespace YleService
{

    public class YleProgramSearchService : MonoBehaviour
    {
        public string BaseUrl = "https://external.api.yle.fi";
        public string AppId = "YOURAPPID";
        public string AppKey = "YOURAPPKEY";

        public UnityAction<List<YleProgram>, string> LoadProgramBatchFinished;

        public List<YleProgram> Programs { get; private set; }
        public bool EndReached           { get; private set; }
        public bool IsLoading            { get; private set; }

        private string _currentQuery;
        private long _currentLimit;
        private long _currentOffset;

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
            if (!IsLoading && !string.IsNullOrEmpty(_currentQuery))
                StartCoroutine(PerformRequestForNextBatch());
        }

        private IEnumerator PerformRequestForNextBatch()
        {
            IsLoading = true;

            object[] args = {BaseUrl, WWW.EscapeURL(_currentQuery), _currentLimit, _currentOffset, AppKey, AppId};
            string url = string.Format("{0}/v1/programs/items.json?q={1}&limit={2}&offset={3}&app_key={4}&app_id={5}", args);

            UnityWebRequest request = UnityWebRequest.Get(url);
            yield return request.Send();

            if(request.isError)
            {
                if (LoadProgramBatchFinished != null)
                    LoadProgramBatchFinished(null, request.error);
            }
            else
            {
                Dictionary<string, object> dict = Json.Deserialize(request.downloadHandler.text) as Dictionary<string, object>;
                YleSearchResults results = new YleSearchResults(dict);
                _currentOffset += results.Programs.Count;
                Programs.AddRange(results.Programs);
                EndReached = Programs.Count >= results.Meta.Count;

                if (LoadProgramBatchFinished != null)
                    LoadProgramBatchFinished(results.Programs, null);
            }

            IsLoading = false;
        }

    }
}

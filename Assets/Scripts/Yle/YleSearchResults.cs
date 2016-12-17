using System.Collections.Generic;

namespace YleService
{
    using JsonDictionary = Dictionary<string, object>;
    using JsonArray = List<object>;

    public class YleSearchResults
    {
        public YleSearchMeta Meta        { get; private set; }
        public List<YleProgram> Programs { get; private set; }

        public YleSearchResults(JsonDictionary jsonDictionary)
        {
            JsonDictionary metaJsonDictionary = jsonDictionary["meta"] as JsonDictionary;
            if (metaJsonDictionary != null)
                Meta = new YleSearchMeta(metaJsonDictionary);

            JsonArray dataJsonArray = jsonDictionary["data"] as JsonArray;
            if (dataJsonArray != null)
                Programs = YleProgram.YleProgramsFromJsonArray(dataJsonArray);
        }
    }
}



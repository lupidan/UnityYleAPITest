using System.Collections.Generic;

namespace YleService
{

    public class YleSearchResults
    {
        public YleSearchMeta Meta        { get; private set; }
        public List<YleProgram> Programs { get; private set; }

        public YleSearchResults(Dictionary<string, object> dict)
        {
            Dictionary<string, object> metaDict = dict["meta"] as Dictionary<string, object>;
            if (metaDict != null)
                Meta = new YleSearchMeta(metaDict);

            List<object> dataArray = dict["data"] as List<object>;
            if (dataArray != null)
                Programs = YleProgram.YleProgramsFromJsonArray(dataArray);
        }
    }

}



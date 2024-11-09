using Newtonsoft.Json;

namespace PRN231PE_FA23_665511_taipdse172357_Pages.DTO
{
    public class OdataResponse<T>
    {
        [JsonProperty("value")]
        public List<T> Value { get; set; }

        [JsonProperty("@odata.count")]
        public int Count { get; set; }
    }
}

namespace HRIS_CoreMVC_dotNet6.Models
{
    public class jDataTableAjxParams
    {
        public int draw { get; set; }
        public int start { get; set; }
        public int length { get; set; }
        public string search { get; set; }
        public int sortColIndex { get; set; }
        public string sortDir { get; set; }
    }
}

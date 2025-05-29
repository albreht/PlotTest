namespace Model
{
    public class PlotData
    {
        public PlotData()
        {
                PlotItems = new List<PlotItem>();
        }
        public string PlotName { get; set; }

        public List<PlotItem> PlotItems { get; set; }
    }
}

using System.Diagnostics;
using Model;
using OxyPlot;
using OxyPlot.Annotations;
using OxyPlot.Axes;
using OxyPlot.ImageSharp;
using OxyPlot.Series;


SampleDataFactory sampleDataFactory = new SampleDataFactory();
var sampleData = sampleDataFactory.GenerateSampleData();

PlotModel model = new PlotModel();
model.Background = OxyColor.FromRgb(255,255,255);
model.Title = sampleData.PlotName;
model.PlotAreaBorderThickness = new OxyThickness(0);

var categoryAxis = new CategoryAxis();
categoryAxis.Position = AxisPosition.Left;

categoryAxis.Key = "KategoriaOś";

categoryAxis.Position = AxisPosition.Left;
categoryAxis.MinorTickSize = 0;
categoryAxis.MajorTickSize = 0;
categoryAxis.GapWidth = 0;
categoryAxis.TickStyle = TickStyle.None;
categoryAxis.MajorGridlineStyle = LineStyle.None;
categoryAxis.MinorGridlineStyle = LineStyle.None;
categoryAxis.Maximum = sampleData.PlotItems.Count + 1;


categoryAxis.ItemsSource = sampleData.PlotItems.Select(p=>p.Name).ToList();

var categoryAxis1 = new CategoryAxis();
categoryAxis1.Position = AxisPosition.Left;
            
categoryAxis1.Key = "KategoriaOśGradient";
            
categoryAxis1.Position = AxisPosition.Left;
categoryAxis1.MinorTickSize = 0;
categoryAxis1.MajorTickSize = 0;
categoryAxis1.GapWidth = 0;
categoryAxis1.TickStyle = TickStyle.None;
categoryAxis1.MajorGridlineStyle = LineStyle.None;
categoryAxis1.MinorGridlineStyle = LineStyle.None;
categoryAxis1.Minimum = 0;
categoryAxis1.TextColor = OxyColors.Transparent;


categoryAxis1.ItemsSource = Enumerable.Range(-1, sampleData.PlotItems.Count+2).ToList();

model.Axes.Add(categoryAxis1);



var pointSeries = new LineSeries
{
    Color = OxyColors.Red,
    YAxisKey = "KategoriaOśGradient",
    
};

pointSeries.Points.Add(new DataPoint(100, sampleData.PlotItems.Count+1));
model.Series.Add(pointSeries);

var serie = new LineSeries
{
    Color = OxyColors.SteelBlue,
    // LabelPlacement = LabelPlacement.Inside,
   // LabelFormatString = "Wartość {0}",
    YAxisKey = "KategoriaOś"
};

for (int i = 0; i < sampleData.PlotItems.Count; i++)
{
     serie.Points.Add(new DataPoint(sampleData.PlotItems[i].Value, i));
}



model.Axes.Add(categoryAxis);
model.Series.Add(serie);





var xAxis = new LinearAxis
{
    Position = AxisPosition.Bottom,
    //odsunięcie wykresu od granic
    Minimum = sampleData.PlotItems.Min(p=>p.Value)-100,
    Maximum = sampleData.PlotItems.Max(p => p.Value) + 100,     
    IsPanEnabled = false,
    IsZoomEnabled = false
};

model.Axes.Add(xAxis);

int N = sampleData.PlotItems.Count;


double xMin = 0;
double xMax = 1100;


double steps = 100;

for (int i = 0; i < steps; i++)
{
    double x1 = xMin + i * (xMax - xMin) / steps;
    double x2 = xMin + (i + 1) * (xMax - xMin) / steps;

    var color = OxyColor.FromRgb(
        (byte)(255 * i / steps),       
        0,
        (byte)(255 * (1 - (double)i / steps)) 
    );

    var rect = new RectangleAnnotation
    {
        MinimumX = x1,
        MaximumX = x2,
        MinimumY = sampleData.PlotItems.Count ,
        MaximumY = sampleData.PlotItems.Count + 1,
        Fill = color,
        StrokeThickness = 0,
        Layer = AnnotationLayer.AboveSeries,
        ClipByXAxis = false,  
        ClipByYAxis = false   
       
    };

    model.Annotations.Add(rect);
}




var verticalLine = new LineAnnotation
{
    Type = LineAnnotationType.Vertical,
    X = 550, 
    Color = OxyColors.Red,
    LineStyle = LineStyle.Solid,
    TextOrientation = AnnotationTextOrientation.Vertical,
    TextHorizontalAlignment = HorizontalAlignment.Left,
    TextVerticalAlignment = VerticalAlignment.Top
};

model.Annotations.Add(verticalLine);

for (int i =0; i< sampleData.PlotItems.Count ; i++)
{
    model.Annotations.Add(new LineAnnotation
    {
        Type = LineAnnotationType.Horizontal,
        Y = i,  
        YAxisKey = "KategoriaOś",
        Color = OxyColors.LightGray,
        LineStyle = LineStyle.Dash,
        StrokeThickness = 1,
        TextOrientation = AnnotationTextOrientation.Horizontal,
        Layer = AnnotationLayer.BelowAxes 

    });

    var annotation = new TextAnnotation
    {
        Text = $"My Label {sampleData.PlotItems[i].ValueDescription}",
        TextPosition = new DataPoint(sampleData.PlotItems[i].Value, i),
        Stroke = OxyColors.Transparent,
        FontWeight = FontWeights.Bold,
        FontSize = 12,
        TextColor = OxyColors.Black
    };

    model.Annotations.Add(annotation);
}


PngExporter.Export(model, "plot.png", 1280, 720);

Process.Start(new ProcessStartInfo(){ FileName ="plot.png", UseShellExecute=true});




using System.Diagnostics;
using System.IO;
using Model;
using ScottPlot;
using ScottPlot.Plottables;


SampleDataFactory sampleDataFactory = new SampleDataFactory();
var sampleData = sampleDataFactory.GenerateSampleData();


string[] categories = sampleData.PlotItems.Select(p=>p.Name).ToArray();
double[] values = sampleData.PlotItems.Select(p => p.Value).ToArray();

var plt = new Plot();
plt.Title(sampleData.PlotName);


double[] positions = Enumerable.Range(0, categories.Length).Select(i => (double)i).ToArray();


var line = plt.Add.Scatter(values, positions);
line.LineWidth = 2;
line.Color = Colors.Blue;
line.MarkerShape = MarkerShape.FilledCircle;
line.MarkerSize = 5;


plt.Axes.Left.TickGenerator = new ScottPlot.TickGenerators.NumericManual(positions, categories);

plt.Axes.Bottom.MajorTickStyle.Length = 0;
plt.Axes.Bottom.MinorTickStyle.Length = 0;
plt.Axes.Bottom.FrameLineStyle.Width = 0;

plt.Axes.SetLimitsX(0, 1100);

for (int i = 0; i < values.Length; i++)
{
    plt.Add.Text($"({values[i]})", values[i], i);
}

plt.Axes.Left.MajorTickStyle.Length = 0;
plt.Axes.Left.MinorTickStyle.Length = 0;
plt.Axes.Left.FrameLineStyle.Width = 0;

plt.Axes.Top.FrameLineStyle.Width = 0;
plt.Axes.Right.FrameLineStyle.Width = 0;

var vline = plt.Add.VerticalLine(550);
vline.LineWidth = 2;
vline.Color = Colors.Red;


plt.SavePng("plotScoot.png", 1000, 800);


Process.Start(new ProcessStartInfo() { FileName = "plotScoot.png", UseShellExecute = true });

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class SampleDataFactory
    {
        private Random random;
        public SampleDataFactory() {
        random = new Random(DateTime.Now.Millisecond);
        }

        public PlotData GenerateSampleData() { 
            


            var plotData = new PlotData();
            plotData.PlotName = $"Przykładowy wykres = {random.Next(10000, 90000)}";

            for (int i = 0; i < random.Next(10,20); i++)
            {

                var randomValue = random.Next(100, 1000);
                plotData.PlotItems.Add(
                    new PlotItem()
                    {
                        Name = $"Przykładowa kategoria {i.ToString()}",
                        Value = randomValue,
                        ValueDescription = $"Opis punktu ({randomValue}) {randomValue+100}"

                    })
                ;
            }
            return plotData;
        
        }
    }
}

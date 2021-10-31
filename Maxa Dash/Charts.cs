using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LiveCharts;
using LiveCharts.Wpf;
using LiveCharts.Defaults;

namespace Maxa_Dash
{
    public class Charts
    {
        public Charts()
        { }

        public Charts(NotifyNewData notifier)
        {
            //notifier.YFormatter = Value => Value.ToString();
            //notifier.DatesFormatter = Value => Value.TimeOfDay.ToString();
        }
        public void AddTempDataPoint(NotifyNewData notifier, DateTimePoint newData, int seriesIdex = 0)
        {
            notifier.Temps[seriesIdex].Values.Add(newData);

            notifier.dateTimes.Append(newData.DateTime);

            notifier.stringTime.Append(newData.DateTime.TimeOfDay.ToString());
        }

        public void AddSeriesToChart(NotifyNewData notifier, string seriesName)
        {
            //notifier.YFormatter = Value => Value.ToString();
            //notifier.DatesFormatter = Value => Value.TimeOfDay.ToString();

            notifier.Temps.Add(

                new LineSeries
                {
                    Title = seriesName,
                    Values = new ChartValues<DateTimePoint>()
                }
            );
        }

    }
}

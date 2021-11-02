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
        {
        }

        public Charts(NotifyNewData notifier)
        {
            //notifier.YFormatter = Value => Value.ToString();
            //notifier.DatesFormatter = Value => Value.TimeOfDay.ToString();
        }

        public void AddTempDataPoint(NotifyNewData notifier, DateTimePoint newData, int seriesIdex = 0)
        {
            notifier.Temps[seriesIdex].Values.Add(newData);

            SetAxisLimits(notifier, DateTime.Now);
        }

        private void SetAxisLimits(NotifyNewData notifier, DateTime now)
        {
            notifier.AxisMax = now.Ticks + TimeSpan.FromSeconds(1).Ticks; // lets force the axis to be 1 second ahead
            notifier.AxisMin = now.Ticks - TimeSpan.FromMinutes(notifier.chartTimeSpan).Ticks; // and 8 seconds behind
        }

        public void AddSeriesToChart(NotifyNewData notifier, string seriesName)
        {

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

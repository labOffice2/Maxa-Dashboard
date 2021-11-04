using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LiveCharts;
using LiveCharts.Wpf;
using LiveCharts.Defaults;
using System.Windows.Media;

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

        public void AddDataPointTempChart(NotifyNewData notifier, DateTimePoint newData, int seriesIdex = 0)
        {
            notifier.Temps[seriesIdex].Values.Add(newData);

            SetAxisLimitsTempChart(notifier, DateTime.Now);
        }

        private void SetAxisLimitsTempChart(NotifyNewData notifier, DateTime now)
        {
            notifier.TempAxisMax = now.Ticks + TimeSpan.FromSeconds(1).Ticks; // lets force the axis to be 1 second ahead
            notifier.TempAxisMin = now.Ticks - TimeSpan.FromMinutes(notifier.tempChartTimeSpan).Ticks; // and 8 seconds behind
        }

        public int AddSeriesToTempChart(NotifyNewData notifier, string seriesName, Brush seriesColor = null)
        {
            seriesColor = seriesColor == null ? Brushes.LightBlue : seriesColor;
            notifier.Temps.Add(

                new LineSeries
                {
                    Title = seriesName,
                    Values = new ChartValues<DateTimePoint>(),
                    Fill = Brushes.Transparent,
                    Stroke = seriesColor,
                }
            );

            return notifier.Temps.Count - 1;
        }

        public void AddDataPointPressureChart(NotifyNewData notifier, DateTimePoint newData, int seriesIdex = 0)
        {
            notifier.Pressures[seriesIdex].Values.Add(newData);

            SetAxisLimitsPressureChart(notifier, DateTime.Now);
        }

        private void SetAxisLimitsPressureChart(NotifyNewData notifier, DateTime now)
        {
            notifier.PressureAxisMax = now.Ticks + TimeSpan.FromSeconds(1).Ticks; // lets force the axis to be 1 second ahead
            notifier.PressureAxisMin = now.Ticks - TimeSpan.FromMinutes(notifier.PressureChartTimeSpan).Ticks; // and 8 seconds behind
        }

        public int AddSeriesToPressureChart(NotifyNewData notifier, string seriesName, Brush seriesColor = null)
        {
            seriesColor = seriesColor == null ? Brushes.LightBlue : seriesColor;
            notifier.Pressures.Add(

                new LineSeries
                {
                    Title = seriesName,
                    Values = new ChartValues<DateTimePoint>(),
                    Fill = Brushes.Transparent,
                    Stroke = seriesColor,
                }
            );

            return notifier.Pressures.Count - 1;
        }

        /*
        public class Chart 
        {
            private double axisMax;
            private double axisMin;
            int timeSpan;
            SeriesCollection collection;
            UpdateAxis AxisUpdater;

        public delegate void UpdateAxis(DateTime time);

            public Chart(double _axisMax, double _axisMin, int _timeSpan, SeriesCollection _collection)
            {
                axisMax = _axisMax;
                axisMin = _axisMin;
                timeSpan = _timeSpan;
                collection = _collection;
            }

            public Chart(NotifyNewData notifier, UpdateAxis updater)
        {
            AxisUpdater = updater;
        }

            public int AddSeriesToChart(string seriesName, Brush seriesColor = null)
            {
                seriesColor = seriesColor == null ? Brushes.LightBlue : seriesColor;
                collection.Add(

                    new LineSeries
                    {
                        Title = seriesName,
                        Values = new ChartValues<DateTimePoint>(),
                        Fill = Brushes.Transparent,
                        Stroke = seriesColor,
                    }
                );

            return collection.Count - 1;
            }

            public void AddDataPoint(DateTimePoint newData, int seriesIdex = 0)
            {
                collection[seriesIdex].Values.Add(newData);

                SetAxisLimits(DateTime.Now);
            }

            private void SetAxisLimits(DateTime now)
            {
                axisMax = now.Ticks + TimeSpan.FromSeconds(1).Ticks; // lets force the axis to be 1 second ahead
                axisMin = now.Ticks - TimeSpan.FromMinutes(timeSpan).Ticks; // and 8 seconds behind
            }
        }
        */

    }
}

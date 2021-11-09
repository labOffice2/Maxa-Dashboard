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


/// <summary>
/// This class manages the charts in the program
/// The program has 2 charts - for temperatures and for pressures
/// </summary>
namespace Maxa_Dash
{
    public class Charts
    {
        private int maxDataPoints = 100;
        public Charts()
        {
        }

        public Charts(NotifyNewData notifier)
        {
            //notifier.YFormatter = Value => Value.ToString();
            //notifier.DatesFormatter = Value => Value.TimeOfDay.ToString();
        }

        /// <summary>
        /// This function adds new data to an exiting series in the temperatures charts
        /// </summary>
        /// <param name="notifier">Object holding all variables for data biniding</param>
        /// <param name="newData">The data to be added to the series</param>
        /// <param name="seriesIdex">this index points to the specific series to which the new data belongs</param>
        public void AddDataPointTempChart(NotifyNewData notifier, DateTimePoint newData, int seriesIdex = 0)
        {
            notifier.Temps[seriesIdex].Values.Add(newData);

            if (RemoveRedandency(notifier.Temps[seriesIdex].Values))
                notifier.Temps[seriesIdex].Values.RemoveAt(notifier.Temps[seriesIdex].Values.Count - 2);

            if (notifier.Temps[seriesIdex].Values.Count > maxDataPoints)
                notifier.Temps[seriesIdex].Values.RemoveAt(0);
        }

        /// <summary>
        /// This function updates the time axis of the temperature chart according to the time span set by user
        /// </summary>
        /// <param name="notifier">Object holding all variables for data biniding</param>
        /// <param name="now">the time when the function is called</param>
        public void SetAxisLimitsTempChart(NotifyNewData notifier, DateTime now)
        {
            notifier.TempAxisMax = now.Ticks + TimeSpan.FromSeconds(1).Ticks; // lets force the axis to be 1 second ahead
            notifier.TempAxisMin = now.Ticks - TimeSpan.FromMinutes(notifier.tempChartTimeSpan).Ticks; // and 8 seconds behind
        }

        /// <summary>
        /// This function initializes a new data series to add to the temperatures chart
        /// </summary>
        /// <param name="notifier">Object holding all variables for data biniding</param>
        /// <param name="seriesName">Name of the new data series</param>
        /// <param name="seriesColor">The color of the series to display ono the chart</param>
        /// <returns></returns>
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
                    Visibility = System.Windows.Visibility.Visible,
                }
            );

            return notifier.Temps.Count - 1;
        }

        /// <summary>
        /// This function is used to control the visibility of the series in the temperature chart
        /// </summary>
        /// <param name="notifier">Object holding all variables for data biniding</param>
        /// <param name="index">index to the series in question</param>
        /// <param name="visibility">the vixibility to set to said series</param>
        public void SetSeriesVisibilityTempChart(NotifyNewData notifier, int index, System.Windows.Visibility visibility)
        {
            LineSeries series = (LineSeries)notifier.Temps[index];
            series.Visibility = visibility;
        }


        /// <summary>
        /// This function adds new data to an exiting series in the pressures charts
        /// </summary>
        /// <param name="notifier">Object holding all variables for data biniding</param>
        /// <param name="newData">The data to be added to the series</param>
        /// <param name="seriesIdex">this index points to the specific series to which the new data belongs</param>
        public void AddDataPointPressureChart(NotifyNewData notifier, DateTimePoint newData, int seriesIdex = 0)
        {
            notifier.Pressures[seriesIdex].Values.Add(newData);

            if(RemoveRedandency(notifier.Pressures[seriesIdex].Values))
                notifier.Pressures[seriesIdex].Values.RemoveAt(notifier.Pressures[seriesIdex].Values.Count -2);

            if (notifier.Pressures[seriesIdex].Values.Count > maxDataPoints)
                notifier.Pressures[seriesIdex].Values.RemoveAt(0);
        }

        private bool RemoveRedandency(IChartValues chartValues)
        {
            try
            {
                int lastIndex = chartValues.Count - 1;
                DateTimePoint[] lastdata = { (DateTimePoint)chartValues[lastIndex], (DateTimePoint)chartValues[lastIndex - 1], (DateTimePoint)chartValues[lastIndex - 2] };
                if(lastdata[0].Value == lastdata[1].Value && lastdata[1].Value == lastdata[2].Value)
                return true;
            }
            catch(Exception ex)
            {

            }
            return false;
        }

        /// <summary>
        /// This function updates the time axis of the pressures chart according to the time span set by user
        /// </summary>
        /// <param name="notifier">Object holding all variables for data biniding</param>
        /// <param name="now">the time when the function is called</param>
        public void SetAxisLimitsPressureChart(NotifyNewData notifier, DateTime now)
        {
            notifier.PressureAxisMax = now.Ticks + TimeSpan.FromSeconds(1).Ticks; // lets force the axis to be 1 second ahead
            notifier.PressureAxisMin = now.Ticks - TimeSpan.FromMinutes(notifier.PressureChartTimeSpan).Ticks; // and 8 seconds behind
        }

        /// <summary>
        /// This function initializes a new data series to add to the pressures chart
        /// </summary>
        /// <param name="notifier">Object holding all variables for data biniding</param>
        /// <param name="seriesName">Name of the new data series</param>
        /// <param name="seriesColor">The color of the series to display ono the chart</param>
        /// <returns></returns>
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

        /// <summary>
        /// This function is used to control the visibility of the series in the pressures chart
        /// </summary>
        /// <param name="notifier">Object holding all variables for data biniding</param>
        /// <param name="index">index to the series in question</param>
        /// <param name="visibility">the vixibility to set to said series</param>
        public void SetSeriesVisibilityPressureChart(NotifyNewData notifier, int index, System.Windows.Visibility visibility)
        {
            LineSeries series = (LineSeries)notifier.Pressures[index];
            series.Visibility = visibility;
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

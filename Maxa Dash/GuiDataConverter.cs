﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace Maxa_Dash
{
    public class GuiDataConverter
    {
        public static Brush GetAlarmColor(bool status)
        {
            return status ? Brushes.Red : Brushes.Gray;
        }
    }
}
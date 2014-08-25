/*
#############################################################################
#                                                                           #
#              IMPINJ CONFIDENTIAL AND PROPRIETARY                          #
#                                                                           #
# This source code is the sole property of Impinj, Inc. Reproduction or     #
# utilization of this source code in whole or in part is forbidden without  #
# the prior written consent of Impinj, Inc.                                 #
#                                                                           #
# (c) Copyright Impinj, Inc. 2010. All rights reserved.                     #
#                                                                           #
#############################################################################
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Data;
using System.Windows;

namespace Template_Wpf
{
    [ValueConversion(typeof(string), typeof(Style))]
    public class StringStyleConverter : IValueConverter
    {
        #region IValueConverter Members

        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            var styleName = value.ToString();
            Style style = null;

            foreach (var r in Application.Current.Resources.Keys)
            {
                if (r.Equals(styleName))
                {
                    style = Application.Current.Resources[r] as Style;
                    if (null != style)
                    {
                        break;
                    }
                }
                else
                {
                    continue;
                }
            }

            return style;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return null;
        }

        #endregion
    }

    [ValueConversion(typeof(string), typeof(Visibility))]
    public class StringVisibilityConverter : IValueConverter
    {
        #region IValueConverter Members

        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            Visibility visibility = Visibility.Visible;

            if (Enum.IsDefined(typeof(Visibility), value.ToString()))
                visibility = (Visibility)Enum.Parse(typeof(Visibility), value.ToString());

            return visibility;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return null;
        }

        #endregion
    }
}

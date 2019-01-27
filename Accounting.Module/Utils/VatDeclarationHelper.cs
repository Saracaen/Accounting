using Accounting.Module.BusinessObjects;
using DevExpress.ExpressApp.Utils;
using System;
using System.Collections.Generic;

namespace Accounting.Module.Utils
{
    public static class VatDeclarationHelper
    {
        public static DateTime GetFirstDayOfPeriod(int year, VatDeclarationPeriod period)
        {
            switch (period)
            {
                case VatDeclarationPeriod.None:
                case VatDeclarationPeriod.FirstQuarter:
                case VatDeclarationPeriod.January:
                    return new DateTime(year, 1, 1);

                case VatDeclarationPeriod.SecondQuarter:
                case VatDeclarationPeriod.April:
                    return new DateTime(year, 4, 1);

                case VatDeclarationPeriod.ThirdQuarter:
                case VatDeclarationPeriod.July:
                    return new DateTime(year, 7, 1);

                case VatDeclarationPeriod.FourthQuarter:
                case VatDeclarationPeriod.October:
                    return new DateTime(year, 10, 1);

                case VatDeclarationPeriod.February:
                    return new DateTime(year, 2, 1);

                case VatDeclarationPeriod.March:
                    return new DateTime(year, 3, 1);

                case VatDeclarationPeriod.May:
                    return new DateTime(year, 5, 1);

                case VatDeclarationPeriod.June:
                    return new DateTime(year, 6, 1);

                case VatDeclarationPeriod.August:
                    return new DateTime(year, 8, 1);

                case VatDeclarationPeriod.September:
                    return new DateTime(year, 9, 1);

                case VatDeclarationPeriod.November:
                    return new DateTime(year, 11, 1);

                case VatDeclarationPeriod.December:
                    return new DateTime(year, 12, 1);

                default:
                    throw new InvalidOperationException(CaptionHelper.GetLocalizedText(@"Exceptions\UserVisibleExceptions", "UnsupportedVatDeclarationPeriod"));
            }
        }

        public static DateTime GetLastDayOfPeriod(int year, VatDeclarationPeriod period)
        {
            switch (period)
            {
                case VatDeclarationPeriod.None:
                case VatDeclarationPeriod.FourthQuarter:
                case VatDeclarationPeriod.December:
                    return new DateTime(year, 12, 31);

                case VatDeclarationPeriod.FirstQuarter:
                case VatDeclarationPeriod.March:
                    return new DateTime(year, 3, 31);

                case VatDeclarationPeriod.SecondQuarter:
                case VatDeclarationPeriod.June:
                    return new DateTime(year, 6, 30);

                case VatDeclarationPeriod.ThirdQuarter:
                case VatDeclarationPeriod.September:
                    return new DateTime(year, 9, 30);

                case VatDeclarationPeriod.January:
                    return new DateTime(year, 1, 31);

                case VatDeclarationPeriod.February:
                    return new DateTime(year, 3, 1).AddDays(-1);

                case VatDeclarationPeriod.April:
                    return new DateTime(year, 4, 30);

                case VatDeclarationPeriod.May:
                    return new DateTime(year, 5, 31);

                case VatDeclarationPeriod.July:
                    return new DateTime(year, 7, 31);

                case VatDeclarationPeriod.August:
                    return new DateTime(year, 8, 31);

                case VatDeclarationPeriod.October:
                    return new DateTime(year, 10, 31);

                case VatDeclarationPeriod.November:
                    return new DateTime(year, 11, 30);

                default:
                    throw new InvalidOperationException(CaptionHelper.GetLocalizedText(@"Exceptions\UserVisibleExceptions", "UnsupportedVatDeclarationPeriod"));
            }
        }

        public static IEnumerable<VatDeclarationPeriod> GetPeriods(VatDeclarationType interval)
        {
            switch (interval)
            {
                case VatDeclarationType.Monthly:
                    return new[]
                    {
                        VatDeclarationPeriod.January,
                        VatDeclarationPeriod.February,
                        VatDeclarationPeriod.March,
                        VatDeclarationPeriod.April,
                        VatDeclarationPeriod.May,
                        VatDeclarationPeriod.June,
                        VatDeclarationPeriod.July,
                        VatDeclarationPeriod.August,
                        VatDeclarationPeriod.September,
                        VatDeclarationPeriod.October,
                        VatDeclarationPeriod.November,
                        VatDeclarationPeriod.December
                    };

                case VatDeclarationType.Quarterly:
                    return new[]
                    {
                        VatDeclarationPeriod.FirstQuarter,
                        VatDeclarationPeriod.SecondQuarter,
                        VatDeclarationPeriod.ThirdQuarter,
                        VatDeclarationPeriod.FourthQuarter
                    };

                case VatDeclarationType.Annual:
                    return new[]
                    {
                        VatDeclarationPeriod.None
                    };

                default:
                    throw new InvalidOperationException(CaptionHelper.GetLocalizedText(@"Exceptions\UserVisibleExceptions", "UnsupportedVatDeclarationPeriod"));
            }
        }
    }
}